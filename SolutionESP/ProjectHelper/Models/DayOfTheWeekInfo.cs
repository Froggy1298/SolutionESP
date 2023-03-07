using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelper.Models
{
    public class DayOfTheWeekInfo
    {
        public KeyValuePair<DateTime, decimal> Lundi { get; set; }
        public KeyValuePair<DateTime, decimal> Mardi { get; set; }
        public KeyValuePair<DateTime, decimal> Mercredi { get; set; }
        public KeyValuePair<DateTime, decimal> Jeudi { get; set; }
        public KeyValuePair<DateTime, decimal> Vendredi { get; set; }
        public KeyValuePair<DateTime, decimal> Samedi { get; set; }
        public KeyValuePair<DateTime, decimal> Dimanche { get; set; }

        //public DayOfTheWeekInfo(DateTime lundiDate, decimal[] lesInfoDesJours)
        //{
        //    Lundi = new KeyValuePair<DateTime, decimal>(lundiDate, lesInfoDesJours[0]);
        //    Mardi = new KeyValuePair<DateTime, decimal>(lundiDate.AddDays(1), lesInfoDesJours[1]);
        //    Mercredi = new KeyValuePair<DateTime, decimal>(lundiDate.AddDays(2), lesInfoDesJours[2]);
        //    Jeudi = new KeyValuePair<DateTime, decimal>(lundiDate.AddDays(3), lesInfoDesJours[3]);
        //    Vendredi = new KeyValuePair<DateTime, decimal>(lundiDate.AddDays(4), lesInfoDesJours[4]);
        //    Samedi = new KeyValuePair<DateTime, decimal>(lundiDate.AddDays(5), lesInfoDesJours[5]);
        //    Dimanche = new KeyValuePair<DateTime, decimal>(lundiDate.AddDays(6), lesInfoDesJours[6]);
        //}
    }
}
