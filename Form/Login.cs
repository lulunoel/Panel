using Loader;
using MySql.Data.MySqlClient;
using Siticone.UI.WinForms;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static KeyAuth.Warn;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Discord.Webhook;
using Discord;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using System.Security.Principal;
using System.Drawing;

namespace KeyAuth
{
    public partial class Login : Form
    {
        public static api KeyAuthApp = new api(
            name: "JediSky",
            ownerid: "gelPqRWLib",
            secret: "f3dbf6b9a6e29068897d064afb6f4b5676754cbfe9eb4d24d7682aecd9f4b3f7",
            version: "1.0"
        );
        public readonly MySqlConnection connection = new MySqlConnection("database=s2_test; server=83.150.217.78; user id=pterodactyl; pwd=4wYYZkQ6nccgvbZR4LgK");

        public Login()
        {
            InitializeComponent();
            string cheminFichier = System.AppDomain.CurrentDomain.BaseDirectory + "login.txt";
            ChargerIdentifiantsDepuisFichier(cheminFichier);
            string hwid = WindowsIdentity.GetCurrent().User.Value;
            label8.Text = hwid;
        }


        public static bool SubExist(string name)
        {
            if(KeyAuthApp.user_data.subscriptions.Exists(x => x.subscription == name))
                return true;
            return false;
        }



        private async void Login_Load(object sender, EventArgs e)
        {
            KeyAuthApp.init();
            if (KeyAuthApp.response.message == "invalidver")
            {
                if (!string.IsNullOrEmpty(KeyAuthApp.app_data.downloadLink))
                {
                    DialogResult dialogResult = MessageBox.Show("Yes to open file in browser\nNo to download file automatically", "Auto update", MessageBoxButtons.YesNo);
                    switch (dialogResult)
                    {
                        case DialogResult.Yes:
                            Process.Start(KeyAuthApp.app_data.downloadLink);
                            Environment.Exit(0);
                            break;
                        case DialogResult.No:
                            WebClient webClient = new WebClient();
                            string destFile = Application.ExecutablePath;

                            string rand = Random_string();

                            destFile = destFile.Replace(".exe", $"-{rand}.exe");
                            webClient.DownloadFile(KeyAuthApp.app_data.downloadLink, destFile);

                            Process.Start(destFile);
                            Process.Start(new ProcessStartInfo()
                            {
                                Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + Application.ExecutablePath + "\"",
                                WindowStyle = ProcessWindowStyle.Hidden,
                                CreateNoWindow = true,
                                FileName = "cmd.exe"
                            });
                            Environment.Exit(0);

                            break;
                        default:
                            MessageBox.Show("Invalid option");
                            Environment.Exit(0);
                            break;
                    }
                }
                MessageBox.Show("Version of this program does not match the one online. Furthermore, the download link online isn't set. You will need to manually obtain the download link from the developer");
                Environment.Exit(0);
            }
            
            if (!KeyAuthApp.response.success)
            {
                MessageBox.Show(KeyAuthApp.response.message);
                Environment.Exit(0);
            }

            string apiEndpoint = "https://keyauth.win/api/seller/?sellerkey=fd2267d8a99e7a5324d8a5276be26ed1&type=fetchallsessions";
            string apiEndpoint2 = "https://api.ipify.org?format=json";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiEndpoint);
                    HttpResponseMessage response2 = await client.GetAsync(apiEndpoint2);

