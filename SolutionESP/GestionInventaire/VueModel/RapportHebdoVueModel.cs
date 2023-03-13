using ProjectHelper.Models;
using ProjectHelper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestionInventaire.VueModel
{
    public class RapportHebdoVueModel : BaseViewModel
    {
        public RapportHebdoVueModel(DayOfTheWeekInfo nombreVenteDeLaSemaine, DayOfTheWeekInfo sommeVenteDeLaSemaine, decimal moyPrixFactureForTheWeek)
        {
            NombreVenteDeLaSemaine = nombreVenteDeLaSemaine;
            SommeVenteDeLaSemaine = sommeVenteDeLaSemaine;
            MoyPrixFactureForTheWeek = moyPrixFactureForTheWeek;
        }

        private DayOfTheWeekInfo _nombreVenteDeLaSemaine;public DayOfTheWeekInfo NombreVenteDeLaSemaine
        {
            get { return _nombreVenteDeLaSemaine; }
            set { _nombreVenteDeLaSemaine = value; OnPropertyChanged(); }
        }


        private DayOfTheWeekInfo _sommeVenteDeLaSemaine; public DayOfTheWeekInfo SommeVenteDeLaSemaine
        {
            get { return _sommeVenteDeLaSemaine; }
            set { _sommeVenteDeLaSemaine = value; OnPropertyChanged(); }
        }


        private decimal _moyPrixFactureForTheWeek;public decimal MoyPrixFactureForTheWeek
        {
            get { return decimal.Round(_moyPrixFactureForTheWeek,2); }
            set { _moyPrixFactureForTheWeek = value; OnPropertyChanged(); }
        }

    }
}
