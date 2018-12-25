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
using System.Windows.Xps.Packaging;
using Word = Microsoft.Office.Interop.Word;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Page_Ordonnance.xaml
    /// </summary>
    public partial class Page_Ordonnance : Page
    {
        public struct DataofOrdo
        {
            public int numero;
            public DateTime date { get; set; }
        }
        private DataTable table { get; set; }
        protected class Medic_a_modif
        {
            protected String Nom_medic;
            protected String formedic;
            public Medic_a_modif(String n, String f)
            {
                Nom_medic = n;
                formedic = f;
            }
            public String Chaine()
            {
                return ("WHERE Id_Medicament='" + Nom_medic + "' AND Forme='" + formedic + "'");
            }
        }
        private Ordonnance ordonn;
        private String nom_medi { get; set; }
        private String dose { get; set; }
        private String forme { get; set; }
        private int quantite { get; set; }
        private String utilisation { get; set; }
        private String durer { get; set; }
        private Medic_a_modif medic { get; set; }
        public int nbre_medic;
        public Boolean BienSelect = false;
        private DataTable data { get; set; }
        public int taille = 0;
        private int idOrdo { get; set; }
        private int idPat { get; set; }
        private int idMed { get; set; }
        private int IDDOSS { get; set; }
        private int IDFC { get; set; }
        private bool ADMIN { get; set; }
        public Page_Ordonnance(int idpat, int idmed, int idfc, int iddoss, bool admin, bool consulter)
        {
            InitializeComponent();
            if (!consulter)
            {
                this.GridBarre.Visibility = Visibility.Collapsed;
                this.Affichage2.Visibility = Visibility.Hidden;
                this.Visualisation.Visibility = Visibility.Visible;
                //this.GridMain.Height += 60;
                //this.DatagGrid.Height += 60;
                Close_.Visibility = Visibility.Hidden;
            }
            else
            {
                Visualisation.Visibility = Visibility.Hidden;
                Affichage2.Visibility = Visibility.Visible;
                Close_.Visibility = Visibility.Visible;
            }
            IDDOSS = iddoss; idPat = idpat; idMed = idmed; IDFC = idfc; ADMIN = admin;
            ordonn = new Ordonnance(IDFC);
            nbre_medic = 0;
            Affich_Medic_Ajouté.Margin = new Thickness(5, 5, 5, 5);
            this.updateButton.IsEnabled = false;
            data = new DataTable();
            Medicament.Select_Medic(data);
            datag_medic.ItemsSource = data.DefaultView;
            //this.AffichageStack.Visibility = Visibility.Visible;
            //this.Affichage2.Visibility = Visibility.Hidden;
            loadOrdo();
            this.Mois.Items.Add(""); this.Annee.Items.Add("");
            for (int i = 1; i < 13; i++) this.Mois.Items.Add(i);
            for (int i = 2010; i < 2101; i++) this.Annee.Items.Add(i);
        }
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    int index = int.Parse(((Button)e.Source).Uid);
        //    GridCursor.Margin = new Thickness(10 + (200 * index), 45, 0, 10);

        //    switch (index)
        //    {
        //        case 0:
        //            this.AffichageStack.Visibility = Visibility.Visible;
        //            this.Affichage2.Visibility = Visibility.Hidden;
        //            break;
        //        case 1:
        //            this.AffichageStack.Visibility = Visibility.Hidden;
        //            this.Affichage2.Visibility = Visibility.Visible;
        //            break;
        //    }
        //}
        private void loadOrdo()
        {
            table = new DataTable();
            DataGridTextColumn g0 = new DataGridTextColumn();
            DataGridTextColumn g1 = new DataGridTextColumn();
            g0.Binding = new Binding("numero"); g0.Header = ""; g0.MaxWidth = 0;
            g1.Binding = new Binding("date"); g1.Header = "Date"; g1.ClipboardContentBinding.StringFormat = "d";
            DatagGrid.Columns.Add(g0); DatagGrid.Columns.Add(g1);
            DatagGrid.Columns[0].DisplayIndex = 2;
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command;
            if (ADMIN) Command = "SELECT * from Ordonnance where Id_FC in (SELECT Id_FC from Contient where Id_DM=" + IDDOSS + ")";
            else Command = "SELECT * from Ordonnance where Id_FC in (select Id_Fiche_Consultation from Fiche_Consultation where Id_Fiche_Consultation in (SELECT Id_FC from Contient where Id_DM=" + IDDOSS + ") and Id_Med=" + idMed + ")";
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            SqlDataReader dr = Macmd.ExecuteReader();
            while (dr.Read())
            {
                DataofOrdo ordodata = new DataofOrdo();
                ordodata.numero = int.Parse(dr[0].ToString());
                ordodata.date = DateTime.Parse(dr[2].ToString());
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
                DataofOrdo dfg = (DataofOrdo)(DatagGrid.SelectedItem);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    if (row[0].ToString() == dfg.numero.ToString())
                    {
                        string path = row[3].ToString();
                        if (File.Exists((string)path))
                        {
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
                        else
                        {
                            //Fichier n'existe pas vous voulez creer un nouveau ? 
                        }
                        
                    }
                }
            }
            else
            {
                MessageBox.Show("Aucun compte sélectionné");
            }
        }
        private void Nom_Medic_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.Etat_dajout.Visibility == System.Windows.Visibility.Visible)
            {
                this.Etat_dajout.Visibility = System.Windows.Visibility.Hidden;
                this.Etat_dajoutTxt.Visibility = System.Windows.Visibility.Hidden;
            }
            int i = -1;
            String nom = Nom_Medic.Text;
            DataTable dt = data.Copy();
            for (i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow row = dt.Rows[i];
                if (row[0].ToString().StartsWith(nom.ToUpper()))
                {

                }
                else
                {
                    dt.Rows.RemoveAt(i);
                }
            }
            BienSelect = false;
            datag_medic.ItemsSource = dt.DefaultView;


        }
        public bool verif()
        {
            if ((Nom_Medic.Text != "") && (Dose_Medic.Text != "") && (Quant_Medic.Text != "") && (Forme_Medic.Text != "") && (frequence.Text != "") && (Nbre_fois.Text != "")) return true;
            else return false;
        }

        public void vider()
        {
            Nom_Medic.Clear(); Dose_Medic.Text = ""; Quant_Medic.Text = ""; Forme_Medic.Text = ""; frequence.Text = ""; Nbre_fois.Text = ""; Dure.Text = "";
            BienSelect = false;
            taille = 0;
            datag_medic.ItemsSource = data.DefaultView;
        }
        private void addMedic()
        {
            nom_medi = Nom_Medic.Text;
            dose = Dose_Medic.Text;
            forme = Forme_Medic.Text;
            quantite = int.Parse(Quant_Medic.Text);
            utilisation = Nbre_fois.Text + " chaque " + frequence.Text;
            durer = Dure.Text;
            Medicament medica = new Medicament(nom_medi, dose, forme, quantite, utilisation, durer);
            if (nbre_medic == 0)
            {
                ordonn.Insert_Ordonnance();
                this.idOrdo = ordonn.Get_Ordo();
            }
            medica.Prescrire_medic(ordonn.Get_Ordo());
            nbre_medic++;
            vider();
        }
        private void Button_Click_Ajouter(object sender, RoutedEventArgs e)
        {
            bool Repet = false;
            if (!verif()) //Données manquantes
            {
                this.Etat_dajoutTxt.Content = "Verifier la saisie de tous les champs!";
                this.Etat_dajoutTxt.Visibility = System.Windows.Visibility.Visible;
                this.Etat_dajout.Source = Animations.GetImage(@"Images\False.png");
                this.Etat_dajout.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                if (BienSelect == false)
                {

                }
                else
                {
                    if (nbre_medic != 0) //Si l'ordonnance contient déja des medicament on verifie si le medicament entrés n'existe pas déja
                    {
                        int cpt = CompterMedic(ordonn.Get_Ordo(), Nom_Medic.Text, Forme_Medic.Text);
                        if (cpt != 0)
                        {
                            Repet = true;
                            this.Etat_dajoutTxt.Content = "Medicament Dupliqué";
                            this.Etat_dajoutTxt.Visibility = System.Windows.Visibility.Visible;
                            this.Etat_dajout.Source = Animations.GetImage(@"Images\False.png");
                            this.Etat_dajout.Visibility = System.Windows.Visibility.Visible;
                        }
                    }
                    if (!Repet)
                    {
                        addMedic();
                        addtoListview();
                        this.Etat_dajoutTxt.Content = "Medicament ajouté avec succés";
                        this.Etat_dajoutTxt.Visibility = System.Windows.Visibility.Visible;
                        this.Etat_dajout.Source = Animations.GetImage(@"Images\True.png");
                        this.Etat_dajout.Visibility = System.Windows.Visibility.Visible;
                    }
                    else vider();
                }
            }
        }
        public void addtoListview()
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String command = "SELECT Id_Medicament AS Médicament,Forme,Dose,Quantité,Dure,Nbre_Utilisation AS Utilisation FROM Prescrire WHERE Id_Ordonnance=" + ordonn.Get_Ordo();
            SqlCommand Macmd = new SqlCommand(command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            SqlDataAdapter datAdap = new SqlDataAdapter(Macmd);
            DataTable DT = new DataTable("Prescrire");
            datAdap.Fill(DT);
            Affich_Medic_Ajouté.ItemsSource = DT.DefaultView;
            datAdap.Update(DT);
        }


        public int CompterMedic(int idord, String nomMedic, String Form)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            Datab.cmd.CommandType = CommandType.StoredProcedure;
            Datab.cmd.CommandText = "SP_CompterMedic";
            Datab.cmd.Parameters.Add("@IdOrdo", SqlDbType.Int).Value = idord;
            Datab.cmd.Parameters.Add("@Medic", SqlDbType.NVarChar, 50).Value = nomMedic;
            Datab.cmd.Parameters.Add("@Form", SqlDbType.NVarChar, 30).Value = Form;
            SqlParameter Sort = new SqlParameter("@compt", SqlDbType.Int);
            Sort.Direction = ParameterDirection.Output;
            Datab.cmd.Parameters.Add(Sort);
            Datab.cmd.Connection = Datab.cnx;
            Datab.cmd.ExecuteNonQuery();
            Datab.deconnecter();
            return ((int)Sort.Value);
        }
        private void Nom_Medic_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            if (this.Etat_dajout.Visibility == System.Windows.Visibility.Visible)
            {
                this.Etat_dajout.Visibility = System.Windows.Visibility.Hidden;
                this.Etat_dajoutTxt.Visibility = System.Windows.Visibility.Hidden;
            }

        }
        private void Button_Click_Modifier(object sender, RoutedEventArgs e)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            if (!BienSelect)
            {

                /*this.Etat_dajoutTxt.Content = "Médicament non valide";
                this.Etat_dajoutTxt.Visibility = System.Windows.Visibility.Visible;
                this.Etat_dajout.Source = Animations.GetImage(@"False.png");
                this.Etat_dajout.Visibility = System.Windows.Visibility.Visible;*/
            }
            else
            {
                String nomedic = Nom_Medic.Text; String forme = Forme_Medic.Text; String utilis = Nbre_fois.Text + " chaque " + frequence.Text;
                String dose = Dose_Medic.Text; int quan = int.Parse(Quant_Medic.Text); String durer = Dure.Text;
                String Command = @"UPDATE Prescrire SET Id_Medicament='" + nomedic + "', Dose='" + dose + "', Forme='" + forme + "', Dure='" + durer + "', Quantité=" + quan + ", Nbre_Utilisation='" + utilis + "' " + medic.Chaine();
                SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
                Macmd.ExecuteNonQuery();
                this.vider();
                this.addtoListview();
                this.Etat_dajoutTxt.Content = "Medicament modifié avec succés";
                this.Etat_dajoutTxt.Visibility = System.Windows.Visibility.Visible;
                this.Etat_dajout.Source = Animations.GetImage(@"True.png");
                this.Etat_dajout.Visibility = System.Windows.Visibility.Visible;
                addButton.IsEnabled = true; updateButton.IsEnabled = false;
                Datab.deconnecter();
            }
        }
        private void Affich_Medic_Ajouté_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid Dg = sender as DataGrid;
            DataRowView Drv = Dg.SelectedItem as DataRowView;
            if (Drv != null)
            {
                Nom_Medic.Text = Drv["Médicament"].ToString();
                Dose_Medic.Text = Drv["Dose"].ToString();
                Forme_Medic.Text = Drv["Forme"].ToString();
                Dure.Text = Drv["Dure"].ToString();
                Quant_Medic.Text = Drv["Quantité"].ToString();
                string Utilisation = Drv["Utilisation"].ToString();
                String util = Utilisation.Remove(Utilisation.IndexOf("c"), 7);
                Nbre_fois.Text = Utilisation.Remove(util.IndexOf("s") + 1);
                frequence.Text = util.Remove(0, util.IndexOf("s") + 2);
                medic = new Medic_a_modif(Nom_Medic.Text, Forme_Medic.Text);
                addButton.IsEnabled = false; updateButton.IsEnabled = true;
                BienSelect = true;
            }
        }
        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {

            DataGrid Dg = (Affich_Medic_Ajouté) as DataGrid;
            DataRowView Drv = Dg.SelectedItem as DataRowView;
            if (Drv != null)
            {
                String nomedic = Drv["Médicament"].ToString();
                String forme = Drv["Forme"].ToString();

                Enable_Disable(false);
                Confirmer.Content = null;
                //
                //
                //Confirmer la supression
                //
                //
            }
        }
        public void Enable_Disable(Boolean value)
        {
            this.Nom_Medic.IsEnabled = value; this.Forme_Medic.IsEnabled = value; this.Dose_Medic.IsEnabled = value;
            this.Quant_Medic.IsEnabled = value; this.Dure.IsEnabled = value; this.Nbre_fois.IsEnabled = value;
            this.frequence.IsEnabled = value; this.addButton.IsEnabled = value; this.updateButton.IsEnabled = value;
            this.Affich_Medic_Ajouté.IsEnabled = value;
        }
        private void Nom_Medic_KeyDown(object sender, KeyEventArgs e)
        {
            VerifLettre(e);
        }
        public void VerifLettre(KeyEventArgs e)
        {
            if ((e.Key >= Key.A && e.Key <= Key.Z) || e.Key == Key.Tab)
            {
            }
            else e.Handled = true;
        }

        private void Imprimer_Click(object sender, RoutedEventArgs e)
        {
            if (nbre_medic > 0)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Fichiers Word|*.docx";
                save.FileName = "Ordonnance" + ordonn.Get_Ordo();
                if (save.ShowDialog() == true)
                {
                    string sav = save.FileName;
                    Misenforme mis = new Misenforme();
                    mis.CreateWordDocument(sav, this.idMed, this.idPat, this.idOrdo);
                    ConnexionBDD Datab = new ConnexionBDD();
                    Datab.connecter();
                    String Command = "update Ordonnance set Fichier_Ordo='" + save.FileName + "' where Id_Ordonnance=" + ordonn.Get_Ordo();
                    SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
                    Macmd.ExecuteNonQuery();
                }
            }
            else
            {
                MessageBox.Show("Vide");
            }
        }

        private void datag_medic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid Dg = sender as DataGrid;
            DataRowView Drv = Dg.SelectedItem as DataRowView;
            if (Drv != null)
            {
                Nom_Medic.Text=Drv[0].ToString();
                BienSelect = true;
            }
        }

        private void Supprimer_Click_1(object sender, RoutedEventArgs e)
        {
            if (DatagGrid.SelectedIndex >= 0)
            {
                DataofOrdo dataa = (DataofOrdo)(DatagGrid.SelectedItem);
                ConnexionBDD Datab = new ConnexionBDD();
                Datab.connecter();
                String Command = "DELETE FROM Ordonnance where Id_Ordonnance=" + dataa.numero;
                SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
                Macmd.ExecuteNonQuery();
                Datab.deconnecter();
                DatagGrid.Items.Remove(dataa);
            }
        }

        private void Terminer_Click(object sender, RoutedEventArgs e)
        {
            if (nbre_medic > 0)
            {
                //
                string path=""; DateTime dat= new DateTime();
                Misenforme mm = new Misenforme();
                mm.get_Patient(this.idPat,ref path,ref dat);
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Fichiers Word|*.docx";
                string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                path = documents + save.InitialDirectory + @"\CureIt\" + path + "_" + this.idPat + @"\Ordonnances";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                save.InitialDirectory = path;
                save.FileName = "Ordonnance" + ordonn.Get_Ordo();
                if (save.ShowDialog() == true)
                {
                    string sav = save.FileName;
                    Misenforme mis = new Misenforme();
                    mis.CreateWordDocument(sav, this.idMed, this.idPat, this.idOrdo);
                    ConnexionBDD Datab = new ConnexionBDD();
                    Datab.connecter();
                    string filename = save.FileName.Replace("'", "''");
                    String Command = "update Ordonnance set Fichier_Ordo='" + filename + "' where Id_Ordonnance=" + ordonn.Get_Ordo();
                    SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
                    Macmd.ExecuteNonQuery();
                }
                //
                vider();
                DataofOrdo ordodata = new DataofOrdo();
                ordodata.numero = ordonn.Get_Ordo();
                ordodata.date = DateTime.Now;
                DatagGrid.Items.Add(ordodata);
                var dataa = new DataTable();
                Affich_Medic_Ajouté.ItemsSource = dataa.DefaultView;
                nbre_medic = 0;
                App.iff.support_fonction.NavigationService.Navigate(new Page_Ordonnance(idPat, idMed, IDFC, IDDOSS, ADMIN, true));
                this.AffichageStack.Visibility = Visibility.Hidden;
                this.Affichage2.Visibility = Visibility.Visible;
                //for (int i = Affich_Medic_Ajouté.Items.Count - 1; i >= 0; i--) Affich_Medic_Ajouté.Items.RemoveAt(i);
                //ffich_Medic_Ajouté.Items.Clear();
            }
        }

        private void DatagGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatagGrid.SelectedIndex >= 0) Supprimer.IsEnabled = true;
            else Supprimer.IsEnabled = false;
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
                        DateTime datee = DateTime.Parse(row[2].ToString());
                        if ((datee.Year == int.Parse(this.Annee.SelectedItem.ToString())) && (datee.Month == int.Parse(this.Mois.SelectedItem.ToString()))) { }
                        else tmp.Rows.RemoveAt(i);
                    }
                }
                else
                {
                    for (i = tmp.Rows.Count - 1; i >= 0; i--)
                    {
                        row = tmp.Rows[i];
                        DateTime datee = DateTime.Parse(row[2].ToString());
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
                        DateTime datee = DateTime.Parse(row[2].ToString());
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
                DataofOrdo ordodata = new DataofOrdo();
                ordodata.numero = int.Parse(dr[0].ToString());
                ordodata.date = DateTime.Parse(dr[2].ToString());
                DatagGrid.Items.Add(ordodata);
            }
        }

        private void DatagGrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete) Supprimer_Click_1(sender, new RoutedEventArgs());    
        }

        private void ouvr_Click(object sender, RoutedEventArgs e)
        {
            if (DatagGrid.SelectedIndex != -1)
            {
                DataofOrdo dfg = (DataofOrdo)(DatagGrid.SelectedItem);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    if (row[0].ToString() == dfg.numero.ToString())
                    {
                        string path = row[3].ToString();
                        if  (!File.Exists(path))
                        {
                            MessageBox.Show("Le fichier est invalide!");
                        }
                        else
                        {
                            Visualisation.Visibility = Visibility.Visible;
                            Affichage2.Visibility = Visibility.Hidden;
                            string convertedXpsDoc = string.Concat(System.IO.Path.GetTempPath(), "\\", Guid.NewGuid().ToString(), ".xps");
                            XpsDocument xpsDocument = ConvertWordToXps(path, convertedXpsDoc);
                            if (xpsDocument != null) Viewer.Document = xpsDocument.GetFixedDocumentSequence();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Aucune ligne sélectionnée!");
            }
        }

        private void Close__Click(object sender, RoutedEventArgs e)
        {
            Visualisation.Visibility = Visibility.Hidden;
            Affichage2.Visibility = Visibility.Visible;
        }
        private void openDirectory_Click(object sender, RoutedEventArgs e)
        {
            if (DatagGrid.SelectedIndex != -1)
            {
                DataofOrdo dfg = (DataofOrdo)(DatagGrid.SelectedItem);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    if (row[0].ToString() == dfg.numero.ToString())
                    {
                        string path = row[3].ToString();
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
            else
            {
                MessageBox.Show("Aucune ligne sélectionnée!");
            }
        }
        private XpsDocument ConvertWordToXps(string wordFilename, string xpsFilename)
        {
            // Create a WordApplication and host word document
            Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            try
            {
                wordApp.Documents.Open(wordFilename);

                // To Invisible the word document
                wordApp.Application.Visible = false;

                // Minimize the opened word document
                wordApp.WindowState = Word.WdWindowState.wdWindowStateMinimize;

                Word.Document doc = wordApp.ActiveDocument;

                doc.SaveAs(xpsFilename, Word.WdSaveFormat.wdFormatXPS);

                XpsDocument xpsDocument = new XpsDocument(xpsFilename, FileAccess.Read);
                return xpsDocument;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurs, The error message is  " + ex.ToString());
                return null;
            }
            finally
            {
                wordApp.Documents.Close();
                ((Word._Application)wordApp).Quit(Word.WdSaveOptions.wdDoNotSaveChanges);
            }
        }

    }
}
