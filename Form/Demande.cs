using Discord;
using Discord.Webhook;
using KeyAuth;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    public partial class Demande : Form
    {

        public class PlayerInfo
        {
            public string PlayerName { get; set; }
            public string id { get; set; }
        }

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private readonly MySqlConnection connection = new MySqlConnection("Server=83.150.217.78;Port=3306;Database=s2_test;User Id=pterodactyl;Password=4wYYZkQ6nccgvbZR4LgK");

        public Demande()
        {
            InitializeComponent();
            Datedederniéreédition.Text = DateTime.Now.ToString();
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;
            Application.EnableVisualStyles();
        }

        public MySqlConnection Conect()
        {
            string servidor = "83.150.217.78";
            string usuario = "pterodactyl";
            string password = "4wYYZkQ6nccgvbZR4LgK";
            string BaseDeDatos = "s2_test";
            string Conexion = "Server=" + servidor + ";Port=3306;Database=" + BaseDeDatos + ";User Id=" + usuario + ";Password=" + password;

            MySqlConnection conexionDB = new MySqlConnection(Conexion);
            conexionDB.Open();

            return conexionDB;
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

        public List<warns> ListdesWarns()
        {
            List<warns> list = new List<warns>();

            string consult = "SELECT * FROM Demande;";
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
                    RaisonWarn = lecturaDatos.GetString("Raison"),
                    PseudoStaff = lecturaDatos.GetString("PseudoStaff"),
                    Datedederniereedition = lecturaDatos.GetString("Datedederniéreédition"),
                    Statue = lecturaDatos.GetString("Statue")
                };
                list.Add(warn);
            }
            comando.Connection.Close();

            return list;
        }

        internal void Ajouter(warns warn)
        {
            string ajouter = "INSERT INTO Warns (PseudoJoueur, Raison, PseudoStaff, Datedederniéreédition, Statue) VALUES (@PseudoJoueur, @RaisonWarn, @PseudoStaff, @Datedederniereedition, @Statue);";
            MySqlCommand comando = new MySqlCommand(ajouter)
            {
                Connection = Conect()
            };
            comando.Parameters.AddWithValue("@PseudoJoueur", warn.PseudoJoueur);
            comando.Parameters.AddWithValue("@RaisonWarn", warn.RaisonWarn);
            comando.Parameters.AddWithValue("@PseudoStaff", warn.PseudoStaff);
            comando.Parameters.AddWithValue("@Datedederniereedition", warn.Datedederniereedition);
            comando.Parameters.AddWithValue("@Statue", warn.Statue);
            comando.ExecuteNonQuery();

            comando.Connection.Close();
        }

        internal void Retirer(warns warn)
        {
            string retirer = "DELETE FROM Warns WHERE PseudoJoueur = @PseudoJoueur";
            MySqlCommand comando = new MySqlCommand(retirer)
            {
                Connection = Conect()
            };
            comando.Parameters.AddWithValue("@PseudoJoueur", warn.PseudoJoueur);
            comando.ExecuteNonQuery();

            comando.Connection.Close();
        }

        internal void Editer(warns warn)
        {
            string editer = "INSERT INTO Warns (PseudoJoueur, Raison, PseudoStaff, Datedederniéreédition, Datedefin, Statue) VALUES (@PseudoJoueur, @RaisonWarn, @PseudoStaff, @Datedederniereedition, @Datedefin, @Statue);";
            MySqlCommand comando = new MySqlCommand(editer)
            {
                Connection = Conect()
            };
            comando.Parameters.AddWithValue("@PseudoJoueur", warn.PseudoJoueur);
            comando.Parameters.AddWithValue("@RaisonWarn", warn.RaisonWarn);
            comando.Parameters.AddWithValue("@PseudoStaff", warn.PseudoStaff);
            comando.Parameters.AddWithValue("@Datedederniereedition", warn.Datedederniereedition);
            comando.Parameters.AddWithValue("@Datedefin", warn.Datedefin);
            comando.Parameters.AddWithValue("@Statue", warn.Statue);
            comando.ExecuteNonQuery();

            comando.Connection.Close();
        }

        private async void Demande_Load(object sender, EventArgs e)
        {
            ActualizarLista();
            siticoneRoundedTextBox3.Text = AppDomain.CurrentDomain.BaseDirectory + "J_starwars_cercle_2.ico";
            siticoneRoundedTextBox4.Text = "J_starwars_cercle_2.ico";
            key.Text = Login.KeyAuthApp.user_data.username;
            label14.Text = Login.KeyAuthApp.user_data.subscriptions[0].subscription;
            string PlayerName = key.Text;
            string playerUUID = await GetPlayerUUID(PlayerName);

            if (!string.IsNullOrEmpty(playerUUID))
            {
                GetMinecraftHead(playerUUID);
            }

            // Initialisation du Timer pour envoyer key.Text à la base de données toutes les 5 minutes.
            System.Timers.Timer timer2 = new System.Timers.Timer(300000);
            timer2.Elapsed += SendTimer_Elapsed;
            timer2.AutoReset = true;
            timer2.Start();

            System.Timers.Timer timer = new System.Timers.Timer(60000);
            timer.Elapsed += TimerElapsed;
            timer.AutoReset = true;
            timer.Start();

            if (label14.Text == "Gérant" || key.Text == "Lulunoel2016")
            {
                button2.Enabled = true;
                button1.Enabled = true;
                btnTools.Enabled = true;
                siticoneRoundedButton3.Enabled = true;
                siticoneRoundedButton10.Enabled = true;
                button5.Enabled = true;

            }
            else if (label14.Text == "Admin")
            {
                button2.Enabled = true;
                button1.Enabled = true;
                btnTools.Enabled = true;
                siticoneRoundedButton3.Enabled = true;
                siticoneRoundedButton10.Enabled = true;
                button5.Enabled = true;

            }
            else if (label14.Text == "Manager")
            {
                button2.Enabled = true;
                button1.Enabled = true;
                btnTools.Enabled = true;
                siticoneRoundedButton3.Enabled = true;
                siticoneRoundedButton10.Enabled = true;
                button5.Enabled = true;

            }
            else if (label14.Text == "SuperModo")
            {
                button2.Enabled = false;
                button1.Enabled = true;
                btnTools.Enabled = true;
                siticoneRoundedButton3.Enabled = true;
                siticoneRoundedButton10.Enabled = true;
                button5.Enabled = true;

            }
            else if (label14.Text == "ModoConfirmé")
            {
                button2.Enabled = false;
                button1.Enabled = false;
                btnTools.Enabled = true;
                siticoneRoundedButton3.Enabled = true;
                siticoneRoundedButton10.Enabled = true;
                button5.Enabled = true;

            }
            else if (label14.Text == "Modo")
            {
                button2.Enabled = false;
                button1.Enabled = false;
                btnTools.Enabled = true;
                siticoneRoundedButton3.Enabled = false;
                siticoneRoundedButton10.Enabled = true;
                button5.Enabled = false;
            }
            else if (label14.Text == "Guide")
            {
                button2.Enabled = false;
                button1.Enabled = false;
                btnTools.Enabled = false;
                siticoneRoundedButton3.Enabled = false;
                siticoneRoundedButton10.Enabled = false;
                button5.Enabled = false;

            }
            else
            {
                btnTools.Enabled = false;
                button2.Enabled = false;
                button1.Enabled = false;
                siticoneRoundedButton3.Enabled = false;
                siticoneRoundedButton10.Enabled = false;
                button5.Enabled = false;
            }
            this.FormClosing += Form_Closing;
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            // Annuler la fermeture
            e.Cancel = true;
        }

        private void SendTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            string valueToSend = key.Text;
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("dd/MM/yyyy HH:mm:ss");

            string DatedefinValue = formattedDateTime;

            DateTime datedefinDateTime = DateTime.ParseExact(DatedefinValue, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            datedefinDateTime = datedefinDateTime.AddMinutes(5);
            string updatedDatedefinValue = datedefinDateTime.ToString("dd/MM/yyyy HH:mm:ss");

            using (MySqlConnection connection = new MySqlConnection("Server=83.150.217.78;Port=3306;Database=s2_test;User Id=pterodactyl;Password=4wYYZkQ6nccgvbZR4LgK;"))
            {
                connection.Open();

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
            using (MySqlConnection connection = new MySqlConnection("Server=83.150.217.78;Port=3306;Database=s2_test;User Id=pterodactyl;Password=4wYYZkQ6nccgvbZR4LgK"))
            {
                connection.Open();
                DateTime currentDateTime = DateTime.Now;
                string deleteQuery = "DELETE FROM UserOnline WHERE Datedefin < @CurrentDateTime";
                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@CurrentDateTime", currentDateTime);
                int rowsAffected = deleteCommand.ExecuteNonQuery();
                //MessageBox.Show($"{rowsAffected} enregistrements supprimés.");
                connection.Close();
            }
        }

        public List<Demandes> ListdesDemande()
        {
            List<Demandes> list = new List<Demandes>();

            string consult = "SELECT * FROM Demande;";
            MySqlCommand comando = new MySqlCommand(consult)
            {
                Connection = Conect()
            };
            MySqlDataReader lecturaDatos = comando.ExecuteReader();
            while (lecturaDatos.Read())
            {
                Demandes demande = new Demandes
                {
                    Id = lecturaDatos.GetString("id"),
                    Type = lecturaDatos.GetString("Type"),
                    PseudoJoueur = lecturaDatos.GetString("PseudoJoueur"),
                    Raison = lecturaDatos.GetString("Raison"),
                    PseudoStaff = lecturaDatos.GetString("PseudoStaff"),
                    Datedederniéreédition = lecturaDatos.GetString("Datedederniéreédition"),
                    Statut = lecturaDatos.GetString("Statue")
                };
                list.Add(demande);
            }
            comando.Connection.Close();

            return list;
        }

        private void ActualizarLista()
        {
            Demande BaseDeDatos = new Demande();
            List<Demandes> listaDB = BaseDeDatos.ListdesDemande();

            listBox1.Items.Clear();
            for (int i = 0; i < listaDB.Count; i++)
            {
                Demandes cliente = listaDB[i];
                listBox1.Items.Add(cliente);
            }
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

        private async void GetMinecraftHead(string id)
        {
            string baseUrl = "https://crafatar.com/avatars/";
            string url = $"{baseUrl}{id}";

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
                        MessageBox.Show($"Impossible de récupérer la tête de {id}. Réponse HTTP : {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}");
            }
        }

        private async void AddDemande()
        {
            string webhookUrl3 = "https://discord.com/api/webhooks/1160288258341752914/rS2Y4kL1L9lGdYdsenm9MmNpWacYlydjp7vNDb0dm3snTv3PpbExjMOFPRAaJ4WrQrSB";
            var client2 = new DiscordWebhookClient(webhookUrl3);
            try
            {
                var embed = new EmbedBuilder
                {
                    Title = "Demande",
                    Description = "Staff: " + key.Text,
                    Color = Discord.Color.Green,
                    Timestamp = DateTimeOffset.Now
                };

                embed.AddField("Joueur: ", siticoneRoundedTextBox1.Text);
                embed.AddField("Type: ", comboBox1.Text);
                embed.AddField("Info de la demande: ", siticoneRoundedTextBox2.Text);
                embed.AddField("Preuve: ", siticoneRoundedTextBox4.Text);

                await client2.SendMessageAsync(embeds: new[] { embed.Build() }, isTTS: false, username: "Panel staff logs");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'envoi du log : " + ex.Message);
            }
            finally
            {
                client2.Dispose();
            }
            DateTime now = DateTime.Now;
            string formattedDate = now.ToString("dd-MM-yyyy HH:mm:ss");

            string pseudo = key.Text;
            string joueur = siticoneRoundedTextBox1.Text;
            string raison = siticoneRoundedTextBox2.Text;
            string action = "Ajout de demande";
            string date = formattedDate;

            string editer = "INSERT INTO `Logs`(`Pseudo`, `Action`, `Joueur`, `Raison`, `Date`) VALUES (@Pseudo, @Action, @Joueur, @Raison, @Date)";

            using (MySqlConnection connection = Conect())
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

        private void RgstrBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(siticoneRoundedTextBox3.Text))
            {
                MessageBox.Show("Veuillez sélectionner un fichier.");
                return;
            }

            byte[] fichierBytes = File.ReadAllBytes(siticoneRoundedTextBox3.Text);
            string connectionString = "Server=83.150.217.78;Port=3306;Database=s2_test;User Id=pterodactyl;Password=4wYYZkQ6nccgvbZR4LgK";
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                string query = "INSERT INTO Demande (Type, PseudoJoueur, Raison, Preuve, nomFichier, PseudoStaff, Datedederniéreédition, Statue) " +
                                "VALUES (@Type, @PseudoJoueur, @Raison, @Preuve, @nomFichier, @PseudoStaff, @Datedederniéreédition, @Statue)";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Type", comboBox1.Text);
                command.Parameters.AddWithValue("@PseudoJoueur", siticoneRoundedTextBox1.Text);
                command.Parameters.AddWithValue("@Raison", siticoneRoundedTextBox2.Text);
                command.Parameters.AddWithValue("@nomFichier", siticoneRoundedTextBox4.Text);
                command.Parameters.AddWithValue("@Preuve", fichierBytes);
                command.Parameters.AddWithValue("@PseudoStaff", key.Text);
                command.Parameters.AddWithValue("@Datedederniéreédition", Datedederniéreédition.Text);
                command.Parameters.AddWithValue("@Statue", label11.Text);
                command.ExecuteNonQuery();

                connection.Close();

                ActualizarLista();
                AddDemande();
                siticoneRoundedTextBox1.Clear();
                siticoneRoundedTextBox2.Clear();
                comboBox1.Text = siticoneRoundedTextBox1.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite : " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void EnableButtonsBasedOnRole()
        {
            string role = label14.Text;

            button2.Enabled = (role == "Gérant" || key.Text == "Lulunoel2016" || role == "Admin" || role == "Manager");
            button1.Enabled = (role == "Gérant" || key.Text == "Lulunoel2016" || role == "Admin" || role == "Manager" || role == "SuperModo" || role == "ModoConfirmé");
            btnTools.Enabled = (role != "Guide");
            siticoneRoundedButton3.Enabled = (role != "Guide");
            siticoneRoundedButton10.Enabled = (role != "Guide");
            button5.Enabled = (role != "Guide");
        }

        private void SetupTimers()
        {
            System.Timers.Timer sendTimer = new System.Timers.Timer(300000);
            sendTimer.Elapsed += SendTimer_Elapsed;
            sendTimer.AutoReset = true;
            sendTimer.Start();

            System.Timers.Timer deleteTimer = new System.Timers.Timer(60000);
            deleteTimer.Elapsed += DeleteTimerElapsed;
            deleteTimer.AutoReset = true;
            deleteTimer.Start();
        }

        private string UpdateDateDefin(string userName, string formattedDateTime)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM UserOnline WHERE UserName=@UserName", connection);
            command.Parameters.AddWithValue("@UserName", userName);
            command.Connection = connection;
            MySqlDataReader reader = command.ExecuteReader();

            string newDatedefinValue = "";

            if (reader.Read())
            {
                string previousDatedefinValue = reader["Datedefin"].ToString();

                DateTime previousDateTime = DateTime.ParseExact(previousDatedefinValue, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime currentDateTime = DateTime.ParseExact(formattedDateTime, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                int totalMinutes = Convert.ToInt32((currentDateTime - previousDateTime).TotalMinutes);
                int previousMinutes = Convert.ToInt32(previousDateTime.Minute);
                int totalHours = Convert.ToInt32((currentDateTime - previousDateTime).TotalHours);
                int previousHours = Convert.ToInt32(previousDateTime.Hour);
                int totalDays = Convert.ToInt32((currentDateTime - previousDateTime).TotalDays);

                if (totalMinutes < 0)
                {
                    totalMinutes = 0;
                }
                if (totalHours < 0)
                {
                    totalHours = 0;
                }
                if (totalDays < 0)
                {
                    totalDays = 0;
                }

                if (totalMinutes >= 60)
                {
                    if (totalHours >= 24)
                    {
                        newDatedefinValue = totalDays + " jours, " + (totalHours - totalDays * 24) + " heures et " + (totalMinutes - totalHours * 60) + " minutes";
                    }
                    else
                    {
                        newDatedefinValue = totalHours + " heures et " + (totalMinutes - totalHours * 60) + " minutes";
                    }
                }
                else
                {
                    newDatedefinValue = totalMinutes + " minutes";
                }

                if (newDatedefinValue == "")
                {
                    if (totalMinutes >= 60)
                    {
                        if (totalHours >= 24)
                        {
                            newDatedefinValue = totalDays + " jours, " + (totalHours - totalDays * 24) + " heures et " + (totalMinutes - totalHours * 60) + " minutes";
                        }
                        else
                        {
                            newDatedefinValue = totalHours + " heures et " + (totalMinutes - totalHours * 60) + " minutes";
                        }
                    }
                    else
                    {
                        newDatedefinValue = totalMinutes + " minutes";
                    }
                }
            }
            else
            {
                newDatedefinValue = "0 minutes";
            }

            reader.Close();
            return newDatedefinValue;
        }

        private async void DeleteTimerElapsed(object sender, ElapsedEventArgs e)
        {
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("dd/MM/yyyy HH:mm:ss");

            MySqlCommand command = new MySqlCommand("SELECT * FROM UserOnline", connection);
            command.Connection = connection;
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string userName = reader["UserName"].ToString();
                string previousDatedefinValue = reader["Datedefin"].ToString();

                DateTime previousDateTime = DateTime.ParseExact(previousDatedefinValue, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime currentDateTimeParsed = DateTime.ParseExact(formattedDateTime, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                if (currentDateTimeParsed > previousDateTime)
                {
                    reader.Close();

                    MySqlCommand deleteCommand = new MySqlCommand("DELETE FROM UserOnline WHERE UserName=@UserName", connection);
                    deleteCommand.Parameters.AddWithValue("@UserName", userName);
                    deleteCommand.ExecuteNonQuery();
                    reader = command.ExecuteReader();
                }
            }

            reader.Close();
        }

        private void siticoneRoundedButton1_Click(object sender, EventArgs e)
        {
            ActualizarLista();
        }

        private void siticoneRoundedButton2_Click(object sender, EventArgs e)
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
                    siticoneRoundedTextBox6.Text = id;
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
                    menuItem2.Click += siticoneRoundedButton3_Click;
                    contextMenu.Items.Add(menuItem2);
                    contextMenu.Items.Add(menuItem1);
                }
                string id2 = selectedItem.Substring(indexDebut + 1, indexFin - indexDebut - 1).Trim();
                siticoneRoundedTextBox6.Text = id2;
                listBox1.ContextMenuStrip = contextMenu;
                string idRecherche = siticoneRoundedTextBox6.Text;

                using (MySqlConnection connection = new MySqlConnection("database=s2_test; server=83.150.217.78; user id=pterodactyl; pwd=4wYYZkQ6nccgvbZR4LgK"))
                {
                    connection.Open();

                    string selectQuery = "SELECT * FROM Demande WHERE Id = @Id";
                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id", idRecherche);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int id = reader.GetInt32("Id");
                                string Raison = reader.GetString("Raison");
                                string nomFichier = reader.GetString("nomFichier");
                                string Preuve = reader.GetString("Preuve");
                                string PseudoStaff = reader.GetString("PseudoStaff");
                                string Statue = reader.GetString("Statue");
                                string Type = reader.GetString("Type");
                                string PseudoJoueur = reader.GetString("PseudoJoueur");

                                label22.Text = PseudoStaff;
                                label23.Text = nomFichier;
                                label24.Text = Raison;
                                label25.Text = Type;
                                label27.Text = PseudoJoueur;
                                label17.Text = Statue;
                                label9.Text = Preuve;
                            }
                            else
                            {
                                label22.Text = "N/A";
                                label23.Text = "N/A";
                                label24.Text = "N/A";
                                label25.Text = "N/A";
                                label27.Text = "N/A";
                                label17.Text = "N/A";
                                label9.Text = "N/A";
                            }
                        }
                    }
                    connection.Close();
                }
            }

            string PlayerName = label27.Text;
            string playerUUID = await GetPlayerUUID(PlayerName);

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
                int indexDebut = selectedItem.IndexOf(":");
                int indexFin = selectedItem.IndexOf("|");
                if (indexDebut != -1 && indexFin != -1)
                {
                    string id = selectedItem.Substring(indexDebut + 1, indexFin - indexDebut - 1).Trim();
                    string dossierApplication = AppDomain.CurrentDomain.BaseDirectory;
                    string cheminDossierDestination = Path.Combine(dossierApplication, "Preuve");
                    Directory.CreateDirectory(cheminDossierDestination);

                    using (MySqlConnection connexion = new MySqlConnection("database=s2_test; server=83.150.217.78; user id=pterodactyl; pwd=4wYYZkQ6nccgvbZR4LgK"))
                    {
                        connexion.Open();
                        string commandeSQL = "SELECT nomFichier, Preuve FROM Demande WHERE id = @id";
                        MySqlCommand commande = new MySqlCommand(commandeSQL, connexion);
                        commande.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = commande.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                byte[] fichierBytes = (byte[])reader["Preuve"];
                                string nomFichier = reader["nomFichier"].ToString();
                                string cheminComplet = Path.Combine(cheminDossierDestination, nomFichier);
                                File.WriteAllBytes(cheminComplet, fichierBytes);
                                Process.Start(cheminDossierDestination);
                            }
                        }
                    }
                }
            }
        }

        private void siticoneRoundedButton5_Click(object sender, EventArgs e)
        {
            string idRecherche = siticoneRoundedTextBox6.Text;
            using (MySqlConnection connection = new MySqlConnection("database=s2_test; server=83.150.217.78; user id=pterodactyl; pwd=4wYYZkQ6nccgvbZR4LgK"))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM Demande WHERE Id = @Id";
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", idRecherche);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.GetInt32("Id");
                            string Raison = reader.GetString("Raison");
                            string nomFichier = reader.GetString("nomFichier");
                            string Preuve

         = reader.GetString("Preuve");
                            string PseudoStaff = reader.GetString("PseudoStaff");
                            string Statue = reader.GetString("Statue");
                            string Type = reader.GetString("Type");
                            string PseudoJoueur = reader.GetString("PseudoJoueur");

                            label22.Text = PseudoStaff;
                            label23.Text = nomFichier;
                            label24.Text = Raison;
                            label25.Text = Type;
                            label27.Text = PseudoJoueur;
                            label17.Text = Statue;
                            label9.Text = Preuve;
                        }
                        else
                        {
                            label22.Text = "N/A";
                            label23.Text = "N/A";
                            label24.Text = "N/A";
                            label25.Text = "N/A";
                            label27.Text = "N/A";
                            label17.Text = "N/A";
                            label9.Text = "N/A";
                        }
                    }
                }
                connection.Close();
            }
        }

        private async void Removedem()
        {
            string webhookUrl = "https://discord.com/api/webhooks/1160288258341752914/rS2Y4kL1L9lGdYdsenm9MmNpWacYlydjp7vNDb0dm3snTv3PpbExjMOFPRAaJ4WrQrSB";
            var client = new DiscordWebhookClient(webhookUrl);
            try
            {
                // Créez un message en embed.
                var embed = new EmbedBuilder
                {
                    Title = "Demande Delete",
                    Description = "Delete par: " + key.Text,
                    Color = Discord.Color.Red,
                    Timestamp = DateTimeOffset.Now
                };

                // Ajoutez des champs à l'embed (optionnel).
                embed.AddField("Raison", label24.Text);
                embed.AddField("Preuve", label23.Text);
                embed.AddField("Statut", label17.Text);

                // Ajoutez l'embed au message.
                await client.SendMessageAsync(embeds: new[] { embed.Build() }, isTTS: false, username: "Panel staff logs");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Erreur lors de l'envoi du log : " + ex.Message);
            }
            finally
            {
                // N'oubliez pas de libérer les ressources du client.
                client.Dispose();
            }
        }

        private void siticoneRoundedButton3_Click(object sender, EventArgs e)
        {
            // Utilisez des requêtes paramétrées pour éviter l'injection SQL
            string retirerQuery = "DELETE FROM Demande WHERE Id = @id";

            using (MySqlConnection connection = Conect())
            {
                connection.Open();
                using (MySqlCommand comando = new MySqlCommand(retirerQuery, connection))
                {
                    // Assurez-vous de définir le type de paramètre approprié, en fonction de la colonne "Id" de la table Demande.
                    comando.Parameters.AddWithValue("@id", siticoneRoundedTextBox6.Text);

                    comando.ExecuteNonQuery();
                }
            }

            // Le reste de votre code pour la mise à jour de l'interface utilisateur
            Removedem();
            ActualizarLista();
            label22.Text = "N/A";
            label23.Text = "N/A";
            label24.Text = "N/A";
            label25.Text = "N/A";
            label27.Text = "N/A";
            label17.Text = "N/A";
            label9.Text = "N/A";
            siticoneRoundedTextBox6.Clear();
        }

        private void siticoneRoundedButton10_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            string formattedDate = now.ToString("yyyy-MM-dd");

            ActualizarLista();
            Demandes warns = new Demandes
            {
                Preuve = label9.Text,
                nomFichier = "indisponible.txt",
                Raison = label24.Text,
                PseudoStaff = label22.Text,
                Type = label25.Text,
                PseudoJoueur = label27.Text,
                Datedederniéreédition = formattedDate,
                Statut = statutActuel,
            };
            // Utilisez une chaîne de requête paramétrée pour éviter les problèmes de sécurité et d'erreurs de syntaxe.
            string agregar = "INSERT INTO Demande (nomFichier, Raison, Type, PseudoJoueur,PseudoStaff, Datedederniéreédition, Statue) VALUES (@nomFichier, @Raison, @Type, @PseudoJoueur, @PseudoStaff, @Datedederniereedition, @Statue);";

            using (MySqlConnection connexion = Conect())
            {
                using (MySqlCommand commande = new MySqlCommand(agregar, connexion))
                {
                    commande.Parameters.AddWithValue("@nomFichier", warns.nomFichier);
                    commande.Parameters.AddWithValue("@Raison", warns.Raison);
                    commande.Parameters.AddWithValue("@Type", warns.Type);
                    commande.Parameters.AddWithValue("@PseudoJoueur", warns.PseudoJoueur);
                    commande.Parameters.AddWithValue("@PseudoStaff", warns.PseudoStaff);
                    commande.Parameters.AddWithValue("@Datedederniereedition", warns.Datedederniéreédition);
                    commande.Parameters.AddWithValue("@Statue", warns.Statut);

                    commande.ExecuteNonQuery();
                }

                string retirer = "DELETE FROM Demande WHERE Id = @Id";
                MySqlCommand comando = new MySqlCommand(retirer, connexion);
                comando.Parameters.AddWithValue("@Id", siticoneRoundedTextBox6.Text);
                comando.ExecuteNonQuery();

                Removedem();
                ActualizarLista();
                ChangeStatut();
                label22.Text = "N/A";
                label23.Text = "N/A";
                label24.Text = "N/A";
                label25.Text = "N/A";
                label27.Text = "N/A";
                label17.Text = "N/A";
                label9.Text = "N/A";
                siticoneRoundedTextBox6.Clear();
            }
        }

        private async void ChangeStatut()
        {
            string webhookUrl = "https://discord.com/api/webhooks/1160288258341752914/rS2Y4kL1L9lGdYdsenm9MmNpWacYlydjp7vNDb0dm3snTv3PpbExjMOFPRAaJ4WrQrSB";
            var client = new DiscordWebhookClient(webhookUrl);
            try
            {
                // Créez un message en embed.
                var embed = new EmbedBuilder
                {
                    Title = "Demande Statut",
                    Description = "Modifier par: " + key.Text,
                    Color = Discord.Color.Red,
                    Timestamp = DateTimeOffset.Now
                };

                // Ajoutez des champs à l'embed (optionnel).
                embed.AddField("Demandeur: ", label22.Text);
                embed.AddField("Raison: ", label24.Text);
                embed.AddField("Preuve: ", label23.Text);
                embed.AddField("Statut: ", statutActuel);

                // Ajoutez l'embed au message.
                await client.SendMessageAsync(embeds: new[] { embed.Build() }, isTTS: false, username: "Panel staff logs");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Erreur lors de l'envoi du log : " + ex.Message);
            }
            finally
            {
                // N'oubliez pas de libérer les ressources du client.
                client.Dispose();
            }
        }

        private string statutActuel = "En attente"; // Initialisez le statut actuel à "En attente"

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                statutActuel = "Accepter ✅";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                statutActuel = "Refuser ❌";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Vérif Vérif = new Vérif();
            Vérif.Show();
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

        private void button4_Click(object sender, EventArgs e)
        {
            Demande demande = new Demande();
            demande.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Paramètre oaramètre = new Paramètre();
            oaramètre.Show();
            this.Hide();
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
        }

        private async void btnExit_Click(object sender, EventArgs e)
        {
            Déconnexion(); // Exécute la fonction de déconnexion
            await Task.Delay(TimeSpan.FromSeconds(1));

            Environment.Exit(0); // Quittez l'application après le délai
            this.Close(); // Cela déclenchera à nouveau l'événement Form_Closing

        }

        private async void siticoneRoundedButton6_Click(object sender, EventArgs e)
        {
            Déconnexion(); // Exécute la fonction de déconnexion
            await Task.Delay(TimeSpan.FromSeconds(1));

            Environment.Exit(0); // Quittez l'application après le délai
            this.Close(); // Cela déclenchera à nouveau l'événement Form_Closing
        }
    }
}
