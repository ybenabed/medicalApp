using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Core;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.IO;
using System.Diagnostics;


namespace WpfApplication1
{
    class Misenforme
    {
        private string file { get; set; }
        public Misenforme()
        {
            string chemin = System.IO.Directory.GetCurrentDirectory();
            chemin = chemin.Remove(chemin.LastIndexOf(@"\"));
            chemin = chemin + @"\Release\Modele_Ordonnance.docx";
            file = chemin;
        }
        private void writeinHeader(Word.Document doc, String nomed, String s, String a, String n, DateTime naiss, DateTime datordo)
        {
            foreach (Section sec in doc.Sections)
            {
                Word.Range range = sec.Headers[WdHeaderFooterIndex.wdHeaderFooterFirstPage].Range;
                range.Text = "Docteur " + nomed + " - " + s + "\n";
                range.Text += a + "\n";
                range.Text += n + " Né(e) le " + naiss.ToShortDateString() + "\n";
                range.Text += "Le: " + datordo.ToShortDateString();
                /*Word.Range bold = sec.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                bold.Start = bold.Start;
                bold.End = bold.Start + 5;
                bold.Bold = 1;*/
            }
        }
        private void writeTitle(Word.Document doc, String titre)
        {
            Word.Paragraph parag = doc.Paragraphs.Add();
            parag.Range.Text = titre.ToUpper() + "\n\n";
            object start = 0; object end = (int)start + titre.Length;
            Word.Range bold = doc.Range(ref start, ref end);
            Word.Range under = bold, center = bold, size = bold;
            bold.Bold = 1;
            under.Underline = Word.WdUnderline.wdUnderlineSingle;
            center.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            size.Font.Size = 13;
        }
        private void Ecrire_Medic(Word.Document doc, int nb, String nom, String forme, String dose, int quant, String utili, String durer, bool dernier)
        {
            Word.Paragraph parag = doc.Paragraphs.Add();
            String boi = "";
            if (quant == 1) boi = " boite";
            else boi = " boites";
            parag.Range.Text = "\t" + nb + ") " + nom + "   " + forme.ToLower() + "  " + dose.ToLower() + "  " + quant + boi;
            parag.Range.Font.Size = 11;
            object start = parag.Range.Start + 1; object end = parag.Range.Start + nom.Length + 3 + nb.ToString().Length;
            Word.Range bold = doc.Range(ref start, ref end);
            Word.Range under = bold;
            bold.Bold = 1;
            under.Underline = Word.WdUnderline.wdUnderlineSingle;
            doc.Paragraphs.Add();
            if (!dernier) parag.Range.Text = "\t       " + utili.ToLower() + " pendant " + durer.ToLower() + "\n\n";
            else parag.Range.Text = "\t       " + utili + " pendant " + durer;
        }
        public void CreateWordDocument(object savaAs, int idmed, int idpat, int idordo)
        {
            string nomedcin = "", adres = "", spec = "", nomal = "";
            DateTime naissance = new DateTime();
            object missing = Missing.Value;
            object filename = file;
            get_Medcin(idmed, ref nomedcin, ref adres, ref spec);
            get_Patient(idpat, ref nomal, ref naissance);
            Word.Application wordApp = new Word.Application();
            Word.Document aDoc = null;
                DateTime dateordo = get_DateOrdo(idordo);


                object readOnly = false; //default
                object isVisible = false;

                wordApp.Visible = false;

                aDoc = wordApp.Documents.Add(ref missing, ref missing, ref missing, ref missing);


                aDoc.Activate();

                aDoc.PageSetup.PageWidth = (float)(16 * (400 / 14.11));
                aDoc.PageSetup.PageHeight = (float)(20 * (400 / 14.11));

                aDoc.PageSetup.DifferentFirstPageHeaderFooter = -1;
                this.writeTitle(aDoc, "Ordonnance");
                //Saisir l'en-tete de l'ordonnance
                this.writeinHeader(aDoc, nomedcin, spec, adres, nomal, naissance, dateordo);


                //Ajouter les médicaments
                this.ajouterlesMedic(idordo, aDoc);

            //Save as: filename
            aDoc.SaveAs2(ref savaAs, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing);

            //Close Document:
            ((Microsoft.Office.Interop.Word._Document)aDoc).Close(ref missing, ref missing, ref missing);
            ((Microsoft.Office.Interop.Word._Application)wordApp).Quit(ref missing, ref missing, ref missing);
            Console.WriteLine("File created.");
        }
        private void get_Medcin(int idmed, ref string nommed, ref string Adresse, ref string Spec)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = @"select Nom,Prenom,Spécialité,Adresse from Doctor LEFT OUTER JOIN Person";
            Command = Command + " on Doctor.Id_Person=Person.Id_Person where Id_Doctor=" + idmed;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            SqlDataReader dr = Macmd.ExecuteReader();
            if (dr.Read())
            {
                nommed = dr[0].ToString() + " " + dr[1].ToString();
                Spec = dr[2].ToString();
                Adresse = dr[3].ToString();
            }
            Datab.deconnecter();
        }
        public void get_Patient(int idpat, ref string nomcomplet, ref DateTime datnais)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = @"select Nom,Prenom,Date_de_naissance from Patient LEFT OUTER JOIN Person";
            Command = Command + @" on Patient.Id_Person=Person.Id_Person where Id_Patient=" + idpat;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            SqlDataReader dr = Macmd.ExecuteReader();
            if (dr.Read())
            {
                nomcomplet = dr[0].ToString() + " " + dr[1].ToString();
                try
                {
                    datnais = DateTime.Parse(dr[2].ToString());
                }
                catch (Exception ex)
                {
                    ex.GetType();
                }
            }
            Datab.deconnecter();
        }
        private DateTime get_DateOrdo(int idOrdo)
        {
            try
            {
                ConnexionBDD Datab = new ConnexionBDD();
                Datab.connecter();
                String Command = @"select Date_Ordo from Ordonnance where Id_Ordonnance=" + idOrdo;
                SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
                SqlDataReader dr = Macmd.ExecuteReader();
                if (dr.Read())
                {
                    return (DateTime.Parse(dr[0].ToString()));
                }
                else return (new DateTime());
            }
            catch (Exception ex) { ex.GetType(); return (new DateTime()); }
        }
        private int Nb_Medicament(int idordo)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = @"select Count(*) from Prescrire where Id_Ordonnance=" + idordo;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            SqlDataReader dr = Macmd.ExecuteReader();
            if (dr.Read())
            {
                return int.Parse(dr[0].ToString());
            }
            else { return (-1); }
        }
        private void ajouterlesMedic(int idOrdo, Word.Document doc)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = @"select Id_Medicament,Forme,Dose,Quantité,Nbre_Utilisation,Dure from Prescrire where Id_Ordonnance=" + idOrdo;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            SqlDataReader dr = Macmd.ExecuteReader();
            int cpt = 1, nb = Nb_Medicament(idOrdo);
            bool fin = false;
            while (dr.Read())
            {
                string nomedic = dr[0].ToString();
                string forme = dr[1].ToString();
                string dose = dr[2].ToString();
                int quant = int.Parse(dr[3].ToString());
                string util = dr[4].ToString();
                string dure = dr[5].ToString();
                if (cpt == nb) fin = true;
                Ecrire_Medic(doc, cpt, nomedic, forme, dose, quant, util, dure, fin);
                cpt++;
            }
        }
    }
}
