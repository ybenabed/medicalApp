using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WpfApplication1
{
    class Certaficat_Médical
    {
        private int Id_Cert_Médic { get; set; }
        private int Id_FC { get; set; }
        private int Nb_jours { get; set; }
        private String Commentaire { get; set; }

        public Certaficat_Médical(int nbj, String Cmnt)
        {
            this.Nb_jours = nbj;
            this.Commentaire = Cmnt;
        }
        public int Get_Id_Cert()
        {
            return Id_Cert_Médic;
        }
        public void Set_Id_fc(int id_fc)
        {
            this.Id_FC = id_fc;
        }
        public void insert_nvl_certaficat_medic()
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            Datab.cmd.CommandType = CommandType.StoredProcedure;
            Datab.cmd.CommandText = "SP_Insert_Cert_medic";
            Datab.cmd.Parameters.Add("@Id_fc", SqlDbType.Int).Value = Id_FC;
            Datab.cmd.Parameters.Add("@nb_jours", SqlDbType.Int).Value = Nb_jours;
            Datab.cmd.Parameters.Add("@Commentaire", SqlDbType.NVarChar, 1000).Value = Commentaire;
            SqlParameter sort = new SqlParameter("@Id_Cert", SqlDbType.Int);
            sort.Direction = ParameterDirection.Output;
            Datab.cmd.Parameters.Add(sort);
            Datab.cmd.Connection = Datab.cnx;
            Datab.cmd.ExecuteNonQuery();
            Id_Cert_Médic = (int)sort.Value;

        }
    }
}
