using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WpfApplication1
{
    class Lettre
    {
        private int ID_lettre { get; set; } 
        private int id_DM { get; set; }
        private String nom_medecin { get; set; }
        private String specialite { get; set; }
        private String adresse { get; set; }
        private String comment_lettre { get; set; }
        public Lettre(String nom_med, String speci, String adr, String comment_let,int idd)
        {
            nom_medecin = nom_med;
            specialite = speci;
            adresse = adr;
            comment_lettre = comment_let;
            id_DM = idd;
        }
        public void Set_ID_lettre(int id)
        {
            ID_lettre = id;
        }
        public int Get_id()
        {
            return this.ID_lettre;
        }
        public void Insert_Nvl_Lettre()
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            Datab.cmd.CommandType = CommandType.StoredProcedure;
            Datab.cmd.CommandText = "SP_Insert_Lettre";
            Datab.cmd.Parameters.Add("@nom_med", SqlDbType.NVarChar, 50).Value = nom_medecin;
            Datab.cmd.Parameters.Add("@spec", SqlDbType.NVarChar, 20).Value = specialite;
            Datab.cmd.Parameters.Add("@adresse", SqlDbType.NVarChar, 80).Value = adresse;
            Datab.cmd.Parameters.Add("@id_dm", SqlDbType.Int).Value = id_DM;
            Datab.cmd.Parameters.Add("@cmnt", SqlDbType.NVarChar, 1000).Value = comment_lettre;
            SqlParameter sort = new SqlParameter("@Id_let", SqlDbType.Int);
            sort.Direction = ParameterDirection.Output;
            Datab.cmd.Parameters.Add(sort);
            Datab.cmd.Connection = Datab.cnx;
            Datab.cmd.ExecuteNonQuery();
            ID_lettre = (int)sort.Value;
        }
    }
}
