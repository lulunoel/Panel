using Discord.Webhook;
using Discord;
using KeyAuth;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Siticone.UI.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Diagnostics;
using System.Globalization;
using System.Timers;

namespace Loader
{
    public partial class Vérif : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public Vérif()
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
                        System.Windows.MessageBox.Show($"Impossible de récupérer la tête de {playerUUID}. Réponse HTTP : {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Une erreur s'est produite : {ex.Message}");
            }
        }

        private async void Vérif_Load(object sender, EventArgs e)
        {


            siticoneRoundedTextBox5.Text = AppDomain.CurrentDomain.BaseDirectory + "J_starwars_cercle_2.ico";
            siticoneRoundedTextBox4.Text = "J_starwars_cercle_2.ico";
            key.Text = Login.KeyAuthApp.user_data.username;
            label14.Text = Login.KeyAuthApp.user_data.subscriptions[0].subscription;
            string playerName = key.Text;
            string playerUUID = await GetPlayerUUID(playerName);
            ActualizarLista();
            if (!string.IsNullOrEmpty(playerUUID))
            {
                GetMinecraftHead(playerUUID);
            }
            if (label14.Text == "Gérant" || key.Text == "Lulunoel2016")
            {
                button2.Enabled = true;
                button1.Enabled = true;
                btnTools.Enabled = true;
                siticoneRoundedButton2.Enabled = true;

            }
            else if (label14.Text == "Admin")
            {
                button2.Enabled = true;
                button1.Enabled = true;
                btnTools.Enabled = true;
                siticoneRoundedButton2.Enabled = true;

            }
            else if (label14.Text == "Manager")
            {
                button2.Enabled = true;
                button1.Enabled = true;
                btnTools.Enabled = true;
                siticoneRoundedButton2.Enabled = true;

            }
            else if (label14.Text == "SuperModo")
            {
                button2.Enabled = false;
                button1.Enabled = true;
                btnTools.Enabled = true;
                siticoneRoundedButton2.Enabled = true;

            }
            else if (label14.Text == "ModoConfirmé")
            {
                button2.Enabled = false;
                button1.Enabled = false;
                btnTools.Enabled = true;
                siticoneRoundedButton2.Enabled = false;

            }
            else if (label14.Text == "Modo")
            {
                button2.Enabled = false;
                button1.Enabled = false;
                btnTools.Enabled = true;
                siticoneRoundedButton2.Enabled = false;

            }
            else if (label14.Text == "Guide")
            {
                button2.Enabled = false;
                button1.Enabled = false;
                btnTools.Enabled = false;
                siticoneRoundedButton2.Enabled = false;

            }
            else
            {
                btnTools.Enabled = false;
                button2.Enabled = false;
                button1.Enabled = false;
                siticoneRoundedButton2.Enabled = false;

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

        private async void siticoneRoundedButton6_Click(object sender, EventArgs e)
        {
            Déconnexion(); // Exécute la fonction de déconnexion
            await Task.Delay(TimeSpan.FromSeconds(1));

            Environment.Exit(0); // Quittez l'application après le délai
        }

        private void RgstrBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(siticoneRoundedTextBox2.Text) || string.IsNullOrEmpty(siticoneRoundedTextBox1.Text))
            {
                System.Windows.MessageBox.Show("Veuillez remplir les champs.");
                return;
            }
            if (string.IsNullOrEmpty(siticoneRoundedTextBox5.Text))
            {
                System.Windows.MessageBox.Show("Veuillez sélectionner un fichier.");
                return;
            }

            // Lisez le contenu du fichier sélectionné.
            byte[] fichierBytes = File.ReadAllBytes(siticoneRoundedTextBox5.Text);

            vérif vérifs = new vérif
            {
                PseudoJoueur = siticoneRoundedTextBox1.Text,
                Raison = siticoneRoundedTextBox2.Text,
                PseudoStaff = key.Text,
                Datedederniéreédition = Datedederniéreédition.Text,
                Statut = label11.Text,
                nomFichier = siticoneRoundedTextBox4.Text,
            };

            // Utilisez une requête paramétrée pour insérer les données en toute sécurité
            string agregar = "INSERT INTO Verif (PseudoJoueur, Raison, Preuve, nomFichier, PseudoStaff, Datedederniéreédition, Statut) VALUES (@PseudoJoueur, @Raison, @Preuve, @nomFichier, @PseudoStaff, @Datedederniéreédition, @Statut);";

            using (MySqlConnection connection = Conect())
            {
                using (MySqlCommand command = new MySqlCommand(agregar, connection))
                {
                    command.Parameters.AddWithValue("@PseudoJoueur", vérifs.PseudoJoueur);
                    command.Parameters.AddWithValue("@Raison", vérifs.Raison);
                    command.Parameters.AddWithValue("@Preuve", fichierBytes);
                    command.Parameters.AddWithValue("@nomFichier", vérifs.nomFichier);
                    command.Parameters.AddWithValue("@PseudoStaff", vérifs.PseudoStaff);
                    command.Parameters.AddWithValue("@Datedederniéreédition", vérifs.Datedederniéreédition);
                    command.Parameters.AddWithValue("@Statut", vérifs.Statut);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }

            ActualizarLista();
            AddVerif();
            siticoneRoundedTextBox1.Clear();
            siticoneRoundedTextBox2.Clear();
        }


        private void ActualizarLista()
        {
            Vérif BaseDeDatos = new Vérif();
            List<vérif> listaDB = BaseDeDatos.ListdesVérif();

            listBox1.Items.Clear();
            for (int i = 0; i < listaDB.Count; i++)
            {
                vérif cliente = listaDB[i];
                listBox1.Items.Add(cliente);
            }
        }

        public List<vérif> ListdesVérif()
        {
            List<vérif> list = new List<vérif>();

            string consult = "SELECT * FROM Verif;";
            MySqlCommand comando = new MySqlCommand(consult)
            {
                Connection = Conect()
            };
            MySqlDataReader lecturaDatos = comando.ExecuteReader();
            while (lecturaDatos.Read())
            {
                vérif warn = new vérif
                {
                    Id = lecturaDatos.GetString("id"),
                    PseudoJoueur = lecturaDatos.GetString("PseudoJoueur"),
                    Raison = lecturaDatos.GetString("Raison"),
                    PseudoStaff = lecturaDatos.GetString("PseudoStaff"),
                    Datedederniéreédition = lecturaDatos.GetString("Datedederniéreédition"),
                    Statut = lecturaDatos.GetString("Statut")
                };
                list.Add(warn);
            }
            comando.Connection.Close();

            return list;
        }

        private async void AddVerif()
        {
            string webhookUrl = "https://discord.com/api/webhooks/1158729642723786802/dno--daaxACdnpQufIW7aSDL3_M04M2JBfhLKCt8ug7dVmHbZdG8mgSgyWruLVm1iu11";
            var client = new DiscordWebhookClient(webhookUrl);
            try
            {
                // Créez un message en embed.
                var embed = new EmbedBuilder
                {
                    Title = "Vérif",
                    Description = "Vérificateur: " + key.Text,
                    Color = Discord.Color.Blue, // Couleur de la bordure de l'embed
                    Timestamp = DateTimeOffset.Now
                };

                // Ajoutez des champs à l'embed (optionnel).
                embed.AddField("Pseudo joueur: ", siticoneRoundedTextBox1.Text);
                embed.AddField("Cheat trouvé: ", siticoneRoundedTextBox2.Text);

                // Ajoutez l'embed au message.
                await client.SendMessageAsync(embeds: new[] { embed.Build() }, isTTS: false, username: "Panel staff Vérif");
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
        }
        private readonly MySqlConnection connection = new MySqlConnection("database=s2_test; server=83.150.217.78; user id=pterodactyl; pwd=4wYYZkQ6nccgvbZR4LgK");

        private async void siticoneRoundedButton3_Click(object sender, EventArgs e)
        {
            string idRecherche = siticoneRoundedTextBox3.Text;
#pragma warning disable CS0642 // Possibilité d'instruction vide erronée
            using (MySqlConnection connection = new MySqlConnection("database=s2_test; server=83.150.217.78; user id=pterodactyl; pwd=4wYYZkQ6nccgvbZR4LgK")) ;
#pragma warning restore CS0642 // Possibilité d'instruction vide erronée
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Verif WHERE Id = @Id"; // Remplacez "VotreTable" par le nom de votre table
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
                            string RaisonWarn = reader.GetString("Raison");
                            string PseudoStaff = reader.GetString("PseudoStaff");
                            string Statue = reader.GetString("Statut");
                            // Ajoutez d'autres colonnes en fonction de votre table
                            label22.Text = PseudoStaff;
                            label23.Text = PseudoJoueur;
                            label24.Text = RaisonWarn;
                        }
                        else
                        {
                            label22.Text = "N/A";
                            label23.Text = "N/A";
                            label24.Text = "N/A";
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

        private void siticoneRoundedButton2_Click(object sender, EventArgs e)
        {
            string retirer = "DELETE FROM Verif WHERE Id ='" + siticoneRoundedTextBox3.Text + "';";
            MySqlCommand comando = new MySqlCommand(retirer)
            {
                Connection = Conect()
            };
            comando.ExecuteNonQuery();
            comando.Connection.Close();
            Removeverif();
            ActualizarLista();
        }

        private async void Removeverif()
        {
            string webhookUrl = "https://discord.com/api/webhooks/1158729642723786802/dno--daaxACdnpQufIW7aSDL3_M04M2JBfhLKCt8ug7dVmHbZdG8mgSgyWruLVm1iu11";
            var client = new DiscordWebhookClient(webhookUrl);
            try
            {
                // Créez un message en embed.
                var embed = new EmbedBuilder
                {
                    Title = "Verif Delete",
                    Description = "Delete par: " + key.Text,
                    Color = Discord.Color.Red, // Couleur de la bordure de l'embed
                    Timestamp = DateTimeOffset.Now
                };

                // Ajoutez des champs à l'embed (optionnel).
                embed.AddField("Pseudo joueur:", label23.Text);
                embed.AddField("Cheat trouvé: ", label24.Text);

                // Ajoutez l'embed au message.
                await client.SendMessageAsync(embeds: new[] { embed.Build() }, isTTS: false, username: "Panel staff Vérif");
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
            label22.Text = "N/A";
            label23.Text = "N/A";
            label24.Text = "N/A";
            siticoneRoundedTextBox3.Clear();
        }

        private void siticoneRoundedButton1_Click(object sender, EventArgs e)
        {
            ActualizarLista();
        }

        private async void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.ContextMenuStrip = null; // Désactive le ContextMenuStrip

            int selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex >= 0)
            {
                string selectedItem = listBox1.Items[selectedIndex].ToString();

                // Trouvez l'index de ":" et "|"
                int indexDebut = selectedItem.IndexOf(":");
                int indexFin = selectedItem.IndexOf("|");

                // Assurez-vous que les index ont été trouvés
                if (indexDebut != -1 && indexFin != -1)
                {
                    // Extrayez l'ID entre ":" et "|"
                    string id = selectedItem.Substring(indexDebut + 1, indexFin - indexDebut - 1).Trim();
                    siticoneRoundedTextBox3.Text = id;
                }
                ContextMenuStrip contextMenu = new ContextMenuStrip();
                if (label14.Text != "ModoConfirmé" || label14.Text != "Modo" || label14.Text != "Guide")
                {
                    // Change la couleur de fond en utilisant les composants R, G, B
                    contextMenu.BackColor = System.Drawing.Color.FromArgb(35, 39, 42);
                    contextMenu.ShowImageMargin = false;
                    ToolStripMenuItem menuItem1 = new ToolStripMenuItem("Download");
                    menuItem1.ForeColor = System.Drawing.Color.White;
                    menuItem1.Click += download_Click;
                    ToolStripMenuItem menuItem2 = new ToolStripMenuItem("Delete");
                    menuItem2.ForeColor = System.Drawing.Color.White;
                    menuItem2.Click += siticoneRoundedButton2_Click;
                    contextMenu.Items.Add(menuItem2);
                    contextMenu.Items.Add(menuItem1);
                }
                string id2 = selectedItem.Substring(indexDebut + 1, indexFin - indexDebut - 1).Trim();
                siticoneRoundedTextBox3.Text = id2;
                listBox1.ContextMenuStrip = contextMenu;
                string idRecherche = siticoneRoundedTextBox3.Text;
#pragma warning disable CS0642 // Possibilité d'instruction vide erronée
                using (MySqlConnection connection = new MySqlConnection("database=s2_test; server=83.150.217.78; user id=pterodactyl; pwd=4wYYZkQ6nccgvbZR4LgK")) ;
#pragma warning restore CS0642 // Possibilité d'instruction vide erronée
                {
                    connection.Open();

                    string selectQuery = "SELECT * FROM Verif WHERE Id = @Id"; // Remplacez "VotreTable" par le nom de votre table
                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id", idRecherche);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Accédez aux colonnes de la ligne résultante ici
                                int id = reader.GetInt32("Id");
                                string Raison = reader.GetString("Raison");
                                string PseudoStaff = reader.GetString("PseudoStaff");
                                string PseudoJoueur = reader.GetString("PseudoJoueur");
                                // Ajoutez d'autres colonnes en fonction de votre table
                                label22.Text = PseudoStaff;
                                label24.Text = Raison;
                                label23.Text = PseudoJoueur;

                            }
                            else
                            {
                                label22.Text = "N/A";
                                label23.Text = "N/A";
                                label24.Text = "N/A";
                            }
                        }
                    }
                    connection.Close();
                }
            }
            string playerName = label23.Text;
            string playerUUID = await GetPlayerUUID(playerName);

            if (!string.IsNullOrEmpty(playerUUID))
            {
                GetMinecraftHead(playerUUID);
            }
        }

        private void download_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex >= 0)
            {
                string selectedItem = listBox1.Items[selectedIndex].ToString();

                // Trouvez l'index de ":" et "|"
                int indexDebut = selectedItem.IndexOf(":");
                int indexFin = selectedItem.IndexOf("|");

                // Assurez-vous que les index ont été trouvés
                if (indexDebut != -1 && indexFin != -1)
                {
                    // Extrayez l'ID entre ":" et "|"
                    string id = selectedItem.Substring(indexDebut + 1, indexFin - indexDebut - 1).Trim();

                    // Obtenez le chemin du dossier de l'application
                    string dossierApplication = AppDomain.CurrentDomain.BaseDirectory;

                    // Construisez le chemin complet du dossier de destination (avec le nom du dossier "Preuve")
                    string cheminDossierDestination = Path.Combine(dossierApplication, "Verif");

                    // Créez le dossier s'il n'existe pas
                    Directory.CreateDirectory(cheminDossierDestination);

                    // Obtenez le nom du fichier à partir de la base de données
                    using (MySqlConnection connexion = new MySqlConnection("database=s2_test; server=83.150.217.78; user id=pterodactyl; pwd=4wYYZkQ6nccgvbZR4LgK"))
                    {
                        connexion.Open();

                        string commandeSQL = "SELECT nomFichier, Preuve FROM Verif WHERE id = @id";
                        MySqlCommand commande = new MySqlCommand(commandeSQL, connexion);
                        commande.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = commande.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                byte[] fichierBytes = (byte[])reader["Preuve"];
                                string nomFichier = reader["nomFichier"].ToString(); // Obtenez le nom du fichier

                                // Construisez le chemin complet du fichier dans le dossier de destination
                                string cheminComplet = Path.Combine(cheminDossierDestination, nomFichier);

                                // Enregistrez le fichier dans le dossier de destination
                                File.WriteAllBytes(cheminComplet, fichierBytes);

                                // Ouvrez le dossier de destination dans l'explorateur de fichiers
                                Process.Start(cheminDossierDestination);
                            }
                        }
                    }
                }
            }
        }

        private void siticoneRoundedButton4_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Tous les fichiers (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;

                    // Afficher le chemin du fichier sélectionné dans siticoneRoundedTextBox3
                    siticoneRoundedTextBox3.Text = selectedFilePath;

                    // Extraire le nom du fichier à partir du chemin du fichier
                    string nomFichier = Path.GetFileName(selectedFilePath);

                    // Afficher le nom du fichier dans siticoneRoundedTextBox4
                    siticoneRoundedTextBox4.Text = nomFichier;
                }
            }
        }

        private void btnTools_Click(object sender, EventArgs e)
        {
            Warn warn = new Warn();
            warn.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
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

        private void button4_Click(object sender, EventArgs e)
        {
            Demande demande = new Demande();
            demande.Show();
            this.Hide();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Paramètre oaramètre = new Paramètre();
            oaramètre.Show();
            this.Hide();
        }

        private async void btnExit_Click(object sender, EventArgs e)
        {
            Déconnexion(); // Exécute la fonction de déconnexion
            await Task.Delay(TimeSpan.FromSeconds(1));

            Environment.Exit(0); // Quittez l'application après le délai
        }
    }
}
