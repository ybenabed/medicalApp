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
    /// Interaction logic for Page_Lettre_Orientation.xaml
    /// </summary>
    public partial class Page_Lettre_Orientation : Page
    {
        public struct DataofLettre
        {
            public int numero { get; set; }
            public string nom { get; set; }
            public string prenom { get; set; }
            public string spec { get; set; }
            public string adr { get; set; }
            public string cmnt { get; set; }

        }
        private DataTable table { get; set; }
        private int IDDOSS { get; set; }
        private int IDMED { get; set; }
        private bool ADMIN { get; set; }
        private int IDPAT { get; set; }
        public Page_Lettre_Orientation(int id, bool admin,int idm, bool consulter,int idpat)
        {
            InitializeComponent();
            IDMED = idm;
            if (!consulter)
            {
                this.GridBarre.Visibility = Visibility.Collapsed;
                Visualisation.Visibility = Visibility.Visible;
                Affichage2.Visibility = Visibility.Hidden;
                Close_.Visibility = Visibility.Hidden;
            }
            else
            {
                Visualisation.Visibility = Visibility.Hidden;
                Affichage2.Visibility = Visibility.Visible;
                Close_.Visibility = Visibility.Visible;
            }
            IDDOSS = id; ADMIN = admin; IDPAT = idpat;
            pdfViewer.Navigate(new Uri("about:blank"));
            loadOrien();
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
        private void loadOrien()
        {
            table = new DataTable();
            DataGridTextColumn g0 = new DataGridTextColumn(); g0.Binding = new Binding("numero"); g0.Header = ""; g0.MaxWidth = 0;
            DataGridTextColumn g1 = new DataGridTextColumn(); g1.Binding = new Binding("nom"); g1.Header = "Nom medcin"; g1.Width = 133;
            DataGridTextColumn g3 = new DataGridTextColumn(); g3.Binding = new Binding("spec"); g3.Header = "Spécialité"; g3.Width = 133;
            DatagGrid.Columns.Add(g0); DatagGrid.Columns.Add(g1); 
            DatagGrid.Columns.Add(g3); 
            DatagGrid.Columns[0].DisplayIndex = 3;
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = "Select * from Lettre where Id_DM=" + IDDOSS;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            SqlDataReader dr = Macmd.ExecuteReader();
            while (dr.Read())
            {
                DataofLettre ordodata = new DataofLettre();
                ordodata.numero = int.Parse(dr[0].ToString());
                ordodata.nom = dr[2].ToString();
                ordodata.spec = dr[3].ToString();
                ordodata.adr = dr[4].ToString();
                ordodata.cmnt = dr[5].ToString();
                DatagGrid.Items.Add(ordodata);
            }
            dr.Close();
            SqlDataAdapter Daptr = new SqlDataAdapter(Macmd);
            Daptr.Fill(table);
        }
        private void btnGrid_Click(object sender, RoutedEventArgs e)
        {
            if (DatagGrid.SelectedIndex != -1)
            {
                DataofLettre dfg = (DataofLettre)(DatagGrid.SelectedItem);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    if (row[0].ToString() == dfg.numero.ToString())
                    {
                        string path = row[6].ToString();
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
            if ((nom_med.Text != "") && (adr.Text != "") && (Spec.Text != "") && (com.Text != "")) return true;
            else return false;
        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            if (verif())
            {
                String nom = this.nom_med.Text;
                String adr = this.adr.Text;
                String spec = this.Spec.Text;
                String com = this.com.Text;
                Lettre lettre = new Lettre(nom, spec, adr, com, IDDOSS);
                lettre.Insert_Nvl_Lettre();
                DataofLettre ordodata = new DataofLettre();
                ordodata.numero = lettre.Get_id();
                ordodata.nom = nom;
                ordodata.spec = spec;
                ordodata.adr = adr;
                ordodata.cmnt = com;
                DatagGrid.Items.Add(ordodata);
                string path = ""; DateTime dat = new DateTime();
                Misenforme mm = new Misenforme();
                mm.get_Patient(IDPAT, ref path, ref dat);
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Fichiers PDF|*.pdf";
                string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                path = documents + save.InitialDirectory + @"\CureIt\" + path + "_" + IDPAT + @"\Lettres d'orientation";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                save.InitialDirectory = path;
                save.FileName = "Lettre" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
                if (save.ShowDialog() == true)
                {
                    string nomdoc = save.FileName;
                    MiseEnFormLettre forme = new MiseEnFormLettre(IDDOSS, IDMED);
                    Document doc = forme.CreateDoc(save.FileName);
                    doc.Open();
                    doc = forme.Remplir_doc(doc, nom , adr, spec, com, IDMED, IDDOSS);
                    doc.Close();
                    ConnexionBDD Datab = new ConnexionBDD();
                    Datab.connecter();
                    string filename = save.FileName.Replace("'", "''");
                    String Command = @"update Lettre set Fichier='" + filename + "' where Id_Lettre=" + lettre.Get_id();
                    SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
                    Macmd.ExecuteNonQuery();
                    //App.iff.support_fonction.NavigationService.Navigate(new Page_Lettre_Orientation(IDDOSS, ADMIN, IDMED, true,IDPAT));
                    //this.AffichageStack.Visibility = Visibility.Hidden;
                    //this.Affichage2.Visibility = Visibility.Visible;
                }
                Stackbienajouté.Visibility = System.Windows.Visibility.Hidden;
                RecStackbienajouté.Visibility = System.Windows.Visibility.Hidden;
                this.vider();
                grdlettre.IsEnabled = true;
            }
            else
            {
                Stacknonremplett.Visibility = System.Windows.Visibility.Visible;
                RecStacknonremplett.Visibility = System.Windows.Visibility.Visible;
                grdlettre.IsEnabled = false;

            }
                
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        }

        private void nom_med_KeyDown(object sender, KeyEventArgs e)
        {
            VerifLettre(e);
        }
        public void VerifLettre(KeyEventArgs e)
        {
            if (e.Key >= Key.A && e.Key <= Key.Z)
            {
            }
            else e.Handled = true;
        }

        private void prenom_med_KeyDown(object sender, KeyEventArgs e)
        {
            VerifLettre(e);
        }

        private void ic_close_lett_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Hidden;
        }
        private void Modifier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Supprimer_Click_1(object sender, RoutedEventArgs e)
        {
            if (DatagGrid.SelectedIndex >= 0)
            {
                DataofLettre dataa = (DataofLettre)(DatagGrid.SelectedItem);
                ConnexionBDD Datab = new ConnexionBDD();
                Datab.connecter();
                String Command = "DELETE FROM Lettre where Id_Lettre=" + dataa.numero;
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
        public void vider()
        {
            nom_med.Text = null;
            Spec.Text = null;
            adr.Text = null;
            com.Text = null;
        }

        private void buttoklettre_Click(object sender, RoutedEventArgs e)
        {
            Stacknonremplett.Visibility = System.Windows.Visibility.Hidden;
            RecStacknonremplett.Visibility = System.Windows.Visibility.Hidden;
            grdlettre.IsEnabled = true;
        }

        private void buttokenreg_Click(object sender, RoutedEventArgs e)
        {
            this.vider();
            Stackbienajouté.Visibility = System.Windows.Visibility.Hidden;
            RecStackbienajouté.Visibility = System.Windows.Visibility.Hidden;
            grdlettre.IsEnabled = true;
        }

        private void ouvr_Click(object sender, RoutedEventArgs e)
        {
            if (DatagGrid.SelectedIndex != -1)
            {
                DataofLettre dfg = (DataofLettre)(DatagGrid.SelectedItem);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    if (row[0].ToString() == dfg.numero.ToString())
                    {
                        string path = row[6].ToString();
                        try
                        {
                            pdfViewer.Navigate(new Uri(path));
                            Visualisation.Visibility = Visibility.Visible;
                            Affichage2.Visibility = Visibility.Hidden;
                            //System.Diagnostics.Process process = new System.Diagnostics.Process();
                            //process.StartInfo.FileName = path;
                            //process.Start();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                }
            }

        }

        private void openDirectory_Click(object sender, RoutedEventArgs e)
        {
            if (DatagGrid.SelectedIndex != -1)
            {
                DataofLettre dfg = (DataofLettre)(DatagGrid.SelectedItem);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    if (row[0].ToString() == dfg.numero.ToString())
                    {
                        string path = row[6].ToString();
                        path = path.Remove(path.LastIndexOf(@"\"));
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

        private void Close__Click(object sender, RoutedEventArgs e)
        {
            Visualisation.Visibility = Visibility.Hidden;
            Affichage2.Visibility = Visibility.Visible;
        }
    }
}
