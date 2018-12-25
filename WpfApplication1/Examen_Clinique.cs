using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WpfApplication1
{
    class Examen_Clinique : Examen
    {
        private int Id_Ex_Cl { get; set; }
        private int Id_FC { get; set; }
        private String Tention { get; set; }
        private int Tempurature { get; set; }
        private float Poids { get; set; }
        private int Taille { get; set; }
        public Examen_Clinique()
        {

        }
        public void Update(String Attribut, String NvlValeur)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            string Command = @"Update Examen SET " + Attribut + "='" + NvlValeur + "' where Id_Doctor=" + Id_Examen;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            Macmd.ExecuteNonQuery();
        }
        public void Update_Id_FC(int Id_FC)
        {
            String tmp = "" + Id_FC;
            Update("Id_FC", tmp);
        }
        public void Update_Tention(String tent)
        {
            Update("Tention", tent);
        }
        public void Update_Tempera(int temperature)
        {
            String tmp = "" + temperature;
            Update("Temperature", tmp);
        }
        public void Update_Poids(float pds)
        {
            String tmp = "" + pds;
            Update("Poids", tmp);
        }
        public void Update_Taille(int Tay)
        {
            String tmp = "" + Tay;
            Update("Taille", tmp);
        }
    }
}
