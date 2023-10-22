using KeyAuth;
using System;
using System.Windows.Forms;

namespace Loader
{
    public partial class Overlays : Form
    {
        public Overlays()
        {
            InitializeComponent();
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

        private void Timer_Tick(object sender, EventArgs e)
        {
            // À chaque tick du Timer, mettez à jour le temps écoulé
            UpdateElapsedTime();
        }

        private readonly DateTime startTime;

        private void UpdateElapsedTime()
        {
            // Calculez la durée écoulée depuis le démarrage de l'application
            TimeSpan elapsedTime = DateTime.Now - startTime;

            // Mettez à jour le texte du Label avec le temps écoulé
            label1.Text = $"{elapsedTime.Hours:D2}:{elapsedTime.Minutes:D2}:{elapsedTime.Seconds:D2}";
        }

        private void Overlays_Load(object sender, EventArgs e)
        {

            this.Top = 0;
            this.Left = 0;
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.FormClosing += Form_Closing;
            label4.Text = Login.KeyAuthApp.user_data.username;
            label5.Text = Login.KeyAuthApp.user_data.subscriptions[0].subscription;
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            // Annuler la fermeture
            e.Cancel = true;
        }
    }
}
