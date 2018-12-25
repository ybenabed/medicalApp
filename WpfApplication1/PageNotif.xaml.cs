using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for PageNotif.xaml
    /// </summary>
    public partial class PageNotif : Page
    {
        public PageNotif(DateTime date,String lieu,String nomprenom)
        {
            InitializeComponent();
            TimeSpan ts = date.Subtract(DateTime.Now);
            Contenu.Text = "Vous avez un rendez-vous aprés ";
            if (ts.Hours != 0)
            {
                Contenu.Text += ts.Hours + "heure(s) et " + ts.Minutes + " minute(s) ";
            }
            else
            {
                if (ts.Minutes != 0) Contenu.Text += ts.Minutes + " minute(s) ";
                else Contenu.Text += ts.Seconds + "seconde(s) ";
            }
            if (nomprenom != " ") Contenu.Text += "avec "+nomprenom;
            Contenu.Text += "\nLieu: " + lieu;
            //this.Opacity = 0.5f;
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            App.acc.suppor_notif.Visibility = Visibility.Hidden;
        }
    }
}
