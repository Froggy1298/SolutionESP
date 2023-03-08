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
    }
}
