using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;


using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;

namespace WpfApplication1
{
    class MiseEnFormLettre
    {
        private int id_dm, id_m;

        public MiseEnFormLettre(int id_dm, int id_m)
        {
            this.id_dm = id_dm;
            this.id_m = id_m;
        }
        public Document CreateDoc(String nomdoc)
        {


            iTextSharp.text.Rectangle rec = new iTextSharp.text.Rectangle(100, 520);
            rec.BorderWidth = (100);

            rec.BackgroundColor = new BaseColor(System.Drawing.Color.White);
            FileStream fs = new FileStream(nomdoc, FileMode.Create, FileAccess.Write, FileShare.None);
            Document doc = new Document(iTextSharp.text.PageSize.A4, 10, 10, 10, 10);


            PdfWriter write = PdfWriter.GetInstance(doc, fs);
            doc.Open();
            PdfContentByte cb = write.DirectContent;
            cb.RoundRectangle(5F, 360F, 580F, 350F, 20f);

            cb.Stroke();

            return doc;

        }
        public String[] Recuperer_info_pat(int id_dm)
        {
            String[] Tab = new String[3];

            DateTime dat = new DateTime();
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();


            int id_pat = 0;
            int id_per = 0;
            DateTime date_naiss = DateTime.Now;
            string Command = " SELECT Id_Patient FROM Dossier_medical WHERE Id_Dossier =" + id_dm;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            SqlDataReader read = Macmd.ExecuteReader();

            while (read.Read())
            {
                id_pat = (int)read[0];
            }
            read.Close();
            string Commande = @"SELECT Id_Person,Date_de_naissance  FROM Patient WHERE Id_Patient=" + id_pat;
            SqlCommand Macmde = new SqlCommand(Commande, Datab.cnx);
            SqlDataReader reade = Macmde.ExecuteReader();

            while (reade.Read())
            {
                id_per = (int)reade["Id_Person"];
                date_naiss = (DateTime)reade["Date_de_naissance"];
                dat = date_naiss;
            }

            reade.Close();
            string Comman = @"SELECT Nom,Prenom FROM Person WHERE Id_Person=" + id_per;
            SqlCommand Macm = new SqlCommand(Comman, Datab.cnx);
            SqlDataReader myReader = Macm.ExecuteReader();

            while (myReader.Read())
            {
                Tab[0] = myReader[0].ToString();
                Tab[1] = myReader[1].ToString();
                Tab[2] = calcul_age(dat);


            }
            myReader.Close();
            return Tab;
        }

        public String[] Recuperer_info_Docteur(int id_m)
        {
            String[] Tab = new String[6];

            DateTime dat = new DateTime();
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();


            int id_per = 0;

            string Commande = @"SELECT Id_Person,Spécialité  FROM Doctor WHERE Id_Doctor=" + id_m;
            SqlCommand Macmde = new SqlCommand(Commande, Datab.cnx);
            SqlDataReader reade = Macmde.ExecuteReader();

            while (reade.Read())
            {
                id_per = (int)reade["Id_Person"];
                Tab[2] = reade[1].ToString();
            }

            reade.Close();
            string Comman = @"SELECT Nom,Prenom,Adresse,Email,Num_Tel FROM Person WHERE Id_Person=" + id_per;
            SqlCommand Macm = new SqlCommand(Comman, Datab.cnx);
            SqlDataReader myReader = Macm.ExecuteReader();

            while (myReader.Read())
            {

                Tab[0] = myReader[0].ToString(); ;
                Tab[1] = myReader[1].ToString();
                Tab[3] = myReader[2].ToString();//Adresse 
                Tab[4] = myReader[3].ToString();//Email 
                Tab[5] = myReader[4].ToString();//Num_Tel  


            }
            myReader.Close();
            return Tab;
        }


