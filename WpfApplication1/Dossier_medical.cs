using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WpfApplication1
{
    class Dossier_medical
    {
        private int Id_DM { get; set; }
        private int Id_pat { get; set; }
        private List<int> List_Fich = new List<int>();
        private List<int> List_Antecedent = new List<int>();
        private List<int> List_Examen = new List<int>();
        public Dossier_medical(int IdP)
        {
            Id_pat = IdP;
        }
        /*public int Compter()
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            Datab.cmd.CommandType = CommandType.StoredProcedure;
            Datab.cmd.CommandText = "Compter_dossier";
            Datab.cmd.Parameters.Add("@patient", SqlDbType.Int).Value = Id_pat;
            SqlParameter sort = new SqlParameter("@compt", SqlDbType.Int);
            sort.Direction = ParameterDirection.Output;
            Datab.cmd.Parameters.Add(sort);
            Datab.cmd.Connection = Datab.cnx;
            Datab.cmd.ExecuteNonQuery();
            return ((int)sort.Value);
        }*/
        public int get_id()
        {
            return this.Id_DM;
        }
        public void Creer_DM()
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            Datab.cmd.CommandType = CommandType.StoredProcedure;
            Datab.cmd.CommandText = "SP_Insert_DM";
            Datab.cmd.Parameters.Add("@Id_Patient", SqlDbType.Int).Value = Id_pat;
            SqlParameter Sort = new SqlParameter("@Id_DM", SqlDbType.Int);
            Sort.Direction = ParameterDirection.Output;
            Datab.cmd.Parameters.Add(Sort);
            Datab.cmd.Connection = Datab.cnx;
            Datab.cmd.ExecuteNonQuery();
            Id_DM = (int)Sort.Value;
        }
        /*public void Aff_Id_DM()
        {
            int compte = Compter();
            if (compte != 0)
            {
                ConnexionBDD Datab = new ConnexionBDD();
                Datab.connecter();
                string Command = @"SELECT Id_Dossier FROM Dossier_medical WHERE Id_Patient=" + Id_pat;
                SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
                SqlDataReader dr;
                dr = Macmd.ExecuteReader();
                if (dr.Read())
                {
                    Id_DM = (int)dr[0];
                }
            }
            else
            {
                Creer_DM();
            }
        }*/
        public void Update(String Attribut, String NvlValeur)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            string Command = @"Update Dossier_medical SET " + Attribut + "='" + NvlValeur + "' where Id_Dossier=" + Id_DM;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            Macmd.ExecuteNonQuery();
        }
        public void Update_ES(String NvlES)
        {
            Update("Etat_de_Santé", NvlES);
        }
        public void Ajouter_Asso(String Table, String Attribut, int Id_Att)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String command = @"INSERT INTO " + Table + " (Id_DM," + Attribut + ") values (" + Id_DM + "," + Id_Att + ")";
            SqlCommand Macmd = new SqlCommand(command, Datab.cnx);
            Macmd.ExecuteNonQuery();
        }
        public void Ajouter_Avoir_ANT(int Id_Ant)
        {
            Ajouter_Asso("Avoir_ANT", "Id_Antecedent", Id_Ant);
            List_Antecedent.Add(Id_Ant);
        }
        public void Ajouter_Examine_Comp(int Id_Examen)
        {
            Ajouter_Asso("Examine_Comp", "id_Ex_Comp", Id_Examen);
            List_Examen.Add(Id_Examen);
        }
        public void Ajouter_Contient(int Id_fiche)
        {
            Ajouter_Asso("Contient", "Id_FC", Id_fiche);
            List_Fich.Add(Id_fiche);
        }
        public void Afficher_LFC()
        {
            foreach (int FCC in List_Fich)
            {
                Console.WriteLine(FCC);
            }
        }
    }
}
