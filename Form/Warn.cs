using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Loader;
using MySql.Data.MySqlClient;
using RestSharp;
using Newtonsoft.Json;
using static Loader.Gestion;
using System.Linq;
using System.Drawing;
using Discord.Webhook;
using Discord;
using System.Net.Http;
using System.Globalization;
using System.Timers;

namespace KeyAuth
{
    public partial class Warn : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private readonly MySqlConnection connection = new MySqlConnection("database=s2_test; server=83.150.217.78; user id=pterodactyl; pwd=4wYYZkQ6nccgvbZR4LgK");

        public Warn()
        {
            InitializeComponent();
            Datedederniéreédition.Text = DateTime.Now.ToString();
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;
            Application.EnableVisualStyles();
            // Enregistrez l'heure de démarrage de l'application
            startTime = DateTime.Now;

            // Créez et configurez le Timer
            timer1 = new System.Windows.Forms.Timer
            {
                Interval = 1000 // Mettez à jour toutes les 1000 millisecondes (1 seconde)
            };
            timer1.Tick += Timer_Tick;
            timer1.Start(); // Démarrez le Timer

            // Appelez la mise à jour initiale
            UpdateElapsedTime();
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
        public List<warns> ListdesWarns()
        {
            List<warns> list = new List<warns>();

            string consult = "SELECT * FROM Warns;";
            MySqlCommand comando = new MySqlCommand(consult)
            {
                Connection = Conect()
            };
            MySqlDataReader lecturaDatos = comando.ExecuteReader();
            while (lecturaDatos.Read())
            {
                warns warn = new warns
                {
                    Id = lecturaDatos.GetString("id"),
                    PseudoJoueur = lecturaDatos.GetString("PseudoJoueur"),
                    RaisonWarn = lecturaDatos.GetString("RaisonWarn"),
                    PseudoStaff = lecturaDatos.GetString("PseudoStaff"),
                    Datedederniereedition = lecturaDatos.GetString("Datedederniéreédition"),
                    Datedefin = lecturaDatos.GetString("Datedefin"),
                    Statue = lecturaDatos.GetString("Statue")
                };
                list.Add(warn);
            }
            comando.Connection.Close();

            return list;
        }
        internal void Ajouter(warns warn)
        {
            string ajouter = "INSERT INTO Warns (PseudoJoueur, RaisonWarn, PseudoStaff, Datedederniéreédition, Datedefin, Statue) VALUES ('" + warn.PseudoJoueur + "', '" + warn.PseudoJoueur + "', '" + warn.RaisonWarn + "', '" + warn.PseudoStaff + "', '" + warn.Datedefin + "', '" + warn.Statue + "');";
            MySqlCommand comando = new MySqlCommand(ajouter)
            {
                Connection = Conect()
            };
            comando.ExecuteNonQuery();

            comando.Connection.Close();
        }
        internal void Retirer(warns warn)
        {
            string retirer = "DELETE FROM Warns PseudoJoueur WHERE '" + warn.PseudoJoueur + "';";
            MySqlCommand comando = new MySqlCommand(retirer)
            {
                Connection = Conect()
            };
            comando.ExecuteNonQuery();

            comando.Connection.Close();
        }
        internal void Editer(warns warn)
        {
            string editer = "INSERT INTO Warns (PseudoJoueur, RaisonWarn, PseudoStaff, Datedederniéreédition, Datedefin) VALUES ('"
                + warn.PseudoJoueur + "', '" + warn.RaisonWarn + "', '" + warn.PseudoStaff + "', '" + warn.Datedederniereedition + "', '" + warn.Datedefin + "', '" + warn.Statue + "');";
            MySqlCommand comando = new MySqlCommand(editer)
            {
                Connection = Conect()
            };
            comando.ExecuteNonQuery();

            comando.Connection.Close();
        }
        private void SiticoneControlBox1_Click(object sender, EventArgs e)
        {
            Déconnexion(); // Exécute la fonction de déconnexion

        }
        private async void Main_Load(object sender, EventArgs e)
        {


            key.Text = Login.KeyAuthApp.user_data.username;
            DateTime dateSelectionnee = Datedederniéreédition.Value;

            int joursAAjouter = 15;
            DateTime nouvelleDate = dateSelectionnee.AddDays(joursAAjouter);

            Datedefin.Value = nouvelleDate;
            ActualizarListAsync();
            ActualizarDataGridView();
            //expiry.Text = "Expiry: " + UnixTimeToDateTime(long.Parse(Login.KeyAuthApp.user_data.subscriptions[0].expiry));
            label14.Text = Login.KeyAuthApp.user_data.subscriptions[0].subscription;
            //ip.Text = "IP Address: " + Login.KeyAuthApp.user_data.ip;
            //hwid.Text = "HWID: " + Login.KeyAuthApp.user_data.hwid;
            //createDate.Text = "Creation date: " + UnixTimeToDateTime(long.Parse(Login.KeyAuthApp.user_data.createdate));
            //lastLogin.Text = "Last login: " + UnixTimeToDateTime(long.Parse(Login.KeyAuthApp.user_data.lastlogin));
            //subscriptionDaysLabel.Text = "Expiry in Days: "+ expirydaysleft();
            string playerName = key.Text;
            string playerUUID = await GetPlayerUUID(playerName);

            if (!string.IsNullOrEmpty(playerUUID))
            {
                GetMinecraftHead(playerUUID);
            }
            if (label14.Text == "Gérant" || key.Text == "Lulunoel2016")
            {
                siticoneRoundedButton3.Enabled = true;
                button2.Enabled = true;
                siticoneRoundedButton10.Enabled = true;
                button1.Enabled = true;
                button5.Enabled = true;

            }
            else if (label14.Text == "Admin")
            {
                siticoneRoundedButton3.Enabled = true;
                button2.Enabled = true;
                siticoneRoundedButton10.Enabled = true;
                button1.Enabled = true;
                button5.Enabled = true;

            }
            else if (label14.Text == "Manager")
            {
                siticoneRoundedButton3.Enabled = true;
                button2.Enabled = true;
                siticoneRoundedButton10.Enabled = true;
                button1.Enabled = true;
                button5.Enabled = true;

            }
            else if (label14.Text == "SuperModo")
            {
                siticoneRoundedButton3.Enabled = true;
                button2.Enabled = false;
                siticoneRoundedButton10.Enabled = true;
                button1.Enabled = true;
                button5.Enabled = true;

            }
            else if (label14.Text == "Modo")
            {
                siticoneRoundedButton3.Enabled = false;
                button2.Enabled = false;
                siticoneRoundedButton10.Enabled = false;
                button1.Enabled = false;
                button5.Enabled = false;

            }
            else if (label14.Text == "ModoConfirmé")
            {
                siticoneRoundedButton3.Enabled = false;
                button2.Enabled = false;
                siticoneRoundedButton10.Enabled = false;
                button1.Enabled = false;
                button5.Enabled = true;

            }
            else if (label14.Text == "Guide")
            {
                siticoneRoundedButton3.Enabled = false;
                button2.Enabled = false;
                siticoneRoundedButton10.Enabled = false;
                button1.Enabled = false;
                button5.Enabled = false;

            }
            else
            {
                siticoneRoundedButton3.Enabled = false;
                button2.Enabled = false;
                siticoneRoundedButton10.Enabled = false;
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

                Console.WriteLine($"{rowsAffected} enregistrements supprimés.");

                connection.Close();
            }
        }

        public static bool SubExist(string name, int len)
        {
            for (var i = 0; i < len; i++)
            {
                if (Login.KeyAuthApp.user_data.subscriptions[i].subscription == name)
                {
                    return true;
                }
            }
            return false;
        }
        public string Expirydaysleft()
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
            dtDateTime = dtDateTime.AddSeconds(long.Parse(Login.KeyAuthApp.user_data.subscriptions[0].expiry)).ToLocalTime();
            TimeSpan difference = dtDateTime - DateTime.Now;
            return Convert.ToString(difference.Days + " Days " + difference.Hours + " Hours Left");
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
        private void CheckBDD_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection("database=s2_test; server=83.150.217.78; user id=pterodactyl; pwd=4wYYZkQ6nccgvbZR4LgK");

            try
            {
                connection.Open();
                MessageBox.Show("Connecté");
            }
            catch (MySqlException co)
            {
                MessageBox.Show(co.ToString());
                MessageBox.Show("Non Connecté");
            }
        }

