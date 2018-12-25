using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using WpfApplication1.Properties;

namespace WpfApplication1
{
    public class Notification
    {
        private System.Threading.Timer timer { get; set; }
        private TimeSpan timespan { get; set; }
        private int idrdv { get; set; }
        private DateTime date { get; set; }
        private String Lieu { get; set; }
        private String Commentaire { get; set; }
        private String NomComplet { get; set; }
        public Notification(DateTime d, String l, String c, String np, int id)
        {
            timespan = new TimeSpan();
            int notif = int.Parse(WpfApplication1.Properties.Settings.Default["Notification"].ToString());
            this.idrdv = id; this.date = d; this.Lieu = l; this.Commentaire = c; this.NomComplet = np;
            if (date.AddHours(-notif) >= DateTime.Now)
            {
                d = d.AddHours(-notif);
                this.timespan = d.Subtract(DateTime.Now);
                this.timer = new System.Threading.Timer(new System.Threading.TimerCallback(traitement));
                this.timer.Change(this.timespan, new TimeSpan(-1));
            }
            else
            {
                if (date > DateTime.Now)
                {
                    this.timespan = new TimeSpan(0, 0, 10);
                    this.timer = new System.Threading.Timer(new System.Threading.TimerCallback(traitement));
                    this.timer.Change(this.timespan, new TimeSpan(-1));
                }
                else
                {
                    DeleteRdv(id);
                }
            }
        }
        private void traitement(object state)
        {
            try
            {
                App.acc.Dispatcher.Invoke(() =>
                {
                    if (App.acc.IsActive)
                    {
                        App.acc.TraitementNotif(date, Lieu, Commentaire, NomComplet);
                    }
                });
            }
            catch (Exception exc) { }
            
            
        }
        private void DeleteRdv(int id)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = @"DELETE FROM [Rendez-vous] where Id_RDV=" + id;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            Macmd.ExecuteNonQuery();
        }
        public void killTimer()
        {
            this.timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }
        public DateTime getDate()
        {
            return this.date;
        }
    }
}
