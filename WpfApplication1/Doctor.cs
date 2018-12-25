using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WpfApplication1
{
    class Doctor : Person
    {
        private int Id_Doc { get; set; }
        private String Specialite { get; set; }
        private String Username { get; set; }
        private String Password { get; set; }
        public Doctor(String n, String p, String mail, String adr, String numte, String sekse, String Spec, String user, String password)
        {
            Nom = n;
            Prenom = p;
            Email = mail;
            Adresse = adr;
            Num = numte;
            Sexe = sekse;
            Specialite = Spec;
            Username = user;
            Password = password;
        }
        public Doctor(int Idd)
        {
            this.Id_Doc = Idd;
        }
        public int Get_id_doc()
        {
            return this.Id_Doc;
        }
        public void Insert_Nv_Doctor(string type)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            Insert_Nvl_Person();
            Datab.cmd.CommandType = CommandType.StoredProcedure;
            Datab.cmd.CommandText = "SP_Insert_Doctor";
            Datab.cmd.Parameters.Add("@Id_Pers", SqlDbType.Int).Value = Id_pers;
            Datab.cmd.Parameters.Add("@Spec", SqlDbType.NVarChar, 50).Value = Specialite;
            Datab.cmd.Parameters.Add("@user", SqlDbType.NVarChar, 20).Value = Username;
            Datab.cmd.Parameters.Add("@mdp", SqlDbType.NVarChar, 200).Value = Password;
            Datab.cmd.Parameters.Add("@type", SqlDbType.NVarChar, 20).Value = type;
            SqlParameter Sort = new SqlParameter("@Id_Doc", SqlDbType.Int);
            Sort.Direction = ParameterDirection.Output;
            Datab.cmd.Parameters.Add(Sort);
            Datab.cmd.Connection = Datab.cnx;
            Datab.cmd.ExecuteNonQuery();
            Id_Doc = ((int)Sort.Value);
        }
        public void Update(String Attribut, String NvlValeur)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            string Command = @"Update Doctor SET " + Attribut + "='" + NvlValeur + "' where Id_Doctor=" + Id_Doc;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            Macmd.ExecuteNonQuery();
        }
        public void Modifier_Username(String NvUsername)
        {
            Update("Username", NvUsername);
        }
        public void Modifier_Mdp(String NvMdp)
        {
            Update("Passwordd", NvMdp);
        }
        public void Modifier_Specialite(String Nvl_Specialite)
        {
            Update("Spécialité", Nvl_Specialite);
        }
    }
}
