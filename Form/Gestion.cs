using Discord;
using Discord.Webhook;
using KeyAuth;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static KeyAuth.Warn;
using static Loader.Gestion;

namespace Loader
{
    public partial class Gestion : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public Gestion()
        {
            InitializeComponent();
            ActualizarListAsync();
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;
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

        private async Task<List<string>> GetPlayerUsernamesAsync(string apiKey)
        {
            string baseApiUrl = "https://keyauth.win/api/seller/?sellerkey=" + apiKey;

            // Première requête pour obtenir les noms d'utilisateur
            var usernameClient = new RestClient(baseApiUrl + "&type=fetchallusernames");
            var usernameRequest = new RestRequest("");
            usernameRequest.AddHeader("accept", "application/json");

            // Exécutez la première requête de manière asynchrone
            RestResponse usernameResponse = await usernameClient.ExecuteAsync(usernameRequest);

            if (!usernameResponse.IsSuccessful)
            {
                MessageBox.Show($"Erreur de requête pour les noms d'utilisateur : {usernameResponse.ErrorMessage}");
                return null;
            }

            // Désérialisez la réponse JSON en un objet C#
            MyResponse result = JsonConvert.DeserializeObject<MyResponse>(usernameResponse.Content);

            if (result == null || result.usernames == null)
            {
                MessageBox.Show("La réponse JSON de la première requête est vide ou mal formée.");
                return null;
            }

            // Accédez à la liste des noms d'utilisateur
            return result.usernames.Select(u => u.username).ToList();
        }

        private async Task<string> GetUserGradeAsync(string apiKey, string username)
        {
            string baseApiUrl = "https://keyauth.win/api/seller/?sellerkey=" + apiKey;

            // Requête pour obtenir le grade d'un utilisateur
            var gradeClient = new RestClient(baseApiUrl + "&type=userdata");
            var gradeRequest = new RestRequest("");
            gradeRequest.AddHeader("accept", "application/json");
            gradeRequest.AddParameter("user", username);

            // Exécutez la requête de manière asynchrone
            RestResponse gradeResponse = await gradeClient.ExecuteAsync(gradeRequest);

            if (gradeResponse.IsSuccessful)
            {
                // Désérialisez la réponse JSON en un objet C#
                MyResponse2 gradeResult = JsonConvert.DeserializeObject<MyResponse2>(gradeResponse.Content);

                if (gradeResult != null && gradeResult.subscriptions != null && gradeResult.subscriptions.Count > 0)
                {
                    return gradeResult.subscriptions[0].subscription;
                }
            }

            return "Inconnu";
        }

        private async void ActualizarListAsync()
        {
            string apiKey = "fd2267d8a99e7a5324d8a5276be26ed1"; // Remplacez par votre clé API

            listBox1.Items.Clear();

            List<string> usernames = await GetPlayerUsernamesAsync(apiKey);

            if (usernames == null)
            {
                return;
            }

            // Utilisez Parallel.ForEach pour exécuter les requêtes de grade en parallèle
            Parallel.ForEach(usernames, async (username) =>
            {
                string userGrade = await GetUserGradeAsync(apiKey, username);
                string listItem = $"{username}: {userGrade}";

                // Assurez-vous de synchroniser l'ajout à la liste
                listBox1.Invoke(new Action(() => listBox1.Items.Add(listItem)));
            });
        }

        public class MyResponse2
        {
            public bool success { get; set; }
            public string message { get; set; }
            public List<Subscriptions> subscriptions { get; set; }
        }

        public class Subscriptions
        {
            public string subscription { get; set; }
        }

        private void siticoneRoundedButton8_Click(object sender, EventArgs e)
        {
            Warn warn = new Warn();
            warn.Show();
            this.Hide();
        }

        private async void siticoneRoundedButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(siticoneRoundedTextBox1.Text))
            {
                System.Windows.MessageBox.Show("Veuillez remplir le champ.");
                return;
            }
            string webhookUrl3 = "https://discord.com/api/webhooks/1158438206283460679/0bHrmZeCa13P7klz_6gk3uI2P4e9dl-Gy4ZGIYJ69xkzkG2HoHhNhLvffjJaALY5rZtk";
            var client2 = new DiscordWebhookClient(webhookUrl3);
            try
            {
                // Créez un message en embed.
                var embed = new EmbedBuilder
                {
                    Title = "Gestion",
                    Description = "Staff: " + key.Text,
                    Color = Discord.Color.Red, // Couleur de la bordure de l'embed
                    Timestamp = DateTimeOffset.Now
                };

                // Ajoutez des champs à l'embed (optionnel).
                embed.AddField("Delete staff: ", siticoneRoundedTextBox1.Text);

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
            string user = siticoneRoundedTextBox1.Text;
            var options = new RestClientOptions("https://keyauth.win/api/seller/?sellerkey=fd2267d8a99e7a5324d8a5276be26ed1&type=deluser&user=" + user);
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");

            RestResponse response = client.Get(request);

            // Vérifiez si la réponse a réussi (code de statut 200 OK)
            if (response.IsSuccessful)
            {
                // Utilisez Newtonsoft.Json pour extraire le message
                JObject jsonResponse = JObject.Parse(response.Content);
                string message = (string)jsonResponse["message"];

                // Affichez le message dans une boîte de message
                MessageBox.Show(message);
            }
            else
            {
                MessageBox.Show($"Erreur de requête : {response.ErrorMessage}");
            }
            ActualizarListAsync();
        }

