using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WpfApplication1
{
    class Person
    {
        protected int Id_pers { get; set; }
        protected String Nom { get; set; }
        protected String Prenom { get; set; }
        protected String Email { get; set; }
        protected String Adresse { get; set; }
        protected String Num { get; set; }
        protected String Sexe { get; set; }
        public Person()
        {

        }
        public Person(String n, String p, String mail, String adr, String numte, String sekse)
        {
            Nom = n;
            Prenom = p;
            Email = mail;
            Adresse = adr;
            Num = numte;
            Sexe = sekse;
        }
        public void Set_Id(int Idd)
        {
            this.Id_pers = Idd;
        }
        /*Ajouter une nouvelle ligne à la liste Person, et retourner son identifiant*/
        public void Insert_Nvl_Person()
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            Datab.cmd.CommandType = CommandType.StoredProcedure;
            Datab.cmd.CommandText = "SP_Insert_Person";
            Datab.cmd.Parameters.Add("@Nom", SqlDbType.NVarChar, 30).Value = Nom;
            Datab.cmd.Parameters.Add("@Prenom", SqlDbType.NVarChar, 50).Value = Prenom;
            Datab.cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 30).Value = Email;
            Datab.cmd.Parameters.Add("@Adr", SqlDbType.NVarChar, 150).Value = Adresse;
            Datab.cmd.Parameters.Add("@Num", SqlDbType.NVarChar, 10).Value = Num;
            Datab.cmd.Parameters.Add("@Sexe", SqlDbType.NVarChar, 5).Value = Sexe;
            SqlParameter sort = new SqlParameter("@Id_pers", SqlDbType.Int);
            sort.Direction = ParameterDirection.Output;
            Datab.cmd.Parameters.Add(sort);
            Datab.cmd.Connection = Datab.cnx;
            Datab.cmd.ExecuteNonQuery();
            Id_pers = ((int)sort.Value);
        }
    }
}
