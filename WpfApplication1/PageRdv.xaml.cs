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
    /// Interaction logic for PageRdv.xaml
    /// </summary>
    public partial class PageRdv : Page
    {
        public bool vrf;
        private Rendez_Vous Rdv;
        private DateTime Dte_rdv;
        private int heur_rdv;
        private int min_rdv;
        private String lieu_rdv;
        private String cmnt_rdv;
        private int important;
        private int idd_pat = new int();
        private bool count;
        private bool boool;
        private bool bl;
        private int id_r;
        //************************************
        private DataTable DATB { get; set; }
        private int idpatient { get; set; }
        private int idmed { get; set; }
        private String nom_p { get; set; }
        private String prenom_p { get; set; }
        //************************************
        public struct DataOfRdv
        {
            public int numero;
            public DateTime date { get; set; }
            public string important { get; set; }
            public string lieu { get; set; }
            public string commentaire { get; set; }
            public string nom { get; set; }
            public BitmapImage img { get; set; }
        }
        private bool existNom { get; set; }
        private bool existPrenom { get; set; }
        DataRow row;
        private List<int> List_p = new List<int>();
        private int Idmedc { get; set; }
        private String nom_patt;
        private String prenom_patt;
        public PageRdv(int idmed)
        {
            InitializeComponent();
            existNom = existPrenom = false;
            Idmedc = idmed;
            init_combo();
            Load_Rdv();
            vrf = true;
            Loadpatient();
        }
        public void Set_idp(int id)
        {
            this.idd_pat = id;
        }
        public void Set_nom_prenom(String np, String pp)
        {
            textbox_name_pat.Text = np + " " + pp;
        }
        private void annuler_but_Click(object sender, RoutedEventArgs e)
        {
            vider();

        }
        private void vider()
        {
            heur_combo.Text = ""; min_combo.Text = ""; lieu_textbox.Clear(); cmnt_textbox.Clear(); Daterdv_Datepicker.Text = null;
            import_checkbox.IsChecked = false; 
              Rdv_perso_checkbox.IsChecked = false; textbox_name_pat.Clear();
              valider_but.IsEnabled = true;
              annuler_but.IsEnabled = true;
              modifier_but.IsEnabled = false;
        }
        private void Rdv_perso_checkbox_Checked(object sender, RoutedEventArgs e)
        {
            if (Rdv_perso_checkbox.IsChecked == true)
            {
                patient_selec_lab.Visibility = System.Windows.Visibility.Hidden;
                textbox_name_pat.Visibility = System.Windows.Visibility.Hidden;
                recherche_but.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void Rdv_perso_checkbox_Unchecked(object sender, RoutedEventArgs e)
        {

            patient_selec_lab.Visibility = System.Windows.Visibility.Visible;
            textbox_name_pat.Visibility = System.Windows.Visibility.Visible;
            recherche_but.Visibility = System.Windows.Visibility.Visible;
        }

        private void valider_but_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
                if ((Rdv_perso_checkbox.IsChecked == true) || textbox_name_pat.Text != "")
                {
                    update_table_rdv();
                    int idrdv = -1;
                    add_rdv(ref idrdv);
                    if (Rdv_perso_checkbox.IsChecked == false)
                    {
                        Rdv.update_Id_pat(idd_pat);
                    }
                    if (!count && boool)
                    {

                        Stacksuccès.Visibility = System.Windows.Visibility.Visible;
                        RecStacksuccès.Visibility = System.Windows.Visibility.Visible;
                        RdvGrd.IsEnabled = false;
                        if (this.important == 1)
                        {
                            Notification notif = new Notification(Dte_rdv, this.lieu_rdv, this.cmnt_rdv, this.nom_patt + " " + this.prenom_patt, Rdv.get_Id_RDV());
                            App.acc.lisdesnotif.Add(notif);
                        }
                        DateTime dt = DateTime.Parse(Daterdv_Datepicker.Text);
                        DateTime dtt = new DateTime(dt.Year, dt.Month, dt.Day, int.Parse(heur_combo.Text), int.Parse(min_combo.Text), 0);
                        this.Ajouter_DataGrid(idrdv, dtt.ToString(), this.important, this.lieu_textbox.Text, this.cmnt_textbox.Text,
                            textbox_name_pat.Text);
                        vider();
                    }
                }
                else
                {
                    SelectPat.Visibility = System.Windows.Visibility.Visible;
                    RecSelectPat.Visibility = System.Windows.Visibility.Visible;
                    RdvGrd.IsEnabled = false;
                }
            //}
            //catch (Exception exc) { }
        }
        private void add_rdv(ref int idrdv)
        {
            verifier(ref boool);
            idrdv = -1;
            if (boool)
            {
                this.Dte_rdv = DateTime.Parse(Daterdv_Datepicker.Text);
                this.heur_rdv = int.Parse(heur_combo.Text);
                this.min_rdv = int.Parse(min_combo.Text);
                DateTime abc = new DateTime(Dte_rdv.Year, Dte_rdv.Month, Dte_rdv.Day, heur_rdv, min_rdv, 00);
                Dte_rdv = abc;
                this.lieu_rdv = lieu_textbox.Text;
                this.cmnt_rdv = cmnt_textbox.Text;
                if (import_checkbox.IsChecked == true) this.important = 1;
                else this.important = 0;
                Rdv = new Rendez_Vous(abc, important, cmnt_rdv, lieu_rdv);
                RdvAsuppr rdvasup = new RdvAsuppr(Rdv.get_Id_RDV(),Dte_rdv);
                App.acc.listasupp.Add(rdvasup);
                recherche_rdv(ref count);
                if (!count)
                {
                    Rdv.Ajouter_RDV();
                    Insert_av_rdv();
                    idrdv = Rdv.get_Id_RDV();
                }
                else if (count)
                {
                    rdv_exst.Visibility = System.Windows.Visibility.Visible;
                    Recrdv_exst.Visibility = System.Windows.Visibility.Visible;
                    RdvGrd.IsEnabled = false;
                    heur_combo.Text = null;
                    min_combo.Text = null;
                    Daterdv_Datepicker.Text = null;
                }
            }
            else
            {
                Stacknonremp.Visibility = System.Windows.Visibility.Visible;
                RecStacknonremp.Visibility = System.Windows.Visibility.Visible;
                RdvGrd.IsEnabled = false ;
            }

        }
        private void recherche_rdv(ref bool cpt)
        {
            cpt = false;
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            SqlCommand cmd = new SqlCommand("SP_Rech_RDV", Datab.cnx);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param1;
            param1 = cmd.Parameters.Add("@d_r", SqlDbType.DateTime);
            param1.Value = Dte_rdv;
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (this.Idmedc == (int)dr["Id_Doctor"]) cpt = true;
            }

        }
        public void Load_Rdv()
        {
            DataGridTextColumn g0 = new DataGridTextColumn(); g0.Binding = new Binding("numero"); g0.Header = ""; g0.MaxWidth = 0;
            DataGridTextColumn g1 = new DataGridTextColumn(); g1.Binding = new Binding("date"); g1.Header = "Date"; g1.MaxWidth = 120; g1.MinWidth = 120;
            DataGridTextColumn g2 = new DataGridTextColumn(); g2.Binding = new Binding("important"); g2.Header = "A notifier"; g2.MaxWidth = 0;
            DataGridTextColumn g3 = new DataGridTextColumn(); g3.Binding = new Binding("lieu"); g3.Header = "Lieu";
            DataGridTextColumn g4 = new DataGridTextColumn(); g4.Binding = new Binding("commentaire"); g4.Header = "Commentaire";
            DataGridTextColumn g5 = new DataGridTextColumn(); g5.Binding = new Binding("nom"); g5.Header = "Nom Patient";
            Rdv_DataGrid.Columns.Add(g0); Rdv_DataGrid.Columns.Add(g1); Rdv_DataGrid.Columns.Add(g2);
            Rdv_DataGrid.Columns.Add(g3); Rdv_DataGrid.Columns.Add(g4); Rdv_DataGrid.Columns.Add(g5); 
            g1.ClipboardContentBinding.StringFormat = @"dd/MM/yyyy HH:mm";
            Rdv_DataGrid.Columns[0].DisplayIndex = 7;
            Rdv_DataGrid.Columns[1].DisplayIndex = 6;
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            string Command = @"select Id_RDV,Date_Rdv,Important,Lieu,Commentaire,Nom,Prenom from [Rendez-vous] LEFT OUTER JOIN Patient";
            Command += @" on [Rendez-vous].Id_Patient=Patient.Id_Patient LEFT OUTER JOIN Person on Patient.Id_Person=Person.Id_Person";
            Command += @" where Id_Rdv in ( SELECT Id_RDV FROM Avoir_RDV WHERE Id_Doctor=" + this.Idmedc + ") order by [Rendez-vous].Date_Rdv";
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            SqlDataReader dr = Macmd.ExecuteReader();
            while (dr.Read())
            {
                this.Ajouter_DataGrid(int.Parse(dr[0].ToString()), dr[1].ToString(), int.Parse(dr[2].ToString()), dr[3].ToString(),
                    dr[4].ToString(), dr[5].ToString() +" "+ dr[6].ToString());
            }
            dr.Close();
            Datab.deconnecter();
        }
        public void Ajouter_DataGrid(int id, string date, int imp, string lieu, string comm, string nom)
        {
            DataOfRdv RDVV = new DataOfRdv();
            RDVV.numero = id; RDVV.date = DateTime.Parse(date); RDVV.lieu = lieu; RDVV.commentaire = comm;
            RDVV.nom = nom;
            if (imp == 1)
            {
                RDVV.important = "Oui";
                RDVV.img = Animations.GetImage(@"Images\star_Light.png");
            }
            else
            {
                RDVV.important = "Non";
                RDVV.img = Animations.GetImage(@"Images\star_noLight.png");
            }
            Rdv_DataGrid.Items.Add(RDVV);

        }
        //public void load_tab_grid()
        //{
        //    update_table_rdv();
        //    int i, id_perso;
        //    String nom_pp;
        //    String prenom_pp;

        //    ConnexionBDD Datab = new ConnexionBDD();
        //    Datab.connecter();
        //    String Command = "SELECT Date_Rdv AS Date,Lieu,Important,Commentaire,Id_Patient AS Num_Patient From [Rendez-vous] WHERE Id_Rdv in ( SELECT Id_RDV FROM Avoir_RDV WHERE Id_Doctor=" + this.Idmedc + ") order by [Rendez-vous].Date_Rdv";
        //    SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
        //    Macmd.ExecuteNonQuery();
        //    SqlDataAdapter DAPTR = new SqlDataAdapter(Macmd);
        //    DataTable DATBL = new DataTable("Rendez-vous");
        //    DAPTR.Fill(DATBL);
        //    for (i = DATBL.Rows.Count - 1; i >= 0; i--) DATBL.Rows.RemoveAt(i);
        //    DATBL.Columns.Add("Nom Patient");
        //    DATBL.Columns.Add("Prénom Patient");
        //    DAPTR.Fill(DATBL);
        //    Rdv_DataGrid.ItemsSource = DATBL.DefaultView;
        //    DAPTR.Update(DATBL);
        //    SqlParameter param1, param2;
        //    SqlDataReader dr;
        //    Datab.deconnecter();
        //    Datab.connecter();
        //    for (i = 0; i < DATBL.Rows.Count; i++)
        //    {
        //        row = DATBL.Rows[i];
        //        if (row["Num_Patient"].ToString() != "")
        //        {
        //            SqlCommand cmd1 = new SqlCommand("SP_Id_Pers", Datab.cnx);
        //            cmd1.CommandType = CommandType.StoredProcedure;
        //            param1 = cmd1.Parameters.Add("@id_pat", SqlDbType.Int);
        //            param1.Value = row["Num_Patient"];
        //            dr = cmd1.ExecuteReader();
        //            id_perso = -1;
        //            if (dr.Read())
        //            {
        //                id_perso = (int)dr["Id_Person"];
        //            }
        //            SqlCommand cmd2 = new SqlCommand("SP_Nom_Prenom_Patient", Datab.cnx);
        //            cmd2.CommandType = CommandType.StoredProcedure;
        //            param2 = cmd2.Parameters.Add("@id_pers", SqlDbType.Int);
        //            param2.Value = id_perso;
        //            dr.Close();
        //            dr = cmd2.ExecuteReader();

        //            nom_pp = "";
        //            prenom_pp = "";
        //            if (dr.Read())
        //            {
        //                nom_pp = dr["Nom"].ToString();
        //                prenom_pp = dr["Prenom"].ToString();
        //            }
        //            row["Nom Patient"] = nom_pp;
        //            row["Prénom Patient"] = prenom_pp;
        //            dr.Close();
        //        }

        //    }


        //}

        private void recherche_but_Click(object sender, RoutedEventArgs e)
        {
            Stackrechpat.Visibility = System.Windows.Visibility.Visible;
            RecStackrechpat.Visibility = System.Windows.Visibility.Visible;
            //App.inter_pat_rdv = new Interface_rech_pat_rdv();
            //App.inter_pat_rdv.Show();
        }
        private void verifier(ref bool verif)
        {
            verif = true;
            if (Daterdv_Datepicker.Text == "" || heur_combo.Text == ""|| min_combo.Text=="" || lieu_textbox.Text == "" || cmnt_textbox.Text == "")
            {
                verif = false;
            }
            if (Rdv_perso_checkbox.IsChecked == false)
            {
            }
        }
        private void init_combo()
        {
            int i;
            for (i = 0; i < 24; i++)
            {
                if (i < 10) heur_combo.Items.Add("0" + i);
                else heur_combo.Items.Add(i);
            }
            for (i = 0; i < 60; i++)
            {
                if (i < 10) min_combo.Items.Add("0" + i);
                else min_combo.Items.Add(i);
            }
        }
        private void modifier_but_Click(object sender, RoutedEventArgs e)
        {
            //ConnexionBDD Datab = new ConnexionBDD();
            //Datab.connecter();
            //Dte_rdv = DateTime.Parse(Daterdv_Datepicker.Text);
            //heur_rdv = int.Parse(heur_combo.Text);
            //min_rdv = int.Parse(min_combo.Text);
            //DateTime D = new DateTime(Dte_rdv.Year, Dte_rdv.Month, Dte_rdv.Day, heur_rdv, min_rdv, 00);
            //lieu_rdv = lieu_textbox.Text;
            //cmnt_rdv = cmnt_textbox.Text;
            //if (import_checkbox.IsChecked == true) important = 1;
            //else important = 0;
            //if (bl == true) idd_pat = int.Parse(Id_box.Text);
            //SqlCommand cmd = new SqlCommand("SP_Update_RDV", Datab.cnx);
            //cmd.CommandType = CommandType.StoredProcedure;
            //SqlParameter param1;
            //param1 = cmd.Parameters.Add("@dte", SqlDbType.DateTime);
            //param1.Value = D;
            //SqlParameter param2;
            //param2 = cmd.Parameters.Add("@imp", SqlDbType.Int);
            //param2.Value = important;
            //SqlParameter param3;
            //param3 = cmd.Parameters.Add("@cmnt", SqlDbType.NVarChar, 1000);
            //param3.Value = cmnt_rdv;
            //SqlParameter param4;
            //param4 = cmd.Parameters.Add("@lieu", SqlDbType.NVarChar, 50);
            //param4.Value = lieu_rdv;
            //SqlParameter param5;
            //param5 = cmd.Parameters.Add("@id_rd", SqlDbType.Int);
            //param5.Value = id_r;
            //cmd.ExecuteNonQuery();
            //if (bl == true)
            //{
            //    string Command = @"Update [Rendez-vous] SET Id_Patient = " + idd_pat + " WHERE Id_RDV=" + id_r;
            //    SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            //    Macmd.ExecuteNonQuery();
            //}
            //vider();
            //load_tab_grid();
            int index = Rdv_DataGrid.SelectedIndex;
            if (Rdv_DataGrid.SelectedIndex > -1)
            {
                DataOfRdv datardv = (DataOfRdv)(Rdv_DataGrid.SelectedItem);
                Rdv_DataGrid.Items.RemoveAt(index);
                DateTime dt = DateTime.Parse(Daterdv_Datepicker.Text);
                datardv.date = new DateTime(dt.Year, dt.Month, dt.Day, int.Parse(heur_combo.Text), int.Parse(min_combo.Text), 0);
                if (import_checkbox.IsChecked == true)
                {
                    datardv.important = "Oui";
                    datardv.img = Animations.GetImage(@"Images\star_Light.png");
                }
                else
                {
                    datardv.important = "Non";
                    datardv.img = Animations.GetImage(@"Images\star_noLight.png");
                }
                datardv.lieu = lieu_textbox.Text;
                datardv.commentaire = cmnt_textbox.Text;
                datardv.nom = textbox_name_pat.Text;
                Rdv_DataGrid.Items.Insert(index, datardv);
                ConnexionBDD Datab = new ConnexionBDD();
                Datab.connecter();
                SqlCommand cmd = new SqlCommand("SP_Update_RDV", Datab.cnx);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param1;
                param1 = cmd.Parameters.Add("@dte", SqlDbType.DateTime);
                param1.Value = datardv.date;
                SqlParameter param2;
                param2 = cmd.Parameters.Add("@imp", SqlDbType.Int);
                if(datardv.important=="Oui") param2.Value =1 ;
                else param2.Value = 0;
                SqlParameter param3;
                param3 = cmd.Parameters.Add("@cmnt", SqlDbType.NVarChar, 1000);
                param3.Value = datardv.commentaire;
                SqlParameter param4;
                param4 = cmd.Parameters.Add("@lieu", SqlDbType.NVarChar, 50);
                param4.Value = datardv.lieu;
                SqlParameter param5;
                param5 = cmd.Parameters.Add("@id_rd", SqlDbType.Int);
                param5.Value = datardv.numero;
                cmd.ExecuteNonQuery();
                valider_but.IsEnabled = true;
                annuler_but.IsEnabled = true;
                modifier_but.IsEnabled = false;
                this.vider();
            }
        }
        private void Rdv_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Rdv_DataGrid.SelectedIndex > -1)
                {
                    modifier_but.IsEnabled = true;
                    annuler_but.IsEnabled = false;
                    valider_but.IsEnabled = false;
                    DataOfRdv datardv = (DataOfRdv)(Rdv_DataGrid.SelectedItem);
                    Daterdv_Datepicker.Text = datardv.date.ToString().Remove(10);
                    if (datardv.date.Hour.ToString().Length == 1) heur_combo.Text = "0" + datardv.date.Hour;
                    else heur_combo.Text = datardv.date.Hour.ToString();
                    if (datardv.date.Minute.ToString().Length==1) min_combo.Text = "0"+datardv.date.Minute;
                    else min_combo.Text = datardv.date.Minute.ToString();
                    if (datardv.important == "Oui") import_checkbox.IsChecked = true;
                    else import_checkbox.IsChecked = false;
                    lieu_textbox.Text = datardv.lieu;
                    cmnt_textbox.Text = datardv.commentaire;
                    if (datardv.nom == " " )
                    {
                        Rdv_perso_checkbox.IsChecked = true;
                        bl = true;   
                    }
                    else
                    {
                        Rdv_perso_checkbox.IsChecked = false;
                        textbox_name_pat.Text = datardv.nom;
                    }
                }
            }
            catch (Exception exc) { }
            //try
            //{
            //    bl = false;
            //    DataGrid Dg = sender as DataGrid;
            //    DataRowView Drv = Dg.SelectedItem as DataRowView;
            //    if (Drv != null)
            //    {
            //        String Dtt = null;
            //        Dtt = Drv["Date"].ToString();
            //        Daterdv_Datepicker.Text = Dtt.Remove(10);
            //        String h = Dtt.Remove(0, 10);
            //        int heu = int.Parse(h.Remove(3));
            //        String mi = h.Remove(6);
            //        int mii = int.Parse(mi.Remove(0, 4));
            //        heur_combo.Text = heu.ToString(); ;
            //        min_combo.Text = mii.ToString();
            //        if (int.Parse(Drv["Important"].ToString()) == 1) import_checkbox.IsChecked = true;
            //        else import_checkbox.IsChecked = false;
            //        lieu_textbox.Text = Drv["Lieu"].ToString();
            //        cmnt_textbox.Text = Drv["Commentaire"].ToString();
            //        if (Drv["Num_Patient"].ToString() != "")
            //        {
            //            Rdv_perso_checkbox.IsChecked = true;
            //            Id_radio.IsChecked = true;
            //            Id_box.Text = Drv["Num_Patient"].ToString();
            //            bl = true;
            //        }
            //        else
            //        {
            //            Rdv_perso_checkbox.IsChecked = false;
            //            Id_radio.IsChecked = false;
            //            Id_box.Text = "";
            //        }
            //        ConnexionBDD Datab = new ConnexionBDD();
            //        Datab.connecter();
            //        Datab.cmd.CommandType = CommandType.StoredProcedure;
            //        Datab.cmd.CommandText = "SP_Rech_RDV";
            //        Datab.cmd.Parameters.Add("@d_r", SqlDbType.DateTime).Value = Dtt;
            //        Datab.cmd.Connection = Datab.cnx;
            //        SqlDataReader dr;
            //        dr = Datab.cmd.ExecuteReader();
            //        if (dr.Read())
            //        {
            //            id_r = (int)dr["Id_RDV"];
            //        }
            //        dr.Close();
            //        valider_but.IsEnabled = false;
            //        modifier_but.IsEnabled = true;
            //    }
            //}
            //catch (Exception exc) { }
        }




        //private void Id_box_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (Id_box.Text != "")
        //    {
        //        int ii_pp = -1;
        //        ConnexionBDD Datab = new ConnexionBDD();
        //        Datab.connecter();
        //        String command = "SELECT Id_Person From Patient WHERE Id_Patient=" + Id_box.Text;
        //        SqlCommand Macmd = new SqlCommand(command, Datab.cnx);
        //        SqlDataReader dr;
        //        dr = Macmd.ExecuteReader();
        //        if (dr.Read())
        //        {
        //            ii_pp = (int)dr["Id_Person"];
        //        }
        //        dr.Close();
        //        if (ii_pp != -1)
        //        {
        //            String command1 = "SELECT * FROM Person WHERE Id_Person=" + ii_pp;
        //            SqlCommand Macmd1 = new SqlCommand(command1, Datab.cnx);
        //            dr = Macmd1.ExecuteReader();
        //            if (dr.Read())
        //            {
        //                nom_patt = dr["Nom"].ToString();
        //                prenom_patt = dr["Prenom"].ToString();
        //            }
        //            dr.Close();
        //            textbox_name_pat.Text = nom_patt + " " + prenom_patt;
        //        }
        //        else
        //        {
        //            textbox_name_pat.Clear();
        //        }
        //    }

        //    else
        //    {
        //        textbox_name_pat.Clear();
        //    }
        //}
        private void Insert_av_rdv()
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String command = "INSERT INTO Avoir_RDV Values (" + Rdv.get_Id_RDV() + "," + Idmedc + ")";
            SqlCommand Macmd = new SqlCommand(command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            Datab.deconnecter();

        }
        private void update_table_rdv()
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            SqlCommand cmd = new SqlCommand("SP_Update_table_rdv", Datab.cnx);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@dt", SqlDbType.DateTime);
            param.Value = DateTime.Now;
            cmd.ExecuteNonQuery();
            Datab.deconnecter();
        }
        private void PackIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            confirmsupp.Visibility = System.Windows.Visibility.Visible;
            Recconfirmsupp.Visibility = System.Windows.Visibility.Visible;
            RdvGrd.IsEnabled = false ;
        }
        private void deletefromNotif(DateTime dateRdv)
        {
            int i=0;bool trouv = false;
            while ((i < App.acc.lisdesnotif.Count) && (!trouv))
            {
                if (App.acc.lisdesnotif[i].getDate() == dateRdv) trouv = true;
                else i++;
            }
            if (trouv)
            {
                App.acc.lisdesnotif[i].killTimer();
                App.acc.lisdesnotif.RemoveAt(i);
                i = 0;
                trouv=false;
                while ((i < App.acc.listText.Count) && (!trouv))
                {
                    if (App.acc.listText[i].Text.StartsWith("Rendez-vous à " + dateRdv.ToShortTimeString())) trouv = true;
                    else i++;
                }
                if (trouv) App.acc.listText.RemoveAt(i);
            }
        }

        private void butoksuccès_Click(object sender, RoutedEventArgs e)
        {
            Stacksuccès.Visibility = System.Windows.Visibility.Hidden;
            RecStacksuccès.Visibility = System.Windows.Visibility.Hidden;
            RdvGrd.IsEnabled = true;
        }

        private void buttoknonremp_Click(object sender, RoutedEventArgs e)
        {
            Stacknonremp.Visibility = System.Windows.Visibility.Hidden;
            RecStacknonremp.Visibility = System.Windows.Visibility.Hidden;
            RdvGrd.IsEnabled = true; 
        }

        private void butok_Click(object sender, RoutedEventArgs e)
        {

            rdv_exst.Visibility = System.Windows.Visibility.Hidden;
            Recrdv_exst.Visibility = System.Windows.Visibility.Hidden;
            RdvGrd.IsEnabled = true;
        }

        private void butokselectpat_Click(object sender, RoutedEventArgs e)
        {
            RecSelectPat.Visibility = System.Windows.Visibility.Hidden;
            SelectPat.Visibility = System.Windows.Visibility.Hidden;
            RdvGrd.IsEnabled = true;
        }

        private void butouisup_Click(object sender, RoutedEventArgs e)
        {
            //DataGrid Dg = (Rdv_DataGrid) as DataGrid;
            //DataRowView Drv = Dg.SelectedItem as DataRowView;
            //if (Drv != null)
            //{
            //    ConnexionBDD Datab = new ConnexionBDD();
            //    Datab.connecter();
            //    DateTime dt = (DateTime)Drv["Date"];
            //    this.deletefromNotif(dt);
            //    int imp = (int)Drv["Important"];
            //    String lieu = Drv["Lieu"].ToString();
            //    String cmnt = Drv["Commentaire"].ToString();
            //    SqlCommand cmd = new SqlCommand("SP_DELETE_RDV", Datab.cnx);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    SqlParameter param1, param2, param3, param4;
            //    param1 = cmd.Parameters.Add("@dt", SqlDbType.DateTime);
            //    param1.Value = dt;
            //    param2 = cmd.Parameters.Add("@imp", SqlDbType.Int);
            //    param2.Value = imp;
            //    param3 = cmd.Parameters.Add("@lieu", SqlDbType.NVarChar, 50);
            //    param3.Value = lieu;
            //    param4 = cmd.Parameters.Add("@cmnt", SqlDbType.NVarChar, 1000);
            //    param4.Value = cmnt;
            //    cmd.ExecuteNonQuery();
            //    load_tab_grid();

            //}
            if (Rdv_DataGrid.SelectedIndex > -1)
            {
                DataOfRdv datardv = (DataOfRdv)(Rdv_DataGrid.SelectedItem);
                ConnexionBDD Datab = new ConnexionBDD();
                Datab.connecter();
                string Command = @"DELETE FROM [Rendez-vous] where Id_RDV=" + datardv.numero;
                SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
                Macmd.ExecuteNonQuery();
                Command = @"DELETE from Avoir_RDV where Id_RDV=" + datardv.numero;
                Macmd = new SqlCommand(Command, Datab.cnx);
                Macmd.ExecuteNonQuery();
                Rdv_DataGrid.Items.Remove(datardv);
                confirmsupp.Visibility = System.Windows.Visibility.Hidden;
                Recconfirmsupp.Visibility = System.Windows.Visibility.Hidden;
                Stacksuppsucc.Visibility = System.Windows.Visibility.Visible;
                RecStacksuppsucc.Visibility = System.Windows.Visibility.Visible;
                RdvGrd.IsEnabled = false;
                this.vider();
            }
        }

        private void butnonsup_Click(object sender, RoutedEventArgs e)
        {
            confirmsupp.Visibility = System.Windows.Visibility.Hidden;
            Recconfirmsupp.Visibility = System.Windows.Visibility.Hidden;
            RdvGrd.IsEnabled = true;
            this.vider();
        }

        private void butoksuppsucc_Click(object sender, RoutedEventArgs e)
        {
            this.vider();
            Stacksuppsucc.Visibility = System.Windows.Visibility.Hidden;
            RecStacksuppsucc.Visibility = System.Windows.Visibility.Hidden;
            RdvGrd.IsEnabled = true; ;
        }
        public void supprRow(int id)
        {
            bool suppr = false; int i = 0;
            while (i < Rdv_DataGrid.Items.Count && !suppr)
            {
                DataOfRdv datardv = (DataOfRdv)(Rdv_DataGrid.Items[i]);
                if (id == datardv.numero)
                {
                    Rdv_DataGrid.Items.Remove(datardv);
                    suppr = true;
                }
                i++;
            }
        }
        private void buttonSélèctioner_Click(object sender, RoutedEventArgs e)
        {
            //App.acc.pagerdv.Set_idp(this.idpatient);
            this.idd_pat = this.idpatient;
            //App.acc.pagerdv.Set_nom_prenom(this.nom_p, this.prenom_p);
            textbox_name_pat.Text = nom_p + " " + prenom_p;
            Stackrechpat.Visibility = System.Windows.Visibility.Hidden;
            RecStackrechpat.Visibility = System.Windows.Visibility.Hidden;
        }
        //**************************************************************************
        private void patientGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DataGrid Dg = sender as DataGrid;
            //DataRowView Drv = Dg.SelectedItem as DataRowView;
            //if (Drv != null)
            //{
            //    this.idpatient = int.Parse(Drv[4].ToString());
            //    this.nom_p = Drv[0].ToString();
            //    this.prenom_p = Drv[1].ToString();
            //    buttonSélèctioner.IsEnabled = true;
            //}
            if(patientGrid.SelectedIndex > -1)
            {
                DataofPatient datardv = (DataofPatient)(patientGrid.SelectedItem);
                this.idpatient = datardv.numero;
                this.nom_p = datardv.nom;
                this.prenom_p = datardv.prenom;
                buttonSélèctioner.IsEnabled = true;
            }
        }

        private void patientName_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
            DataTable tmp = DATB.Copy();
            DataRow row;
            int i;
            if (patientName.Text != "")
            {
                if (patientPrenom.Text != "")
                {
                    //Rechercher par Nom et Prenom
                    for (i = DATB.Rows.Count - 1; i >= 0; i--)
                    {
                        row = tmp.Rows[i];
                        if (row[0].ToString().ToUpper().StartsWith(patientName.Text.ToUpper()) && row[1].ToString().ToUpper().StartsWith(patientPrenom.Text.ToUpper())) { }
                        else
                        {
                            tmp.Rows.RemoveAt(i);
                        }
                    }
                    patientGrid.ItemsSource = tmp.DefaultView;
                }
                else
                {
                    //Rechercher par Nom
                    for (i = DATB.Rows.Count - 1; i >= 0; i--)
                    {
                        row = tmp.Rows[i];
                        if (row[0].ToString().ToUpper().StartsWith(patientName.Text.ToUpper())) { }
                        else
                        {
                            tmp.Rows.RemoveAt(i);
                        }
                    }
                    patientGrid.ItemsSource = tmp.DefaultView;
                }
            }
            else
            {
                if (patientPrenom.Text != "")
                {
                    //Recherch par Prenom
                    for (i = DATB.Rows.Count - 1; i >= 0; i--)
                    {
                        row = tmp.Rows[i];
                        if (row[1].ToString().ToUpper().StartsWith(patientPrenom.Text.ToUpper())) { }
                        else
                        {
                            tmp.Rows.RemoveAt(i);
                        }
                    }
                    patientGrid.ItemsSource = tmp.DefaultView;
                }
                else
                {
                    //Affichage de tous les patients
                    patientGrid.ItemsSource = DATB.DefaultView;
                }
            }
        }
        private void Loadpatient()
        {
            //ConnexionBDD Datab = new ConnexionBDD();
            //Datab.connecter();
            //String Command = @"select Nom,Prenom,Date_de_naissance,Adresse";
            //Command = Command + @",Id_Patient as [Référence Patient] from Patient LEFT OUTER JOIN Person on Person.Id_Person=Patient.Id_Person order by Nom";
            //SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            //SqlDataAdapter DAPTR = new SqlDataAdapter(Macmd);
            //DATB = new DataTable();
            //DAPTR.Fill(DATB);
            //patientGrid.ItemsSource = DATB.DefaultView;
            DataGridTextColumn g0 = new DataGridTextColumn(); g0.Binding = new Binding("nom"); g0.Header = "Nom";
            DataGridTextColumn g1 = new DataGridTextColumn(); g1.Binding = new Binding("prenom"); g1.Header = "Prenom";
            DataGridTextColumn g2 = new DataGridTextColumn(); g2.Binding = new Binding("date"); g2.Header = "Date de naissance"; g2.MaxWidth = 150;
            DataGridTextColumn g3 = new DataGridTextColumn(); g3.Binding = new Binding("adr"); g3.Header = "Adresse"; g3.MaxWidth = 150;
            DataGridTextColumn g6 = new DataGridTextColumn(); g6.Binding = new Binding("numero"); g6.Header = "Référence Patient";
            patientGrid.Columns.Add(g0); patientGrid.Columns.Add(g1); patientGrid.Columns.Add(g2);
            patientGrid.Columns.Add(g3); patientGrid.Columns.Add(g6);
            g2.ClipboardContentBinding.StringFormat = "d";
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = @"select Nom,Prenom,Date_de_naissance,Adresse";
            Command += @",Id_Patient from Patient LEFT OUTER JOIN Person on Person.Id_Person=Patient.Id_Person order by Nom";
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            SqlDataReader dr = Macmd.ExecuteReader();
            while (dr.Read())
            {
                DataofPatient dofpatient = new DataofPatient();
                dofpatient.nom = dr[0].ToString(); dofpatient.prenom = dr[1].ToString(); dofpatient.date = DateTime.Parse(dr[2].ToString());
                dofpatient.adr = dr[3].ToString(); dofpatient.numero = int.Parse(dr[4].ToString());
                patientGrid.Items.Add(dofpatient);
            }
            dr.Close();
        }

        private void butretour_Click(object sender, RoutedEventArgs e)
        {
            Stackrechpat.Visibility = System.Windows.Visibility.Hidden;
            RecStackrechpat.Visibility = System.Windows.Visibility.Hidden;
        }
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
        //************************************************************************
    }
}
