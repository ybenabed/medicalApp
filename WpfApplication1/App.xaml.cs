using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using System.Security.Cryptography;
using System.Text;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static InterfaceFonctionnalité iff;
        public static GestionComptes gc;
        public static PageGestionCompte gestcpt;
        public static GestionComptes gest_adm;
        public static InterfaceFonctionnalité accueil1;
        public static Nv_acceuil acc;
        public static NvConsultation Nfc;
        public static PageFicheConsultation fichecons;
        public static auth authent;
        public App()
        {
            int notif = int.Parse(WpfApplication1.Properties.Settings.Default["Notification"].ToString());
            int deco = int.Parse(WpfApplication1.Properties.Settings.Default["Deconnexion"].ToString());
            if (notif == 0)
            {
                WpfApplication1.Properties.Settings.Default["Notification"] = 8;
                WpfApplication1.Properties.Settings.Default.Save();
            }
            if (deco == 0)
            {
                WpfApplication1.Properties.Settings.Default["Deconnexion"] = 5;
                WpfApplication1.Properties.Settings.Default.Save();
            }
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            string Command = @"SELECT * FROM DOCTOR";
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            authent = new auth();
            authent.Show();
            
        }
        public static string CrypterMdp(string passwrd)
        {
            MD5CryptoServiceProvider serv = new MD5CryptoServiceProvider();
            serv.ComputeHash(ASCIIEncoding.ASCII.GetBytes(passwrd));
            byte[] encrypted = serv.Hash;
            StringBuilder strbuild = new StringBuilder();
            for (int i = 0; i < encrypted.Length; i++)
            {
                strbuild.Append(encrypted[i].ToString("x2"));
            }
            return strbuild.ToString();
        } 
    }
}
