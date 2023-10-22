using Discord;
using Discord.Webhook;
using KeyAuth;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Loader
{
    public partial class Paramètre : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public Paramètre()
        {
            InitializeComponent();
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;
            Application.EnableVisualStyles();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = false;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = this.Location;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        public MySqlConnection Conect()
        {
            string servidor = "83.150.217.78";
            string usuario = "pterodactyl";
            string password = "4wYYZkQ6nccgvbZR4LgK";
            string BaseDeDatos = "s2_test";
            string Conexion = "Database=" + BaseDeDatos + "; Data Source=" + servidor
                + "; User Id=" + usuario + "; Password=" + password + "";

            MySqlConnection conexionDB = new MySqlConnection(Conexion);
            conexionDB.Open();

            return conexionDB;
        }

        private async void Déconnexion()
        {
            string webhookUrl3 = "https://discord.com/api/webhooks/1158438206283460679/0bHrmZeCa13P7klz_6gk3uI2P4e9dl-Gy4ZGIYJ69xkzkG2HoHhNhLvffjJaALY5rZtk";
            var client2 = new DiscordWebhookClient(webhookUrl3);
            try
            {
                // Créez un message en embed.
                var embed = new EmbedBuilder
                {
                    Title = "Déconnexion",
                    Description = "Staff: " + key.Text + " s'est deconnecter",
                    Color = Discord.Color.Red, // Couleur de la bordure de l'embed
                    Timestamp = DateTimeOffset.Now
                };

                // Ajoutez des champs à l'embed (optionnel).
                embed.AddField("Déconnexion:", "Autorisée");

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
            DateTime now = DateTime.Now;
            string formattedDate = now.ToString("dd-MM-yyyy HH:mm:ss");

            string pseudo = key.Text;
            string action = "Déconnexion";
            string date = formattedDate;

            string editer = "INSERT INTO `Logs`(`Pseudo`, `Action`, `Date`) VALUES (@Pseudo, @Action, @Date)";

            using (MySqlConnection connection = Conect())
            {
                using (MySqlCommand comando = new MySqlCommand(editer, connection))
                {
                    comando.Parameters.AddWithValue("@Pseudo", pseudo);
                    comando.Parameters.AddWithValue("@Action", action);
                    comando.Parameters.AddWithValue("@Date", date);

                    comando.ExecuteNonQuery();
                }
            }
        }

        private async void SiticoneRoundedButton6_Click(object sender, EventArgs e)
        {
            Déconnexion(); // Exécute la fonction de déconnexion
            await Task.Delay(TimeSpan.FromSeconds(1));

            Environment.Exit(0); // Quittez l'application après le délai
            this.Close(); // Cela déclenchera à nouveau l'événement Form_Closing
        }

        public DateTime UnixTimeToDateTime(long unixtime)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
            try
            {
                dtDateTime = dtDateTime.AddSeconds(unixtime).ToLocalTime();
            }
            catch
            {
                dtDateTime = DateTime.MaxValue;
            }
            return dtDateTime;
        }

        private async void Paramètre_Load(object sender, EventArgs e)
        {


            siticoneRoundedTextBox1.Text = Login.KeyAuthApp.user_data.username;
            key.Text = Login.KeyAuthApp.user_data.username;
            label14.Text = Login.KeyAuthApp.user_data.subscriptions[0].subscription;
            label8.Text = Login.KeyAuthApp.user_data.ip;
            label9.Text = Login.KeyAuthApp.user_data.hwid;
            label3.Text = "" + UnixTimeToDateTime(long.Parse(Login.KeyAuthApp.user_data.createdate));
            label7.Text = "" + UnixTimeToDateTime(long.Parse(Login.KeyAuthApp.user_data.lastlogin));
            string playerName = key.Text;
            string playerUUID = await GetPlayerUUID(playerName);

            if (!string.IsNullOrEmpty(playerUUID))
            {
                GetMinecraftHead(playerUUID);
            }
            if (label14.Text == "Gérant" || key.Text == "Lulunoel2016")
            {
                button2.Enabled = true;
                button1.Enabled = true;
                btnTools.Enabled = true;
                button5.Enabled = true;


            }
            else if (label14.Text == "Admin")
            {
                button2.Enabled = true;
                button1.Enabled = true;
                btnTools.Enabled = true;
                button5.Enabled = true;

            }
            else if (label14.Text == "Manager")
            {
                button2.Enabled = true;
                button1.Enabled = true;
                btnTools.Enabled = true;
                button5.Enabled = true;

            }
            else if (label14.Text == "SuperModo")
            {
                button2.Enabled = false;
                button1.Enabled = true;
                btnTools.Enabled = true;
                button5.Enabled = true;

            }
            else if (label14.Text == "ModoConfirmé")
            {
                button2.Enabled = false;
                button1.Enabled = false;
                btnTools.Enabled = true;
                button5.Enabled = true;

            }
            else if (label14.Text == "Modo")
            {
                button2.Enabled = false;
                button1.Enabled = false;
                btnTools.Enabled = true;
                button5.Enabled = false;

            }
            else if (label14.Text == "Guide")
            {
                button2.Enabled = false;
                button1.Enabled = false;
                btnTools.Enabled = false;
                button5.Enabled = false;

            }
            else
            {
                btnTools.Enabled = false;
                button2.Enabled = false;
                button1.Enabled = false;
                button5.Enabled = false;

            }
            // Initialisation du Timer pour envoyer key.Text à la base de données toutes les 5 minutes.
            System.Timers.Timer timer2 = new System.Timers.Timer(300000);
            timer2.Elapsed += SendTimer_Elapsed;
            timer2.AutoReset = true; // Définir AutoReset à true pour que le minuteur se répète
            timer2.Start();

            System.Timers.Timer timer = new System.Timers.Timer(60000);
            timer.Elapsed += TimerElapsed;
            timer.AutoReset = true; // Définir AutoReset à true pour que le minuteur se répète
            timer.Start();
            this.FormClosing += Form_Closing;
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            // Annuler la fermeture
            e.Cancel = true;
        }

        private void SendTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            string valueToSend = key.Text; // Get the value from your user interface.
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("dd/MM/yyyy HH:mm:ss");
            string DatedefinValue = formattedDateTime; // Get the value from your user interface.

            // Convertir DatedefinValue en DateTime
            DateTime datedefinDateTime = DateTime.ParseExact(DatedefinValue, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            // Ajouter 5 minutes à la date
            datedefinDateTime = datedefinDateTime.AddMinutes(5);

            // Convertir la nouvelle date en une chaîne formatée
            string updatedDatedefinValue = datedefinDateTime.ToString("dd/MM/yyyy HH:mm:ss");

            // Utiliser updatedDatedefinValue dans votre requête SQL
            using (MySqlConnection connection = new MySqlConnection("Server=83.150.217.78;Port=3306;Database=s2_test;User Id=pterodactyl;Password=4wYYZkQ6nccgvbZR4LgK;"))
            {
                connection.Open();

                // Insérer la valeur dans la table appropriée de votre base de données.
                string insertQuery = "INSERT INTO UserOnline (UserName, Datedefin) VALUES (@Value, @Datedefin)";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Value", valueToSend);
                insertCommand.Parameters.AddWithValue("@Datedefin", updatedDatedefinValue);
                insertCommand.ExecuteNonQuery();

                connection.Close();
            }

        }

        private static void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection("database=s2_test; server=83.150.217.78; user id=pterodactyl; pwd=4wYYZkQ6nccgvbZR4LgK"))
            {
                connection.Open();

                // Récupérer la date et l'heure actuelles
                DateTime currentDateTime = DateTime.Now;

                // Supprimer les enregistrements dont la Datedefin est dépassée
                string deleteQuery = "DELETE FROM UserOnline WHERE Datedefin < @CurrentDateTime";
                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@CurrentDateTime", currentDateTime);
                int rowsAffected = deleteCommand.ExecuteNonQuery();

                //MessageBox.Show($"{rowsAffected} enregistrements supprimés.");

                connection.Close();
            }
        }

        public class PlayerInfo
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        private async Task<string> GetPlayerUUID(string playerName)
        {
            string baseUrl = "https://api.mojang.com/users/profiles/minecraft/";
            string url = $"{baseUrl}{playerName}";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        // L'extraction de l'UUID à partir de la réponse JSON dépend de la structure de la réponse.
                        // Vous devrez peut-être utiliser une classe de désérialisation JSON ou extraire l'UUID manuellement.
                        // Par exemple :
                        var playerInfo = JsonConvert.DeserializeObject<PlayerInfo>(json);
                        return playerInfo?.id;
                    }
                    else
                    {
                        MessageBox.Show($"Impossible d'obtenir l'UUID de {playerName}. Réponse HTTP : {response.StatusCode}");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}");
                return null;
            }
        }

        private async void GetMinecraftHead(string playerUUID)
        {
            string baseUrl = "https://crafatar.com/avatars/";
            string url = $"{baseUrl}{playerUUID}";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();
                        System.Drawing.Image playerHead = System.Drawing.Image.FromStream(new MemoryStream(imageBytes));
                        pictureBox2.Image = playerHead;
                    }
                    else
                    {
                        MessageBox.Show($"Impossible de récupérer la tête de {playerUUID}. Réponse HTTP : {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}");
            }
        }

        private void btnTools_Click(object sender, EventArgs e)
        {
            Warn warn = new Warn();
            warn.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Gestion gestion = new Gestion();
            gestion.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Rembourse rembourse = new Rembourse();
            rembourse.Show();
            this.Hide();
        }

        private async void siticoneRoundedButton3_Click(object sender, EventArgs e)
        {
            var options = new RestClientOptions("https://keyauth.win/api/seller/?sellerkey=fd2267d8a99e7a5324d8a5276be26ed1&type=editemail&user=" + key.Text + "&email=" + siticoneRoundedTextBox3.Text);
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            var response = await client.GetAsync(request);

            MessageBox.Show("{0}", response.Content);
            siticoneRoundedTextBox1.Text = Login.KeyAuthApp.user_data.username;
            key.Text = Login.KeyAuthApp.user_data.username;
            label14.Text = Login.KeyAuthApp.user_data.subscriptions[0].subscription;
            label8.Text = Login.KeyAuthApp.user_data.ip;
            label9.Text = Login.KeyAuthApp.user_data.hwid;
            label3.Text = "" + UnixTimeToDateTime(long.Parse(Login.KeyAuthApp.user_data.createdate));
            label7.Text = "" + UnixTimeToDateTime(long.Parse(Login.KeyAuthApp.user_data.lastlogin));

        }

        private void siticoneRoundedButton1_Click(object sender, EventArgs e)
        {
            string username = siticoneRoundedTextBox1.Text;
            Login.KeyAuthApp.ChangeUsername(username);
            siticoneRoundedTextBox1.Text = Login.KeyAuthApp.user_data.username;
            key.Text = Login.KeyAuthApp.user_data.username;
            label14.Text = Login.KeyAuthApp.user_data.subscriptions[0].subscription;
            label8.Text = Login.KeyAuthApp.user_data.ip;
            label9.Text = Login.KeyAuthApp.user_data.hwid;
            label3.Text = "" + UnixTimeToDateTime(long.Parse(Login.KeyAuthApp.user_data.createdate));
            label7.Text = "" + UnixTimeToDateTime(long.Parse(Login.KeyAuthApp.user_data.lastlogin));
        }

        private void siticoneRoundedButton2_Click(object sender, EventArgs e)
        {
            string username = key.Text;
            string email = siticoneRoundedTextBox2.Text;
            Login.KeyAuthApp.forgot(username, email);
            siticoneRoundedTextBox1.Text = Login.KeyAuthApp.user_data.username;
            key.Text = Login.KeyAuthApp.user_data.username;
            label14.Text = Login.KeyAuthApp.user_data.subscriptions[0].subscription;
            label8.Text = Login.KeyAuthApp.user_data.ip;
            label9.Text = Login.KeyAuthApp.user_data.hwid;
            label3.Text = "" + UnixTimeToDateTime(long.Parse(Login.KeyAuthApp.user_data.createdate));
            label7.Text = "" + UnixTimeToDateTime(long.Parse(Login.KeyAuthApp.user_data.lastlogin));
        }

        private async void siticoneRoundedButton4_Click(object sender, EventArgs e)
        {
            var options = new RestClientOptions("https://keyauth.win/api/seller/?sellerkey=fd2267d8a99e7a5324d8a5276be26ed1&type=resetuser&user=" + key.Text);
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            var response = await client.GetAsync(request);

            MessageBox.Show("{0}", response.Content);
            siticoneRoundedTextBox1.Text = Login.KeyAuthApp.user_data.username;
            key.Text = Login.KeyAuthApp.user_data.username;
            label14.Text = Login.KeyAuthApp.user_data.subscriptions[0].subscription;
            label8.Text = Login.KeyAuthApp.user_data.ip;
            label9.Text = Login.KeyAuthApp.user_data.hwid;
            label3.Text = "" + UnixTimeToDateTime(long.Parse(Login.KeyAuthApp.user_data.createdate));
            label7.Text = "" + UnixTimeToDateTime(long.Parse(Login.KeyAuthApp.user_data.lastlogin));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Vérif Vérif = new Vérif();
            Vérif.Show();
            this.Hide();
        }

        private void siticoneRoundedButton5_Click(object sender, EventArgs e)
        {
            Process.Start("https://docs.google.com/spreadsheets/d/1NZFOAEe__1vOpMlI2EbPkyJBn1qyTGKUrXQn6Sl7sdc/");
        }

        private void siticoneRoundedButton7_Click(object sender, EventArgs e)
        {
            Process.Start("https://docs.google.com/document/d/1PpT2Ac4UR4ZcnOjrtm2XPgqnaIOZJOiEcFfAWIIaSsc/");
        }

        private void siticoneRoundedButton8_Click(object sender, EventArgs e)
        {
            Process.Start("https://ban.jedisky.fr/index.php");
        }

        private async void btnExit_Click(object sender, EventArgs e)
        {
            Déconnexion(); // Exécute la fonction de déconnexion
            await Task.Delay(TimeSpan.FromSeconds(1));

            Environment.Exit(0); // Quittez l'application après le délai
            this.Close(); // Cela déclenchera à nouveau l'événement Form_Closing
        }
    }
}
