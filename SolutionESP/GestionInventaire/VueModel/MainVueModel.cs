using GestionInventaire.Vue;
using Microsoft.EntityFrameworkCore;
using ProjectHelper.Models;
using ProjectHelper.Models.ModelsProduit;
using ProjectHelper.ViewModel;
using ProjetHelper.Models;
using RelayCommandLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices;
using System.Linq;
using System.Printing;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace GestionInventaire.VueModel
{
    public class MainVueModel : BaseViewModel
    {
        public MainVueModel()
        {
            BdContext = new A22Sda2031887Context();
            View1 = new ListProduit();
            ListProduitVm = new ListVueModel();
            ListProduitVm.ChangeToUpdatePage += ChangeUpdatePage;
            ListProduitVm.ChangeToCreatePage += ChangeCreatePage;
            View1.DataContext = ListProduitVm;

            DateRapportMensuel = SelectFirstDayOfLastMonth();
            MaxDateMensuel = SelectLastDayOfLastMonth();

            DateRapportHebdo = GetPrecedingLundi();
            MaxDateHebdo = GetPrecedingDimanche();

            RapportHebdomadaire = new RelayCommand(RapportHebdomadaire_Execute, RapportHebdomadaire_CanExecute);
            RapportMensuel = new RelayCommand(RapportMensuel_Execute, RapportMensuel_CanExecute);

        }

        public A22Sda2031887Context BdContext { get; set; }
        public ListVueModel ListProduitVm { get; set; }
        private Page _view1; public Page View1
        {
            get { return _view1; }
            set { _view1 = value; OnPropertyChanged(); }
        }
        private Page _view2; public Page View2
        {
            get { return _view2; }
            set { _view2 = value; OnPropertyChanged(); }
        }

        #region Pour le rapport hebdomadaire
        public RelayCommand RapportHebdomadaire { get; set; }
        public void RapportHebdomadaire_Execute(object? _)
        {
            DayOfTheWeekInfo NombreVenteDeLaSemaine = GetNbVenteParJoursPourSemaine(GetLundiOfSelectedWeek(DateRapportHebdo));
            DayOfTheWeekInfo SommeVenteDeLaSemaine = GetTotalVenteParJoursPourSemaine(GetLundiOfSelectedWeek(DateRapportHebdo));
            Decimal prixMoyenTransaction = GetMoyenneOfFactureForWeek(GetLundiOfSelectedWeek(DateRapportHebdo));

            View2 = new Hebdomadaire();
            View2.DataContext = new RapportHebdoVueModel(NombreVenteDeLaSemaine, SommeVenteDeLaSemaine, prixMoyenTransaction);
        }
        public bool RapportHebdomadaire_CanExecute(object? _)
        {
            return true;
        }

        private DateTime _dateRapportHebdo; public DateTime DateRapportHebdo
        {
            get { return _dateRapportHebdo; }
            set { _dateRapportHebdo = value; OnPropertyChanged(); }
        }

        private DateTime _maxDateHebdo; public DateTime MaxDateHebdo
        {
            get { return _maxDateHebdo; }
            set { _maxDateHebdo = value; OnPropertyChanged(); }
        }


        private DateTime GetPrecedingDimanche()
        {
            DateTime currentDate = DateTime.Now;

            // Calcule le nombre de jours qui se sont écoulés depuis dimanche
            int daysSinceSunday = (int)currentDate.DayOfWeek;

            // Calcule la date du dernier dimanche en soustrayant le nombre de jours depuis dimanche
            DateTime lastSunday = currentDate.AddDays(-daysSinceSunday);

            return lastSunday;
        }
        private DateTime GetPrecedingLundi()
        {
            DateTime today = DateTime.Today;
            DateTime lastSunday = today.AddDays(-(int)today.DayOfWeek);
            DateTime lastMonday = lastSunday.AddDays(-6);

            return lastMonday;
        }
        private decimal GetMoyenneOfFactureForWeek(DateTime lundiOfWeek)
        {
            List<Tblfacture> LesFacturesDeLaSemaine = BdContext.Tblfactures.Where(
               e => e.Date.Date >= lundiOfWeek.Date &&
               e.Date.Date <= lundiOfWeek.AddDays(6).Date)
               .ToList();
            BdContext.Tblproduitfactures.Load();
            if (LesFacturesDeLaSemaine.Count() == 0)
                return 0m;

            decimal moyenne = 0m;

            foreach(Tblfacture facture in LesFacturesDeLaSemaine)
            {
                moyenne += facture.CoutPartiel;
            }
            return moyenne / LesFacturesDeLaSemaine.Count();
        }


        public DateTime GetLundiOfSelectedWeek(DateTime dateInTheWeek)
        {
            int daysUntilMonday = ((int)dateInTheWeek.DayOfWeek - 1 + 7) % 7; // Récupère le nombre de jours restants avant lundi
            DateTime monday = dateInTheWeek.AddDays(-daysUntilMonday); // Soustrait ce nombre de jours à la date pour obtenir le lundi de la semaine sélectionnée
            return monday;
        }

        public DayOfTheWeekInfo GetNbVenteParJoursPourSemaine(DateTime lundiOfWeek)
        {
            List<Tblfacture> LesFacturesDeLaSemaine = BdContext.Tblfactures.Where(
               e => e.Date.Date >= lundiOfWeek.Date &&
               e.Date.Date <= lundiOfWeek.AddDays(6).Date)
               .ToList();

            return new DayOfTheWeekInfo
            {
                Lundi = new KeyValuePair<DateTime, decimal>(lundiOfWeek, LesFacturesDeLaSemaine.Where(e => e.Date.Date == lundiOfWeek.Date).Count()),
                Mardi = new KeyValuePair<DateTime, decimal>(lundiOfWeek.AddDays(1), LesFacturesDeLaSemaine.Where(e => e.Date.Date == lundiOfWeek.AddDays(1).Date).Count()),
                Mercredi = new KeyValuePair<DateTime, decimal>(lundiOfWeek.AddDays(2), LesFacturesDeLaSemaine.Where(e => e.Date.Date == lundiOfWeek.AddDays(2).Date).Count()),
                Jeudi = new KeyValuePair<DateTime, decimal>(lundiOfWeek.AddDays(3), LesFacturesDeLaSemaine.Where(e => e.Date.Date == lundiOfWeek.AddDays(3).Date).Count()),
                Vendredi = new KeyValuePair<DateTime, decimal>(lundiOfWeek.AddDays(4), LesFacturesDeLaSemaine.Where(e => e.Date.Date == lundiOfWeek.AddDays(4).Date).Count()),
                Samedi = new KeyValuePair<DateTime, decimal>(lundiOfWeek.AddDays(5), LesFacturesDeLaSemaine.Where(e => e.Date.Date == lundiOfWeek.AddDays(5).Date).Count()),
                Dimanche = new KeyValuePair<DateTime, decimal>(lundiOfWeek.AddDays(6), LesFacturesDeLaSemaine.Where(e => e.Date.Date == lundiOfWeek.AddDays(6).Date).Count())
            };
        }

        public DayOfTheWeekInfo GetTotalVenteParJoursPourSemaine(DateTime lundiOfWeek)
        {
            List<Tblfacture> LesFacturesDeLaSemaine = BdContext.Tblfactures.Where(
               e => e.Date.Date >= lundiOfWeek.Date &&
               e.Date.Date <= lundiOfWeek.AddDays(6).Date)
               .ToList();
            BdContext.Tblproduitfactures.Load();

            return new DayOfTheWeekInfo
            {
                Lundi = new KeyValuePair<DateTime, decimal>(lundiOfWeek, GetTotalVenteForDay(LesFacturesDeLaSemaine.Where(e => e.Date.Day == lundiOfWeek.Day).ToList())),
                Mardi = new KeyValuePair<DateTime, decimal>(lundiOfWeek.AddDays(1), GetTotalVenteForDay(LesFacturesDeLaSemaine.Where(e => e.Date.Day == lundiOfWeek.AddDays(1).Day).ToList())),
                Mercredi = new KeyValuePair<DateTime, decimal>(lundiOfWeek.AddDays(2), GetTotalVenteForDay(LesFacturesDeLaSemaine.Where(e => e.Date.Day == lundiOfWeek.AddDays(2).Day).ToList())),
                Jeudi = new KeyValuePair<DateTime, decimal>(lundiOfWeek.AddDays(3), GetTotalVenteForDay(LesFacturesDeLaSemaine.Where(e => e.Date.Day == lundiOfWeek.AddDays(3).Day).ToList())),
                Vendredi = new KeyValuePair<DateTime, decimal>(lundiOfWeek.AddDays(4), GetTotalVenteForDay(LesFacturesDeLaSemaine.Where(e => e.Date.Day == lundiOfWeek.AddDays(4).Day).ToList())),
                Samedi = new KeyValuePair<DateTime, decimal>(lundiOfWeek.AddDays(5), GetTotalVenteForDay(LesFacturesDeLaSemaine.Where(e => e.Date.Day == lundiOfWeek.AddDays(5).Day).ToList())),
                Dimanche = new KeyValuePair<DateTime, decimal>(lundiOfWeek.AddDays(6), GetTotalVenteForDay(LesFacturesDeLaSemaine.Where(e => e.Date.Day == lundiOfWeek.AddDays(6).Day).ToList())),
            };
        }
        public decimal GetTotalVenteForDay(List<Tblfacture> factureDuJours)
        {
            decimal total = 0m;

            foreach (Tblfacture facture in factureDuJours)
                total += facture.CoutPartiel;

             return total;
        }
        #endregion


        #region Rapport Mensuel
        public RelayCommand RapportMensuel { get; set; }
        public void RapportMensuel_Execute(object? _)
        {
            Tblrapportmensuel rapportRecherche = BdContext.Tblrapportmensuels
                .FirstOrDefault(e => e.DateRapport.Year == DateRapportMensuel.Year && e.DateRapport.Month == DateRapportMensuel.Month);

            if (rapportRecherche is null)
            {
                Tblrapportmensuel newTblRapportMensuel = CreateAndReturnRapportMensuel();
                View2 = new Mensuel();
                View2.DataContext = new RapportMensuelVueModel(newTblRapportMensuel);
            }
            else
            {
                Tblrapportmensuel newTblRapportMensuel = CreateRapportMensuelFromSelectedDate();
                if (!rapportRecherche.Equals(newTblRapportMensuel))
                    UpdateRapportMensuel(newTblRapportMensuel, rapportRecherche.IdRapportMensuel);

                View2 = new Mensuel();
                View2.DataContext = new RapportMensuelVueModel(newTblRapportMensuel);
            }
        }
        public bool RapportMensuel_CanExecute(object? _)
        {
            return true;
        }
        private DateTime _dateRapportMensuel; public DateTime DateRapportMensuel
        {
            get { return _dateRapportMensuel; }
            set { _dateRapportMensuel = value; OnPropertyChanged(); }
        }


        private DateTime _maxDateMensuel; public DateTime MaxDateMensuel
        {
            get { return _maxDateMensuel; }
            set { _maxDateMensuel = value; OnPropertyChanged(); }
        }
        private DateTime SelectFirstDayOfLastMonth()
        {
            DateTime today = DateTime.Today;
            return new DateTime(today.Year, today.Month, 1).AddMonths(-1);
        }
        private DateTime SelectLastDayOfLastMonth()
        {
            return SelectFirstDayOfLastMonth().AddMonths(1).AddDays(-1);
        }

        private void UpdateRapportMensuel(Tblrapportmensuel newData, int id)
        {
            newData.IdRapportMensuel = id;
            BdContext.Tblrapportmensuels.Update(newData);
            BdContext.SaveChanges();
        }


        private Tblrapportmensuel CreateRapportMensuelFromSelectedDate()
        {
            List<Tblfacture> facturesForThisMonth = BdContext.Tblfactures
                   .Where(e => e.Date.Year == DateRapportMensuel.Year && e.Date.Month == DateRapportMensuel.Month)
                   .ToList();
            BdContext.Tblproduitfactures.Load();
            decimal sommeVente = 0m;
            foreach (Tblfacture facture in facturesForThisMonth)
                sommeVente += facture.CoutPartiel;

            decimal valeurMoyenneTransaction = sommeVente / facturesForThisMonth.Count;
            int nbTransaction = facturesForThisMonth.Count;

            Tblrapportmensuel thisRapportMensuel = new Tblrapportmensuel
            {
                DateRapport = DateRapportMensuel,
                SommeVente = sommeVente,
                NbTransactionTotal = nbTransaction,
                ValeurMoyTransaction = valeurMoyenneTransaction
            };
            return thisRapportMensuel;
        }
        private int CreateAndReturnRapportMensuelID()
        {
            try
            {
                return CreateAndReturnRapportMensuel().IdRapportMensuel;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        private Tblrapportmensuel CreateAndReturnRapportMensuel()
        {
            try
            {
                Tblrapportmensuel tempRapportMensuel = CreateRapportMensuelFromSelectedDate();
                BdContext.Tblrapportmensuels.Add(tempRapportMensuel);
                BdContext.SaveChanges();
                return tempRapportMensuel;
            }
            catch (Exception)
            {
                return default(Tblrapportmensuel);
            }
        }
        #endregion


        #region Event
        public void ChangeUpdatePage(object? sender, EventArgs e)
        {
            AjouterModifierVueModel tempsVm = new AjouterModifierVueModel(ListProduitVm.SelectedProduct);
            tempsVm.ChangeToListPageUpdate += ChangeToListPageUpdate;
            tempsVm.ChangeToListPageNoUpdate += ChangeToListPageNoUpdate;
            View1 = new AjouterModifier();
            View1.DataContext = tempsVm;
        }

        public void ChangeCreatePage(object? sender, EventArgs e)
        {
            AjouterModifierVueModel tempsVm = new AjouterModifierVueModel();
            tempsVm.ChangeToListPageUpdate += ChangeToListPageUpdate;
            tempsVm.ChangeToListPageNoUpdate += ChangeToListPageNoUpdate;
            View1 = new AjouterModifier();
            View1.DataContext = tempsVm;
        }

        public void ChangeToListPageUpdate(object? sender, EventArgs e)
        {
            View1 = new ListProduit();
            ListProduitVm.LesProduits = new ObservableCollection<Tblproduit>(BdContext.Tblproduits.ToList());
            BdContext.Tbldepartements.Load();
            View1.DataContext = ListProduitVm;
        }
        public void ChangeToListPageNoUpdate(object? sender, EventArgs e)
        {
            View1 = new ListProduit();
            View1.DataContext = ListProduitVm;
        }

        #endregion

    }
}
