using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApplication1.Properties;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for InterfaceFonctionnalité.xaml
    /// </summary>
    public partial class InterfaceFonctionnalité : Window
    {
        private int IDPAT { get; set; }
        private int IDMED { get; set; }
        private int IDFC { get; set; }
        private int IDDOSS { get; set; }
        private bool ADMIN { get; set; }
        private bool CONSULTER { get; set; }
        public TimeSpan timespan { get; set; }
        private System.Windows.Threading.DispatcherTimer timer { get; set; }
        private int minactiv { get; set; }
        private int secondsToclose { get; set; }
        private System.Windows.Threading.DispatcherTimer inactivityTimer { get; set; }
        public InterfaceFonctionnalité(int idpat, int idmed, int idfc, int iddoss,bool admin,bool consulter)
        {
            InitializeComponent();
            timer = null;
            minactiv = int.Parse(WpfApplication1.Properties.Settings.Default["Deconnexion"].ToString()) - 1;
            inactivityTimer = new System.Windows.Threading.DispatcherTimer { Interval = new TimeSpan(0, minactiv, 30) };
            inactivityTimer.Tick += delegate
            {
                inactivityTimer.Stop();
                //Traitement
                this.MainStack.IsEnabled = false;
                this.inactivityMessage.Visibility = Visibility.Visible;
                this.RecinactivityMessage.Visibility = Visibility.Visible;
                timespan = new TimeSpan(0, 00, 30);
                DateTime dt = DateTime.Now;
                timer = new System.Windows.Threading.DispatcherTimer { Interval = new TimeSpan(0, 0, 30) };
                timer.Tick += delegate
                {
                    this.timer.Stop();
                    App.authent = new auth();
                    inactivityTimer.Stop();
                    App.authent.Show();
                    App.iff.Close();
                };
                timer.Start();
                //timer = new System.Threading.Timer(new System.Threading.TimerCallback(traitement));
                //timer.Change(timespan, new TimeSpan(-1));
                secondsToclose = 1;
                minuteur.Content = ((int)timespan.Seconds).ToString();
                var secondTimer = new System.Windows.Threading.DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
                secondTimer.Tick += delegate
                {
                    secondTimer.Stop();
                    int i = 30 - ((TimeSpan)((DateTime.Now).Subtract(dt))).Seconds;
                    minuteur.Content = i.ToString();
                    if(i!=0) secondTimer.Start();
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
            CONSULTER = consulter;
            IDPAT = idpat; IDMED = idmed; IDFC = idfc; IDDOSS = iddoss; ADMIN = admin;
            this.support_fonction.NavigationService.Navigate(new PageFicheConsultation(IDFC, ADMIN, IDMED,IDPAT,consulter));
        }
        //public void traitement(object state)
        //{
        //    App.iff.Dispatcher.Invoke(()=>{
        //        App.authent = new auth();
        //        inactivityTimer.Stop();
        //        App.authent.Show();
        //        App.iff.Close();
        //        MessageBox.Show("sayi");
        //    });
        //}
        public void vide(object state)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            GridCursor.Margin = new Thickness(10 + (200 * index), 45, 0, 10);

            switch (index)
            {
                case 0:
                    this.support_fonction.NavigationService.Navigate(new PageFicheConsultation(IDFC,ADMIN,IDMED,IDPAT,CONSULTER));
                    break;
                case 1:
                    this.support_fonction.NavigationService.Navigate(new Page_Examen_Comp(IDDOSS,IDMED, ADMIN, CONSULTER,IDPAT));
                    break;
                case 2:
                    this.support_fonction.NavigationService.Navigate(new Page_Ordonnance(IDPAT, IDMED, IDFC, IDDOSS, ADMIN, CONSULTER));
                    break;
                case 3:
                    this.support_fonction.NavigationService.Navigate(new Page_Certificat(IDFC, IDDOSS, IDMED, ADMIN, CONSULTER,IDPAT));
                    break;
                case 4:
                    this.support_fonction.NavigationService.Navigate(new Page_Lettre_Orientation(IDDOSS, ADMIN,IDMED, CONSULTER,IDPAT));
                    break;
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (timer != null) timer.Stop();
            this.inactivityTimer.Stop();
            App.acc = new Nv_acceuil(IDMED, ADMIN);
            App.acc.Show();
            App.iff.Close();
        }

        private void annuler_Click(object sender, RoutedEventArgs e)
        {
            this.MainStack.IsEnabled = true;
            this.inactivityMessage.Visibility = Visibility.Hidden;
            this.RecinactivityMessage.Visibility = Visibility.Hidden;
            this.timer.Stop();
            this.minuteur.Content = "";
            secondsToclose = 1;
        }

        private void maingrd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