        public String calcul_age(DateTime date_nais)
        {
            if (DateTime.Now.Year - date_nais.Year > 0) return (DateTime.Now.Year - date_nais.Year + " ans");
            else return (DateTime.Now.Month - date_nais.Month + " mois");
        }
        public Document Remplir_doc(Document doc, String nom, String adresse, String spec, String etat, int id_m, int id_dm)
        {
            String[] Med = Recuperer_info_Docteur(id_m);
            String[] Pat = Recuperer_info_pat(id_dm);
            DateTime date = DateTime.Now;

            iTextSharp.text.Font f1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 15f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font f2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 15f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font b = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 20f, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font b1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);



            Paragraph par1 = new Paragraph(" Lieu :" + Med[3], f1);
            Paragraph par2 = new Paragraph(" Téléphone :" + Med[5], f1);
            Paragraph par3 = new Paragraph(" Email :" + Med[4], f1);
            String d = date.Day.ToString() + "/" + date.Month.ToString() + "/" + date.Year.ToString();
            Paragraph parag0 = new Paragraph("\nLe: " + d + "\n", b1);
            parag0.Alignment = Element.ALIGN_RIGHT;
            doc.Add(par1);
            doc.Add(par2);
            doc.Add(par3);
            doc.Add(parag0);



            Paragraph parag = new iTextSharp.text.Paragraph(" \nLettre d'orientation\n\n", b);
            parag.Alignment = Element.ALIGN_CENTER;

            Paragraph parag1 = new Paragraph("\nDu docteur : " + Med[0] + " " + Med[1] + "           Spécialité : " + Med[2] + ".", b1);
            Paragraph parag2 = new Paragraph("\n Au docteur : " + nom + "         Spécialité : " + spec + "       Adresse :" + adresse + ".", b1);
            Paragraph parag3 = new Paragraph("\n\n                                Mon cher confrère,\n       Je vous adresse le patient : " + Pat[0] + " " + Pat[1] + " qui a " + Pat[2] + ".\n       Son état de santé: " + etat + ". \n\n Je vous l'adresse pour avis et je vous remercie de l'attention que vous lui portez .", b1);
            /* Chunk parax = new Chunk("Allah ", FontFactory.GetFont("Times New Roman"));
            parax.Font.Size = 14;
            doc.Add(parax);*/


            doc.Add(parag);
            doc.Add(parag1);
            doc.Add(parag2);
            doc.Add(parag3);
            doc.Add(new Paragraph("\n\n                                                                                        Signature: ", b1));
            return doc;
        }
        public Document Remplir_Exam_comp(Document doc, int id_m, int id_dm, String typeExam, String cause)
        {
            String[] Med = Recuperer_info_Docteur(id_m);
            String[] Pat = Recuperer_info_pat(id_dm);
            DateTime date = DateTime.Now;

            iTextSharp.text.Font f1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 15f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font f2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 15f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font b = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 20f, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font b1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 15f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);



            Paragraph par1 = new Paragraph(" Lieu :" + Med[3], f1);
            Paragraph par2 = new Paragraph(" Téléphone :" + Med[5], f1);
            Paragraph par3 = new Paragraph(" Email :" + Med[4], f1);
            String d = date.Day.ToString() + "/" + date.Month.ToString() + "/" + date.Year.ToString();
            Paragraph parag0 = new Paragraph("\nLe: " + d + "\n", b1);
            parag0.Alignment = Element.ALIGN_RIGHT;
            doc.Add(par1);
            doc.Add(par2);
            doc.Add(par3);
            doc.Add(parag0);

            Paragraph parag = new iTextSharp.text.Paragraph("\nExamen Complémentaire\n\n", b);
            parag.Alignment = Element.ALIGN_CENTER;

            Paragraph parag2 = new Paragraph("\n\n          Je soussigné , Docteur :" + Med[0] + " " + Med[1] + ".\n     Apres avoir examiner : M." + Pat[0] + " " + Pat[1] + " ,\n     certifie que son état de santé nécessite un examen : " + typeExam + "\n     A cause de :" + cause, b1);
            Paragraph par = new Paragraph(" \n\n\n                                                                   Signature :  ", b1);
            doc.Add(parag);
            doc.Add(parag2);
            doc.Add(par);
            return doc;
        }
    }
}
