using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class MiseEnFormCert
    {
        private int id_dm;
        private int id_m;

        public MiseEnFormCert(int id_dm, int id_m)
        {
            this.id_dm = id_dm;
            this.id_m = id_m;
        }
        public Document Remplir_doc(Document doc, string com, int i, string period, int id_m, int id_dm)
        {
            MiseEnFormLettre forme = new MiseEnFormLettre(id_dm, id_m);
            String[] Med = forme.Recuperer_info_Docteur(id_m);
            String[] Pat = forme.Recuperer_info_pat(id_dm);
            DateTime date = DateTime.Now;

            iTextSharp.text.Font f1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 15f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font b = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 20f, iTextSharp.text.Font.UNDERLINE, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font b1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);


            Paragraph par1 = new Paragraph("Lieu :" + Med[3], f1);
            Paragraph par2 = new Paragraph(" Téléphone :" + Med[5], f1);
            Paragraph par3 = new Paragraph(" Email :" + Med[4], f1);
            String d = date.Day.ToString() + "/" + date.Month.ToString() + "/" + date.Year.ToString();
            Paragraph parag0 = new Paragraph("\nLe " + d + "\n\n", b1);
            parag0.Alignment = Element.ALIGN_RIGHT;


            doc.Add(par1);
            doc.Add(par2);
            doc.Add(par3);
            doc.Add(parag0);


            Paragraph parag = new iTextSharp.text.Paragraph("Certificat Medical\n", b);
            parag.Alignment = Element.ALIGN_CENTER;

            Paragraph parag1 = new Paragraph("\n\n\n Docteur :" + Med[0] + " " + Med[1] + "            Spécialité : " + Med[2], b1);
            Paragraph parag2 = new Paragraph("\n\n Je soussigné , Docteur :" + Med[0] + " " + Med[1] + " , certifie que l'état de santé de : M." + Pat[0] + " " + Pat[1], b1);
            Paragraph parag3 = new Paragraph("\nNécessite un traitement avec arret de travail de : " + i + " " + period + " à partir de : " + d, b1);
            Paragraph parag4 = new Paragraph("\n\n\n\n\n                                                                                         Signature :", b1);

            doc.Add(parag);

            doc.Add(parag1);
            doc.Add(parag2);
            doc.Add(parag3);
            doc.Add(parag4);
            return doc;
        }


    }
}