                    if (response.IsSuccessStatusCode && response2.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        string responseBody2 = await response2.Content.ReadAsStringAsync();
                        JObject jsonResponse = JObject.Parse(responseBody);
                        JObject jsonResponse2 = JObject.Parse(responseBody2);

                        // Vous pouvez directement accéder à la valeur de la clé "ip" dans jsonResponse2
                        string myIp = jsonResponse2["ip"].ToString();

                        if (jsonResponse.TryGetValue("sessions", out var sessions))
                        {
                            foreach (JObject session in sessions)
                            {
                                string sessionIp = session.Value<string>("ip");
                                string sessionId = session.Value<string>("id");
                                string sessionCredential = session.Value<string>("credential");

                                string ipToCompare = myIp;

                                if (sessionIp == ipToCompare)
                                {
                                    KeyAuthApp.loginSessions(sessionCredential, password.Text, sessionId);
                                    if (KeyAuthApp.response.success)
                                    {
                                        string webhookUrl = "https://discord.com/api/webhooks/1158438206283460679/0bHrmZeCa13P7klz_6gk3uI2P4e9dl-Gy4ZGIYJ69xkzkG2HoHhNhLvffjJaALY5rZtk";
                                        var client2 = new DiscordWebhookClient(webhookUrl);
                                        try
                                        {
                                            // Créez un message en embed.
                                            var embed = new EmbedBuilder
                                            {
                                                Title = "Connexion",
                                                Description = "Staff: " + username.Text + " s'est connecter",
                                                Color = Discord.Color.Green, // Couleur de la bordure de l'embed
                                                Timestamp = DateTimeOffset.Now
                                            };

                                            // Ajoutez des champs à l'embed (optionnel).
                                            embed.AddField("Connexion", "Autorisée");

                                            // Ajoutez l'embed au message.
                                            await client2.SendMessageAsync(embeds: new[] { embed.Build() }, isTTS: false, username: "Panel staff logs");
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Erreur lors de l'envoi du log : " + ex.Message);
                                        }
                                        finally
                                        {
                                            // N'oubliez pas de libérer les ressources du client.
                                            client2.Dispose();
                                        }
                                    }
                                    else
                                    {
                                        status.Text = "Status: " + KeyAuthApp.response.message;
                                    }
                                }
                            }
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        static string Random_string()
        {
            string str = null;

            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                str += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))).ToString();
            }
            return str;

        }

        private void UpgradeBtn_Click(object sender, EventArgs e)
        {
            KeyAuthApp.upgrade(username.Text, key.Text); // success is set to false so people can't press upgrade then press login and skip logging in. it doesn't matter, since you shouldn't take any action on succesfull upgrade anyways. the only thing that needs to be done is the user needs to see the message from upgrade function
            status.Text = "Status: " + KeyAuthApp.response.message;
        }

        public static void EnregistrerIdentifiants(string cheminFichier, string nomUtilisateur, string motDePasse)
        {
            try
            {
                // Ouvre le fichier en mode écriture (créer le fichier s'il n'existe pas)
                using (StreamWriter writer = new StreamWriter(cheminFichier))
                {
                    // Écrit les identifiants dans le fichier
                    writer.WriteLine("Nom d'utilisateur : " + nomUtilisateur);
                    writer.WriteLine("Mot de passe : " + motDePasse);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue lors de l'enregistrement des identifiants : " + ex.Message);
            }
        }

        private void ChargerIdentifiantsDepuisFichier(string cheminFichier)
        {
            try
            {
                if (File.Exists(cheminFichier))
                {
                    using (StreamReader reader = new StreamReader(cheminFichier))
                    {
                        string ligne;
                        while ((ligne = reader.ReadLine()) != null)
                        {
                            string[] parties = ligne.Split(':');
                            if (parties.Length == 2)
                            {
                                string clé = parties[0].Trim();
                                string valeur = parties[1].Trim();

                                if (clé == "Nom d'utilisateur")
                                {
                                    // Affichez le nom d'utilisateur dans le TextBox approprié
                                    username.Text = valeur;
                                }
                                else if (clé == "Mot de passe")
                                {
                                    // Affichez le mot de passe dans le TextBox approprié
                                    password.Text = valeur;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue lors du chargement des identifiants : " + ex.Message);
            }
        }


        private async void LoginBtn_Click(object sender, EventArgs e)
        {

            KeyAuthApp.login(username.Text, password.Text);
            if (KeyAuthApp.response.success)
            {
                string webhookUrl = "https://discord.com/api/webhooks/1158438206283460679/0bHrmZeCa13P7klz_6gk3uI2P4e9dl-Gy4ZGIYJ69xkzkG2HoHhNhLvffjJaALY5rZtk";
                var client2 = new DiscordWebhookClient(webhookUrl);
                try
                {
                    // Créez un message en embed.
                    var embed = new EmbedBuilder
                    {
                        Title = "Connexion",
                        Description = "Staff: " + username.Text + " s'est connecter",
                        Color = Discord.Color.Green, // Couleur de la bordure de l'embed
                        Timestamp = DateTimeOffset.Now
                    };

                    // Ajoutez des champs à l'embed (optionnel).
                    embed.AddField("Connexion", "Autorisée");

                    // Ajoutez l'embed au message.
                    await client2.SendMessageAsync(embeds: new[] { embed.Build() }, isTTS: false, username: "Panel staff logs");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de l'envoi du log : " + ex.Message);
                }
                finally
                {
                    // N'oubliez pas de libérer les ressources du client.
                    client2.Dispose();
                }
                string cheminFichier = System.AppDomain.CurrentDomain.BaseDirectory + "login.txt";
                string nomUtilisateur = username.Text;
                string motDePasse = password.Text;
                EnregistrerIdentifiants(cheminFichier, nomUtilisateur, motDePasse);
                Main main = new Main();
                main.Show();
                this.Hide();
            }
            else
            status.Text = "Status: " + KeyAuthApp.response.message;
        }


        private async void RgstrBtn_Click(object sender, EventArgs e)
        {
            KeyAuthApp.register(username.Text, password.Text, key.Text);
            if (KeyAuthApp.response.success)
            {
                string webhookUrl3 = "https://discord.com/api/webhooks/1158438206283460679/0bHrmZeCa13P7klz_6gk3uI2P4e9dl-Gy4ZGIYJ69xkzkG2HoHhNhLvffjJaALY5rZtk";
                var client = new DiscordWebhookClient(webhookUrl3);
                try
                {
                    // Créez un message en embed.
                    var embed = new EmbedBuilder
                    {
                        Title = "Connexion",
                        Description = "Staff: " + username.Text + " s'est connecter",
                        Color = Discord.Color.Green, // Couleur de la bordure de l'embed
                        Timestamp = DateTimeOffset.Now
                    };

                    // Ajoutez des champs à l'embed (optionnel).
                    embed.AddField("Connexion", "Autorisée");

                    // Ajoutez l'embed au message.
                    await client.SendMessageAsync(embeds: new[] { embed.Build() }, isTTS: false, username: "Panel staff logs");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de l'envoi du log : " + ex.Message);
                }
                finally
                {
                    // N'oubliez pas de libérer les ressources du client.
                    client.Dispose();
                }
                Main main = new Main();
                main.Show();
                this.Hide();
            }
            else
                status.Text = "Status: " + KeyAuthApp.response.message;
        }

        private void LicBtn_Click(object sender, EventArgs e)
        {
            KeyAuthApp.license(key.Text);
            if (KeyAuthApp.response.success)
            {
                Warn warn = new Warn();
                warn.Show();
                this.Hide();
            }
            else
                status.Text = "Status: " + KeyAuthApp.response.message;
        }

        private void SiticoneRoundedButton1_Click(object sender, EventArgs e)
        {
            string hwid = WindowsIdentity.GetCurrent().User.Value;
            Clipboard.SetText(label8.Text);
            MessageBox.Show("HWID: " + hwid);
        }
    }
}
