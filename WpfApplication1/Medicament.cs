using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WpfApplication1
{
    class Medicament
    {
        private String nom_medi { get; set; }
        private int Id_ordonnance { get; set; }
        private String dose { get; set; }
        private String forme { get; set; }
        private int quantite { get; set; } // le nombre de boites a acheter 
        private String utilisation { get; set; }
        private String durer { get; set; }
        public Medicament(String medic, String dos, String form, int quanti,String util,String dur)
        {
            nom_medi = medic;
            dose = dos;
            forme = form;
            quantite = quanti;
            utilisation = util;
            durer = dur;
        }
        public Medicament()
        {

        }
        public void Set_nom_medi(String medic)
        {
            nom_medi = medic;
        }
        public void Prescrire_medic(int Ordo)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            Datab.cmd.CommandType=CommandType.StoredProcedure;
            Datab.cmd.CommandText="SP_Prescrire";
            Datab.cmd.Parameters.Add("@Id_Ordo",SqlDbType.Int).Value=Ordo;
            Datab.cmd.Parameters.Add("@Id_Medic",SqlDbType.NVarChar,50).Value=nom_medi;
            Datab.cmd.Parameters.Add("@dose",SqlDbType.NVarChar,10).Value=dose;
            Datab.cmd.Parameters.Add("@forme",SqlDbType.NVarChar,30).Value=forme;
            Datab.cmd.Parameters.Add("@quant",SqlDbType.Int).Value=quantite;
            Datab.cmd.Parameters.Add("@durer", SqlDbType.NVarChar, 50).Value = durer;
            Datab.cmd.Parameters.Add("@util",SqlDbType.NVarChar,100).Value=utilisation;
            Datab.cmd.Connection=Datab.cnx;
            Datab.cmd.ExecuteNonQuery();
        }
        /*Recherche des medicaments qui commence par la chaine Medic*/
        public static void Select_Medic(DataTable DATBL)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String command = @"select Medicament from Medicaments";
            SqlCommand Macmd = new SqlCommand(command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            SqlDataAdapter Daptr = new SqlDataAdapter(Macmd);
            Daptr.Fill(DATBL);
        }
        public static void Insert_medic(string nom_medi)
        {
            nom_medi = nom_medi.ToUpper();
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = "select count(*) from Medicaments where Medicament='" + nom_medi + "'";
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            SqlDataReader dr = Macmd.ExecuteReader();
            int count = 0;
            if (dr.Read())
            {
                count = int.Parse(dr[0].ToString());
            }
            dr.Close();
            if (count == 0)
            {
                Datab.cmd.CommandType = CommandType.StoredProcedure;
                Datab.cmd.CommandText = "SP_Insert_Medicament";
                Datab.cmd.Parameters.Add("@param1", SqlDbType.NVarChar, 80).Value = nom_medi;
                Datab.cmd.Connection = Datab.cnx;
                Datab.cmd.ExecuteNonQuery();
                Datab.deconnecter();
            }
        }
        public int Compter_Medic()
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = @"SELECT COUNT(*) from Medicaments where Medicament='"+nom_medi+"'";
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            SqlDataReader dr;
            dr = Macmd.ExecuteReader();
            if (dr.Read())
            {
                return ((int)dr[0]);
            }
            return 0;
        }
    }
}
