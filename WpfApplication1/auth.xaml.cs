using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for auth.xaml
    /// </summary>
    public partial class auth : Window
    {
        public auth()
        {
            InitializeComponent();
        }

        private void log_butt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ConnexionBDD Datab = new ConnexionBDD();
                Datab.connecter();
                string user = Username.Text;
                string pass = App.CrypterMdp( Password.Password);
                string command = "SELECT COUNT(1) FROM Doctor WHERE Username='" + user + "' AND Passwordd='" + pass + "'";
                SqlCommand Macmd = new SqlCommand(command, Datab.cnx);
                SqlDataReader dr;
                dr = Macmd.ExecuteReader();
                int count = 0;
                if (dr.Read())
                {
                    count = (int)dr[0];
                }
                dr.Close();
                if (count == 1)
                {
                    command = "select Id_Doctor,CompteAdmin,EtatCompte from Doctor where Username='" + user + "'";
                    Macmd = new SqlCommand(command, Datab.cnx);
                    dr = Macmd.ExecuteReader();
                    int idmed = -1; string typecompt; int etat;
                    if (dr.Read())
                    {
                        idmed = (int)dr[0];
                        typecompt = dr[1].ToString();
                        etat = int.Parse(dr[2].ToString());
                        if (etat == 1)
                        {
                            if (typecompt == "Admin")
                            {
                                //Gestion des compte
                                App.gest_adm = new GestionComptes();
                                App.gest_adm.Show();
                                App.authent.Close();
                            }
                            else
                            {
                                if (typecompt == "MedcinAdmin")
                                {
                                    App.acc = new Nv_acceuil(idmed, true);
                                    App.acc.Show();
                                    App.authent.Close();
                                    App.authent = null;
                                }
                                else
                                {
                                    if (typecompt == "Medcin")
                                    {
                                        App.acc = new Nv_acceuil(idmed, false);
                                        App.acc.Show();
                                        App.authent.Close();
                                        App.authent = null;
                                    }
                                }
                            }
                        }
                        else 
                        {
                            cptdsctvé.Visibility = System.Windows.Visibility.Visible;
                            Reccptdsctvé.Visibility = System.Windows.Visibility.Visible;
                            authgrd.IsEnabled = false;
                        }
                    }
                }
                else
                {
                    Stackerrauth.Visibility = System.Windows.Visibility.Visible;
                    RecStackerrauth.Visibility = System.Windows.Visibility.Visible;
                    authgrd.IsEnabled = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ic_close_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void ic_reduire_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void rect_auth_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void Password_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                log_butt_Click(sender, new RoutedEventArgs());
            }
        }

        private void buttok_Click(object sender, RoutedEventArgs e)
        {
            cptdsctvé.Visibility = System.Windows.Visibility.Hidden;
            Reccptdsctvé.Visibility = System.Windows.Visibility.Hidden;
            authgrd.IsEnabled = true;

        }

        private void buttokerr_Click(object sender, RoutedEventArgs e)
        {
            Stackerrauth.Visibility = System.Windows.Visibility.Hidden;
            RecStackerrauth.Visibility = System.Windows.Visibility.Hidden;
            authgrd.IsEnabled = true;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
