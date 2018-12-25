using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WpfApplication1
{
    class Ordonnance
    {
        private int ID_ordo { get; set; }
        private int Id_FC { get; set; }
        private DateTime Date_Or { get; set; }
        private List<Medicament> list_medi = new List<Medicament>();
        public Ordonnance()
        {
            DateTime D = DateTime.Now;
            Date_Or = D.Date;
        }

        public Ordonnance(int id)
        {
            DateTime D = DateTime.Now;
            Date_Or = D.Date;
            Id_FC = id;
        }

        public int Get_Ordo()
        {
            return ID_ordo;
        }
        public void Ajouter_Medic_list_Medi(Medicament Medic)
        {
            list_medi.Add(Medic);
        }
        public void Etablir_Ordonnance()
        {
            
            foreach (Medicament medic in list_medi)
            {
                medic.Prescrire_medic(ID_ordo);
            }
        }
        public void Insert_Ordonnance()
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            Datab.cmd.CommandType = CommandType.StoredProcedure;
            Datab.cmd.CommandText = "SP_Insert_Ordonnance";
            Datab.cmd.Parameters.Add("@Id_fc", SqlDbType.Int).Value = Id_FC;
            Datab.cmd.Parameters.Add("@datte", SqlDbType.Date).Value = Date_Or;
            SqlParameter Sort = new SqlParameter("@id_ORd", SqlDbType.Int);
            Sort.Direction = ParameterDirection.Output;
            Datab.cmd.Parameters.Add(Sort);
            Datab.cmd.Connection = Datab.cnx;
            Datab.cmd.ExecuteNonQuery();
            ID_ordo = (int)Sort.Value;
        }
    }
}
