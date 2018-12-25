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
    /// Interaction logic for PagePatients.xaml
    /// </summary>
    public partial class PagePatients : Page
    {
        public struct DataofPatient
        {
            public int numero { get; set; }
            public string nom { get; set; }
            public string prenom { get; set; }
            public DateTime date { get; set; }
            public string adr { get; set; }
            public string grp { get; set; }
            public string num { get; set; }
        }
        private DataTable DATBL { get; set; }
        private int idpatient { get; set; }
        private int idmed { get; set; }
        private bool ADMIN { get; set; }
        private Key dernier { get; set; }
        private Key avantdernier { get; set; }
        //Data Consult
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
        public PagePatients(int id, bool admin)
        {
            InitializeComponent();
            idmed = id; ADMIN = admin;
            dernier = avantdernier = Key.None;
            Grid_Nvl_Consultation.Visibility = System.Windows.Visibility.Hidden;
            Loadpatient();
        }
        private void patientGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatagGrid.SelectedIndex > -1)
            {
                DataofPatient dofpatient = (DataofPatient)(DatagGrid.SelectedItem);
                this.idpatient = dofpatient.numero;
                buttonConsulter.IsEnabled = true;
                buttonDelpat.IsEnabled = true;
                this.Afficher.IsEnabled = true;
            }
        }

        private void patientName_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
            DataTable tmp = DATBL.Copy();
            DataRow row;
            int i;
            if (patientName.Text != "")
            {
                if (patientPrenom.Text != "")
                {
                    //Rechercher par Nom et Prenom
                    for (i = DATBL.Rows.Count - 1; i >= 0; i--)
                    {
                        row = tmp.Rows[i];
                        if (row[0].ToString().ToUpper().StartsWith(patientName.Text.ToUpper()) && row[1].ToString().ToUpper().StartsWith(patientPrenom.Text.ToUpper())) { }
                        else
                        {
                            tmp.Rows.RemoveAt(i);
                        }
                    }
                    Rempliravec(tmp);
                }
                else
                {
                    //Rechercher par Nom
                    for (i = DATBL.Rows.Count - 1; i >= 0; i--)
                    {
                        row = tmp.Rows[i];
                        if (row[0].ToString().ToUpper().StartsWith(patientName.Text.ToUpper())) { }
                        else
                        {
                            tmp.Rows.RemoveAt(i);
                        }
                    }
                    Rempliravec(tmp);
                }
            }
            else
            {
                if (patientPrenom.Text != "")
                {
                    //Recherch par Prenom
                    for (i = DATBL.Rows.Count - 1; i >= 0; i--)
                    {
                        row = tmp.Rows[i];
                        if (row[1].ToString().ToUpper().StartsWith(patientPrenom.Text.ToUpper())) { }
                        else
                        {
                            tmp.Rows.RemoveAt(i);
                        }
                    }
                    Rempliravec(tmp);
                }
                else
                {
                    //Affichage de tous les patients
                    Rempliravec(DATBL);
                }
            }
        }
        private void Loadpatient()
        {
            DATBL = new DataTable();
            DataGridTextColumn g0 = new DataGridTextColumn(); g0.Binding = new Binding("nom"); g0.Header = "Nom";
            DataGridTextColumn g1 = new DataGridTextColumn(); g1.Binding = new Binding("prenom"); g1.Header = "Prenom";
            DataGridTextColumn g2 = new DataGridTextColumn(); g2.Binding = new Binding("date"); g2.Header = "Date de naissance"; g2.MaxWidth = 150;
            DataGridTextColumn g3 = new DataGridTextColumn(); g3.Binding = new Binding("adr"); g3.Header = "Adresse"; g3.MaxWidth = 150;
            DataGridTextColumn g4 = new DataGridTextColumn(); g4.Binding = new Binding("grp"); g4.Header = "Groupe sanguin";
            DataGridTextColumn g5 = new DataGridTextColumn(); g5.Binding = new Binding("num"); g5.Header = "Numero Telephone";
            DataGridTextColumn g6 = new DataGridTextColumn(); g6.Binding = new Binding("numero"); g6.Header = "Référence Patient"; g6.MaxWidth = 0;
            DatagGrid.Columns.Add(g0); DatagGrid.Columns.Add(g1); DatagGrid.Columns.Add(g2);
            DatagGrid.Columns.Add(g3); DatagGrid.Columns.Add(g4); DatagGrid.Columns.Add(g5); DatagGrid.Columns.Add(g6);
            g2.ClipboardContentBinding.StringFormat = "d";
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = @"select Nom,Prenom,Date_de_naissance,Adresse,Groupe_sanguin,Num_Tel";
            Command += @",Id_Patient from Patient LEFT OUTER JOIN Person on Person.Id_Person=Patient.Id_Person order by Nom";
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            SqlDataReader dr = Macmd.ExecuteReader();
            while (dr.Read())
            {
                DataofPatient dofpatient = new DataofPatient();
                dofpatient.nom = dr[0].ToString(); dofpatient.prenom = dr[1].ToString(); dofpatient.date = DateTime.Parse(dr[2].ToString());
                dofpatient.adr = dr[3].ToString(); dofpatient.grp = dr[4].ToString(); dofpatient.num = dr[5].ToString();
                dofpatient.numero = int.Parse(dr[6].ToString());
                DatagGrid.Items.Add(dofpatient);
            }
            dr.Close();
            SqlDataAdapter DAPTR = new SqlDataAdapter(Macmd);
            DATBL = new DataTable();
            DAPTR.Fill(DATBL);
            Datab.deconnecter();
        }

        private void buttonConsulter_Click(object sender, RoutedEventArgs e)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = "SELECT * from Dossier_medical where Id_Patient=" + this.idpatient;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            SqlDataReader dr = Macmd.ExecuteReader();
            if (dr.Read())
            {
                int iddoss = (int)dr["Id_Dossier"];
                Initialiser(iddoss, idpatient, idmed, ADMIN);
                Grid_Nvl_Consultation.Visibility = Visibility.Visible;
                GridPatient.IsEnabled = false;
                //if (App.Nfc != null) App.Nfc.Close();
                //App.Nfc = new NvConsultation(iddoss, idpatient, idmed, ADMIN);
                //App.Nfc.Show();
            }
        }
        private void buttonNvpat_Click(object sender, RoutedEventArgs e)
        {
            int timestamp = new TimeSpan(DateTime.Now.Ticks).Seconds;
            var mouseevent = new MouseButtonEventArgs(Mouse.PrimaryDevice, timestamp, MouseButton.Left)
            {
                RoutedEvent = UIElement.MouseLeftButtonUpEvent,
                Source = App.acc.nv_pat_item
            };
            App.acc.nv_pat_item.RaiseEvent(mouseevent);
        }

        private void buttonDelpat_Click(object sender, RoutedEventArgs e)
        {
            ////***********************************************************
            //int id_doss = -1, id_prs = -1;
            //ConnexionBDD Datab = new ConnexionBDD();
            //Datab.connecter();
            //String command = "SELECT Id_Dossier FROM Dossier_medical WHERE Id_Patient=" + idpatient;
            //SqlCommand Macmd = new SqlCommand(command, Datab.cnx);
            //SqlDataReader dr = Macmd.ExecuteReader();
            //if (dr.Read())
            //{
            //    id_doss = (int)dr["Id_Dossier"];
            //}
            //dr.Close();
            //command = "SELECT Id_Person FROM Patient WHERE Id_patient=" + idpatient;
            //Macmd = new SqlCommand(command, Datab.cnx);
            //dr = Macmd.ExecuteReader();
            //if (dr.Read())
            //{
            //    id_prs = (int)dr["Id_Person"];
            //}

            //dr.Close();
            //command = "DELETE FROM Certeficat_medical WHERE Id_FC in (SELECT Id_FC FROM Contient WHERE Id_DM =" + id_doss + ")";
            //Macmd = new SqlCommand(command, Datab.cnx);
            //Macmd.ExecuteNonQuery();
            //command = "DELETE FROM Prescrire WHERE Id_Ordonnance in (SELECT Id_Ordonnance FROM Ordonnance WHERE Id_FC in (SELECT Id_FC FROM Contient WHERE Id_DM =" + id_doss + "))";
            //Macmd = new SqlCommand(command, Datab.cnx);
            //Macmd.ExecuteNonQuery();
            //command = "DELETE FROM Ordonnance WHERE Id_FC in (SELECT Id_FC FROM Contient WHERE Id_DM =" + id_doss + ")";
            //Macmd = new SqlCommand(command, Datab.cnx);
            //Macmd.ExecuteNonQuery();
            //command = "DELETE FROM Contient WHERE Id_DM=" + id_doss;
            //Macmd = new SqlCommand(command, Datab.cnx);
            //Macmd.ExecuteNonQuery();
            //command = "DELETE FROM Fiche_Consultation WHERE Id_Fiche_Consultation not in (SELECT Contient.Id_FC FROM Contient)";
            //Macmd = new SqlCommand(command, Datab.cnx);
            //Macmd.ExecuteNonQuery();
            //command = "DELETE FROM Lettre WHERE Id_DM =" + id_doss;
            //Macmd = new SqlCommand(command, Datab.cnx);
            //Macmd.ExecuteNonQuery();
            //command = "DELETE FROM Examine_Comp WHERE Id_DM =" + id_doss;
            //Macmd = new SqlCommand(command, Datab.cnx);
            //Macmd.ExecuteNonQuery();
            //command = "DELETE FROM Examen_Complementaire WHERE Id_Ex_Comp not in (SELECT Id_DM From Examine_Comp)";
            //Macmd = new SqlCommand(command, Datab.cnx);
            //Macmd.ExecuteNonQuery();
            //command = "DELETE FROM Avoir_RDV WHERE Id_RDV in(SELECT Id_RDV FROM [Rendez-vous] WHERE Id_Patient=" + idpatient + ")";
            //Macmd = new SqlCommand(command, Datab.cnx);
            //Macmd.ExecuteNonQuery();
            //command = "DELETE FROM [Rendez-vous] WHERE Id_Patient=" + idpatient;
            //Macmd = new SqlCommand(command, Datab.cnx);
            //Macmd.ExecuteNonQuery();
            //command = "DELETE FROM Dossier_medical WHERE Id_Patient=" + idpatient;
            //Macmd = new SqlCommand(command, Datab.cnx);
            //Macmd.ExecuteNonQuery();
            //command = "DELETE FROM Patient WHERE Id_Patient=" + idpatient;
            //Macmd = new SqlCommand(command, Datab.cnx);
            //Macmd.ExecuteNonQuery();
            //command = "DELETE FROM Person WHERE Id_Person=" + id_prs;
            //Macmd = new SqlCommand(command, Datab.cnx);
            //Macmd.ExecuteNonQuery();
            //bool del = false; int i = 0;
            //while (!del && i < DatagGrid.Items.Count)
            //{
            //    DataofPatient da = (DataofPatient)(DatagGrid.Items[i]);
            //    if (da.numero == this.idpatient)
            //    {
            //        DatagGrid.Items.RemoveAt(i);
            //        del = true;
            //    }
            //    i++;
            //}
            //this.buttonDelpat.IsEnabled = false;
            //this.buttonConsulter.IsEnabled = false;
            //this.Afficher.IsEnabled = false;
            //********************************************************************
            Stackconfirmsupp.Visibility = System.Windows.Visibility.Visible;
            RecStackconfirmsupp.Visibility = System.Windows.Visibility.Visible;
            grdpatt.IsEnabled = false;
        }
        private void patientGrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) buttonConsulter_Click(sender, new RoutedEventArgs());
            if (e.Key == Key.Delete) buttonDelpat_Click(sender, new RoutedEventArgs());
        }
        private void Rempliravec(DataTable tmp)
        {
            for (int i = DatagGrid.Items.Count - 1; i >= 0; i--)
            {
                DatagGrid.Items.RemoveAt(i);
            }
            for (int i = tmp.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = tmp.Rows[i];
                DataofPatient dofpatient = new DataofPatient();
                dofpatient.nom = dr[0].ToString(); dofpatient.prenom = dr[1].ToString(); dofpatient.date = DateTime.Parse(dr[2].ToString());
                dofpatient.adr = dr[3].ToString(); dofpatient.grp = dr[4].ToString(); dofpatient.num = dr[5].ToString();
                dofpatient.numero = int.Parse(dr[6].ToString());
                DatagGrid.Items.Add(dofpatient);
            }
        }
        public void addtoDatagGrid(string nom, string prenom, DateTime date, string adresse, string grp, string numt, int id)
        {
            DataofPatient dofpatient = new DataofPatient();
            dofpatient.nom = nom; dofpatient.prenom = prenom; dofpatient.date = date;
            dofpatient.adr = adresse; dofpatient.grp = grp; dofpatient.num = numt;
            dofpatient.numero = id;
            DatagGrid.Items.Add(dofpatient);
            ConnexionBDD Datab = new ConnexionBDD(); Datab.connecter();
            String Command = @"select Nom,Prenom,Date_de_naissance,Adresse,Groupe_sanguin,Num_Tel";
            Command += @",Id_Patient from Patient LEFT OUTER JOIN Person on Person.Id_Person=Patient.Id_Person order by Nom";
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx); SqlDataAdapter DAPTR = new SqlDataAdapter(Macmd); DATBL = new DataTable();
            DAPTR.Fill(DATBL);Datab.deconnecter();
        }

        private void Afficher_Click(object sender, RoutedEventArgs e)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = "SELECT * from Dossier_medical where Id_Patient=" + this.idpatient;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            SqlDataReader dr = Macmd.ExecuteReader();
            if (dr.Read())
            {
                int iddoss = (int)dr["Id_Dossier"];
                App.iff = new InterfaceFonctionnalité(idpatient, idmed, 0, iddoss, ADMIN, false);
                App.iff.Show();
                App.acc.killTimer();
                App.acc.Close();
            }
        }

        private void buttouisupp_Click(object sender, RoutedEventArgs e)
        {
            int id_doss = -1, id_prs = -1;
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String command = "SELECT Id_Dossier FROM Dossier_medical WHERE Id_Patient=" + idpatient;
            SqlCommand Macmd = new SqlCommand(command, Datab.cnx);
            SqlDataReader dr = Macmd.ExecuteReader();
            if (dr.Read())
            {
                id_doss = (int)dr["Id_Dossier"];
            }
            dr.Close();
            command = "SELECT Id_Person FROM Patient WHERE Id_patient=" + idpatient;
            Macmd = new SqlCommand(command, Datab.cnx);
            dr = Macmd.ExecuteReader();
            if (dr.Read())
            {
                id_prs = (int)dr["Id_Person"];
            }

            dr.Close();
            command = "DELETE FROM Certeficat_medical WHERE Id_FC in (SELECT Id_FC FROM Contient WHERE Id_DM =" + id_doss + ")";
            Macmd = new SqlCommand(command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            command = "DELETE FROM Prescrire WHERE Id_Ordonnance in (SELECT Id_Ordonnance FROM Ordonnance WHERE Id_FC in (SELECT Id_FC FROM Contient WHERE Id_DM =" + id_doss + "))";
            Macmd = new SqlCommand(command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            command = "DELETE FROM Ordonnance WHERE Id_FC in (SELECT Id_FC FROM Contient WHERE Id_DM =" + id_doss + ")";
            Macmd = new SqlCommand(command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            command = "DELETE FROM Contient WHERE Id_DM=" + id_doss;
            Macmd = new SqlCommand(command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            command = "DELETE FROM Fiche_Consultation WHERE Id_Fiche_Consultation not in (SELECT Contient.Id_FC FROM Contient)";
            Macmd = new SqlCommand(command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            command = "DELETE FROM Lettre WHERE Id_DM =" + id_doss;
            Macmd = new SqlCommand(command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            command = "DELETE FROM Examine_Comp WHERE Id_DM =" + id_doss;
            Macmd = new SqlCommand(command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            command = "DELETE FROM Examen_Complementaire WHERE Id_Ex_Comp not in (SELECT Id_DM From Examine_Comp)";
            Macmd = new SqlCommand(command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            command = "DELETE FROM Avoir_RDV WHERE Id_RDV in(SELECT Id_RDV FROM [Rendez-vous] WHERE Id_Patient=" + idpatient + ")";
            Macmd = new SqlCommand(command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            command = "DELETE FROM [Rendez-vous] WHERE Id_Patient=" + idpatient;
            Macmd = new SqlCommand(command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            command = "DELETE FROM Dossier_medical WHERE Id_Patient=" + idpatient;
            Macmd = new SqlCommand(command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            command = "DELETE FROM Patient WHERE Id_Patient=" + idpatient;
            Macmd = new SqlCommand(command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            command = "DELETE FROM Person WHERE Id_Person=" + id_prs;
            Macmd = new SqlCommand(command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            bool del = false; int i = 0;
            while (!del && i < DatagGrid.Items.Count)
            {
                DataofPatient da = (DataofPatient)(DatagGrid.Items[i]);
                if (da.numero == this.idpatient)
                {
                    DatagGrid.Items.RemoveAt(i);
                    del = true;
                }
                i++;
            }
            Stackconfirmsupp.Visibility = System.Windows.Visibility.Hidden;
            RecStackconfirmsupp.Visibility = System.Windows.Visibility.Hidden;
            grdpatt.IsEnabled = true;


        }

        private void buttnonsupp_Click(object sender, RoutedEventArgs e)
        {
            Stackconfirmsupp.Visibility = System.Windows.Visibility.Hidden;
            RecStackconfirmsupp.Visibility = System.Windows.Visibility.Hidden;
            grdpatt.IsEnabled = true ;
        }
        //Code Of "Consultation"
        private void Initialiser(int id_dm, int Idpat, int Idmed, bool admin)
        {
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
            confirmStack.Visibility = System.Windows.Visibility.Hidden;
            RecconfirmStack.Visibility = System.Windows.Visibility.Hidden;
            ComponentsB.IsEnabled = true;
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
            GridPatient.IsEnabled = true;
            Grid_Nvl_Consultation.Visibility = Visibility.Hidden;
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
                App.acc.killTimer();
                App.acc.Close();
                App.iff.Show();
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
