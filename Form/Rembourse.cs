using Discord;
using Discord.Webhook;
using KeyAuth;
using MySql.Data.MySqlClient;
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

namespace Loader
{
    public partial class Rembourse : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        public Rembourse()
        {
            InitializeComponent();
            key.Text = "" + Login.KeyAuthApp.user_data.username;
            label14.Text = Login.KeyAuthApp.user_data.subscriptions[0].subscription;
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

        public List<Rembourse1> ListdesRembourse()
        {
            List<Rembourse1> list = new List<Rembourse1>();

            string consult = "SELECT * FROM Rembourse;";
            MySqlCommand comando = new MySqlCommand(consult)
            {
                Connection = Conect()
            };
            MySqlDataReader lecturaDatos = comando.ExecuteReader();
            while (lecturaDatos.Read())
            {
                Rembourse1 warn = new Rembourse1
                {
                    Id = lecturaDatos.GetString("id"),
                    PseudoJoueur = lecturaDatos.GetString("PseudoJoueur"),
                    RaisonWarn = lecturaDatos.GetString("RaisonWarn"),
                    PseudoStaff = lecturaDatos.GetString("PseudoStaff"),
                    Datedederniéreédition = lecturaDatos.GetString("Datedederniéreédition"),
                    Statue = lecturaDatos.GetString("Statue")
                };
                list.Add(warn);
            }
            comando.Connection.Close();

            return list;
        }

        private void ActualizarList()
        {
            Rembourse BaseDeDatos = new Rembourse();
            List<Rembourse1> listaDB = BaseDeDatos.ListdesRembourse();

            listBox1.Items.Clear();
            for (int i = 0; i < listaDB.Count; i++)
            {
                Rembourse1 cliente = listaDB[i];
                listBox1.Items.Add(cliente);
            }
        }

        private async void AddRembourse()
        {
            string webhookUrl3 = "https://discord.com/api/webhooks/1158438206283460679/0bHrmZeCa13P7klz_6gk3uI2P4e9dl-Gy4ZGIYJ69xkzkG2HoHhNhLvffjJaALY5rZtk";
            var client2 = new DiscordWebhookClient(webhookUrl3);
            try
            {
                // Créez un message en embed.
                var embed = new EmbedBuilder
                {
                    Title = "Remboursement",
                    Description = "Staff: " + key.Text,
                    Color = Discord.Color.Green, // Couleur de la bordure de l'embed
                    Timestamp = DateTimeOffset.Now
                };

                // Ajoutez des champs à l'embed (optionnel).
                embed.AddField("Rembourse:", siticoneRoundedTextBox1.Text);
                embed.AddField("Info:", siticoneRoundedTextBox2.Text);

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
            string joueur = siticoneRoundedTextBox1.Text;
            string raison = siticoneRoundedTextBox2.Text;
            string action = "Remboursement";
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

        private void RgstrBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(siticoneRoundedTextBox2.Text) || string.IsNullOrEmpty(siticoneRoundedTextBox1.Text))
            {
                System.Windows.MessageBox.Show("Veuillez remplir les champs.");
                return;
            }
            string agregar = "INSERT INTO Rembourse (PseudoJoueur, RaisonWarn, PseudoStaff, Datedederniéreédition, Statue) VALUES (@PseudoJoueur, @RaisonWarn, @PseudoStaff, @Datedederniéreédition, @Statue)";
            warns warns = new warns
            {
                PseudoJoueur = siticoneRoundedTextBox1.Text,
                RaisonWarn = siticoneRoundedTextBox2.Text,
                PseudoStaff = key.Text,
                Datedederniereedition = Datedederniéreédition.Text,
                Statue = label11.Text
            };

            using (MySqlConnection connection = Conect())
            {
                using (MySqlCommand comando = new MySqlCommand(agregar, connection))
                {
                    comando.Parameters.AddWithValue("@PseudoJoueur", warns.PseudoJoueur);
                    comando.Parameters.AddWithValue("@RaisonWarn", warns.RaisonWarn);
                    comando.Parameters.AddWithValue("@PseudoStaff", warns.PseudoStaff);
                    comando.Parameters.AddWithValue("@Datedederniéreédition", warns.Datedederniereedition);
                    comando.Parameters.AddWithValue("@Statue", warns.Statue);

                    comando.ExecuteNonQuery();
                }
            }
            AddRembourse();
            ActualizarList();
        }

        private void SiticoneRoundedButton8_Click(object sender, EventArgs e)
        {
            Gestion gestion = new Gestion();
            gestion.Show();
            this.Hide();
        }

        private void SiticoneRoundedButton11_Click(object sender, EventArgs e)
        {
            Warn warn = new Warn();
            warn.Show();
            this.Hide();
        }

