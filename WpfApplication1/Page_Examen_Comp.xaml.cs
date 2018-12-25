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
    /// Interaction logic for Page_Examen_Comp.xaml
    /// </summary>
    public partial class Page_Examen_Comp : Page
    {
        public struct DataofExm
        {
            public int numero;
            public string type { get; set; }
            public string conclusion { get; set; }
        }
        private DataTable table { get; set; }
        private int IDDOSS { get; set; }
        private Uri Fichier { get; set; }
        private bool ADMIN { get; set; }
        private int IDMED { get; set; }
        private int IDPAT { get; set; }
        public Page_Examen_Comp(int iddoss,int idmed, bool admin, bool consulter,int idpat)
        {
            InitializeComponent();
            if (!consulter)
            {
                this.GridBarre.Visibility = Visibility.Collapsed;
                this.GridMain.Height += 60;
                this.DatagGrid.Height += 60;
            }
            IDDOSS = iddoss; ADMIN = admin; IDMED = idmed; IDPAT = idpat;
            loadExm();
            Visualiser.IsEnabled = false;
            this.AffichageStack.Visibility = Visibility.Visible;
            this.Affichage2.Visibility = Visibility.Hidden;
            this.DonnerExam.Visibility = Visibility.Hidden;
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
                    this.DonnerExam.Visibility = Visibility.Hidden;
                    break;
                case 1:
                    this.AffichageStack.Visibility = Visibility.Hidden;
                    this.Affichage2.Visibility = Visibility.Visible;
                    this.DonnerExam.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    this.AffichageStack.Visibility = Visibility.Hidden;
                    this.Affichage2.Visibility = Visibility.Hidden;
                    this.DonnerExam.Visibility = Visibility.Visible;
                    break;
            }
        }
        private void loadExm()
        {
            table = new DataTable();
            DataGridTextColumn g0 = new DataGridTextColumn();
            DataGridTextColumn g1 = new DataGridTextColumn();
            DataGridTextColumn g2 = new DataGridTextColumn();
            g0.Binding = new Binding("numero"); g0.Header = ""; g0.MaxWidth = 0;
            g1.Binding = new Binding("type"); g1.Header = "Type Examen"; g0.Width = 150;
            g2.Binding = new Binding("conclusion"); g2.Header = "Conclusion"; g2.MaxWidth = 300; g2.MinWidth = 300;
            DatagGrid.Columns.Add(g0); DatagGrid.Columns.Add(g1); DatagGrid.Columns.Add(g2);
            DatagGrid.Columns[0].DisplayIndex = 3;
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = "SELECT * from Examen_Complementaire where Id_Dossier="+IDDOSS;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            SqlDataReader dr = Macmd.ExecuteReader();
            while (dr.Read())
            {
                DataofExm ordodata = new DataofExm();
                ordodata.numero = int.Parse(dr[0].ToString());
                ordodata.type = dr[2].ToString();
                ordodata.conclusion = dr[3].ToString();
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
                DataofExm dfg = (DataofExm)(DatagGrid.SelectedItem);
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
        private void BrowseForPic_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog parcouri = new OpenFileDialog();
            parcouri.Filter = "Fichier PDF|*.pdf|Fichiers Word|*.docx|Fichier Text|*.txt|Fichiers Excel|*.xl*";
            if (parcouri.ShowDialog() == true)
            {
                String Nomfich = parcouri.FileName;
                FilePath.Text = Nomfich;
                String Ext = Nomfich.Remove(0, Nomfich.LastIndexOf(".") + 1);
                Fichier = new Uri(Nomfich, UriKind.RelativeOrAbsolute);
                Visualiser.IsEnabled = true;
            }
        }

        private void Creer_nvl_Exam_Comp()
        {

        }
        private BitmapImage Photo(byte[] imgg)
        {
            if (imgg == null) return null;

            BitmapImage pic = new BitmapImage();

            using (MemoryStream Ms = new MemoryStream(imgg, 0, imgg.Length))
            {
                Ms.Write(imgg, 0, imgg.Length);
                Ms.Seek(0, SeekOrigin.Begin);
                pic.BeginInit();
                pic.CacheOption = BitmapCacheOption.OnLoad;
                pic.StreamSource = Ms;
                pic.EndInit();
                return pic;
            }

        }

        private void Visualiser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = Fichier.LocalPath;
                process.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (Type.Text != "" && Conclustion.Text != "")
            {
                Examen_Complémentaire exm = new Examen_Complémentaire(Type.Text, Conclustion.Text, FilePath.Text, IDDOSS);
                exm.Insert_Exm_Comp();
                DataofExm ordodata = new DataofExm();
                ordodata.numero = exm.Get_Id();
                ordodata.type = Type.Text;
                ordodata.conclusion = Conclustion.Text;
                DatagGrid.Items.Add(ordodata);
                MessageBox.Show("Examen enregistré avec succès");
                App.iff.support_fonction.NavigationService.Navigate(new Page_Examen_Comp(IDDOSS, IDMED, ADMIN, true,IDPAT));
                this.AffichageStack.Visibility = Visibility.Hidden;
                this.Affichage2.Visibility = Visibility.Visible;
                this.DonnerExam.Visibility = Visibility.Hidden;
                this.vider();
            }
            else MessageBox.Show("Remplissez toutes les cases!");
        }

        private void Quitter_Click(object sender, RoutedEventArgs e)
        {
        }
        private void Modifier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Supprimer_Click_1(object sender, RoutedEventArgs e)
        {
            if (DatagGrid.SelectedIndex >= 0)
            {
                DataofExm dataa = (DataofExm)(DatagGrid.SelectedItem);
                ConnexionBDD Datab = new ConnexionBDD();
                Datab.connecter();
                String Command = "DELETE FROM Examen_Complementaire where Id_Ex_Comp=" + dataa.numero;
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

        private void Annee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.TYPE.SelectedIndex > 0)
            {
                string selected = ((ComboBoxItem)TYPE.SelectedItem).Content.ToString();
                DataTable tmp = table.Copy();
                for (int i = tmp.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow row = tmp.Rows[i];
                    if (row[2].ToString() != selected) tmp.Rows.RemoveAt(i);
                }
                Rempliravec(tmp);
            }
            else
            {
                Rempliravec(table);
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
                DataofExm ordodata = new DataofExm();
                ordodata.numero = int.Parse(dr[0].ToString());
                ordodata.type = dr[2].ToString();
                ordodata.conclusion = dr[3].ToString();
                DatagGrid.Items.Add(ordodata);
            }
        }

        private void DatagGrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete) Supprimer_Click_1(sender, new RoutedEventArgs());
        }

        private void Imprimer_Click(object sender, RoutedEventArgs e)
        {
            string typeExa = this.TypeExamen.Text;
            string cs = this.cause.Text;
            if (typeExa != "" && cs != "")
            {
                // *****************************  USING iTextSharp  ************************************   
                string path = ""; DateTime dat = new DateTime();
                Misenforme mm = new Misenforme();
                mm.get_Patient(IDPAT, ref path, ref dat);
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Fichiers PDF|*.pdf";
                string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                path = documents + save.InitialDirectory + @"\CureIt\" + path + "_" + IDPAT + @"\Examens complémentaires";
                MessageBox.Show(path);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                save.InitialDirectory = path;
                save.FileName = "Fiche" +DateTime.Now.Year+DateTime.Now.Month+DateTime.Now.Day+ DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
                if (save.ShowDialog() == true)
                {
                    MiseEnFormLettre form = new MiseEnFormLettre(IDDOSS, IDMED);
                    String nomdoc = save.FileName;

                    Document doc = form.CreateDoc(nomdoc);
                    doc.Open();
                    doc = form.Remplir_Exam_comp(doc, IDMED, IDDOSS, typeExa, cs);
                    doc.Close();
                    System.Diagnostics.Process.Start(nomdoc);
                }
                TypeExamen.Text = null;
                cause.Text = null;
             
            }
            else
            {
                //Case vide
            }
        }
        public void vider()
        {
            Type.Text = null;
            Conclustion.Text = null;
            FilePath.Text = null;
        }
    }
}
