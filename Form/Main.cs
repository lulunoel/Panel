using Discord.Webhook;
using Discord;
using KeyAuth;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using HtmlAgilityPack;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Timers;
using System.Globalization;
using System.Net;
using System.Windows.Controls;
using System.Diagnostics;

namespace Loader
{
    public partial class Main : Form
    {

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private readonly MySqlConnection connection = new MySqlConnection("database=s2_test; server=83.150.217.78; user id=pterodactyl; pwd=4wYYZkQ6nccgvbZR4LgK");

        public Main()
        {
            InitializeComponent();
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
            string Conexion = "Database=" + BaseDeDatos + "; Data Source=" + servidor
                + "; User Id=" + usuario + "; Password=" + password + "";

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

        private async void Main_Load(object sender, EventArgs e)
        {


            key.Text = Login.KeyAuthApp.user_data.username;
            label14.Text = Login.KeyAuthApp.user_data.subscriptions[0].subscription;
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
            string playerName = key.Text;
            string playerUUID = await GetPlayerUUID(playerName);
            if (!string.IsNullOrEmpty(playerUUID))
            {
                GetMinecraftHead(playerUUID);
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
            Add();
            Actu();
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

        private void button3_Click(object sender, EventArgs e)
        {

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

        private async void button6_Click(object sender, EventArgs e)
        {
            // URL de la page que vous souhaitez récupérer
            string url = "https://ban.jedisky.fr/index.php";

            // Créez un objet HttpClient pour effectuer la requête HTTP
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Effectuez la requête GET pour obtenir le contenu de la page web
                    string htmlContent = await client.GetStringAsync(url);

                    // Utilisez HtmlAgilityPack pour analyser le contenu HTML
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(htmlContent);

                    // Utilisez XPath ou d'autres méthodes pour extraire les données souhaitées
                    // Par exemple, si vous voulez extraire le texte de tous les paragraphes de la page :
                    var paragraphs = doc.DocumentNode.SelectNodes("//p");

                    // Ajoutez les paragraphes à la ListBox
                    if (paragraphs != null)
                    {
                        foreach (var paragraph in paragraphs)
                        {
                            listBox1.Items.Add(paragraph.InnerText.Trim());
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Gérez les erreurs ici
                    MessageBox.Show("Une erreur s'est produite : " + ex.Message);
                }
            }
        }

        private async void RgstrBtn_Click(object sender, EventArgs e)
        {
            // URL de base du site
            string baseUrl = "https://ban.jedisky.fr/bans.php";

            // Créez un objet HttpClient pour effectuer la requête HTTP
            using (HttpClient client = new HttpClient())
            {
                listBox1.Items.Clear();
                try
                {
                    // Effectuez la requête GET pour obtenir la première page
                    string firstPageHtml = await client.GetStringAsync(baseUrl);

                    // Utilisez HtmlAgilityPack pour analyser le contenu HTML de la première page
                    // Utilisez HtmlAgilityPack pour analyser le contenu HTML de la première page
                    HtmlAgilityPack.HtmlDocument firstPageDoc = new HtmlAgilityPack.HtmlDocument();
                    firstPageDoc.LoadHtml(firstPageHtml);

                    // Trouvez l'élément contenant le texte "Page X/Y"
                    var pagerNumberElement = firstPageDoc.DocumentNode.SelectSingleNode("//div[@class='litebans-pager-number']");

                    if (pagerNumberElement != null)
                    {
                        // Obtenez le texte de l'élément
                        string pagerText = pagerNumberElement.InnerText;

                        // Utilisez une expression régulière pour extraire le nombre après le '/'
                        Match match = Regex.Match(pagerText, @"/(\d+)$");

                        if (match.Success)
                        {
                            // Récupérez la valeur correspondante au groupe capturé
                            string totalPagesText = match.Groups[1].Value;

                            // Convertissez le texte en un entier
                            if (int.TryParse(totalPagesText, out int totalPages))
                            {
                                // Maintenant, vous avez le nombre total de pages
                                numericUpDown1.Value = totalPages;

                                // Vous pouvez utiliser cette valeur pour parcourir les pages
                                for (int currentPage = 1; currentPage <= totalPages; currentPage++)
                                {
                                    if (match.Success)
                                    {
                                            // Construisez l'URL de la page actuelle
                                            string pageUrl = baseUrl + "?page=" + currentPage;

                                            // Effectuez la requête GET pour obtenir le contenu de la page web
                                            string htmlContent = await client.GetStringAsync(pageUrl);

                                            // Utilisez HtmlAgilityPack pour analyser le contenu HTML de la page actuelle
                                            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                                            doc.LoadHtml(htmlContent);

                                            // Utilisez XPath pour cibler la table spécifique (ajustez le sélecteur XPath en conséquence)
                                            var table = doc.DocumentNode.SelectSingleNode("//table[@class='table table-striped table-bordered table-condensed']");

                                            // Vérifiez si la table a été trouvée
                                            if (table != null)
                                            {
                                                // Initialisez une liste pour stocker les éléments regroupés par 5
                                                List<string> groupOfFive = new List<string>();

                                                // Parcourez les lignes de la table
                                                foreach (var row in table.SelectNodes(".//tr"))
                                                {
                                                    // Parcourez les cellules de chaque ligne
                                                    foreach (var cell in row.SelectNodes(".//td"))
                                                    {
                                                        // Ajoutez le texte de chaque cellule à la liste de regroupement
                                                        groupOfFive.Add(cell.InnerText.Trim());

                                                        // Si la liste atteint 5 éléments, ajoutez-la à la ListBox
                                                        if (groupOfFive.Count == 5)
                                                        {

                                                            string formattedGroup = "Joueur: " + string.Join(" | Sanctionné par: ", groupOfFive.Take(2)) + " | Raison: " + string.Join(" | Date: ", groupOfFive.Skip(2).Take(2)) + " | Expire: " + groupOfFive.Last();
                                                            listBox1.Items.Add(formattedGroup);
                                                            groupOfFive.Clear();
                                                        }
                                                    }
                                                }
                                            }
                                    }
                                    for (int i = listBox1.Items.Count - 1; i >= 0; i--)
                                    {
                                        for (int j = i - 1; j >= 0; j--)
                                        {
                                            if (listBox1.Items[i].ToString() == listBox1.Items[j].ToString())
                                            {
                                                listBox1.Items.RemoveAt(i);
                                                break; // Sortir de la boucle intérieure une fois qu'un doublon est supprimé
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nombre total de pages non trouvé sur la première page.");
                    }
                }
                catch (Exception ex)
                {
                    // Gérez les erreurs ici
                    MessageBox.Show("Une erreur s'est produite : " + ex.Message);
                }
            }
        }


        private async void siticoneRoundedButton1_Click(object sender, EventArgs e)
        {
            // URL de base du site
            string baseUrl = "https://ban.jedisky.fr/kicks.php";

            // Créez un objet HttpClient pour effectuer la requête HTTP
            using (HttpClient client = new HttpClient())
            {
                listBox1.Items.Clear();
                try
                {
                    // Effectuez la requête GET pour obtenir la première page
                    string firstPageHtml = await client.GetStringAsync(baseUrl);

                    // Utilisez HtmlAgilityPack pour analyser le contenu HTML de la première page
                    // Utilisez HtmlAgilityPack pour analyser le contenu HTML de la première page
                    HtmlAgilityPack.HtmlDocument firstPageDoc = new HtmlAgilityPack.HtmlDocument();
                    firstPageDoc.LoadHtml(firstPageHtml);

                    // Trouvez l'élément contenant le texte "Page X/Y"
                    var pagerNumberElement = firstPageDoc.DocumentNode.SelectSingleNode("//div[@class='litebans-pager-number']");

                    if (pagerNumberElement != null)
                    {
                        // Obtenez le texte de l'élément
                        string pagerText = pagerNumberElement.InnerText;

                        // Utilisez une expression régulière pour extraire le nombre après le '/'
                        Match match = Regex.Match(pagerText, @"/(\d+)$");

                        if (match.Success)
                        {
                            // Récupérez la valeur correspondante au groupe capturé
                            string totalPagesText = match.Groups[1].Value;

                            // Convertissez le texte en un entier
                            if (int.TryParse(totalPagesText, out int totalPages))
                            {
                                // Maintenant, vous avez le nombre total de pages
                                numericUpDown1.Value = totalPages;

                                // Vous pouvez utiliser cette valeur pour parcourir les pages
                                for (int currentPage = 1; currentPage <= totalPages; currentPage++)
                                {
                                    if (match.Success)
                                    {
                                        // Construisez l'URL de la page actuelle
                                        string pageUrl = baseUrl + "?page=" + currentPage;

                                        // Effectuez la requête GET pour obtenir le contenu de la page web
                                        string htmlContent = await client.GetStringAsync(pageUrl);

                                        // Utilisez HtmlAgilityPack pour analyser le contenu HTML de la page actuelle
                                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                                        doc.LoadHtml(htmlContent);

                                        // Utilisez XPath pour cibler la table spécifique (ajustez le sélecteur XPath en conséquence)
                                        var table = doc.DocumentNode.SelectSingleNode("//table[@class='table table-striped table-bordered table-condensed']");

                                        // Vérifiez si la table a été trouvée
                                        if (table != null)
                                        {
                                            // Initialisez une liste pour stocker les éléments regroupés par 5
                                            List<string> groupOfFive = new List<string>();

                                            // Parcourez les lignes de la table
                                            foreach (var row in table.SelectNodes(".//tr"))
                                            {
                                                // Parcourez les cellules de chaque ligne
                                                foreach (var cell in row.SelectNodes(".//td"))
                                                {
                                                    // Ajoutez le texte de chaque cellule à la liste de regroupement
                                                    groupOfFive.Add(cell.InnerText.Trim());

                                                    // Si la liste atteint 5 éléments, ajoutez-la à la ListBox
                                                    if (groupOfFive.Count == 4)
                                                    {

                                                        string formattedGroup = "Joueur: " + string.Join(" | Sanctionné par: ", groupOfFive.Take(2)) + " | Raison: " + string.Join(" | Date: ", groupOfFive.Skip(2).Take(2));
                                                        listBox1.Items.Add(formattedGroup);
                                                        groupOfFive.Clear();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    for (int i = listBox1.Items.Count - 1; i >= 0; i--)
                                    {
                                        for (int j = i - 1; j >= 0; j--)
                                        {
                                            if (listBox1.Items[i].ToString() == listBox1.Items[j].ToString())
                                            {
                                                listBox1.Items.RemoveAt(i);
                                                break; // Sortir de la boucle intérieure une fois qu'un doublon est supprimé
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nombre total de pages non trouvé sur la première page.");
                    }
                }
                catch (Exception ex)
                {
                    // Gérez les erreurs ici
                    MessageBox.Show("Une erreur s'est produite : " + ex.Message);
                }
            }
        }

        private async void siticoneRoundedButton2_Click(object sender, EventArgs e)
        {
            // URL de base du site
            string baseUrl = "https://ban.jedisky.fr/mutes.php";

            // Créez un objet HttpClient pour effectuer la requête HTTP
            using (HttpClient client = new HttpClient())
            {
                listBox1.Items.Clear();
                try
                {
                    // Effectuez la requête GET pour obtenir la première page
                    string firstPageHtml = await client.GetStringAsync(baseUrl);

                    // Utilisez HtmlAgilityPack pour analyser le contenu HTML de la première page
                    // Utilisez HtmlAgilityPack pour analyser le contenu HTML de la première page
                    HtmlAgilityPack.HtmlDocument firstPageDoc = new HtmlAgilityPack.HtmlDocument();
                    firstPageDoc.LoadHtml(firstPageHtml);

                    // Trouvez l'élément contenant le texte "Page X/Y"
                    var pagerNumberElement = firstPageDoc.DocumentNode.SelectSingleNode("//div[@class='litebans-pager-number']");

                    if (pagerNumberElement != null)
                    {
                        // Obtenez le texte de l'élément
                        string pagerText = pagerNumberElement.InnerText;

                        // Utilisez une expression régulière pour extraire le nombre après le '/'
                        Match match = Regex.Match(pagerText, @"/(\d+)$");

                        if (match.Success)
                        {
                            // Récupérez la valeur correspondante au groupe capturé
                            string totalPagesText = match.Groups[1].Value;

                            // Convertissez le texte en un entier
                            if (int.TryParse(totalPagesText, out int totalPages))
                            {
                                // Maintenant, vous avez le nombre total de pages
                                numericUpDown1.Value = totalPages;

                                // Vous pouvez utiliser cette valeur pour parcourir les pages
                                for (int currentPage = 1; currentPage <= totalPages; currentPage++)
                                {
                                    if (match.Success)
                                    {
                                        // Construisez l'URL de la page actuelle
                                        string pageUrl = baseUrl + "?page=" + currentPage;

                                        // Effectuez la requête GET pour obtenir le contenu de la page web
                                        string htmlContent = await client.GetStringAsync(pageUrl);

                                        // Utilisez HtmlAgilityPack pour analyser le contenu HTML de la page actuelle
                                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                                        doc.LoadHtml(htmlContent);

                                        // Utilisez XPath pour cibler la table spécifique (ajustez le sélecteur XPath en conséquence)
                                        var table = doc.DocumentNode.SelectSingleNode("//table[@class='table table-striped table-bordered table-condensed']");

                                        // Vérifiez si la table a été trouvée
                                        if (table != null)
                                        {
                                            // Initialisez une liste pour stocker les éléments regroupés par 5
                                            List<string> groupOfFive = new List<string>();

                                            // Parcourez les lignes de la table
                                            foreach (var row in table.SelectNodes(".//tr"))
                                            {
                                                // Parcourez les cellules de chaque ligne
                                                foreach (var cell in row.SelectNodes(".//td"))
                                                {
                                                    // Ajoutez le texte de chaque cellule à la liste de regroupement
                                                    groupOfFive.Add(cell.InnerText.Trim());

                                                    // Si la liste atteint 5 éléments, ajoutez-la à la ListBox
                                                    if (groupOfFive.Count == 5)
                                                    {

                                                        string formattedGroup = "Joueur: " + string.Join(" | Sanctionné par: ", groupOfFive.Take(2)) + " | Raison: " + string.Join(" | Date: ", groupOfFive.Skip(2).Take(2)) + " | Expire: " + groupOfFive.Last();
                                                        listBox1.Items.Add(formattedGroup);
                                                        groupOfFive.Clear();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    for (int i = listBox1.Items.Count - 1; i >= 0; i--)
                                    {
                                        for (int j = i - 1; j >= 0; j--)
                                        {
                                            if (listBox1.Items[i].ToString() == listBox1.Items[j].ToString())
                                            {
                                                listBox1.Items.RemoveAt(i);
                                                break; // Sortir de la boucle intérieure une fois qu'un doublon est supprimé
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nombre total de pages non trouvé sur la première page.");
                    }
                }
                catch (Exception ex)
                {
                    // Gérez les erreurs ici
                    MessageBox.Show("Une erreur s'est produite : " + ex.Message);
                }
            }
        }

        private async void siticoneRoundedButton3_Click(object sender, EventArgs e)
        {
            // URL de base du site
            string baseUrl = "https://ban.jedisky.fr/warnings.php";

            // Créez un objet HttpClient pour effectuer la requête HTTP
            using (HttpClient client = new HttpClient())
            {
                listBox1.Items.Clear();
                try
                {
                    // Effectuez la requête GET pour obtenir la première page
                    string firstPageHtml = await client.GetStringAsync(baseUrl);

                    // Utilisez HtmlAgilityPack pour analyser le contenu HTML de la première page
                    // Utilisez HtmlAgilityPack pour analyser le contenu HTML de la première page
                    HtmlAgilityPack.HtmlDocument firstPageDoc = new HtmlAgilityPack.HtmlDocument();
                    firstPageDoc.LoadHtml(firstPageHtml);

                    // Trouvez l'élément contenant le texte "Page X/Y"
                    var pagerNumberElement = firstPageDoc.DocumentNode.SelectSingleNode("//div[@class='litebans-pager-number']");

                    if (pagerNumberElement != null)
                    {
                        // Obtenez le texte de l'élément
                        string pagerText = pagerNumberElement.InnerText;

                        // Utilisez une expression régulière pour extraire le nombre après le '/'
                        Match match = Regex.Match(pagerText, @"/(\d+)$");

                        if (match.Success)
                        {
                            // Récupérez la valeur correspondante au groupe capturé
                            string totalPagesText = match.Groups[1].Value;

                            // Convertissez le texte en un entier
                            if (int.TryParse(totalPagesText, out int totalPages))
                            {
                                // Maintenant, vous avez le nombre total de pages
                                numericUpDown1.Value = totalPages;

                                // Vous pouvez utiliser cette valeur pour parcourir les pages
                                for (int currentPage = 1; currentPage <= totalPages; currentPage++)
                                {
                                    if (match.Success)
                                    {
                                        // Construisez l'URL de la page actuelle
                                        string pageUrl = baseUrl + "?page=" + currentPage;

                                        // Effectuez la requête GET pour obtenir le contenu de la page web
                                        string htmlContent = await client.GetStringAsync(pageUrl);

                                        // Utilisez HtmlAgilityPack pour analyser le contenu HTML de la page actuelle
                                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                                        doc.LoadHtml(htmlContent);

                                        // Utilisez XPath pour cibler la table spécifique (ajustez le sélecteur XPath en conséquence)
                                        var table = doc.DocumentNode.SelectSingleNode("//table[@class='table table-striped table-bordered table-condensed']");

                                        // Vérifiez si la table a été trouvée
                                        if (table != null)
                                        {
                                            // Initialisez une liste pour stocker les éléments regroupés par 5
                                            List<string> groupOfFive = new List<string>();

                                            // Parcourez les lignes de la table
                                            foreach (var row in table.SelectNodes(".//tr"))
                                            {
                                                // Parcourez les cellules de chaque ligne
                                                foreach (var cell in row.SelectNodes(".//td"))
                                                {
                                                    // Ajoutez le texte de chaque cellule à la liste de regroupement
                                                    groupOfFive.Add(cell.InnerText.Trim());

                                                    // Si la liste atteint 5 éléments, ajoutez-la à la ListBox
                                                    if (groupOfFive.Count == 5)
                                                    {

                                                        string formattedGroup = "Joueur: " + string.Join(" | Sanctionné par: ", groupOfFive.Take(2)) + " | Raison: " + string.Join(" | Date: ", groupOfFive.Skip(2).Take(2));
                                                        listBox1.Items.Add(formattedGroup);
                                                        groupOfFive.Clear();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    for (int i = listBox1.Items.Count - 1; i >= 0; i--)
                                    {
                                        for (int j = i - 1; j >= 0; j--)
                                        {
                                            if (listBox1.Items[i].ToString() == listBox1.Items[j].ToString())
                                            {
                                                listBox1.Items.RemoveAt(i);
                                                break; // Sortir de la boucle intérieure une fois qu'un doublon est supprimé
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nombre total de pages non trouvé sur la première page.");
                    }
                }
                catch (Exception ex)
                {
                    // Gérez les erreurs ici
                    MessageBox.Show("Une erreur s'est produite : " + ex.Message);
                }
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


        private void siticoneRoundedButton5_Click(object sender, EventArgs e)
        {
            Add();
            Actu();
        }

        private void Actu() 
        {
            listBox2.Items.Clear();
                // Connexion à la base de données
                string connectionString = "Server=83.150.217.78;Port=3306;Database=s2_test;User Id=pterodactyl;Password=4wYYZkQ6nccgvbZR4LgK;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Requête SQL pour récupérer les noms d'utilisateur
                    string query = "SELECT UserName FROM UserOnline";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    // Utilisation d'un HashSet pour stocker temporairement les noms d'utilisateur uniques
                    HashSet<string> uniqueUserNames = new HashSet<string>();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string userName = reader.GetString("UserName");
                            uniqueUserNames.Add(userName);
                        }
                    }

                    connection.Close();

                    // Ajouter les noms d'utilisateur uniques à la ListBox
                    listBox2.Items.AddRange(uniqueUserNames.ToArray());
                }  
        }
        
        private void Add() 
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
                string deleteQuery = "DELETE FROM UserOnline WHERE STR_TO_DATE(Datedefin, '%d/%m/%Y %H:%i:%s') < @CurrentDateTime";
                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                string currentDateTimeFormatted = currentDateTime.ToString("dd/MM/yyyy HH:mm:ss");
                deleteCommand.Parameters.AddWithValue("@CurrentDateTime", currentDateTimeFormatted);
                int rowsAffected = deleteCommand.ExecuteNonQuery();

                Console.WriteLine($"{rowsAffected} enregistrements supprimés.");

                connection.Close();
            }
        }

        private void siticoneRoundedButton8_Click(object sender, EventArgs e)
        {
            Process.Start("https://ban.jedisky.fr/index.php");
        }
    }
}
