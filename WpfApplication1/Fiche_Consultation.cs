using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WpfApplication1
{
    public class Fiche_Consultation
    {
        protected int Id_FC { get; set; }
        protected DateTime Date_FC { get; set; }
        protected String Diagnostique { get; set; }
        protected int Idmed { get; set; }
        public Fiche_Consultation(DateTime date, String diagno,int idmed)
        {
            Date_FC = date;
            Diagnostique = diagno;
            this.Idmed = idmed;
        }
        public Fiche_Consultation(DateTime date)
        {
            Date_FC = date;
            Diagnostique = "";
        }
        public void Set_Id(int idd)
        {
            Id_FC = idd;
        }
        public int Get_Id()
        {
            return (Id_FC);
        }
        public void Insert_Fiche_Consultation()
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            Datab.cmd.CommandType = CommandType.StoredProcedure;
            Datab.cmd.CommandText = "SP_Insert_FC";
            Datab.cmd.Parameters.Add("@date_f", SqlDbType.Date).Value = Date_FC.Date;
            Datab.cmd.Parameters.Add("@diagno", SqlDbType.NVarChar, 1000).Value = Diagnostique;
            Datab.cmd.Parameters.Add("@idmedc", SqlDbType.Int).Value = this.Idmed;
            SqlParameter Sort = new SqlParameter("@Id_Fc", SqlDbType.Int);
            Sort.Direction = ParameterDirection.Output;
            Datab.cmd.Parameters.Add(Sort);
            Datab.cmd.Connection = Datab.cnx;
            Datab.cmd.ExecuteNonQuery();
            Id_FC = (int)Sort.Value;
        }
        public void Delete_Fiche_Consultation()
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = "DELETE FROM Fiche_Consultation where Fiche_Consultation.Id_Fiche_Consultation=" + this.Id_FC;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            Macmd.ExecuteNonQuery();
        }
    }
}
