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
    /// Interaction logic for NvConsultation.xaml
    /// </summary>
    public partial class NvConsultation : Window
    {
        private String nomf { get; set; }
        private String prenomf { get; set; }
        private int agef { get; set; }
        private DateTime datef = DateTime.Now;
        private String an { get; set; }
        private int idpat { get; set; }
        private int idmedd { get; set; }
        private int id_dm { get; set; }
        private int id_fc { get; set; }
        private bool ADMINN { get; set; }
        public NvConsultation(int id_dm, int Idpat, int Idmed, bool admin)
        {
            InitializeComponent();
            this.id_dm = id_dm; this.idpat = Idpat; this.idmedd = Idmed; ADMINN = admin;
            //date.Text = DateTime.Now.ToString();
            DateTime dat = new DateTime();
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            int id_pat = 0;
            int id_per = 0;
            DateTime date_naiss = DateTime.Now;
            string Command = " SELECT Id_Patient FROM Dossier_medical WHERE Id_Dossier =" + id_dm;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            SqlDataReader read = Macmd.ExecuteReader();

            while (read.Read())
            {
                id_pat = (int)read[0];
            }
            read.Close();
            string Commande = @"SELECT Id_Person,Date_de_naissance  FROM Patient WHERE Id_Patient=" + id_pat;
            SqlCommand Macmde = new SqlCommand(Commande, Datab.cnx);
            SqlDataReader reade = Macmde.ExecuteReader();

            while (reade.Read())
            {
                id_per = (int)reade["Id_Person"];
                date_naiss = (DateTime)reade["Date_de_naissance"];
                dat = date_naiss;
            }

            reade.Close();
            string Comman = @"SELECT Nom,Prenom FROM Person WHERE Id_Person=" + id_per;
            SqlCommand Macm = new SqlCommand(Comman, Datab.cnx);
            SqlDataReader myReader = Macm.ExecuteReader();

            while (myReader.Read())
            {
                nom.Text = myReader[0].ToString(); ;
                prenom.Text = myReader[1].ToString();
                age.Text = calcul_age(dat);
            }
            myReader.Close();
        }

        private void Stack_title_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ic_minimise_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void ic_close_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            confirmStack.Visibility = System.Windows.Visibility.Visible;
            RecconfirmStack.Visibility = System.Windows.Visibility.Visible;
            ComponentsB.IsEnabled = false;
        }
        public bool verifier_vide()
        {
            if ((diagno.Text == "")) return true;
            else return false;
        }


        public String calcul_age(DateTime date_nais)
        {
            if (DateTime.Now.Year - date_nais.Year > 0) return (DateTime.Now.Year - date_nais.Year + " ans");
            else return (DateTime.Now.Month - date_nais.Month + " mois");
        }
        private void Lier()
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String command = "INSERT INTO Contient values (" + this.id_dm + "," + this.id_fc + ")";
            SqlCommand Macmd = new SqlCommand(command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            Datab.deconnecter();
        }


        private void buttoui_Click(object sender, RoutedEventArgs e)
        {
            App.acc = new Nv_acceuil(idmedd, true); //A revoir 
            App.acc.Show();
            App.Nfc.Close();
        }

        private void buttnon_Click(object sender, RoutedEventArgs e)
        {
            confirmStack.Visibility = System.Windows.Visibility.Hidden;
            RecconfirmStack.Visibility = System.Windows.Visibility.Hidden;
            ComponentsB.IsEnabled = true;
        }

        private void buttannuler_Click(object sender, RoutedEventArgs e)
        {
            confirmStack.Visibility = System.Windows.Visibility.Visible;
            RecconfirmStack.Visibility = System.Windows.Visibility.Visible;
            ComponentsB.IsEnabled = false;
        }

        private void buttenreg_Click(object sender, RoutedEventArgs e)
        {
            if (verifier_vide())
            {
                Stackriensaisi.Visibility = System.Windows.Visibility.Visible;
                RecStackriensaisi.Visibility = System.Windows.Visibility.Visible;
                ComponentsB.IsEnabled = false;
            }

            else
            {
                Fiche_Consultation fiche = new Fiche_Consultation(DateTime.Now, diagno.Text, this.idmedd);
                fiche.Insert_Fiche_Consultation();
                this.id_fc = fiche.Get_Id();
                Lier();
                App.iff = new InterfaceFonctionnalité(idpat, idmedd, id_fc, id_dm, ADMINN, true);
                App.iff.Show();
                App.Nfc.Close();
                //************** accueil jdiiiiiida *****************
            }
        }

        private void buttok_Click(object sender, RoutedEventArgs e)
        {

            Stackriensaisi.Visibility = System.Windows.Visibility.Hidden;
            RecStackriensaisi.Visibility = System.Windows.Visibility.Hidden;
            ComponentsB.IsEnabled = true;
        }
    }
}