        private void ActualizarDataGridView()
        {
            Warn BaseDeDatos = new Warn();
            List<warns> listaDB = BaseDeDatos.ListdesWarns();
            dataGridView1.Rows.Clear();

            foreach (warns cliente in listaDB)
            {
                dataGridView1.Rows.Add(cliente.Id, cliente.PseudoJoueur, cliente.RaisonWarn, cliente.PseudoStaff, cliente.Datedederniereedition, cliente.Datedefin, cliente.Statue);
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

            listBox2.Items.Clear();

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
                listBox2.Invoke(new Action(() => listBox2.Items.Add(listItem)));
            });
        }

        public class MyResponse
        {
            public bool success { get; set; }
            public string message { get; set; }
            public List<Subscriptions> usernames { get; set; }
        }

        public class Subscriptions
        {
            public string username { get; set; }
        }

        private async void Déconnexion()
        {
            string webhookUrl3 = "https://discord.com/api/webhooks/1158438206283460679/0bHrmZeCa13P7klz_6gk3uI2P4e9dl-Gy4ZGIYJ69xkzkG2HoHhNhLvffjJaALY5rZtk";
            var client2 = new DiscordWebhookClient(webhookUrl3);
            try
            {
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
                Console.WriteLine("Erreur lors de l'envoi du log : " + ex.Message);
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

        private async void Retirerwarn()
        {
            string webhookUrl = "https://discord.com/api/webhooks/1158438206283460679/0bHrmZeCa13P7klz_6gk3uI2P4e9dl-Gy4ZGIYJ69xkzkG2HoHhNhLvffjJaALY5rZtk";
            var client = new DiscordWebhookClient(webhookUrl);
            try
            {
                // Créez un message en embed.
                var embed = new EmbedBuilder
                {
                    Title = "Warns Retirer",
                    Description = "Retirer par: " + key.Text,
                    Color = Discord.Color.Orange, // Couleur de la bordure de l'embed
                    Timestamp = DateTimeOffset.Now
                };

                // Ajoutez des champs à l'embed (optionnel).
                embed.AddField("Warn", label23.Text);
                embed.AddField("Raison", label24.Text);

                // Ajoutez l'embed au message.
                await client.SendMessageAsync(embeds: new[] { embed.Build() }, isTTS: false, username: "Panel staff logs");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'envoi du log : " + ex.Message);
            }
            finally
            {
                // N'oubliez pas de libérer les ressources du client.
                client.Dispose();
            }
            DateTime now = DateTime.Now;
            string formattedDate = now.ToString("dd-MM-yyyy HH:mm:ss");

            string pseudo = label22.Text;
            string joueur = label23.Text;
            string raison = label24.Text;
            string action = "Retirer un warn";
            string date = formattedDate;

            string editer = "INSERT INTO `Logs`(`Pseudo`, `Action`, `Joueur`, `Raison`, `Date`) VALUES (@Pseudo, @Action, @Joueur, @Raison, @Date)";

            using (MySqlConnection connection = Conect())
            {
                using (MySqlCommand comando = new MySqlCommand(editer, connection))
                {
                    comando.Parameters.AddWithValue("@Pseudo", pseudo);
                    comando.Parameters.AddWithValue("@Action", action);
                    comando.Parameters.AddWithValue("@Joueur", joueur);
                    comando.Parameters.AddWithValue("@Raison", raison);
                    comando.Parameters.AddWithValue("@Date", date);

                    comando.ExecuteNonQuery();
                }
            }
        }

        private async void AddWarn()
        {
            string webhookUrl = "https://discord.com/api/webhooks/1158438206283460679/0bHrmZeCa13P7klz_6gk3uI2P4e9dl-Gy4ZGIYJ69xkzkG2HoHhNhLvffjJaALY5rZtk";
            var client = new DiscordWebhookClient(webhookUrl);
            try
            {
                // Créez un message en embed.
                var embed = new EmbedBuilder
                {
                    Title = "Warns",
                    Description = "Staff: " + key.Text,
                    Color = Discord.Color.Blue, // Couleur de la bordure de l'embed
                    Timestamp = DateTimeOffset.Now
                };

                // Ajoutez des champs à l'embed (optionnel).
                embed.AddField("Warn", siticoneRoundedTextBox1.Text);
                embed.AddField("Raison", siticoneRoundedTextBox2.Text);
                embed.AddField("Date", "Du: " + Datedederniéreédition.Text + " au: " + Datedefin.Text);

                // Ajoutez l'embed au message.
                await client.SendMessageAsync(embeds: new[] { embed.Build() }, isTTS: false, username: "Panel staff logs");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'envoi du log : " + ex.Message);
            }
            finally
            {
                // N'oubliez pas de libérer les ressources du client.
                client.Dispose();
            }

            DateTime now = DateTime.Now;
            string formattedDate = now.ToString("dd-MM-yyyy HH:mm:ss");

            string pseudo = key.Text;
            string joueur = siticoneRoundedTextBox1.Text;
            string raison = siticoneRoundedTextBox2.Text;
            string action = "Ajout de warn";
            string date = formattedDate;

            string editer = "INSERT INTO `Logs`(`Pseudo`, `Action`, `Joueur`, `Raison`, `Date`) VALUES (@Pseudo, @Action, @Joueur, @Raison, @Date)";

            using (MySqlConnection connection = Conect())
            {
                using (MySqlCommand comando = new MySqlCommand(editer, connection))
                {
                    comando.Parameters.AddWithValue("@Pseudo", pseudo);
                    comando.Parameters.AddWithValue("@Action", action);
                    comando.Parameters.AddWithValue("@Joueur", joueur);
                    comando.Parameters.AddWithValue("@Raison", raison);
                    comando.Parameters.AddWithValue("@Date", date);

                    comando.ExecuteNonQuery();
                }
            }
        }
        private async void Removearn()
        {
            string webhookUrl = "https://discord.com/api/webhooks/1158438206283460679/0bHrmZeCa13P7klz_6gk3uI2P4e9dl-Gy4ZGIYJ69xkzkG2HoHhNhLvffjJaALY5rZtk";
            var client = new DiscordWebhookClient(webhookUrl);
            try
            {
                // Créez un message en embed.
                var embed = new EmbedBuilder
                {
                    Title = "Warns Delete",
                    Description = "Delete par: " + key.Text,
                    Color = Discord.Color.Red, // Couleur de la bordure de l'embed
                    Timestamp = DateTimeOffset.Now
                };

                // Ajoutez des champs à l'embed (optionnel).
                embed.AddField("Warn", label23.Text);
                embed.AddField("Raison", label24.Text);

                // Ajoutez l'embed au message.
                await client.SendMessageAsync(embeds: new[] { embed.Build() }, isTTS: false, username: "Panel staff logs");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'envoi du log : " + ex.Message);
            }
            finally
            {
                // N'oubliez pas de libérer les ressources du client.
                client.Dispose();
            }

            DateTime now = DateTime.Now;
            string formattedDate = now.ToString("dd-MM-yyyy HH:mm:ss");

            string pseudo = label22.Text;
            string joueur = label23.Text;
            string raison = label24.Text;
            string action = "Suppression de warn";
            string date = formattedDate;

            string editer = "INSERT INTO `Logs`(`Pseudo`, `Action`, `Joueur`, `Raison`, `Date`) VALUES (@Pseudo, @Action, @Joueur, @Raison, @Date)";

            using (MySqlConnection connection = Conect())
            {
                using (MySqlCommand comando = new MySqlCommand(editer, connection))
                {
                    comando.Parameters.AddWithValue("@Pseudo", pseudo);
                    comando.Parameters.AddWithValue("@Action", action);
                    comando.Parameters.AddWithValue("@Joueur", joueur);
                    comando.Parameters.AddWithValue("@Raison", raison);
                    comando.Parameters.AddWithValue("@Date", date);

                    comando.ExecuteNonQuery();
                }
            }
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            ActualizarDataGridView();
        }
        private void RgstrBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(siticoneRoundedTextBox2.Text) || string.IsNullOrEmpty(siticoneRoundedTextBox1.Text))
            {
                System.Windows.MessageBox.Show("Veuillez remplir les champs.");
                return;
            }

            warns warns = new warns
            {
                PseudoJoueur = siticoneRoundedTextBox1.Text,
                RaisonWarn = siticoneRoundedTextBox2.Text,
                PseudoStaff = key.Text,
                Datedederniereedition = Datedederniéreédition.Text,
                Datedefin = Datedefin.Text,
                Statue = label11.Text
            };
            string agregar = "INSERT INTO Warns (PseudoJoueur, RaisonWarn, PseudoStaff, Datedederniéreédition, Datedefin, Statue) VALUES ('" + warns.Type
+ warns.PseudoJoueur + "', '" + warns.RaisonWarn + "', '" + warns.PseudoStaff + "', '" + warns.Datedederniereedition + "', '" + warns.Datedefin + "', '" + warns.Statue + "');";
            MySqlCommand comando = new MySqlCommand(agregar)
            {
                Connection = Conect()
            };
            comando.ExecuteNonQuery();
            comando.Connection.Close();

            ActualizarDataGridView();
            AddWarn();
            siticoneRoundedTextBox1.Clear();
            siticoneRoundedTextBox2.Clear();
        }
        private void SiticoneRoundedButton1_Click(object sender, EventArgs e)
        {
            ActualizarDataGridView();
        }
        private void SiticoneRoundedButton4_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection("database=jedilitebans; server=142.132.158.144; user id=lulu; pwd=vkfH74eT555NGf");
            try
            {
                connection.Open();
                MessageBox.Show("Connecté");
            }
            catch (InvalidCastException ex)
            {
                MessageBox.Show("Erreur de conversion : " + ex.Message);
                MessageBox.Show("StackTrace : " + ex.StackTrace);
            }
        }
        private void SiticoneRoundedButton1_Click_1(object sender, EventArgs e)
        {
            ActualizarDataGridView();

        }
        private void SiticoneRoundedButton2_Click(object sender, EventArgs e)
        {
            DateTime dateSelectionnee = Datedederniéreédition.Value;

            int joursAAjouter = 15;
            DateTime nouvelleDate = dateSelectionnee.AddDays(joursAAjouter);

            Datedefin.Value = nouvelleDate;

        }
        private void Datedederniéreédition_ValueChanged(object sender, EventArgs e)
        {
            DateTime dateSelectionnee = Datedederniéreédition.Value;

            int joursAAjouter = 15;
            DateTime nouvelleDate = dateSelectionnee.AddDays(joursAAjouter);

            Datedefin.Value = nouvelleDate;
        }
        private void SiticoneRoundedButton2_Click_1(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Logs"; // Remplacez par votre requête SQL

#pragma warning disable CS0642 // Possibilité d'instruction vide erronée
            using (MySqlConnection connection = new MySqlConnection("database=s2_test; server=83.150.217.78; user id=pterodactyl; pwd=4wYYZkQ6nccgvbZR4LgK")) ;
#pragma warning restore CS0642 // Possibilité d'instruction vide erronée
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    using (StreamWriter fileWriter = new StreamWriter("Logs.txt"))
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string value = reader[i].ToString();
                                fileWriter.Write(value);
                                if (i < reader.FieldCount - 1)
                                {
                                    fileWriter.Write("\t"); // Utilisez une tabulation ou un autre délimiteur selon vos besoins
                                }
                            }
                            fileWriter.WriteLine(); // Passage à la ligne suivante pour le prochain enregistrement
                        }
                    }
                    string appDirectory = System.AppDomain.CurrentDomain.BaseDirectory;

                    Process.Start("explorer.exe", appDirectory);
                }
                connection.Close();
            }
        }

        private void SiticoneRoundedButton3_Click_1(object sender, EventArgs e)
        {
            string retirer = "DELETE FROM Warns WHERE Id ='" + siticoneRoundedTextBox3.Text + "';";
            MySqlCommand comando = new MySqlCommand(retirer)
            {
                Connection = Conect()
            };
            comando.ExecuteNonQuery();
            comando.Connection.Close();
            Removearn();
            ActualizarDataGridView();
            label22.Text = "N/A";
            label23.Text = "N/A";
            label24.Text = "N/A";
            label17.Text = "N/A";

        }

        private async void SiticoneRoundedButton5_Click(object sender, EventArgs e)
        {
            string idRecherche = siticoneRoundedTextBox3.Text;
#pragma warning disable CS0642 // Possibilité d'instruction vide erronée
            using (MySqlConnection connection = new MySqlConnection("database=s2_test; server=83.150.217.78; user id=pterodactyl; pwd=4wYYZkQ6nccgvbZR4LgK")) ;
#pragma warning restore CS0642 // Possibilité d'instruction vide erronée
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Warns WHERE Id = @Id"; // Remplacez "VotreTable" par le nom de votre table
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", idRecherche);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Accédez aux colonnes de la ligne résultante ici
                            int id = reader.GetInt32("Id");
                            string PseudoJoueur = reader.GetString("PseudoJoueur");
                            string RaisonWarn = reader.GetString("RaisonWarn");
                            string PseudoStaff = reader.GetString("PseudoStaff");
                            string Statue = reader.GetString("Statue");
                            // Ajoutez d'autres colonnes en fonction de votre table
                            label22.Text = PseudoStaff;
                            label23.Text = PseudoJoueur;
                            label24.Text = RaisonWarn;
                            label17.Text = Statue;

                        }
                        else
                        {
                            label22.Text = "N/A";
                            label23.Text = "N/A";
                            label24.Text = "N/A";
                            label17.Text = "N/A";
                        }
                    }
                }
                connection.Close();
            }
            string playerName = label23.Text;
            string playerUUID = await GetPlayerUUID(playerName);

            if (!string.IsNullOrEmpty(playerUUID))
            {
                GetMinecraftHead(playerUUID);
            }
        }

        private async void SiticoneRoundedButton6_Click(object sender, EventArgs e)
        {
            Déconnexion(); // Exécute la fonction de déconnexion
            await Task.Delay(TimeSpan.FromSeconds(1));

            Environment.Exit(0); // Quittez l'application après le délai
        }

        private void siticoneRoundedButton7_Click(object sender, EventArgs e)
        {
            ActualizarListAsync();
        }

        private void siticoneRoundedButton8_Click(object sender, EventArgs e)
        {
            Gestion gestion = new Gestion();
            gestion.Show();
            this.Hide();
        }

        public List<warns> ListRechercheWarns()
        {
            string idRecherche = siticoneRoundedTextBox4.Text;
            List<warns> list = new List<warns>();
            string consult = "SELECT * FROM Warns WHERE PseudoJoueur = @PseudoJoueur;";

            using (MySqlConnection connection = Conect())
            {
                MySqlCommand comando = new MySqlCommand(consult, connection);
                comando.Parameters.AddWithValue("@PseudoJoueur", idRecherche);

                using (MySqlDataReader lecturaDatos = comando.ExecuteReader())
                {
                    while (lecturaDatos.Read())
                    {
                        warns warn = new warns
                        {
                            Id = lecturaDatos.GetString("id"),
                            PseudoJoueur = lecturaDatos.GetString("PseudoJoueur"),
                            RaisonWarn = lecturaDatos.GetString("RaisonWarn"),
                            PseudoStaff = lecturaDatos.GetString("PseudoStaff"),
                            Datedederniereedition = lecturaDatos.GetString("Datedederniéreédition"),
                            Datedefin = lecturaDatos.GetString("Datedefin"),
                            Statue = lecturaDatos.GetString("Statue")
                        };
                        list.Add(warn);
                    }
                }
            }

            return list;
        }

        private void ActualizarRecherche()
        {
            List<warns> listaDB = ListRechercheWarns();

            // Effacez les lignes existantes dans la DataGridView.
            dataGridView1.Rows.Clear();

            foreach (warns warn in listaDB)
            {
                // Ajoutez une nouvelle ligne à la DataGridView et spécifiez les valeurs des colonnes.
                dataGridView1.Rows.Add(warn.Id, warn.PseudoJoueur, warn.RaisonWarn, warn.PseudoStaff, warn.Datedederniereedition, warn.Datedefin, warn.Statue);
            }
        }


        private void Actualiseretirer()
        {
            string idRecherche = siticoneRoundedTextBox3.Text;
#pragma warning disable CS0642 // Possibilité d'instruction vide erronée
            using (MySqlConnection connection = new MySqlConnection("database=s2_test; server=83.150.217.78; user id=pterodactyl; pwd=4wYYZkQ6nccgvbZR4LgK")) ;
#pragma warning restore CS0642 // Possibilité d'instruction vide erronée
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Warns WHERE Id = @Id"; // Remplacez "VotreTable" par le nom de votre table
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", idRecherche);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Accédez aux colonnes de la ligne résultante ici
                            int id = reader.GetInt32("Id");
                            string PseudoJoueur = reader.GetString("PseudoJoueur");
                            string RaisonWarn = reader.GetString("RaisonWarn");
                            string PseudoStaff = reader.GetString("PseudoStaff");
                            string Statue = reader.GetString("Statue");
                            // Ajoutez d'autres colonnes en fonction de votre table
                            label22.Text = PseudoStaff;
                            label23.Text = PseudoJoueur;
                            label24.Text = RaisonWarn;
                            label17.Text = Statue;

                        }
                        else
                        {
                            Console.WriteLine("Warn non trouvé.");
                        }
                    }
                }
                connection.Close();
            }
        }

        private async void siticoneRoundedButton9_Click(object sender, EventArgs e)
        {
            ActualizarRecherche();
            string playerName = label23.Text;
            string playerUUID = await GetPlayerUUID(playerName);

            if (!string.IsNullOrEmpty(playerUUID))
            {
                GetMinecraftHead(playerUUID);
            }
        }



        private void siticoneRoundedButton10_Click(object sender, EventArgs e)
        {
            Actualiseretirer();
            warns warns = new warns
            {
                PseudoJoueur = label23.Text,
                RaisonWarn = label24.Text,
                PseudoStaff = label22.Text,
                Datedederniereedition = "2001/01/01",
                Datedefin = "2001/01/01",
                Statue = "Inactif ❌"
            };
            string agregar = "INSERT INTO Warns (PseudoJoueur, RaisonWarn, PseudoStaff, Datedederniéreédition, Datedefin, Statue) VALUES ('"
+ warns.PseudoJoueur + "', '" + warns.RaisonWarn + "', '" + warns.PseudoStaff + "', '" + warns.Datedederniereedition + "', '" + warns.Datedefin + "', '" + warns.Statue + "');";
            string retirer = "DELETE FROM Warns WHERE Id ='" + siticoneRoundedTextBox3.Text + "';";
            MySqlCommand comando = new MySqlCommand(retirer)
            {
                Connection = Conect()
            };
            MySqlCommand comando2 = new MySqlCommand(agregar)
            {
                Connection = Conect()
            };

            comando.ExecuteNonQuery();
            comando.Connection.Close();
            comando2.ExecuteNonQuery();
            comando2.Connection.Close();
            Retirerwarn();
            ActualizarDataGridView();
        }
        private readonly DateTime startTime;

        private void Timer_Tick(object sender, EventArgs e)
        {
            // À chaque tick du Timer, mettez à jour le temps écoulé
            UpdateElapsedTime();
        }

        private void UpdateElapsedTime()
        {
            // Calculez la durée écoulée depuis le démarrage de l'application
            TimeSpan elapsedTime = DateTime.Now - startTime;

            // Mettez à jour le texte du Label avec le temps écoulé
            label26.Text = $"{elapsedTime.Hours:D2}:{elapsedTime.Minutes:D2}:{elapsedTime.Seconds:D2}";
        }

        private void siticoneRoundedButton12_Click(object sender, EventArgs e)
        {
            Rembourse rembourse = new Rembourse();
            rembourse.Show();
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

        private async void btnExit_Click(object sender, EventArgs e)
        {
            Déconnexion(); // Exécute la fonction de déconnexion
            await Task.Delay(TimeSpan.FromSeconds(1));

            Environment.Exit(0); // Quittez l'application après le délai
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Paramètre oaramètre = new Paramètre();
            oaramètre.Show();
            this.Hide();
        }

        private void deleteLeWarnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Récupérez la valeur de l'ID à partir de la ligne sélectionnée
                string idToDelete = dataGridView1.SelectedRows[0].Cells["Id"].Value.ToString();

                // Construisez la requête de suppression
                string retirer = "DELETE FROM Warns WHERE Id ='" + idToDelete + "';";

                // Exécutez la requête de suppression
                MySqlCommand comando = new MySqlCommand(retirer)
                {
                    Connection = Conect()
                };
                comando.ExecuteNonQuery();
                comando.Connection.Close();
                Removearn();

                // Mettez à jour la DataGridView après la suppression
                ActualizarDataGridView();
            }
            else
            {
                // Gérer le cas où aucune ligne n'est sélectionnée
                MessageBox.Show("Sélectionnez une ligne à supprimer.", "Aucune ligne sélectionnée", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void retirerLeWarnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Récupérez la valeur de l'ID à partir de la ligne sélectionnée
                string idToRemove = dataGridView1.SelectedRows[0].Cells["Id"].Value.ToString();

                // Construisez la requête de suppression
                string retirer = "DELETE FROM Warns WHERE Id ='" + idToRemove + "';";

                // Exécutez la requête de suppression
                MySqlCommand comando = new MySqlCommand(retirer)
                {
                    Connection = Conect()
                };
                comando.ExecuteNonQuery();
                comando.Connection.Close();

                // Créez un nouvel enregistrement avec les mêmes valeurs que celui que vous avez supprimé
                warns warns = new warns
                {
                    PseudoJoueur = label23.Text,
                    RaisonWarn = label24.Text,
                    PseudoStaff = label22.Text,
                    Datedederniereedition = "2001/01/01",
                    Datedefin = "2001/01/01",
                    Statue = "Inactif ❌"
                };

                // Construisez la requête d'insertion
                string agregar = "INSERT INTO Warns (PseudoJoueur, RaisonWarn, PseudoStaff, Datedederniéreédition, Datedefin, Statue) VALUES ('"
                    + warns.PseudoJoueur + "', '" + warns.RaisonWarn + "', '" + warns.PseudoStaff + "', '" + warns.Datedederniereedition + "', '" + warns.Datedefin + "', '" + warns.Statue + "');";

                // Exécutez la requête d'insertion
                MySqlCommand comando2 = new MySqlCommand(agregar)
                {
                    Connection = Conect()
                };
                comando2.ExecuteNonQuery();
                comando2.Connection.Close();
                Retirerwarn();
                // Mettez à jour la DataGridView après les opérations
                ActualizarDataGridView();
            }
            else
            {
                // Gérer le cas où aucune ligne n'est sélectionnée
                MessageBox.Show("Sélectionnez une ligne à retirer.", "Aucune ligne sélectionnée", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
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

        private async void siticoneRoundedButton2_Click_2(object sender, EventArgs e)
        {
            string playerName = label23.Text;
            string playerUUID = await GetPlayerUUID(playerName);

            if (!string.IsNullOrEmpty(playerUUID))
            {
                GetMinecraftHead(playerUUID);
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
                        Console.WriteLine($"Impossible d'obtenir l'UUID de {playerName}. Réponse HTTP : {response.StatusCode}");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
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

        private void button5_Click(object sender, EventArgs e)
        {
            Vérif Vérif = new Vérif();
            Vérif.Show();
            this.Hide();
        }


        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Vérifie si un clic a eu lieu sur une ligne valide
            {
                if (label14.Text == " " || label14.Text == "Modo" || label14.Text == "Guide")
                {
                    // Accédez aux valeurs des cellules dans la ligne sélectionnée
                    string id = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                    string pseudo = dataGridView1.Rows[e.RowIndex].Cells["PseudoJoueur"].Value.ToString();
                    string raison = dataGridView1.Rows[e.RowIndex].Cells["RaisonWarn"].Value.ToString();
                    string pseudostaff = dataGridView1.Rows[e.RowIndex].Cells["PseudoStaff"].Value.ToString();
                    string datedederniereedition = dataGridView1.Rows[e.RowIndex].Cells["Datedederniereedition"].Value.ToString();
                    string datedefin = dataGridView1.Rows[e.RowIndex].Cells["Date_de_fin"].Value.ToString();
                    string statut = dataGridView1.Rows[e.RowIndex].Cells["Statue"].Value.ToString();

                    // Affectez chaque élément à des contrôles différents
                    siticoneRoundedTextBox3.Text = id;
                    label23.Text = pseudo;
                    label22.Text = raison;
                    label24.Text = pseudostaff;
                    label17.Text = statut;

                    string playerName = label23.Text;
                    string playerUUID = await GetPlayerUUID(playerName);

                    if (!string.IsNullOrEmpty(playerUUID))
                    {
                        GetMinecraftHead(playerUUID);
                    }
                }
                else
                {
                    // Accédez aux valeurs des cellules dans la ligne sélectionnée
                    string id = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                    string pseudo = dataGridView1.Rows[e.RowIndex].Cells["PseudoJoueur"].Value.ToString();
                    string raison = dataGridView1.Rows[e.RowIndex].Cells["RaisonWarn"].Value.ToString();
                    string pseudostaff = dataGridView1.Rows[e.RowIndex].Cells["PseudoStaff"].Value.ToString();
                    string datedederniereedition = dataGridView1.Rows[e.RowIndex].Cells["Datedederniereedition"].Value.ToString();
                    string datedefin = dataGridView1.Rows[e.RowIndex].Cells["Date_de_fin"].Value.ToString();
                    string statut = dataGridView1.Rows[e.RowIndex].Cells["Statue"].Value.ToString();

                    // Affectez chaque élément à des contrôles différents
                    siticoneRoundedTextBox3.Text = id;
                    label23.Text = pseudo;
                    label24.Text = raison;
                    label22.Text = pseudostaff;
                    label17.Text = statut;

                    string playerName = label23.Text;
                    string playerUUID = await GetPlayerUUID(playerName);

                    if (!string.IsNullOrEmpty(playerUUID))
                    {
                        GetMinecraftHead(playerUUID);
                    }
                    ContextMenuStrip contextMenu = new ContextMenuStrip
                    {

                        // Change la couleur de fond en utilisant les composants R, G, B
                        BackColor = System.Drawing.Color.FromArgb(35, 39, 42),
                        ShowImageMargin = false
                    };

                    ToolStripMenuItem menuItem1 = new ToolStripMenuItem("Delete le warn");
                    ToolStripMenuItem menuItem2 = new ToolStripMenuItem("Retirer le warn");

                    // Change la couleur du texte en rouge pour "Option 1"
                    menuItem1.ForeColor = System.Drawing.Color.White;
                    menuItem2.ForeColor = System.Drawing.Color.White;

                    // Associez des gestionnaires d'événements aux éléments de menu si nécessaire
                    menuItem1.Click += deleteLeWarnToolStripMenuItem_Click;
                    menuItem2.Click += retirerLeWarnToolStripMenuItem_Click;

                    // Ajoutez les éléments de menu au ContextMenuStrip
                    contextMenu.Items.Add(menuItem1);
                    contextMenu.Items.Add(menuItem2);

                    // Associez le ContextMenuStrip au DataGridView
                    dataGridView1.ContextMenuStrip = contextMenu;
                }
            }
        }
    }
}