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
    /// Interaction logic for PageGestionCompte.xaml
    /// </summary>
    public partial class PageGestionCompte : Page
    {
        public struct Dataofgrid
        {
            public string Nom { get; set; }
            public string Prenom { get; set; }
            public string Spécialité { get; set; }
            public string Username { get; set; }
            public string Type { get; set; }
            public string Etat { get; set; }
        }
        private System.Threading.Timer timer { get; set; }
        private DataTable table { get; set; }
        public PageGestionCompte()
        {
            InitializeComponent();
            loadCompte();
        }
        private void butt_modif_Click(object sender, RoutedEventArgs e)
        {
            if (DatagGrid.SelectedIndex != -1)
            {
                Dataofgrid dfg = (Dataofgrid)(DatagGrid.SelectedItem);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    if (row[3].ToString() == dfg.Username)
                    {
                        page_modifier_support.NavigationService.Navigate(new PageModifierCompte(dfg.Username, dfg.Spécialité, row[6].ToString(), dfg.Type));
                        page_modifier_support.Visibility = System.Windows.Visibility.Visible;
                    }
                }
            }
            else
            {
                Stackcptselcet.Visibility = System.Windows.Visibility.Visible;
                RecStackcptselcet.Visibility = System.Windows.Visibility.Visible;
                Grdcpt.IsEnabled = false;
            }
        }
        private void loadCompte()
        {
            table = new DataTable();
            DataGridTextColumn g0 = new DataGridTextColumn();
            DataGridTextColumn g1 = new DataGridTextColumn();
            DataGridTextColumn g2 = new DataGridTextColumn();
            DataGridTextColumn g3 = new DataGridTextColumn();
            DataGridTextColumn g4 = new DataGridTextColumn();
            DataGridTextColumn g5 = new DataGridTextColumn();
            g0.Binding = new Binding("Nom");
            g1.Binding = new Binding("Prenom");
            g2.Binding = new Binding("Spécialité");
            g3.Binding = new Binding("Username");
            g4.Binding = new Binding("Type");
            g5.Binding = new Binding("Etat");
            g0.Header = "Nom"; g1.Header = "Prenom"; g2.Header = "Spécialité";
            g3.Header = "Username"; g4.Header = "Type compte"; g5.Header = "Etat";
            DatagGrid.Columns.Add(g0); DatagGrid.Columns.Add(g1); DatagGrid.Columns.Add(g2);
            DatagGrid.Columns.Add(g3); DatagGrid.Columns.Add(g4); DatagGrid.Columns.Add(g5);
            DatagGrid.Columns[0].DisplayIndex = 6;
            DatagGrid.Columns.Remove(g5);
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = "Select Nom,Prenom,Spécialité,Username,CompteAdmin as [Type compte],EtatCompte as [Etat compte],Passwordd from Doctor LEFT OUTER JOIN Person on Doctor.Id_Person=Person.Id_Person where CompteAdmin!='Admin'";
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            SqlDataReader dr = Macmd.ExecuteReader();
            while (dr.Read())
            {
                Dataofgrid gg = new Dataofgrid();
                gg.Nom = dr[0].ToString(); gg.Prenom = dr[1].ToString();
                gg.Spécialité = dr[2].ToString(); gg.Username = dr[3].ToString();
                gg.Type = dr[4].ToString();
                if (dr[5].ToString() == "1") gg.Etat = "Désactiver";
                else gg.Etat = "Activer";
                DatagGrid.Items.Add(gg);
            }
            dr.Close();
            SqlDataAdapter Daptr = new SqlDataAdapter(Macmd);
            Daptr.Fill(table);
        }

        private void btnGrid_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int i;
            if (btn.Content.ToString() == "Activer")
            {
                i = 1;
                btn.Content = "Désactiver";
            }
            else
            {
                i = 0;
                btn.Content = "Activer";
            }
            try
            {
                Dataofgrid dfg = (Dataofgrid)(DatagGrid.SelectedItem);
                ConnexionBDD Datab = new ConnexionBDD();
                Datab.connecter();
                String Command = "update Doctor set EtatCompte=" + i + " where Username='" + dfg.Username + "'";
                SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
                Macmd.ExecuteNonQuery();
                Datab.deconnecter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void modifier_Click(object sender, RoutedEventArgs e)
        {
            if (DatagGrid.SelectedIndex != -1)
            {
                Dataofgrid dfg = (Dataofgrid)(DatagGrid.SelectedItem);

            }
            else
            {
                MessageBox.Show("Aucun compte sélectionné");
            }
        }

        private void med_nom_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
            DataTable tmp = table.Copy();
            DataRow row;
            int i;
            if (med_nom.Text != "")
            {
                if (med_prenom.Text != "")
                {
                    //Rechercher par Nom et Prenom
                    for (i = table.Rows.Count - 1; i >= 0; i--)
                    {
                        row = tmp.Rows[i];
                        if (row[0].ToString().ToUpper().StartsWith(med_nom.Text.ToUpper()) && row[1].ToString().ToUpper().StartsWith(med_prenom.Text.ToUpper())) { }
                        else
                        {
                            tmp.Rows.RemoveAt(i);
                        }
                    }
                    remplirAvec(tmp);
                }
                else
                {
                    //Rechercher par Nom
                    for (i = table.Rows.Count - 1; i >= 0; i--)
                    {
                        row = tmp.Rows[i];
                        if (row[0].ToString().ToUpper().StartsWith(med_nom.Text.ToUpper())) { }
                        else
                        {
                            tmp.Rows.RemoveAt(i);
                        }
                    }
                    remplirAvec(tmp);
                }
            }
            else
            {
                if (med_prenom.Text != "")
                {
                    //Recherch par Prenom
                    for (i = table.Rows.Count - 1; i >= 0; i--)
                    {
                        row = tmp.Rows[i];
                        if (row[1].ToString().ToUpper().StartsWith(med_prenom.Text.ToUpper())) { }
                        else
                        {
                            tmp.Rows.RemoveAt(i);
                        }
                    }
                    remplirAvec(tmp);
                }
                else
                {
                    //Affichage de tous les patients
                    remplirAvec(table);
                }
            }
        }
        private void remplirAvec(DataTable tabledata)
        {
            for (int i = DatagGrid.Items.Count - 1; i >= 0; i--)
            {
                DatagGrid.Items.RemoveAt(i);
            }
            for (int i = tabledata.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = tabledata.Rows[i];
                Dataofgrid gg = new Dataofgrid();
                gg.Nom = dr[0].ToString(); gg.Prenom = dr[1].ToString();
                gg.Spécialité = dr[2].ToString(); gg.Username = dr[3].ToString();
                gg.Type = dr[4].ToString();
                if (dr[5].ToString() == "1") gg.Etat = "Désactiver";
                else gg.Etat = "Activer";
                DatagGrid.Items.Add(gg);
            }
        }

        private void buttokcpt_Click(object sender, RoutedEventArgs e)
        {
            Stackcptselcet.Visibility = System.Windows.Visibility.Hidden;
            RecStackcptselcet.Visibility = System.Windows.Visibility.Hidden;
            Grdcpt.IsEnabled = true ;
        }
    }
}
