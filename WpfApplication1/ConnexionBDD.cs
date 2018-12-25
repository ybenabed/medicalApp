using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WpfApplication1
{
    class ConnexionBDD
    {
        public SqlConnection cnx = new SqlConnection();
        public SqlCommand cmd = new SqlCommand();
        public void connecter()
        {
            if (cnx.State == ConnectionState.Closed)
            {
                string chemin = System.IO.Directory.GetCurrentDirectory();
                int d = chemin.LastIndexOf(@"\");
                chemin = chemin.Remove(d);
                d = chemin.LastIndexOf(@"\");
                chemin = chemin.Remove(d);
                string lien = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + chemin + @"\Database1.mdf;Integrated Security=True";
                cnx.ConnectionString = lien;
                cnx.Open();
                //string chemin = System.IO.Directory.GetCurrentDirectory();
                //string lien = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + chemin + @"\Database1.mdf;Integrated Security=True";
                //cnx.ConnectionString = lien;
                //cnx.Open();
            }
        }
        //public String toString()
        //{
        //    string chemin = System.IO.Directory.GetCurrentDirectory();
        //    string lien = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + chemin + @"\Database1.mdf;Integrated Security=True";
        //    cnx.ConnectionString = lien;
        //    return lien;
        //}
        public void deconnecter()
        {
            if (cnx.State == ConnectionState.Open)
            {
                cnx.Close();
            }
        }
    }
}
