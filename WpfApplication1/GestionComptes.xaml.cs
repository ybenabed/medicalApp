using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for GestionComptes.xaml
    /// </summary>
    public partial class GestionComptes : Window
    {
        public TimeSpan timespan { get; set; }
        private System.Windows.Threading.DispatcherTimer timer { get; set; }
        private int minactiv { get; set; }
        private int secondsToclose { get; set; }
        private System.Windows.Threading.DispatcherTimer inactivityTimer { get; set; }
        public GestionComptes()
        {
            InitializeComponent();
            timer = null;
            App.gestcpt = new PageGestionCompte();
            Gestion_support.NavigationService.Navigate(App.gestcpt);
            minactiv = int.Parse(WpfApplication1.Properties.Settings.Default["Deconnexion"].ToString()) - 1;
            if (minactiv < 0) minactiv = 4;
            inactivityTimer = new System.Windows.Threading.DispatcherTimer { Interval = new TimeSpan(0, minactiv, 30) };
            inactivityTimer.Tick += delegate
            {
                inactivityTimer.Stop();
                //Traitement
                this.GridMain.IsEnabled = false;
                this.inactivityMessage.Visibility = Visibility.Visible;
                timespan = new TimeSpan(0, 00, 30);
                DateTime dt = DateTime.Now;
                timer = new System.Windows.Threading.DispatcherTimer { Interval = new TimeSpan(0, 0, 30) };
                timer.Tick += delegate
                {
                    this.timer.Stop();
                    App.authent = new auth();
                    inactivityTimer.Stop();
                    App.authent.Show();
                    App.gest_adm.Close();
                };
                timer.Start();
                secondsToclose = 1;
                minuteur.Content = ((int)timespan.Seconds).ToString();
                var secondTimer = new System.Windows.Threading.DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
                secondTimer.Tick += delegate
                {
                    secondTimer.Stop();
                    int i = 30 - ((TimeSpan)((DateTime.Now).Subtract(dt))).Seconds;
                    minuteur.Content = i.ToString();
                    if (i != 0) secondTimer.Start();
                };
                secondTimer.Start();
                //
                inactivityTimer.Start();
            };
            inactivityTimer.Start();
            InputManager.Current.PostProcessInput += delegate(object obj, ProcessInputEventArgs input)
            {
                if (input.StagingItem.Input is MouseButtonEventArgs || input.StagingItem.Input is KeyEventArgs)
                    inactivityTimer.Interval = new TimeSpan(0, minactiv, 0);
            };
        }
        private void annuler_Click(object sender, RoutedEventArgs e)
        {
            this.GridMain.IsEnabled = true;
            this.inactivityMessage.Visibility = Visibility.Hidden;
            this.timer.Stop();
            this.minuteur.Content = "";
            secondsToclose = 1;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            GridCursor.Margin = new Thickness(10 + (150 * index), 0, 0, 0);

            switch (index)
            {
                case 0:
                    App.gestcpt = new PageGestionCompte();
                    Gestion_support.NavigationService.Navigate(App.gestcpt);
                    break;
                case 1:
                    // GridMain.Background = Brushes.White;
                    Gestion_support.NavigationService.Navigate(new PageInscription());
                    break;
            }
        }
        private void shut_butt_Click(object sender, RoutedEventArgs e)
        {
            inactivityTimer.Stop();
            if (timer != null) timer.Stop();
            App.authent = new auth();
            App.authent.Show();
            App.gest_adm.Close();
        }

        private void maingrd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
