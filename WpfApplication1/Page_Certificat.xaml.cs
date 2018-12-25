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
using Microsoft.Win32;
using System.IO;
using iTextSharp.text;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Page_Certificat.xaml
    /// </summary>
    public partial class Page_Certificat : Page
    {
        public struct DataofCert
        {
            public int numero;
            public int nb { get; set; }
            public string commentaire { get; set; }
        }
        private DataTable table { get; set; }
        private int IDFC { get; set; }
        private int IDDOSS { get; set; }
        private int IDMED { get; set; }
        private bool ADMIN { get; set; }
        private int IDPAT { get; set; }
        public Page_Certificat(int idfc,int idos,int idmed,bool admin,bool consulter,int idpat)
        {
            InitializeComponent();
            if (!consulter)
            {
                this.GridBarre.Visibility = Visibility.Collapsed;
                this.GridMain.Height += 60;
                this.DatagGrid.Height += 60;
            }
            IDFC = idfc; IDDOSS = idos; ADMIN = admin; IDMED = idmed; IDPAT = idpat;
            loadCert();
            this.AffichageStack.Visibility = Visibility.Visible;
            this.Affichage2.Visibility = Visibility.Hidden;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            GridCursor.Margin = new Thickness(10 + (200 * index), 45, 0, 10);

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
        private void loadCert()
        {
            table = new DataTable();
            DataGridTextColumn g0 = new DataGridTextColumn();
            DataGridTextColumn g1 = new DataGridTextColumn();
            DataGridTextColumn g2 = new DataGridTextColumn();
            g0.Binding = new Binding("numero"); g0.Header = ""; g0.MaxWidth = 0;
            g1.Binding = new Binding("nb"); g1.Header = "Durée de repos(jour)";
            g2.Binding = new Binding("commentaire"); g2.Header = "Commentaire"; g2.MaxWidth = 150;
            DatagGrid.Columns.Add(g0); DatagGrid.Columns.Add(g1); DatagGrid.Columns.Add(g2);
            DatagGrid.Columns[0].DisplayIndex = 3;
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command;
            if (ADMIN) Command = "select * from Certeficat_medical where Id_FC in (SELECT Id_FC from Contient where Id_DM=" + IDDOSS + ")";
            else Command = "select * from Certeficat_medical where Id_FC in (select Id_Fiche_Consultation from Fiche_Consultation where Id_Fiche_Consultation in (SELECT Id_FC from Contient where Id_DM=" + IDDOSS + ") and Id_Med="+IDMED+")";
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            SqlDataReader dr = Macmd.ExecuteReader();
            while (dr.Read())
            {
                DataofCert ordodata = new DataofCert();
                ordodata.numero = int.Parse(dr[0].ToString());
                ordodata.nb = int.Parse(dr[2].ToString());
                ordodata.commentaire = dr[3].ToString();
                DatagGrid.Items.Add(ordodata);
            }
            dr.Close();
            SqlDataAdapter Daptr = new SqlDataAdapter(Macmd);
            Daptr.Fill(table);
            Datab.deconnecter();
        }
        private void btnGrid_Click(object sender, RoutedEventArgs e)
        {
            if (DatagGrid.SelectedIndex != -1)
            {
                DataofCert dfg = (DataofCert)(DatagGrid.SelectedItem);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    if (row[0].ToString() == dfg.numero.ToString())
                    {
                        string path = row[4].ToString();
                        try
                        {
                            System.Diagnostics.Process process = new System.Diagnostics.Process();
                            process.StartInfo.FileName = path;
                            process.Start();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                }
            }
        }
        public bool verif()
        {
            if ((nb.Text != "") && (Nbre_jour.Text != "") && (comment.Text != "")) return (true);
            else return (false);
        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            if (verif())
            {
                int i = int.Parse(this.nb.Text);
                if (this.Nbre_jour.Text == "Semaine") i = i * 7;
                if (this.Nbre_jour.Text == "Mois") i = i * 30;
                if (this.Nbre_jour.Text == "Ans") i = i * 365;
                Certaficat_Médical certi = new Certaficat_Médical(i, this.comment.Text);
                certi.Set_Id_fc(IDFC);
                certi.insert_nvl_certaficat_medic();
                DataofCert ordodata = new DataofCert();
                ordodata.numero = certi.Get_Id_Cert();
                ordodata.nb = i;
                ordodata.commentaire = this.comment.Text;
                DatagGrid.Items.Add(ordodata);
                //FILEDIALOG
                string path = ""; DateTime dat = new DateTime();
                Misenforme mm = new Misenforme();
                mm.get_Patient(this.IDPAT, ref path, ref dat);
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Fichiers PDF|*.pdf";
                string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                path = documents + save.InitialDirectory + @"\CureIt\" + path + "_" + this.IDPAT + @"\Certificiats";
                MessageBox.Show(path);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                save.InitialDirectory = path;
                save.FileName = "Certificat" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
                if (save.ShowDialog() == true)
                {
                    string nomdoc = save.FileName;
                    MiseEnFormLettre forme = new MiseEnFormLettre(IDDOSS, IDMED);
                    MiseEnFormCert form = new MiseEnFormCert(IDDOSS, IDMED);
                    Document doc = forme.CreateDoc(nomdoc);
                    doc.Open();
                    doc = form.Remplir_doc(doc, this.comment.Text, int.Parse(this.nb.Text), this.Nbre_jour.Text, IDMED, IDDOSS);
                    doc.Close();
                    ConnexionBDD Datab = new ConnexionBDD();
                    Datab.connecter();
                    string filename = save.FileName.Replace("'", "''");
                    String Command = @"update Certeficat_medical set Fichier='" + filename + "' where Id_Cert_Med=" + certi.Get_Id_Cert();
                    SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
                    Macmd.ExecuteNonQuery();
                    App.iff.support_fonction.NavigationService.Navigate(new Page_Certificat(IDFC, IDDOSS, IDMED, ADMIN, true,IDPAT));
                    this.AffichageStack.Visibility = Visibility.Hidden;
                    this.Affichage2.Visibility = Visibility.Visible;
                }
                StackcertbienAjouer.Visibility = System.Windows.Visibility.Visible;
                RecStackcertbienAjouer.Visibility = System.Windows.Visibility.Visible;
                grdcert.IsEnabled = false;
            }
            else
            {
                Stacknonrempcert.Visibility = System.Windows.Visibility.Visible;
                RecStacknonrempcert.Visibility = System.Windows.Visibility.Visible;
                grdcert.IsEnabled = false;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        }
        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            if (DatagGrid.SelectedIndex != -1)
            {
                //Modifier
            }
            else
            {
                //Veuillez séléctionner unne fiche pour modifier
            }
        }
        private void Supprimer_Click_1(object sender, RoutedEventArgs e)
        {
            if (DatagGrid.SelectedIndex >= 0)
            {
                DataofCert dataa = (DataofCert)(DatagGrid.SelectedItem);
                ConnexionBDD Datab = new ConnexionBDD();
                Datab.connecter();
                String Command = "DELETE FROM Certeficat_medical where Id_Cert_Med=" + dataa.numero;
                SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
                Macmd.ExecuteNonQuery();
                Datab.deconnecter();
                DatagGrid.Items.Remove(dataa);
            }
        }

        private void DatagGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatagGrid.SelectedIndex >= 0) Supprimer.IsEnabled = true;
            else Supprimer.IsEnabled = false;
        }

        private void DatagGrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete) Supprimer_Click_1(sender, new RoutedEventArgs());
        }

        private void nb_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 && (Keyboard.GetKeyStates(Key.CapsLock) == KeyStates.None) &&
               (Keyboard.IsKeyDown(Key.RightShift) == false && Keyboard.IsKeyDown(Key.LeftShift) == false))
                || e.Key == Key.Tab) { }
            else e.Handled = true;
        }

        private void annulerbutt_Click(object sender, RoutedEventArgs e)
        {
            vider();
        }
        public void vider()
        {
            nb.Text = null;
            Nbre_jour.Text = null;
            comment.Text = null;
        }

        private void buttokcert_Click(object sender, RoutedEventArgs e)
        {
            Stacknonrempcert.Visibility = System.Windows.Visibility.Hidden;
            RecStacknonrempcert.Visibility = System.Windows.Visibility.Hidden;
            grdcert.IsEnabled = true;
        }

        private void buttokcertajouter_Click(object sender, RoutedEventArgs e)
        {
            this.vider();
            StackcertbienAjouer.Visibility = System.Windows.Visibility.Hidden;
            RecStackcertbienAjouer.Visibility = System.Windows.Visibility.Hidden;
            grdcert.IsEnabled = true;
        }
        private void nb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int nbb;
            if (int.TryParse(e.Text, out nbb)) { }
            else e.Handled = true;
        }
    }
}
