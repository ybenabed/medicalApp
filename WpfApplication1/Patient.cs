using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WpfApplication1
{
    class Patient : Person
    {
        private int Id_Patient { get; set; }
        private DateTime Date_naissance { get; set; }
        private string Group_sanguin { get; set; }
        public Patient(String n, String p, String mail, String adr, String numte, String sekse, DateTime Date_n, String Groupage)
        {
            Nom = n;
            Prenom = p;
            Email = mail;
            Adresse = adr;
            Num = numte;
            Sexe = sekse;
            Date_naissance = Date_n.Date;
            Group_sanguin = Groupage;
        }
        public Patient(int Idd)
        {
            this.Id_Patient = Idd;
        }
        public int get_Id()
        {
            return (Id_Patient);
        }
        public void Insert_Nv_Patient()
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            Insert_Nvl_Person();
            Datab.cmd.CommandType = CommandType.StoredProcedure;
            Datab.cmd.CommandText = "SP_Insert_Patient";
            Datab.cmd.Parameters.Add("@Id_Person", SqlDbType.Int).Value = Id_pers;
            Datab.cmd.Parameters.Add("@date", SqlDbType.Date).Value = Date_naissance.Date;
            Datab.cmd.Parameters.Add("@Gs", SqlDbType.NChar, 3).Value = Group_sanguin;
            SqlParameter sort = new SqlParameter("@Id_pat", SqlDbType.Int);
            sort.Direction = ParameterDirection.Output;
            Datab.cmd.Parameters.Add(sort);
            Datab.cmd.Connection = Datab.cnx;
            Datab.cmd.ExecuteNonQuery();
            Id_Patient = (int)sort.Value;
        }
    }
}