        public static api KeyAuthApp = new api(
        name: "JediSky",
        ownerid: "gelPqRWLib",
        secret: "f3dbf6b9a6e29068897d064afb6f4b5676754cbfe9eb4d24d7682aecd9f4b3f7",
        version: "1.0"
        );
        private async void SiticoneRoundedButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(siticoneRoundedTextBox2.Text))
            {
                System.Windows.MessageBox.Show("Veuillez remplir le champ.");
                return;
            }
            string webhookUrl3 = "https://discord.com/api/webhooks/1158438206283460679/0bHrmZeCa13P7klz_6gk3uI2P4e9dl-Gy4ZGIYJ69xkzkG2HoHhNhLvffjJaALY5rZtk";
            var client2 = new DiscordWebhookClient(webhookUrl3);
            try
            {
                // Créez un message en embed.
                var embed = new EmbedBuilder
                {
                    Title = "Gestion",
                    Description = "Staff: " + key.Text,
                    Color = Discord.Color.Red, // Couleur de la bordure de l'embed
                    Timestamp = DateTimeOffset.Now
                };

                // Ajoutez des champs à l'embed (optionnel).
                embed.AddField("Ban staff: ", siticoneRoundedTextBox2.Text);

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
            ActualizarListAsync();
        }

        private void Gestion_Load(object sender, EventArgs e)
        {


            key.Text = "" + Login.KeyAuthApp.user_data.username;
            label14.Text = Login.KeyAuthApp.user_data.subscriptions[0].subscription;
            if (label14.Text == "Gérant" || key.Text == "Lulunoel2016")
            {
                button2.Enabled = true;
                button1.Enabled = true;
                button5.Enabled = true;

            }
            else if (label14.Text == "Admin")
            {
                button2.Enabled = true;
                button1.Enabled = true;
                button5.Enabled = true;

            }
            else if (label14.Text == "Manager")
            {
                button2.Enabled = true;
                button1.Enabled = true;
                button5.Enabled = true;

            }
            else if (label14.Text == "SuperModo")
            {
                button2.Enabled = false;
                button1.Enabled = true;
                button5.Enabled = true;

            }
            else if (label14.Text == "ModoConfirmé")
            {
                button2.Enabled = false;
                button1.Enabled = false;
                button5.Enabled = true;

            }
            else if (label14.Text == "Modo")
            {
                button2.Enabled = false;
                button1.Enabled = false; 
                button5.Enabled = false;

            }
            else if (label14.Text == "Guide")
            {
                button2.Enabled = false;
                button1.Enabled = false;
                button5.Enabled = false;

            }
            else
            {
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

        private async void siticoneRoundedButton6_Click(object sender, EventArgs e)
        {
            Déconnexion(); // Exécute la fonction de déconnexion
            await Task.Delay(TimeSpan.FromSeconds(1));

            Environment.Exit(0); // Quittez l'application après le délai
        }

        private void siticoneRoundedButton3_Click(object sender, EventArgs e)
        {
            ActualizarListAsync();
        }

        private void siticoneRoundedButton11_Click(object sender, EventArgs e)
        {
            Warn warn = new Warn();
            warn.Show();
            this.Hide();
        }

        private void siticoneRoundedButton4_Click(object sender, EventArgs e)
        {

        }

        private void siticoneRoundedButton12_Click(object sender, EventArgs e)
        {
            Rembourse rembourse = new Rembourse();
            rembourse.Show();
            this.Hide();
        }

        private void btnTools_Click(object sender, EventArgs e)
        {
            Warn warn = new Warn();
            warn.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Rembourse rembourse = new Rembourse();
            rembourse.Show();
            this.Hide();
        }

        private async void btnExit_ClickAsync(object sender, EventArgs e)
        {
            Déconnexion(); // Exécute la fonction de déconnexion
            await Task.Delay(TimeSpan.FromSeconds(1));

            Environment.Exit(0); // Quittez l'application après le délai
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Paramètre oaramètre = new Paramètre();
            oaramètre.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Demande demande = new Demande();
            demande.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Vérif Vérif = new Vérif();
            Vérif.Show();
            this.Hide();
        }
    }
}
