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
    /// Interaction logic for PageInscription.xaml
    /// </summary>
    public partial class PageInscription : Page
    {
        private bool emailverif { get; set; }
        private bool userverif { get; set; }
        private bool passverif { get; set; }
        private bool confverif { get; set; }
        private System.Windows.Visibility vizibl { get; set; }
        private System.Windows.Visibility Nonvizibl { get; set; }
        private PageVerifSaisi pageframe { get; set; }
        public PageInscription()
        {
            InitializeComponent();
            vizibl = System.Windows.Visibility.Visible;
            Nonvizibl = System.Windows.Visibility.Hidden;
            pageframe = new PageVerifSaisi();
            this.adressWarning.Content = "Vérifiez la saisi de ce champs"; this.adressWarning.Visibility = Nonvizibl;
            this.confirmpasswordWarning.Content = "Les mots de passes ne sont pas identiques"; this.confirmpasswordWarning.Visibility = Nonvizibl;
            saisiFrame.Visibility = Nonvizibl;
            saisiFrame.Content = pageframe;
            userverif = emailverif = passverif = confverif = false;
            this.doctorConfirmpassword.IsEnabled = false;
            this.doctorConfirmpassword.Width = 100;
        }
        public void VerifLettretChiffre(KeyEventArgs e, bool tiret,bool chiffre, ref bool verif)
        {
            verif = false;
            if ((e.Key >= Key.A && e.Key <= Key.Z) || (e.Key == Key.D8 && tiret) || e.Key == Key.Tab)
            {
                verif = true;
            }
            else if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 && chiffre)||
                ((e.Key==Key.D2 || e.Key==Key.D7 || e.Key==Key.D9 || e.Key==Key.D0)&&(Keyboard.GetKeyStates(Key.CapsLock)==KeyStates.None)
                &&(Keyboard.IsKeyDown(Key.RightShift)==false && Keyboard.IsKeyDown(Key.LeftShift)==false)))
            { verif = true; } else e.Handled = true;
        }
        private void buttonInscription_Click(object sender, RoutedEventArgs e)
        {
            if (!verif())
            {
                warningStack.Visibility = Visibility.Visible;
                RecSwarningStack.Visibility = System.Windows.Visibility.Visible;
                Fenetre.IsEnabled = false;
             //   warningStack.Background = Brushes.Red;
                Warning.Content = "Vérifier les informations que vous avez rentré!";
            }
            else
            {
                this.removeWhites();
                string encryptedpasswrd = App.CrypterMdp(doctorPassword.Password);
                Doctor doc = new Doctor(doctorNom.Text, doctorPrenom.Text, doctorMail.Text, doctorAdress.Text, doctorNumber.Text
                    , doctorSexe.Text, doctorSpec.Text, doctorUsername.Text, encryptedpasswrd);
                string type = "Medcin";
                if (this.type.IsChecked == true) type = "MedcinAdmin";
                doc.Insert_Nv_Doctor(type);
                warningStack.Visibility = Visibility.Visible;
                RecSwarningStack.Visibility = System.Windows.Visibility.Visible;
                Fenetre.IsEnabled = false;
               // warningStack.Background = Brushes.Green;
                Warning.Content = "Compte créé avec succès!";
                vider();
            }
        }

        private void doctorUsername_KeyDown(object sender, KeyEventArgs e)
        {
            bool verify = false;
            this.VerifLettretChiffre(e, true,true, ref verify);
            if (!verify)
            {
                saisiFrame.Margin = new Thickness(242, 212, 13, 118);
                saisiFrame.Visibility = vizibl;
            }
            else saisiFrame.Visibility = Nonvizibl;
        }
        private void doctorUsername_LostFocus(object sender, RoutedEventArgs e)
        {
            saisiFrame.Visibility = Nonvizibl;
            if (this.doctorUsername.Text.Trim() == "")
            {
                this.doctorUsername.Width = 100;
                this.usernamepic.Visibility = vizibl;
                this.usernameWarning.Content = "Non Rempli!";
                this.usernameWarning.Visibility = vizibl;
                userverif = false;
            }
            else
            {
                if (this.doctorUsername.Text.Length <= 5)
                {
                    this.doctorUsername.Width = 100;
                    this.usernamepic.Visibility = vizibl;
                    this.usernameWarning.Content = "Nom d'utilisateur trés court";
                    this.usernameWarning.Visibility = vizibl;
                    userverif = false;
                }
                else
                {
                    ConnexionBDD Datab = new ConnexionBDD();
                    Datab.connecter();
                    String Command = @"SELECT COUNT(*) FROM Doctor WHERE Username='" + this.doctorUsername.Text + "'";
                    SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
                    SqlDataReader dr = Macmd.ExecuteReader();
                    if (dr.Read())
                    {
                        int nb = (int)dr[0];
                        if (nb != 0)
                        {
                            this.doctorUsername.Width = 100;
                            this.usernamepic.Visibility = vizibl;
                            this.usernameWarning.Content = "Nom d'utilisateur déja utilisé";
                            this.usernameWarning.Visibility = vizibl;
                            userverif = false;
                        }
                        else
                        {
                            this.doctorUsername.Width = 160;
                            this.usernamepic.Visibility = Nonvizibl;
                            this.usernameWarning.Visibility = Nonvizibl;
                            userverif = true;
                        }
                    }
                    Datab.deconnecter();
                }
            }
        }
        private void doctorPassword_KeyDown(object sender, KeyEventArgs e)
        {
            bool verify = false;
            this.VerifLettretChiffre(e, true,true, ref verify);
            if (!verify)
            {
                saisiFrame.Margin = new Thickness(242, 247, 13, 83);
                saisiFrame.Visibility = vizibl;
            }
            else saisiFrame.Visibility = Nonvizibl;
            if (this.doctorPassword.Password.Length <= 5)
            {
                passverif = false;
                this.doctorConfirmpassword.IsEnabled = false;
                this.doctorConfirmpassword.Width = 100;
            }
            else
            {
                passverif = true;
                this.doctorConfirmpassword.IsEnabled = true;
                this.doctorConfirmpassword.Width = 160;
                this.passpic.Visibility = Nonvizibl;
                this.passwordWarning.Visibility = Nonvizibl;
            }
        }

        private void doctorConfirmpassword_KeyDown(object sender, KeyEventArgs e)
        {
            this.confirmpasswordWarning.Visibility = Nonvizibl;
            this.confirmpic.Visibility = Nonvizibl;
            this.doctorConfirmpassword.Width = 160;
            bool verify = false;
            this.VerifLettretChiffre(e, false,true, ref verify);
        }

        private void doctorMail_LostFocus(object sender, RoutedEventArgs e)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = @"SELECT COUNT(*) FROM Person WHERE Email='" + this.doctorMail.Text + "'";
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            SqlDataReader dr = Macmd.ExecuteReader();
            if (dr.Read())
            {
                int nb = (int)dr[0];
                if (nb != 0)
                {
                    this.doctorMail.Width = 100;
                    this.mailpic.Visibility = vizibl;
                    this.mailWarning.Content = "Cet Email est déja utilisé";
                    this.mailWarning.Visibility = vizibl;
                    emailverif = false;
                }
                else
                {
                    this.doctorMail.Width = 160;
                    this.mailpic.Visibility = Nonvizibl;
                    this.mailWarning.Visibility = Nonvizibl;
                    emailverif = true;
                }
            }
            Datab.deconnecter();
        }
        private void doctorPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            saisiFrame.Visibility = Nonvizibl;
            if (this.doctorPassword.Password.Trim() == "")
            {
                this.doctorPassword.Width = 100;
                this.passpic.Visibility = vizibl;
                this.passwordWarning.Content = "Indiquez un mot de passe!";
                this.passwordWarning.Visibility = vizibl;
                passverif = false;
            }
            else
            {
                if (this.doctorPassword.Password.Length <= 5)
                {
                    this.doctorPassword.Width = 100;
                    this.passpic.Visibility = vizibl;
                    if (this.doctorPassword.Password.Length == 0) this.passwordWarning.Content = "Indiquez un mot de passe!";
                    else this.passwordWarning.Content = "Mot de passe trés court!";
                    this.passwordWarning.Visibility = vizibl;
                    passverif = false;
                }
                else
                {
                    this.passpic.Visibility = Nonvizibl;
                    this.passwordWarning.Visibility = Nonvizibl;
                    this.doctorPassword.Width = 160;
                    passverif = true;
                }
            }
        }

        private void doctorConfirmpassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.doctorPassword.Password != this.doctorConfirmpassword.Password)
            {
                this.confirmpasswordWarning.Visibility = vizibl;
                this.confirmpic.Visibility = vizibl;
                this.doctorConfirmpassword.Width = 100;
                this.confirmpasswordWarning.Content = "Non identique";
                confverif = false;
            }
            else
            {
                this.confirmpasswordWarning.Visibility = Nonvizibl;
                this.confirmpic.Visibility = Nonvizibl;
                this.doctorConfirmpassword.Width = 160;
                confverif = true;
            }
        }

        private void doctorNom_KeyDown(object sender, KeyEventArgs e)
        {
            bool verify = false;
            this.VerifLettretChiffre(e, true,false, ref verify);
            if (!verify)
            {
                saisiFrame.Margin = new Thickness(61, 55, 190, 270);
                saisiFrame.Visibility = vizibl;
            }
            else
            {
                saisiFrame.Visibility = Nonvizibl;
            }
        }

        private void doctorPrenom_KeyDown(object sender, KeyEventArgs e)
        {
            bool verify = false;
            this.VerifLettretChiffre(e, true,false, ref verify);
            if (!verify)
            {
                saisiFrame.Margin = new Thickness(212, 55, 40, 270);
                saisiFrame.Visibility = vizibl;
            }
            else
            {
                saisiFrame.Visibility = Nonvizibl;
            }
        }

        private void doctorNom_LostFocus(object sender, RoutedEventArgs e)
        {
            saisiFrame.Visibility = Nonvizibl;
        }

        private void doctorPrenom_LostFocus(object sender, RoutedEventArgs e)
        {
            saisiFrame.Visibility = Nonvizibl;
        }

        private void doctorSpec_KeyDown(object sender, KeyEventArgs e)
        {
            bool verify = false;
            this.VerifLettretChiffre(e, true,false, ref verify);
            if (!verify)
            {
                saisiFrame.Margin = new Thickness(242, 104, 13, 226);
                saisiFrame.Visibility = vizibl;
            }
            else saisiFrame.Visibility = Nonvizibl;
        }

        private void doctorSpec_LostFocus(object sender, RoutedEventArgs e)
        {
            saisiFrame.Visibility = Nonvizibl;
        }
        public bool verif()
        {
            if (confverif && passverif && emailverif && userverif)
            {
                if (doctorAdress.Text.Trim() != "" && doctorNom.Text.Trim() != "" && doctorPrenom.Text != "" && doctorNumber.Text.Trim() != ""
                    && doctorSexe.Text != "" && doctorSpec.Text.Trim() != "" && doctorMail.Text.Trim() != "") return true;
                else return false;
            }
            else return false;
        }
        private void vider()
        {
            doctorNom.Clear();
            doctorPrenom.Clear();
            doctorAdress.Clear();
            doctorSpec.Clear();
            doctorNumber.Clear();
            doctorMail.Clear();
            doctorUsername.Clear();
            doctorPassword.Clear();
            this.type.IsChecked = false;
            doctorConfirmpassword.Clear();
            doctorSexe.SelectedItem = null;
        }

        private void buttonRetoure_Click(object sender, RoutedEventArgs e)
        {
            vider();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            warningStack.Visibility = Visibility.Hidden;
            RecSwarningStack.Visibility = System.Windows.Visibility.Hidden;
            Fenetre.IsEnabled = true;
        }
        private void removeWhites()
        {
            doctorNom.Text = doctorNom.Text.Trim(); doctorPrenom.Text = doctorPrenom.Text.Trim(); doctorAdress.Text = doctorAdress.Text.Trim();
            while (doctorAdress.Text.Contains("  "))
            {
                doctorAdress.Text=doctorAdress.Text.Replace("  "," ");
            }
            while (doctorPrenom.Text.Contains("  "))
            {
                doctorPrenom.Text=doctorPrenom.Text.Replace("  "," ");
            }
            while (doctorNom.Text.Contains("  "))
            {
                doctorNom.Text = doctorNom.Text.Replace("  ", " ");
            }
        }

        private void doctorNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) { }
            else e.Handled = true;
        }

        private void doctorNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int eff;
            if (int.TryParse(e.Text, out eff)) { }
            else e.Handled = true;
        }
        
    }
}