        private void SiticoneRoundedButton1_Click(object sender, EventArgs e)
        {
            ActualizarList();
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

        private async void SiticoneRoundedButton6_Click(object sender, EventArgs e)
        {
            Déconnexion(); // Exécute la fonction de déconnexion
            await Task.Delay(TimeSpan.FromSeconds(1));

            Environment.Exit(0); // Quittez l'application après le délai
        }

        private void Rembourse_Load(object sender, EventArgs e)
        {


            ActualizarList();
            if (label14.Text == "Gérant" || key.Text == "Lulunoel2016")
            {
                button2.Enabled = true;
                button1.Enabled = true;
                button5.Enabled = true;
                btnTools.Enabled = true;

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
                btnTools.Enabled = true;

            }
            else if (label14.Text == "SuperModo")
            {
                button2.Enabled = false;
                button1.Enabled = true;
                button5.Enabled = true;
                btnTools.Enabled = true;

            }
            else if (label14.Text == "ModoConfirmé")
            {
                button2.Enabled = false;
                button1.Enabled = false;
                button5.Enabled = true;
                btnTools.Enabled = true;

            }
            else if (label14.Text == "Modo")
            {
                button2.Enabled = false;
                button1.Enabled = false;
                button5.Enabled = false;
                btnTools.Enabled = true;

            }
            else if (label14.Text == "Guide")
            {
                button2.Enabled = false;
                button1.Enabled = false;
                button5.Enabled = true;
                btnTools.Enabled = false;
            
            }
            else
            {
                button2.Enabled = false;
                button1.Enabled = false;
                button5.Enabled = true;
                btnTools.Enabled = false;

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
        private readonly MySqlConnection connection = new MySqlConnection("database=s2_test; server=83.150.217.78; user id=pterodactyl; pwd=4wYYZkQ6nccgvbZR4LgK");

        private void siticoneRoundedButton3_Click(object sender, EventArgs e)
        {

            string idRecherche = siticoneRoundedTextBox3.Text;
#pragma warning disable CS0642 // Possibilité d'instruction vide erronée
            using (MySqlConnection connection = new MySqlConnection("database=s2_test; server=83.150.217.78; user id=pterodactyl; pwd=4wYYZkQ6nccgvbZR4LgK")) ;
#pragma warning restore CS0642 // Possibilité d'instruction vide erronée
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Rembourse WHERE Id = @Id"; // Remplacez "VotreTable" par le nom de votre table
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

        private async void Removeremb()
        {
            string webhookUrl3 = "https://discord.com/api/webhooks/1158438206283460679/0bHrmZeCa13P7klz_6gk3uI2P4e9dl-Gy4ZGIYJ69xkzkG2HoHhNhLvffjJaALY5rZtk";
            var client2 = new DiscordWebhookClient(webhookUrl3);
            try
            {
                // Créez un message en embed.
                var embed = new EmbedBuilder
                {
                    Title = "Remboursement delete",
                    Description = "Staff: " + key.Text,
                    Color = Discord.Color.Red, // Couleur de la bordure de l'embed
                    Timestamp = DateTimeOffset.Now
                };

                // Ajoutez des champs à l'embed (optionnel).
                embed.AddField("Remboursement de: ", label23.Text);

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

        private void siticoneRoundedButton2_Click(object sender, EventArgs e)
        {
            string retirer = "DELETE FROM Rembourse WHERE Id ='" + siticoneRoundedTextBox3.Text + "';";
            MySqlCommand comando = new MySqlCommand(retirer)
            {
                Connection = Conect()
            };
            comando.ExecuteNonQuery();
            comando.Connection.Close();
            Removeremb();
            ActualizarList();
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
            string retirer = "DELETE FROM Rembourse WHERE Id ='" + siticoneRoundedTextBox3.Text + "';";
            MySqlCommand comando = new MySqlCommand(retirer)
            {
                Connection = Conect()
            };
            comando.ExecuteNonQuery();
            comando.Connection.Close();
            Removeremb();
            ActualizarList();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            // Change la couleur de fond en utilisant les composants R, G, B
            contextMenu.BackColor = System.Drawing.Color.FromArgb(35, 39, 42);
            contextMenu.ShowImageMargin = false;

            ToolStripMenuItem menuItem1 = new ToolStripMenuItem("Delete le remboursement");

            // Change la couleur du texte en rouge pour "Option 1"
            menuItem1.ForeColor = System.Drawing.Color.White;

            // Associez des gestionnaires d'événements aux éléments de menu si nécessaire
            menuItem1.Click += deleteLeWarnToolStripMenuItem_Click;

            // Ajoutez les éléments de menu au ContextMenuStrip
            contextMenu.Items.Add(menuItem1);

            // Associez le ContextMenuStrip à la ListBox
            listBox1.ContextMenuStrip = contextMenu;
            int selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex >= 0)
            {
                string selectedItem = listBox1.Items[selectedIndex].ToString();

                // Divisez la chaîne en utilisant le délimiteur "|"
                string[] pipeSplit = selectedItem.Split('|');

                // Assurez-vous qu'il y a au moins six parties après la division
                if (pipeSplit.Length >= 6)
                {
                    // Divisez chaque partie en utilisant le délimiteur ":"
                    string[] colonSplit1 = pipeSplit[0].Split(':');
                    string[] colonSplit2 = pipeSplit[1].Split(':');
                    string[] colonSplit4 = pipeSplit[2].Split(':');
                    string[] colonSplit3 = pipeSplit[3].Split(':');
                    string[] colonSplit5 = pipeSplit[4].Split(':');
                    string[] colonSplit6 = pipeSplit[5].Split('>'); // Modifier le délimiteur si nécessaire

                    // Assurez-vous qu'il y a au moins un élément dans chaque division
                    if (colonSplit1.Length >= 2 && colonSplit2.Length >= 2 &&
                        colonSplit3.Length >= 2 && colonSplit4.Length >= 2 &&
                        colonSplit5.Length >= 2 && colonSplit6.Length >= 2)
                    {
                        // Accédez aux éléments que vous souhaitez
                        string id = colonSplit1[1].Trim();
                        string pseudo = colonSplit2[1].Trim();
                        string info = colonSplit3[1].Trim();
                        string par = colonSplit4[1].Trim();
                        string du = colonSplit5[1].Trim();
                        string statue = colonSplit6[1].Trim();

                        // Affectez la nouvelle chaîne aux contrôles appropriés
                        siticoneRoundedTextBox3.Text = id;
                        label23.Text = pseudo;
                        label22.Text = info;
                        label24.Text = par;
                        // Assurez-vous d'avoir des contrôles pour les autres éléments si nécessaire
                    }
                }
            }
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
