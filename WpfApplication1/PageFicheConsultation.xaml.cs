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
    /// Interaction logic for PageFicheConsultation.xaml
    /// </summary>
    public partial class PageFicheConsultation : Page
    {
        private int id_fich;
        private int nb_cert;
        private int nb_ord;
        private bool count;
        private bool boool;
        private int id_pat;
        public struct DataofFiche
        {
            public int numero;
            public DateTime date { get; set; }
            public string diagnostique { get; set; }
            public string taillepoids { get; set; }
            public string tempe { get; set; }
            public string tention { get; set; }
            public string nomprenom { get; set; }
        }
        private DataTable table { get; set; }
        private Rendez_Vous Rdv { get; set; }
        private DateTime Dte_rdv { get; set; }
        private int heur_rdv { get; set; }
        private int min_rdv { get; set; }
        private String lieu_rdv { get; set; }
        private String cmnt_rdv { get; set; }
        private int important { get; set; }
        private int IDFC { get; set; }
        private int IDMED { get; set; }
        private bool ADMIN { get; set; }
        public PageFicheConsultation(int idfc, bool admin, int idmed,int idpat, bool consulter)
        {
            InitializeComponent();
            //if (!consulter) this.GridBarre.Visibility = Visibility.Collapsed;
            if (!consulter)
            {
                this.GridBarre.Visibility = Visibility.Collapsed;
                this.GridMain.Height += 60;
                this.DatagGrid.Height += 60;
            }
            this.AffichageStack.Visibility = Visibility.Visible;
            this.Affichage2.Visibility = Visibility.Hidden;
            this.id_pat = idpat;
            this.Mois.Items.Add(""); this.Annee.Items.Add("");
            for (int i = 1; i < 13; i++) this.Mois.Items.Add(i);
            for (int i = 2010; i < 2101; i++) this.Annee.Items.Add(i);
            init_combo();
            IDFC = idfc; ADMIN = admin; IDMED = idmed;
            loadFiche();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            GridCursor.Margin = new Thickness(150 + (200 * index), 45, 0, 10);

            switch (index)
            {
                case 0:
                    this.AffichageStack.Visibility = Visibility.Visible;
                    this.Affichage2.Visibility = Visibility.Hidden;
                    break;
                case 1:
                    this.AffichageStack.Visibility = Visibility.Hidden;
                    this.Affichage2.Visibility = Visibility.Visible;
                    break;
            }
        }
        private void loadFiche()
        {
            table = new DataTable();
            DataGridTextColumn g0 = new DataGridTextColumn(); g0.Binding = new Binding("numero"); g0.Header = ""; g0.MaxWidth = 0;
            DataGridTextColumn g1 = new DataGridTextColumn(); g1.Binding = new Binding("date"); g1.Header = "Date";
            DataGridTextColumn g2 = new DataGridTextColumn(); g2.Binding = new Binding("diagnostique"); g2.Header = "Diagnostique"; g2.MaxWidth = 150;
            DataGridTextColumn g3 = new DataGridTextColumn(); g3.Binding = new Binding("taillepoids"); g3.Header = "Taille et Poids";
            DataGridTextColumn g4 = new DataGridTextColumn(); g4.Binding = new Binding("tempe"); g4.Header = "Temperature";
            DataGridTextColumn g5 = new DataGridTextColumn(); g5.Binding = new Binding("tention"); g5.Header = "Tention";
            DataGridTextColumn g6 = new DataGridTextColumn(); g6.Binding = new Binding("nomprenom"); g6.Header = "Fait par:";
            DatagGrid.Columns.Add(g0); DatagGrid.Columns.Add(g1); DatagGrid.Columns.Add(g2);
            DatagGrid.Columns.Add(g3); DatagGrid.Columns.Add(g4); DatagGrid.Columns.Add(g5); DatagGrid.Columns.Add(g6);
            g1.ClipboardContentBinding.StringFormat = "d";
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = "select Id_Fiche_Consultation,Date_F,Diagnostique,[Taille ],Poids,Temperature,Tension,Nom,Prenom from";
            Command += " Fiche_Consultation LEFT OUTER JOIN Doctor on Fiche_Consultation.Id_Med=Doctor.Id_Doctor LEFT OUTER JOIN";
            Command += " Person on Doctor.Id_Person=Person.Id_Person";
            if (!ADMIN) Command += " where Id_Doctor=" + IDMED;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            SqlDataReader dr = Macmd.ExecuteReader();
            while (dr.Read())
            {
                DataofFiche datfich = new DataofFiche();
                datfich.numero = int.Parse(dr[0].ToString());
                datfich.date = DateTime.Parse(dr[1].ToString());
                datfich.diagnostique = dr[2].ToString();
                datfich.taillepoids = dr[3].ToString() +" - "+ dr[4].ToString();
                datfich.tempe = dr[5].ToString();
                datfich.tention = dr[6].ToString();
                datfich.nomprenom = dr[7].ToString() +" "+ dr[8].ToString();
                if ((datfich.taillepoids == " - ") || (datfich.taillepoids == "0 - 0")) datfich.taillepoids = "";
                if (datfich.tempe == "0") datfich.tempe = "";
                if (datfich.tention == "0") datfich.tention = "";
                DatagGrid.Items.Add(datfich);
            }
            dr.Close();
            SqlDataAdapter Daptr = new SqlDataAdapter(Macmd);
            Daptr.Fill(table);
            Datab.deconnecter();
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            if (DatagGrid.SelectedIndex >= 0)
            {
                //A finaliser 

                DataofFiche dataa = (DataofFiche)(DatagGrid.SelectedItem);
                ConnexionBDD Datab = new ConnexionBDD();
                Datab.connecter();
                String Command1 = "SELECT Id_Ordonnance FROM Ordonnance where Id_FC=" + dataa.numero;
                SqlCommand Macmd1 = new SqlCommand(Command1, Datab.cnx);
                SqlDataReader dr = Macmd1.ExecuteReader();
                this.nb_ord = 0;
                while (dr.Read())
                {
                    this.nb_ord++;
                }
                dr.Close();
                Command1 = "SELECT Id_Cert_Med FROM Certeficat_medical where Id_FC=" + dataa.numero;
                Macmd1 = new SqlCommand(Command1, Datab.cnx);
                dr = Macmd1.ExecuteReader();
                this.nb_cert = 0;
                while (dr.Read())
                {
                    this.nb_cert++;
                }
                dr.Close();
                if (this.nb_cert != 0 || this.nb_ord != 0)
                {
                    attentionstack.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    attentionstack.Visibility = System.Windows.Visibility.Collapsed;
                }
                confirmstacksup.Visibility = System.Windows.Visibility.Visible;
               
                AffichageStack.IsEnabled = false;
                //*********************************************************

            }
        }

        private void DatagGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (DatagGrid.SelectedIndex >= 0)
            {
                Supprimer.IsEnabled = true;
                Modifier.IsEnabled = true;
                DataofFiche dfg = (DataofFiche)(DatagGrid.SelectedItem);
                if (this.IDFC == dfg.numero)
                {
                    Supprimer.IsEnabled = false;
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    if (row[0].ToString() == dfg.numero.ToString())
                    {
                        id_fich = dfg.numero;
                        Tent.Text = dfg.tention;
                        Temp.Text = dfg.tempe;
                        diagno.Text = dfg.diagnostique;
                        if ((dfg.taillepoids == " - ") || (dfg.taillepoids == "0 - 0") || (dfg.taillepoids == ""))
                        {
                            Tay.Text = "";
                            Pds.Text = "";
                        }
                        else
                        {
                            Tay.Text = dfg.taillepoids.Remove(dfg.taillepoids.IndexOf("-"));
                            Pds.Text = dfg.taillepoids.Remove(0, dfg.taillepoids.IndexOf("-") + 1);
                        }

                    }
                }
            }
            else
            {
                Supprimer.IsEnabled = false;
                Modifier.IsEnabled = false;
            }

        }

        private void ic_valider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = "Update Fiche_Consultation  SET Tension='" + Tent.Text + "'";
            Command = Command + ", Poids=";
            if (Pds.Text != "") Command = Command + "TRY_CONVERT(float,'" + Pds.Text + "')";
            else Command = Command + "null";
            Command = Command + ", Taille=";
            if (Tay.Text != "") Command = Command + int.Parse(Tay.Text);
            else Command = Command + "null";
            Command = Command + ", Temperature=";
            if (Temp.Text != "") Command = Command + "TRY_CONVERT(float,'" + Temp.Text + "')";
            else Command = Command + "null";
            Command = Command + ", Diagnostique='" + diagno.Text + "'";
            Command = Command + " where Id_Fiche_Consultation=" + this.id_fich;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            StackModif.Visibility = System.Windows.Visibility.Hidden;
            RecStackModif.Visibility = System.Windows.Visibility.Hidden;           
            DataofFiche dofich = new DataofFiche();
            dofich.tention = Tent.Text; dofich.taillepoids = Tay.Text + " - " + Pds.Text; dofich.tempe = Temp.Text;
            dofich.diagnostique = diagno.Text;
            DataofFiche dof = (DataofFiche)(this.DatagGrid.SelectedItem);
            dofich.date=dof.date;dofich.nomprenom=dof.nomprenom;dofich.numero=dof.numero;
            DatagGrid.Items.Insert(DatagGrid.SelectedIndex, dofich);
            DatagGrid.Items.Remove(dof);
            AffichageStack.IsEnabled = true;
        }

        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            StackModif.Visibility = System.Windows.Visibility.Visible;
            RecStackModif.Visibility = System.Windows.Visibility.Visible;         
            AffichageStack.IsEnabled = false;
        }

        private void ic_close_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            confirmstack.Visibility = System.Windows.Visibility.Visible;
            Recconfirmstack.Visibility = System.Windows.Visibility.Visible;
            StackModif.IsEnabled = false;
        }

        private void butnon_Click(object sender, RoutedEventArgs e)
        {
            confirmstack.Visibility = System.Windows.Visibility.Hidden;
            Recconfirmstack.Visibility = System.Windows.Visibility.Hidden;
            StackModif.IsEnabled = true;
        }

        private void butoui_Click(object sender, RoutedEventArgs e)
        {
            StackModif.Visibility = System.Windows.Visibility.Hidden;
            RecStackModif.Visibility = System.Windows.Visibility.Hidden;          
            confirmstack.Visibility = System.Windows.Visibility.Hidden;
            Recconfirmstack.Visibility = System.Windows.Visibility.Hidden;
            StackModif.IsEnabled = true;
            AffichageStack.IsEnabled = true;
        }

        private void butouisup_Click(object sender, RoutedEventArgs e)
        {
            DataofFiche dataa = (DataofFiche)(DatagGrid.SelectedItem);
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = "DELETE FROM Prescrire WHERE Id_Ordonnance in ( SELECT Id_Ordonnance FROM Ordonnance WHERE Id_FC=" + dataa.numero + ")";
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            Command = "DELETE FROM Ordonnance WHERE Id_FC=" + dataa.numero;
            Macmd = new SqlCommand(Command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            Command = "DELETE FROM Certeficat_medical WHERE Id_FC=" + dataa.numero;
            Macmd = new SqlCommand(Command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            Command = "DELETE FROM Fiche_Consultation where Id_Fiche_Consultation=" + dataa.numero;
            Macmd = new SqlCommand(Command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            Datab.deconnecter();
            DatagGrid.Items.Remove(dataa);
            AffichageStack.IsEnabled = true;
            confirmstacksup.Visibility = System.Windows.Visibility.Hidden;
           
            StackModif.IsEnabled = true;
        }

        private void butnonsup_Click(object sender, RoutedEventArgs e)
        {
            confirmstacksup.Visibility = System.Windows.Visibility.Hidden;
           
            AffichageStack.IsEnabled = true;
            StackModif.IsEnabled = true;
        }

        private void Mois_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.Mois.SelectedIndex >= 1)
            {
                DataTable tmp = table.Copy();
                DataRow row; int i;
                if (this.Annee.SelectedIndex >= 1)
                {
                    for (i = tmp.Rows.Count - 1; i >= 0; i--)
                    {
                        row = tmp.Rows[i];
                        DateTime datee = DateTime.Parse(row[1].ToString());
                        if ((datee.Year == int.Parse(this.Annee.SelectedItem.ToString())) && (datee.Month == int.Parse(this.Mois.SelectedItem.ToString()))) { }
                        else tmp.Rows.RemoveAt(i);
                    }
                }
                else
                {
                    for (i = tmp.Rows.Count - 1; i >= 0; i--)
                    {
                        row = tmp.Rows[i];
                        DateTime datee = DateTime.Parse(row[1].ToString());
                        if (datee.Month == int.Parse(this.Mois.SelectedItem.ToString())) { }
                        else tmp.Rows.RemoveAt(i);
                    }
                }
                Rempliravec(tmp);
            }
            else
            {
                if (this.Annee.SelectedIndex >= 1)
                {
                    DataTable tmp = table.Copy();
                    DataRow row; int i;
                    for (i = tmp.Rows.Count - 1; i >= 0; i--)
                    {
                        row = tmp.Rows[i];
                        DateTime datee = DateTime.Parse(row[1].ToString());
                        if (datee.Year == int.Parse(this.Annee.SelectedItem.ToString())) { }
                        else tmp.Rows.RemoveAt(i);
                    }
                    Rempliravec(tmp);
                }
                else
                {
                    Rempliravec(table);
                }
            }
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
                DataofFiche datfich = new DataofFiche();
                datfich.numero = int.Parse(dr[0].ToString());
                datfich.date = DateTime.Parse(dr[1].ToString());
                datfich.diagnostique = dr[2].ToString();
                datfich.taillepoids = dr[3].ToString() + " - " + dr[4].ToString();
                datfich.tempe = dr[5].ToString();
                datfich.tention = dr[6].ToString();
                datfich.nomprenom = dr[7].ToString() + " " + dr[8].ToString();
                if ((datfich.taillepoids == " - ") || (datfich.taillepoids == "0 - 0")) datfich.taillepoids = "";
                if (datfich.tempe == "0") datfich.tempe = "";
                if (datfich.tention == "0") datfich.tention = "";
                DatagGrid.Items.Add(datfich);
            }
        }

        private void DatagGrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete) Supprimer_Click(sender, new RoutedEventArgs());
        }
        private void add_rdv()
        {
            verifier(ref boool);
            if (boool)
            {
                this.Dte_rdv = DateTime.Parse(date_rdv.Text);
                this.heur_rdv = int.Parse(heur_combo.Text);
                this.min_rdv = int.Parse(min_combo.Text);
                DateTime abc = new DateTime(Dte_rdv.Year, Dte_rdv.Month, Dte_rdv.Day, heur_rdv, min_rdv, 00);
                Dte_rdv = abc;
                this.lieu_rdv = lieu_box.Text;
                this.cmnt_rdv = cmnt_box.Text;
                if (imp_checkbox.IsChecked == true) this.important = 1;
                else this.important = 0;
                Rdv = new Rendez_Vous(Dte_rdv, important, cmnt_rdv, lieu_rdv);
                recherche_rdv(ref count);
                if (!count)
                {
                    Rdv.Ajouter_RDV();
                    Insert_av_rdv();

                }
                else if (count)
                {
                    rdv_exst.Visibility = System.Windows.Visibility.Visible;
                    Recrdv_exst.Visibility = System.Windows.Visibility.Visible;
                    Stack_rdv.IsEnabled = false;
                }
            }
            else
            {
                Stacknonremp.Visibility = System.Windows.Visibility.Visible;
                RecStacknonremp.Visibility = System.Windows.Visibility.Visible;
                Stack_rdv.IsEnabled = false;
                AffichageStack.IsEnabled = false;
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
                if (this.IDMED == (int)dr["Id_Doctor"]) cpt = true;
            }
        }
        private void Insert_av_rdv()
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String command = "INSERT INTO Avoir_RDV Values (" + Rdv.get_Id_RDV() + "," + this.IDMED + ")";
            SqlCommand Macmd = new SqlCommand(command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            Datab.deconnecter();

        }
        private void verifier(ref bool verif)
        {
            verif = true;
            if (date_rdv.Text == "" || heur_combo.Text == "" || min_combo.Text == "" || lieu_box.Text == "" || cmnt_box.Text == "")
            {
                verif = false;
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

        private void ic_validrdv_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            update_table_rdv();
            add_rdv();

            if (!count && boool)
            {
                Rdv.update_Id_pat(this.id_pat);
                Stacksuccès.Visibility = System.Windows.Visibility.Visible;
                RecStacksuccès.Visibility = System.Windows.Visibility.Visible;
                Stack_rdv.IsEnabled = false;

            }

        }

        private void vider()
        {
            heur_combo.Text = ""; min_combo.Text = ""; lieu_box.Clear(); cmnt_box.Clear(); date_rdv.Text = null;
            imp_checkbox.IsChecked = false;

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

        private void ic_annul_rdv_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            stackquitrdv.Visibility = System.Windows.Visibility.Visible;
            Recstackquitrdv.Visibility = System.Windows.Visibility.Visible;
            Stack_rdv.IsEnabled = false;

        }

        private void but_rdv_Click(object sender, RoutedEventArgs e)
        {
            Stack_rdv.Visibility = System.Windows.Visibility.Visible;
            RecStack_rdv.Visibility = System.Windows.Visibility.Visible;
            AffichageStack.IsEnabled = false;
            //AffichageStack.Opacity = 0.5f;
        }

        private void butok_Click(object sender, RoutedEventArgs e)
        {
            date_rdv.Text = null;
            heur_combo.Text = null;
            min_combo.Text = null;
            rdv_exst.Visibility = System.Windows.Visibility.Hidden;
            Recrdv_exst.Visibility = System.Windows.Visibility.Hidden;
            Stack_rdv.IsEnabled = true;
        }

        private void buttouiquit_Click(object sender, RoutedEventArgs e)
        {
            Stack_rdv.Visibility = System.Windows.Visibility.Hidden;
            RecStack_rdv.Visibility = System.Windows.Visibility.Hidden; 
            AffichageStack.IsEnabled = true;
            stackquitrdv.Visibility = System.Windows.Visibility.Hidden;
            Recstackquitrdv.Visibility = System.Windows.Visibility.Hidden;
            Stack_rdv.IsEnabled = true;

        }

        private void buttnonquit_Click(object sender, RoutedEventArgs e)
        {
            stackquitrdv.Visibility = System.Windows.Visibility.Hidden;
            Recstackquitrdv.Visibility = System.Windows.Visibility.Hidden;
            Stack_rdv.IsEnabled = true;
        }

        private void buttoknonremp_Click(object sender, RoutedEventArgs e)
        {
            Stacknonremp.Visibility = System.Windows.Visibility.Hidden;
            RecStacknonremp.Visibility = System.Windows.Visibility.Hidden;
            Stack_rdv.IsEnabled = true;
        }

        private void butoksuccès_Click(object sender, RoutedEventArgs e)
        {
            Stacksuccès.Visibility = System.Windows.Visibility.Hidden;
            RecStacksuccès.Visibility = System.Windows.Visibility.Hidden;
            Stack_rdv.IsEnabled = true;
            vider();
            //Stack_rdv.Visibility = System.Windows.Visibility.Hidden;
            //RecStack_rdv.Visibility = System.Windows.Visibility.Hidden;
            //AffichageStack.IsEnabled = true;
            
        }
        private bool IsFloat(string str)
        {
            float n;
            return float.TryParse(str, out n);
        }
        private bool IsInteger(string str)
        {
            int n;
            return int.TryParse(str, out n);
        }
        private void Tay_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == ",")
            {
                if ((sender as TextBox).Text.Contains(",")) e.Handled = true;
            }
            else if (IsFloat(e.Text) == false) e.Handled = true;
        }

        private void Tay_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            if (IsInteger(e.Text) == false) e.Handled = true;
        }
    }
}
