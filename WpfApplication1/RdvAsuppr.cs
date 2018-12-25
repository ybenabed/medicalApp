using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WpfApplication1
{
    public class RdvAsuppr
    {
        private System.Threading.Timer timer { get; set; }
        private TimeSpan timespan { get; set; }
        private int idrdv { get; set; }
        private DateTime date { get; set; }
        public RdvAsuppr(int id, DateTime d)
        {
            this.date = d; this.idrdv = id;
            if (date > DateTime.Now)
            {
                d = d.AddSeconds(10);
                this.timespan = d.Subtract(DateTime.Now);
                this.timer = new System.Threading.Timer(new System.Threading.TimerCallback(traitement));
                this.timer.Change(this.timespan, new TimeSpan(-1));
            }
            else
            {
                DeleteRdv(id);
            }
        }
        private void traitement(object state)
        {
            App.acc.Dispatcher.Invoke(() =>
            {
                DeleteRdv(idrdv);
                if (App.acc.IsActive)
                {
                    App.acc.Remove_at_from_listasupp();
                    App.acc.removeFromNotif(date);
                }
            });
        }
        private void DeleteRdv(int id)
        {
            if (App.acc != null)
            {
                ConnexionBDD Datab = new ConnexionBDD();
                Datab.connecter();
                String Command = @"DELETE FROM [Rendez-vous] where Id_RDV=" + id;
                SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
                Macmd.ExecuteNonQuery();
                App.acc.pagerdv.supprRow(id);
            }
        }
        public void killTimer()
        {
            this.timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }
    }
}

