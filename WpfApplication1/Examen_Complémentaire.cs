using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WpfApplication1
{
    class Examen_Complémentaire : Examen
    {
        private int Id_Ex_Comp { get; set; }
        private String Type { get; set; }
        private String Conclusion { get; set; }
        private String Fichier_Relatif { get; set; }
        private int iddoss { get; set; }
        public Examen_Complémentaire(string t, string c, string f,int id)
        {
            this.Type = t; this.Conclusion = c; this.Fichier_Relatif = f; iddoss = id;
        }
        public int Get_Id()
        {
            return (this.Id_Ex_Comp);
        }
        public void Insert_Exm_Comp()
        {

            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            Datab.cmd.CommandType = CommandType.StoredProcedure;
            Datab.cmd.CommandText = "Insert_Examen_Complementaire";
            Datab.cmd.Parameters.Add("@param1", SqlDbType.NVarChar, 50).Value = this.Type;
            Datab.cmd.Parameters.Add("@param2", SqlDbType.NVarChar, 1000).Value = this.Conclusion;
            Datab.cmd.Parameters.Add("@param3", SqlDbType.Int).Value = iddoss;
            if (this.Fichier_Relatif != "") Datab.cmd.Parameters.Add("@param", SqlDbType.NVarChar).Value = Fichier_Relatif;
            else Datab.cmd.Parameters.Add("@param", SqlDbType.NVarChar).Value = "";
            SqlParameter Sort = new SqlParameter("@id", SqlDbType.Int);
            Sort.Direction = ParameterDirection.Output;
            Datab.cmd.Parameters.Add(Sort);
            Datab.cmd.Connection = Datab.cnx;
            Datab.cmd.ExecuteNonQuery();
            Id_Ex_Comp = ((int)Sort.Value);
        }
    }
}
