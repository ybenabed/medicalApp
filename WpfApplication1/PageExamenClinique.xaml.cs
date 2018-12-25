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
    /// Interaction logic for PageExamenClinique.xaml
    /// </summary>
    public partial class PageExamenClinique : Page
    {
        private int IDFC { get; set; }
        public PageExamenClinique()
        {
            InitializeComponent();
        }

        private bool IsInt(String essai)
        {
            int output;
            return int.TryParse(essai.ToString(), out output);
        }


        private void Temp_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)|| e.Key==Key.Decimal )
            {
                if (e.Key == Key.Decimal)
                {
                    if (!Temp.Text.Contains('.')) { }
                    else e.Handled = true;
                }
            }
            else e.Handled = true;
        }

        private void Pds_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Decimal)
            {
                if (e.Key == Key.Decimal)
                {
                    if (!Pds.Text.Contains('.')) { }
                    else e.Handled = true;
                }
            }
            else e.Handled = true;
        }

        private void Tay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) { }
            else e.Handled = true;
        }

        private void SuppTent_Click(object sender, RoutedEventArgs e)
        {
            Tent.Text = "";
        }

        private void SuppTemp_Click(object sender, RoutedEventArgs e)
        {
            Temp.Text = "";
        }

        private void SuppPds_Click(object sender, RoutedEventArgs e)
        {
            Pds.Text = "";
        }

        private void SuppTay_Click(object sender, RoutedEventArgs e)
        {
            Tay.Text = "";
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            IDFC = 2;
            if (Compter_IDFC() == 0)
            {
                CreerNvExamen();
            }
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = "Update Examen_Clinique SET Tention='" + Tent.Text + "'";
            Command = Command + ", Poids=" ;
            if (Pds.Text != "") Command = Command + "TRY_CONVERT(float,'"+Pds.Text+"')" ;
            else Command = Command + "null";            
            Command = Command + ", Taille=";
            if (Tay.Text != "") Command = Command + int.Parse(Tay.Text);
            else Command = Command + "null";
            Command = Command + ", Temperature=";
            if (Temp.Text != "") Command = Command + "TRY_CONVERT(float,'" + Temp.Text + "')";
            else Command = Command + "null";
            Command = Command + " where Id_FC=" + IDFC;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            Macmd.ExecuteNonQuery();
        }
        private int Compter_IDFC()
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = @"SELECT COUNT(*) from Examen_Clinique where Id_FC="+IDFC;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            SqlDataReader dr;
            dr = Macmd.ExecuteReader();
            if (dr.Read())
            {
                return ((int)dr[0]);
            }
            return 0;
        }
        private void CreerNvExamen()
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = @"INSERT INTO Examen_Clinique (Id_FC) values ("+IDFC+")";
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            Macmd.ExecuteNonQuery();
        }
    }
}
