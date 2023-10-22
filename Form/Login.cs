using Discord;
using Discord.Webhook;
using Loader;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Windows.Forms;

namespace KeyAuth
{
    public partial class Login : Form
    {
        public static readonly api KeyAuthApp = new api("JediSky", "gelPqRWLib", "f3dbf6b9a6e29068897d064afb6f4b5676754cbfe9eb4d24d7682aecd9f4b3f7", "1.0");

        public Login()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            string cheminFichier = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "login.txt");
            LoadCredentialsFromFile(cheminFichier);
            string hwid = WindowsIdentity.GetCurrent().User.Value;
            label8.Text = hwid;
        }

        public static bool SubscriptionExists(string name) => KeyAuthApp.user_data.subscriptions.Exists(x => x.subscription == name);

        private async void Login_Load(object sender, EventArgs e)
        {
            KeyAuthApp.init();

            if (KeyAuthApp.response.message == "invalidver")
            {
                HandleInvalidVersion();
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

                        string myIp = jsonResponse2["ip"].ToString();

                        if (jsonResponse.TryGetValue("sessions", out var sessions))
                        {
                            ProcessSessions(myIp, sessions);
                        }
                    }
                }
                catch (Exception)
                {
                    // Handle the exception as needed
                }
            }
            this.FormClosing += Form_Closing;
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            // Annuler la fermeture
            e.Cancel = true;
        }

        private void HandleInvalidVersion()
        {
            if (!string.IsNullOrEmpty(KeyAuthApp.app_data.downloadLink))
            {
                DialogResult dialogResult = MessageBox.Show("Yes to open file in a browser\nNo to download file automatically", "Auto update", MessageBoxButtons.YesNo);
                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        Process.Start(KeyAuthApp.app_data.downloadLink);
                        Environment.Exit(0);
                        break;
                    case DialogResult.No:
                        DownloadAndReplaceApplication(KeyAuthApp.app_data.downloadLink);
                        break;
                    default:
                        MessageBox.Show("Invalid option");
                        Environment.Exit(0);
                        break;
                }
            }
            else
            {
                MessageBox.Show("Version of this program does not match the one online. Furthermore, the download link online isn't set. You will need to manually obtain the download link from the developer");
                Environment.Exit(0);
            }
        }

        private void DownloadAndReplaceApplication(string downloadLink)
        {
            WebClient webClient = new WebClient();
            string destFile = Application.ExecutablePath;
            string rand = RandomString();
            destFile = destFile.Replace(".exe", $"-{rand}.exe");
            webClient.DownloadFile(downloadLink, destFile);

            Process.Start(destFile);
            Process.Start(new ProcessStartInfo()
            {
                Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + Application.ExecutablePath + "\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            });

            Environment.Exit(0);
        }

        private void ProcessSessions(string myIp, JToken sessions)
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
                        SendLoginWebhookNotification();

                    }
                    else
                    {
                        status.Text = "Status: " + KeyAuthApp.response.message;
                    }
                }
            }
        }

        private void SendLoginWebhookNotification()
        {
            string webhookUrl = "https://discord.com/api/webhooks/1158438206283460679/0bHrmZeCa13P7klz_6gk3uI2P4e9dl-Gy4ZGIYJ69xkzkG2HoHhNhLvffjJaALY5rZtk";
            var client2 = new DiscordWebhookClient(webhookUrl);

            try
            {
                var embed = new EmbedBuilder
                {
                    Title = "Connexion",
                    Description = "Staff: " + username.Text + " s'est connect�",
                    Color = Discord.Color.Green,
                    Timestamp = DateTimeOffset.Now
                };

                embed.AddField("Connexion", "Autoris�e");
                client2.SendMessageAsync(embeds: new[] { embed.Build() }, isTTS: false, username: "Panel staff logs").Wait();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'envoi du log : " + ex.Message);
            }
            finally
            {
                client2.Dispose();
            }

            string cheminFichier = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "login.txt");
            string nomUtilisateur = username.Text;
            string motDePasse = password.Text;
            SaveCredentialsToFile(cheminFichier, nomUtilisateur, motDePasse);

            Main main = new Main();
            main.Show();
            Overlays Overlays = new Overlays();
            Overlays.Show();
            this.Hide();


        }

        private void SaveCredentialsToFile(string cheminFichier, string nomUtilisateur, string motDePasse)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(cheminFichier))
                {
                    writer.WriteLine("Nom d'utilisateur : " + nomUtilisateur);
                    writer.WriteLine("Mot de passe : " + motDePasse);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue lors de l'enregistrement des identifiants : " + ex.Message);
            }
        }

        private void LoadCredentialsFromFile(string cheminFichier)
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
                                string cl� = parties[0].Trim();
                                string valeur = parties[1].Trim();

                                if (cl� == "Nom d'utilisateur")
                                {
                                    username.Text = valeur;
                                }
                                else if (cl� == "Mot de passe")
                                {
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

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            KeyAuthApp.login(username.Text, password.Text);
            if (KeyAuthApp.response.success)
            {
                SendLoginWebhookNotification();
            }
            else
            {
                status.Text = "Status: " + KeyAuthApp.response.message;
            }
        }

        private void RgstrBtn_Click(object sender, EventArgs e)
        {
            KeyAuthApp.register(username.Text, password.Text, key.Text);
            if (KeyAuthApp.response.success)
            {
                SendLoginWebhookNotification();
            }
            else
            {
                status.Text = "Status: " + KeyAuthApp.response.message;
            }
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
            {
                status.Text = "Status: " + KeyAuthApp.response.message;
            }
        }

        private void SiticoneRoundedButton1_Click(object sender, EventArgs e)
        {
            string hwid = WindowsIdentity.GetCurrent().User.Value;
            Clipboard.SetText(label8.Text);
            MessageBox.Show("HWID: " + hwid);
        }

        private static string RandomString()
        {
            string str = null;
            Random random = new Random();

            for (int i = 0; i < 5; i++)
            {
                str += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)).ToString());
            }

            return str;
        }

        private void siticoneControlBox1_Click(object sender, EventArgs e)
        {
            this.Close(); // Cela d�clenchera � nouveau l'�v�nement Form_Closing
        }
    }
}