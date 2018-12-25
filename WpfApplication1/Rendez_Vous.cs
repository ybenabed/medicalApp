using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WpfApplication1
{
    class Rendez_Vous
    {
        private int Id_Rdv { get; set; }
        private int Id_medecin { get; set; }
        private int Id_patient { get; set; }
        private DateTime Date_RDV { get; set; }
        //  private String Heur_RDV { get; set; }
        private int Imporant { get; set; }
        private String Commentaire_Rdv { get; set; }
        private String Lieu { get; set; }
        public Rendez_Vous( /*String heur_rdv,*/DateTime date, int imp, String Cmnt, String lieu)
        {
            // this.Heur_RDV = heur_rdv;
            this.Date_RDV = date;
            this.Imporant = imp;
            this.Commentaire_Rdv = Cmnt;
            this.Lieu = lieu;
        }
        public int get_Id_RDV()
        {
            return Id_Rdv;
        }
        public void Ajouter_RDV()
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            Datab.cmd.CommandType = CommandType.StoredProcedure;
            Datab.cmd.CommandText = "SP_Insert_RDV";
            Datab.cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = Date_RDV;
            Datab.cmd.Parameters.Add("@Imp", SqlDbType.Int).Value = Imporant;
            Datab.cmd.Parameters.Add("@cmnt_rdv", SqlDbType.NVarChar, 1000).Value = Commentaire_Rdv;
            Datab.cmd.Parameters.Add("@lieu", SqlDbType.NVarChar, 50).Value = Lieu;
            //   Datab.cmd.Parameters.Add("@heur", SqlDbType.NVarChar, 5).Value = Heur_RDV;
            SqlParameter sort = new SqlParameter("@Id_rdv", SqlDbType.Int);
            sort.Direction = ParameterDirection.Output;
            Datab.cmd.Parameters.Add(sort);
            Datab.cmd.Connection = Datab.cnx;
            Datab.cmd.ExecuteNonQuery();
            Id_Rdv = (int)sort.Value;
        }
        public void Update1(String Attribut, String NvlValeur)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            string Command = @"Update Rendez-vous SET " + Attribut + "='" + NvlValeur + "' where Id_RDV=" + Id_Rdv;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            Macmd.ExecuteNonQuery();
        }
        public void Update2(String Attribut, int NvlValeur)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            string Command = @"Update Rendez-vous SET " + Attribut + "=" + NvlValeur + " where Id_RDV=" + Id_Rdv;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            Macmd.ExecuteNonQuery();
        }


        public void update_Id_pat(int Id_pat)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            string Command = @"Update [Rendez-vous] SET Id_Patient = " + Id_pat + " WHERE Id_RDV=" + Id_Rdv;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            Macmd.ExecuteNonQuery();
        }
        public void update_Imp(int imp)
        {
            Update2("Imporant", imp);
        }
        public void update_Description(String Cmnt)
        {
            Update1("Commentaire", Cmnt);
        }
        public void update_Lieu(String lieu)
        {
            Update1("Lieu", lieu);
        }
        public void Update_Date(DateTime NvlValeur)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            string Command = @"Update Rendez-vous SET " + "Date_Heure" + "='" + NvlValeur + "' where Id_RDV=" + Id_Rdv;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            Macmd.ExecuteNonQuery();
        }
    }
}
