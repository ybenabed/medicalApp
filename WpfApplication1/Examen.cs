using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WpfApplication1
{
    class Examen
    {
        protected int Id_Examen { get; set; }
        protected DateTime Date { get; set; }
        public Examen()
        {

        }
        public void Update_Date(String Attribut, DateTime NvlDate)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            string Command = @"Update Examen SET " + "Date_comp" + "='" + NvlDate.Date + "' where Id_Doctor=" + Id_Examen;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            Macmd.ExecuteNonQuery();
        }
    }
}
