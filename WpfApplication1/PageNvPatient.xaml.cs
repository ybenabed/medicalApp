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
using System.Net.Mail;


namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for PageNvPatient.xaml
    /// </summary>
    public partial class PageNvPatient : Page
    {
        private int idmed { get; set; }
        private bool ADMIN { get; set; }
        public PageNvPatient(int id, bool admin)
        {
            idmed = id;
            InitializeComponent();
            ADMIN = admin;
            comboSG.Items.Add("AB+"); comboSG.Items.Add("AB-"); comboSG.Items.Add("A+"); comboSG.Items.Add("A-");
            comboSG.Items.Add("B+"); comboSG.Items.Add("B-"); comboSG.Items.Add("O+"); comboSG.Items.Add("O-");
            comboSexe.Items.Add("HOMME"); comboSexe.Items.Add("FEMME");
        }
        private void vider()
        {
            Nom1.Clear();
            Prenom1.Clear();
            Adr.Clear();
            Email.Clear();
            tel.Clear();
            date.Text = null;
            comboSexe.Text = null;
            comboSG.Text = null;
        }
        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            vider();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if ((Nom1.Text == "") || (Prenom1.Text == "") || (Adr.Text == "") || (Email.Text == "") || (tel.Text == "") || (date.Text == "") || (comboSexe.Text == "") || (comboSG.Text == "") || (tel.Text.Length < 10) || (!(est_val(Email.Text))))
            {
                if (tel.Text.Length < 10)
                    nonnum.Visibility = Visibility.Visible;
                else
                    nonnum.Visibility = Visibility.Hidden;
                if ((Nom1.Text == "") || (Prenom1.Text == "") || (Adr.Text == "") || (Email.Text == "") || (tel.Text == "") || (date.Text == "") || (comboSexe.Text == "") || (comboSG.Text == ""))
                {
                    pic2.Visibility = Visibility.Hidden;
                    Pic.Visibility = Visibility.Visible;
                    Remarque.Visibility = Visibility.Visible;
                    Remarque.Content = "Champs Non Rempli(s)";
                }
                if (!(est_val(Email.Text)))
                    nonadr.Visibility = Visibility.Visible;
                else
                    nonadr.Visibility = Visibility.Hidden;
            }
            else
            {
                Patient patient = new Patient(Nom1.Text, Prenom1.Text, Email.Text, Adr.Text, tel.Text, comboSexe.Text, DateTime.Parse(date.Text), comboSG.Text);
                patient.Insert_Nv_Patient();
                Dossier_medical dossier = new Dossier_medical(patient.get_Id());
                dossier.Creer_DM();
                App.acc.pagepatients.addtoDatagGrid(Nom1.Text, Prenom1.Text, DateTime.Parse(date.Text), Adr.Text, comboSG.Text, tel.Text, patient.get_Id());
                Stackajoutersucces.Visibility = System.Windows.Visibility.Visible;
                RecStackajoutersucces.Visibility = System.Windows.Visibility.Visible;
                maingrd.IsEnabled = false;
            }
        }

        private bool est_num(string t)
        {

            int verifili;
            return int.TryParse(t, out verifili);



        }

        private void tel_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (est_num(e.Text) == false)
            e.Handled = true;
        }

        private void Nom1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string c = e.Text;
            char ef;
            char.TryParse(c, out ef);
            if ((char.IsLetter(ef) == false))
                e.Handled = true;
        }

        private bool est_char(string te)
        {
            char verifili1;
            return char.TryParse(te, out verifili1);
        }

        private bool est_val(string a)
        {
            if (a != "")
            {
                try
                {
                    MailAddress m = new MailAddress(a);
                    return true;
                }

                catch (FormatException)
                {
                    return false;
                }
            }
            else
                return false;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Stackajoutersucces.Visibility = System.Windows.Visibility.Hidden;
            RecStackajoutersucces.Visibility = System.Windows.Visibility.Hidden;
            maingrd.IsEnabled = true;
            this.vider();
            Remarque.Visibility = System.Windows.Visibility.Hidden;
            Pic.Visibility = System.Windows.Visibility.Hidden;
        }


    }
}
