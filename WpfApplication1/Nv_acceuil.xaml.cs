using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
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
    /// Interaction logic for Nv_acceuil.xaml
    /// </summary>
    public partial class Nv_acceuil : Window
    {
        private int Idmed { get; set; }
        public bool RdvOp { get; set; }
        public List<Notification> lisdesnotif { get; set; }
        public List<RdvAsuppr> listasupp { get; set; }
        public List<TextBlock> listText { get; set; }
        private PageNotif pagenotif { get; set; }
        public PagePatients pagepatients { get; set; }
        public PageRdv pagerdv { get; set; }
        private int nbnotif { get; set; }
        private bool ADMIN { get; set; }
        private Key dernier { get; set; }
        private Key avantdernier { get; set; }
        public TimeSpan timespan { get; set; }
        private System.Windows.Threading.DispatcherTimer timer { get; set; }
        private int minactiv { get; set; }
        private int secondsToclose { get; set; }
        private System.Windows.Threading.DispatcherTimer inactivityTimer { get; set; }
        public Nv_acceuil(int idm,bool admin)
        {
            InitializeComponent();
            timer = null;
            minactiv = int.Parse(WpfApplication1.Properties.Settings.Default["Deconnexion"].ToString()) - 1;
            if (minactiv < 0) minactiv = 4;
            inactivityTimer = new System.Windows.Threading.DispatcherTimer { Interval = new TimeSpan(0, minactiv, 30) };
            inactivityTimer.Tick += delegate
            {
                inactivityTimer.Stop();
                //Traitement
                this.MainGrid.IsEnabled = false;
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
                    App.acc.Close();
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
            this.pagepatients = new PagePatients(idm, ADMIN);
            this.pagerdv = new PageRdv(idm);
            dernier = avantdernier = Key.None;
            Idmed = idm;
            ADMIN = admin;
            RdvOp = false;
            lisdesnotif = new List<Notification>();
            listasupp = new List<RdvAsuppr>();
            listText = new List<TextBlock>();
            var delaichargement = new System.Windows.Threading.DispatcherTimer { Interval = new TimeSpan(0, 0, 10) };
            delaichargement.Tick += delegate
            {
                delaichargement.Stop();
                loadRdvImpo();
                loadRdvasupp();
            };
            delaichargement.Start();
            Bordure.Visibility = System.Windows.Visibility.Hidden;
            this.support_acc.Content = null;
            this.support_acc.Visibility = Visibility.Visible;
            this.support_acc.NavigationService.Navigate(new PageHome());
            nbnotif = 0;
        }
        //public void traitement(object state)
        //{
        //    App.acc.Dispatcher.Invoke(() =>
        //    {
        //        App.authent = new auth();
        //        this.timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        //        inactivityTimer.Stop();
        //        App.authent.Show();
        //        App.acc.Close();
        //    });
        //}
        private void logOut_Click(object sender, RoutedEventArgs e)
        {
            killTimer();
            App.authent = new auth();
            App.authent.Show();
            App.acc.Close();
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Hidden;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Hidden;
        }

        private void boutonstngs_Click(object sender, RoutedEventArgs e)
        {
            int notif = int.Parse(WpfApplication1.Properties.Settings.Default["Notification"].ToString());
            string notiff;
            if (notif < 24) notiff = notif + " heures";
            else notiff = "1 jour";
            Notifstocké.Text = notiff;
            NotifCombo.SelectedIndex = NotifCombo.Items.IndexOf(notiff);
            notif = int.Parse(WpfApplication1.Properties.Settings.Default["Deconnexion"].ToString());
            if (notif < 60) Deconnexstocké.Text = notif + " mn";
            else Deconnexstocké.Text = "1 heure";
            Stack_settings.Visibility = System.Windows.Visibility.Visible;
            RecStack_settings.Visibility = System.Windows.Visibility.Visible;
        }

        private void consultation_item_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.support_acc.Content = null;
            this.support_acc.Visibility = Visibility.Visible;
            this.support_acc.NavigationService.Navigate(this.pagepatients);
        }

        private void nv_pat_item_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.support_acc.Content = null;
            this.support_acc.Visibility = Visibility.Visible;
            this.support_acc.NavigationService.Navigate(new PageNvPatient(this.Idmed,ADMIN));
        }

        private void rdv_item_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //this.support_acc.Content = null;
            this.support_acc.Visibility = Visibility.Visible;
            this.pagerdv = new PageRdv(Idmed);
            this.support_acc.NavigationService.Navigate(this.pagerdv);
        }

        private void ic_close_acc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            foreach (Notification notif in lisdesnotif)
            {
                notif.killTimer();
            }
            killTimer();
            App.acc.Close();     
        }

        private void ic_reduire_acc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            App.acc.WindowState = System.Windows.WindowState.Minimized;
        }

        private void title_grd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            App.acc.DragMove();
        }
        public void Remove_at_from_listasupp()
        {
            listasupp.RemoveAt(0);
        }
        
        private void loadRdvImpo()
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = @"select Date_Rdv,Lieu,Commentaire,Nom,Prenom,Id_RDV from [Rendez-vous] LEFT OUTER JOIN Patient";
            Command = Command + @" on [Rendez-vous].Id_Patient=Patient.Id_Patient LEFT OUTER JOIN Person on Patient.Id_Person=Person.Id_Person";
            Command = Command + @" where Important=1 and Id_Rdv in ( SELECT Id_RDV FROM Avoir_RDV WHERE Id_Doctor=" + this.Idmed + ") order by [Rendez-vous].Date_Rdv";
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            SqlDataReader dr = Macmd.ExecuteReader();
            DateTime dt; String l, c, np; int id;
            while (dr.Read())
            {
                dt = DateTime.Parse(dr[0].ToString());
                l = dr[1].ToString();
                c = dr[2].ToString();
                np = dr[3].ToString() + " " + dr[4].ToString();
                id = int.Parse(dr[5].ToString());
                Notification notif = new Notification(dt, l, c, np, id);
                lisdesnotif.Add(notif);
            }
            Datab.deconnecter();
        }
        public void TraitementNotif(DateTime date, String lieu, String commentaire, String nomprenom)
        {
            TimeSpan span = date.Subtract(DateTime.Now);
            if (span.Seconds > 10)
            {
                App.acc.suppor_notif.Content = null;
                App.acc.suppor_notif.Visibility = System.Windows.Visibility.Visible;
                pagenotif = new PageNotif(date, lieu, nomprenom);
                App.acc.suppor_notif.NavigationService.Navigate(pagenotif);
                span = new TimeSpan(0, 0, 15);
                System.Threading.Timer timer = new System.Threading.Timer(new System.Threading.TimerCallback(masquerRaccourciNotif));
                timer.Change(span, new TimeSpan(-1));
                TimeSpan span1 = new TimeSpan(0, 0, 10);
                System.Threading.Timer timer1 = new System.Threading.Timer(new System.Threading.TimerCallback(ReduireOpacity));
                timer1.Change(span1, new TimeSpan(-1));
                addNotif(date, lieu, commentaire, nomprenom);
                this.nbnotif++;
                this.BorderNbNotif.Visibility = Visibility.Visible;
                if (this.nbnotif < 100)
                {
                    if (this.nbnotif < 10) this.BorderNbNotif.Margin = new Thickness(302, 32, 756, 15);
                    else this.BorderNbNotif.Margin = new Thickness(302, 32, 752, 15);
                    this.nbNotif.Content = this.nbnotif;
                }
                else
                {
                    this.BorderNbNotif.Margin = new Thickness(302,30,745,15);
                    this.nbNotif.Content = "+99";
                }
                
                //Supprimer de la lise des notifications
            }
        }
        private void ReduireOpacity(object state)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.pagenotif.Opacity = 0.5f;
            });
        }
        private void addNotif(DateTime date, String lieu, String commentaire, String nomprenom)
        {
            TextBlock bloq = new TextBlock();
            bloq.Text = "Rendez-vous à " + date.ToShortTimeString() + "\nLieu: " + lieu;
            if (nomprenom != " ") bloq.Text += " avec " + nomprenom;
            if (commentaire.Length >= 60) bloq.Text += "\n"+commentaire.Remove(57) + "...";
            else bloq.Text += "\n" + commentaire;
            
            //Style
            bloq.Margin = new Thickness(2, 3, 2, 3);
            bloq.Cursor = Cursors.Hand;
            bloq.TextWrapping = TextWrapping.Wrap;
            bloq.Foreground = Brushes.Black;
            BrushConverter nr = new BrushConverter();
            bloq.Background =(Brush)(nr.ConvertFrom("#FF3970D1"));
            //bloq.Background = Brushes.PeachPuff;

            //MouseEvents
            bloq.MouseLeftButtonUp += (sender, e) =>
            {
                //Change the color 
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.Transparent;
            };

            listText.Add(bloq);
        }
        private void trierTextBloq()
        {
            int changes = -1; string tb1, tb2; TextBlock inter;
            while (changes != 0)
            {
                changes = 0;
                for (int i = 0; i < listText.Count - 1; i++)
                {
                    try
                    {
                        tb1 = listText[i].Text;
                        tb2 = listText[i + 1].Text;
                        int t1 = int.Parse(tb1.Remove(2))*100 + int.Parse(tb1.Remove(0, 3).Remove(2));
                        int t2 = int.Parse(tb2.Remove(2))*100 + int.Parse(tb2.Remove(0, 3).Remove(2));
                        int tnow = int.Parse(DateTime.Now.ToShortTimeString().Remove(2))*100;
                        tnow += int.Parse(DateTime.Now.ToShortTimeString().Remove(0,3));
                        if ((tnow < t1) && (tnow < t2))
                        {
                            if (t1 > t2)
                            {
                                inter = listText[i];listText[i] = listText[i + 1];listText[i + 1] = inter;
                                changes++;
                            }
                        }
                        if ((tnow < t2) && (tnow > t1))
                        {
                            inter = listText[i]; listText[i] = listText[i + 1]; listText[i + 1] = inter;
                            changes++;
                        }
                        if ((tnow > t1) && (tnow > t2))
                        {
                            if (t1 > t2)
                            {
                                inter = listText[i]; listText[i] = listText[i + 1]; listText[i + 1] = inter;
                                changes++;
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        tb1 = exc.Message;
                    }
                }
            }
        }
        private void masquerRaccourciNotif(object state)
        {
            this.Dispatcher.Invoke(() =>
            {
                App.acc.suppor_notif.Visibility = System.Windows.Visibility.Hidden;
            });
        }
        private void DeleteRdv(int id)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = @"DELETE FROM [Rendez-vous] where Id_RDV=" + id;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            Macmd.ExecuteNonQuery();
        }
        private void loadRdvasupp()
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = @"select Id_RDV,Date_Rdv from [Rendez-vous]";
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            SqlDataReader dr = Macmd.ExecuteReader();
            DateTime dt; int id;
            while (dr.Read())
            {
                id = int.Parse(dr[0].ToString());
                dt = DateTime.Parse(dr[1].ToString());
                RdvAsuppr rdv = new RdvAsuppr(id, dt);
                listasupp.Add(rdv);
            }
            Datab.deconnecter();
        }

        private void PackIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            this.notification.Visibility = Visibility.Visible;
        }

        private void PackIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            this.notification.Visibility = Visibility.Hidden;
        }

        private void PackIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.Bordure.Visibility == Visibility.Hidden)
            {
                notifStack.Children.Clear();
                this.trierTextBloq();
                foreach (TextBlock tbloq in listText)
                {
                    notifStack.Children.Add(tbloq);
                    notifStack.Children.Add(new Separator());
                }
                this.Bordure.Visibility = Visibility.Visible;
                this.BorderNbNotif.Visibility = Visibility.Hidden;
                this.nbnotif = 0;
            }
            else
            {
                this.Bordure.Visibility = Visibility.Hidden;
            }
            this.notification.Visibility = Visibility.Hidden;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var page = Window.GetWindow(this);
            page.KeyDown += Page_KeyUp;
        }
        private void Page_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.N)
            {
                if (avantdernier == Key.RightShift && dernier == Key.LeftCtrl)
                {
                    int timestamp = new TimeSpan(DateTime.Now.Ticks).Seconds;
                    var mouseevent = new MouseButtonEventArgs(Mouse.PrimaryDevice, timestamp, MouseButton.Left)
                    {
                        RoutedEvent = UIElement.MouseLeftButtonUpEvent,
                        Source = App.acc.nv_pat_item
                    };
                    this.nv_pat_item.RaiseEvent(mouseevent);
                }
                else
                {
                    dernier = avantdernier; avantdernier = e.Key;
                }
            }
            else
            {
                dernier = avantdernier; avantdernier = e.Key;
            }
        }
        public void removeFromNotif(DateTime date)
        {
            int i = 0; bool supp = false;
            while (i < listText.Count && !supp)
            {
                TextBlock bloq = listText[i];
                if (bloq.Text.StartsWith("Rendez-vous à " + date.ToShortTimeString()))
                {
                    listText.RemoveAt(i); supp = true;
                    this.nbnotif--;
                    if (nbnotif == 0) this.BorderNbNotif.Visibility = Visibility.Hidden;
                    else
                    {
                        if (this.nbnotif < 100)
                        {
                            if (this.nbnotif < 10) this.BorderNbNotif.Margin = new Thickness(302, 32, 756, 15);
                            else this.BorderNbNotif.Margin = new Thickness(302, 32, 752, 15);
                            this.nbNotif.Content = this.nbnotif;
                        }
                    }
                }
                i++;
            }
        }

     //**************************************************************************************************
        private void icvaliderdeco_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (SelectionDeconnexion.SelectedIndex > -1)
            {
                int res = 5;
                switch (SelectionDeconnexion.SelectedIndex)
                {
                    case 0:
                        res = 5;
                        break;
                    case 1:
                        res = 10;
                        break;
                    case 2:
                        res = 15;
                        break;
                    case 3:
                        res = 30;
                        break;
                    case 4:
                        res = 45;
                        break;
                    case 5:
                        res = 60;
                        break;
                }
                WpfApplication1.Properties.Settings.Default["Deconnexion"] = res;
                minactiv = res;
                WpfApplication1.Properties.Settings.Default.Save();
            }
            StackDécoauto.Visibility = System.Windows.Visibility.Hidden;
            RecStackDécoauto.Visibility = System.Windows.Visibility.Hidden;
            stngs_grd.IsEnabled = true;
        }

        private void icvalidernotif_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (NotifCombo.SelectedIndex > -1)
            {
                int res = 8;
                switch (NotifCombo.SelectedIndex)
                {
                    case 0:
                        res = 8;
                        break;
                    case 1:
                        res = 12;
                        break;
                    case 2:
                        res = 16;
                        break;
                    case 3:
                        res = 24;
                        break;
                }
                WpfApplication1.Properties.Settings.Default["Notification"] = res;
                WpfApplication1.Properties.Settings.Default.Save();

            }
            Stacknotif.Visibility = System.Windows.Visibility.Hidden;
            RecStacknotif.Visibility = System.Windows.Visibility.Hidden;
            stngs_grd.IsEnabled = true;
        }

        private void icvalideraddmedic_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (ajoutermedicbox.Text != "")
            {
                Medicament.Insert_medic(ajoutermedicbox.Text);
                Stackajoutermedic.Visibility = System.Windows.Visibility.Hidden;
                RecStackajoutermedic.Visibility = System.Windows.Visibility.Hidden;
                stngs_grd.IsEnabled = true;
            }
            else
            {
                stack_rien_saisi.Visibility = System.Windows.Visibility.Visible;
                Recstack_rien_saisi.Visibility = System.Windows.Visibility.Visible;
                Stackajoutermedic.IsEnabled = false;

            }

        }


        private void ic_add_medic_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Stackajoutermedic.Visibility = System.Windows.Visibility.Visible;
            RecStackajoutermedic.Visibility = System.Windows.Visibility.Visible;
            stngs_grd.IsEnabled = false;
        }

        private void icnonaddmedic_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Stackajoutermedic.Visibility = System.Windows.Visibility.Hidden;
            RecStackajoutermedic.Visibility = System.Windows.Visibility.Hidden;
            stngs_grd.IsEnabled = true;

        }

        private void PackIcon_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            Stacknotif.Visibility = System.Windows.Visibility.Visible;
            RecStacknotif.Visibility = System.Windows.Visibility.Visible;
            stngs_grd.IsEnabled = false;
        }

        private void icnonnotif_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Stacknotif.Visibility = System.Windows.Visibility.Hidden;
            RecStacknotif.Visibility = System.Windows.Visibility.Hidden;
            stngs_grd.IsEnabled = true;
        }

        private void ic_deco_auto_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackDécoauto.Visibility = System.Windows.Visibility.Visible;
            RecStackDécoauto.Visibility = System.Windows.Visibility.Visible;
            stngs_grd.IsEnabled = false;
        }

        private void icnonDeco_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StackDécoauto.Visibility = System.Windows.Visibility.Hidden;
            RecStackDécoauto.Visibility = System.Windows.Visibility.Hidden;
            stngs_grd.IsEnabled = true;
        }

        private void buttokk_Click(object sender, RoutedEventArgs e)
        {
            Recstack_rien_saisi.Visibility = System.Windows.Visibility.Hidden;
            stack_rien_saisi.Visibility = System.Windows.Visibility.Hidden;
            Stackajoutermedic.IsEnabled = true;
        }

        private void buttfermer_Click(object sender, RoutedEventArgs e)
        {
            Stack_settings.Visibility = System.Windows.Visibility.Hidden;
            RecStack_settings.Visibility = System.Windows.Visibility.Hidden;
        }

        private void NotifCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Notifstocké.Visibility = Visibility.Hidden;
        }
        private void annuler_Click(object sender, RoutedEventArgs e)
        {
            this.MainGrid.IsEnabled = true;
            this.inactivityMessage.Visibility = Visibility.Hidden;
            this.RecinactivityMessage.Visibility = Visibility.Hidden;
            this.timer.Stop();
            this.minuteur.Content = "";
            secondsToclose = 1;
        }
        private void SelectionDeconnexion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Deconnexstocké.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = System.IO.Directory.GetCurrentDirectory();
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = path + @"\Aide.html";
                process.Start();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message.ToString());
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = System.IO.Directory.GetCurrentDirectory();
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = path + @"\Apropos.html";
                process.Start();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message.ToString());
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ListViewItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.support_acc.Content = null;
            this.support_acc.Visibility = Visibility.Visible;
            this.support_acc.NavigationService.Navigate(new PageHome());
        }
        public void killTimer()
        {
            if (timer != null) timer.Stop();
            this.inactivityTimer.Stop();
        }
    }
}
