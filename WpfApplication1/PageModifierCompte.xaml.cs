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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for PageModifierCompte.xaml
    /// </summary>
    public partial class PageModifierCompte : Page
    {

        private string username { get; set; }
        private System.Threading.Timer timer { get; set; }
        private bool userverif { get; set; }
        private bool passverif { get; set; }
        public PageModifierCompte(string user, string spec, string password, string type)
        {
            InitializeComponent();
            this.username  = user;
            this.Username.Text = user; this.Spécialité.Text = spec; this.Password.Password = password;
            if (type == "Medcin")
            {
                this.Med.IsChecked = true;
            }
            else this.Medadmin.IsChecked = true;
            userverif = passverif = true;
        }
        private void valider_ic_MouseEnter(object sender, MouseEventArgs e)
        {
            TimeSpan ts = new TimeSpan(0, 00, 1);
            timer = new System.Threading.Timer(new System.Threading.TimerCallback(traitement));
            timer.Change(ts, new TimeSpan(-1));

        }
        private void traitement(object state)
        {
            App.gest_adm.Dispatcher.Invoke(() =>
            {
                this.valid_box.Visibility = System.Windows.Visibility.Visible;
            });
        }

        private void valider_ic_MouseLeave(object sender, MouseEventArgs e)
        {
            timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            this.valid_box.Visibility = System.Windows.Visibility.Hidden;
        }

        private void retour_ic_MouseEnter(object sender, MouseEventArgs e)
        {
            TimeSpan ts = new TimeSpan(0, 00, 1);
            timer = new System.Threading.Timer(new System.Threading.TimerCallback(trait));
            timer.Change(ts, new TimeSpan(-1));
        }
        private void trait(object state)
        {
            App.gest_adm.Dispatcher.Invoke(() =>
            {
                this.annul_box.Visibility = System.Windows.Visibility.Visible;
            });
        }

        private void retour_ic_MouseLeave(object sender, MouseEventArgs e)
        {
            timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            this.annul_box.Visibility = System.Windows.Visibility.Hidden;
        }

        private void valider_ic_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (passverif && userverif && this.Spécialité.Text.Trim() != "")
            {
                if (ChangePassword.IsChecked == true)
                {
                    if (Password.Password.Length > 5)
                    {
                        ConnexionBDD Datab = new ConnexionBDD();
                        Datab.connecter();
                        string type;
                        if (this.Med.IsChecked == true) type = "Medcin";
                        else type = "MedcinAdmin";
                        string pass = App.CrypterMdp(this.Password.Password);
                        String Command = @"update Doctor set Username='" + this.Username.Text + "',Passwordd='" + pass + "',Spécialité='";
                        Command += this.Spécialité.Text + "',CompteAdmin='" + type + "' where Username='" + this.username + "'";
                        SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
                        Macmd.ExecuteNonQuery();
                        App.gestcpt.page_modifier_support.Visibility = System.Windows.Visibility.Hidden;
                        Datab.deconnecter();
                        App.gestcpt = new PageGestionCompte();
                        App.gest_adm.Gestion_support.NavigationService.Navigate(App.gestcpt);
                    }
                    else MessageBox.Show("Mot de passe trés court");
                }
                else
                {
                    ConnexionBDD Datab = new ConnexionBDD();
                    Datab.connecter();
                    string type;
                    if (this.Med.IsChecked == true) type = "Medcin";
                    else type = "MedcinAdmin";
                    string pass = App.CrypterMdp(this.Password.Password);
                    String Command = @"update Doctor set Username='" + this.Username.Text + "',Spécialité='";
                    Command += this.Spécialité.Text + "',CompteAdmin='" + type + "' where Username='" + this.username + "'";
                    SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
                    Macmd.ExecuteNonQuery();
                    App.gestcpt.page_modifier_support.Visibility = System.Windows.Visibility.Hidden;
                    Datab.deconnecter();
                    App.gestcpt = new PageGestionCompte();
                    App.gest_adm.Gestion_support.NavigationService.Navigate(App.gestcpt);
                }
            }
            else
            {
                //Données non valides
            }
        }

        private void retour_ic_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            App.gestcpt.page_modifier_support.Visibility = System.Windows.Visibility.Hidden;
        }

        private void ic_close_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            App.gestcpt.page_modifier_support.Visibility = System.Windows.Visibility.Hidden;
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.showPassword.Text = this.Password.Password;
            this.Password.Visibility = Visibility.Hidden;
            this.showPassword.Visibility = Visibility.Visible;
        }

        private void StackPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Password.Visibility = Visibility.Visible;
            this.showPassword.Visibility = Visibility.Hidden;
        }

        private void StackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Password.Visibility = Visibility.Visible;
            this.showPassword.Visibility = Visibility.Hidden;
        }

        private void Username_KeyDown(object sender, KeyEventArgs e)
        {
            bool verify = false;
            this.VerifLettretChiffre(e, true, ref verify);
            if (!verify)
            {
                //Afficher un message
            }
            else
            { //Masquer le message
            }
        }

        private void Username_LostFocus(object sender, RoutedEventArgs e)
        {
            //Masquer le message de la saisi 
            if (this.Username.Text.Trim() == "")
            {
                userverif = false;
                //Case vide 
            }
            else
            {
                if (this.Username.Text.Length <= 5)
                {
                    userverif = false;
                    //Mot de passe court (- de 5 )
                }
                else
                {
                    if (this.Username.Text == this.username) userverif = true;
                    else
                    {

                        ConnexionBDD Datab = new ConnexionBDD();
                        Datab.connecter();
                        String Command = @"SELECT COUNT(*) FROM Doctor WHERE Username='" + this.Username.Text + "'";
                        SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
                        SqlDataReader dr = Macmd.ExecuteReader();
                        if (dr.Read())
                        {
                            int nb = (int)dr[0];
                            if (nb != 0)
                            {
                                //Deja utilisé
                                userverif = false;
                            }
                            else
                            {
                                userverif = true;
                                //Entrée valide
                            }
                        }
                        Datab.deconnecter();
                    }
                }
            }
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            bool verify = false;
            this.VerifLettretChiffre(e, true, ref verify);
            if (!verify)
            {
                //Afficher un message
            }
            else
            { //Masquer le message
            }
        }

        private void Password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.Password.Password.Trim() == "")
            {
                //Case vide
                passverif = false;
            }
            else
            {
                if (this.Password.Password.Length <= 5)
                {
                    passverif = false;
                    //Mot de passe trés court (- de 5 )
                }
                else
                {
                    passverif = true;
                    //Valide!
                }
            }
        }
        public void VerifLettretChiffre(KeyEventArgs e, bool tiret, ref bool verif)
        {
            verif = false;
            if ((e.Key >= Key.A && e.Key <= Key.Z) || (e.Key == Key.D8 && tiret) || e.Key == Key.Tab)
            {
                verif = true;
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) { verif = true; } else e.Handled = true;
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            stack.Visibility = Visibility.Collapsed;
            stack2.Visibility = Visibility.Visible;
            Password.Password = ""; showPassword.Text = "";
        }
    }
}