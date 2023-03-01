using GestionInventaire.Vue;
using Microsoft.EntityFrameworkCore;
using ProjectHelper.Models.ModelsProduit;
using ProjectHelper.ViewModel;
using ProjetHelper.Models;
using RelayCommandLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GestionInventaire.VueModel
{
    public class MainVueModel : BaseViewModel
    {
        public MainVueModel()
        {
            BdContext = new A22Sda2031887Context();
            View = new ListProduit();
            ListProduitVm = new ListVueModel();
            ListProduitVm.ChangeToUpdatePage += ChangeUpdatePage;
            ListProduitVm.ChangeToCreatePage += ChangeCreatePage;
            View.DataContext = ListProduitVm;

            DateRapportMensuel = SelectFirstDayOfLastMonth();
            MaxDateMensuel = SelectLastDayOfLastMonth();

            RapportHebdomadaire = new RelayCommand(RapportHebdomadaire_Execute, RapportHebdomadaire_CanExecute);
            RapportMensuel = new RelayCommand(RapportMensuel_Execute, RapportMensuel_CanExecute);

        }

        public A22Sda2031887Context BdContext { get; set; }
        public ListVueModel ListProduitVm { get; set; }
        private Page _view1; public Page View
        {
            get { return _view1; }
            set { _view1 = value; OnPropertyChanged(); }
        }
        private Page _view2; public Page View2
        {
            get { return _view2; }
            set { _view2 = value; OnPropertyChanged(); }
        }

        public RelayCommand RapportHebdomadaire { get; set; }
        public void RapportHebdomadaire_Execute(object? _)
        {

        }
        public bool RapportHebdomadaire_CanExecute(object? _)
        {
            return false;
        }


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
        private DateTime _dateRapportMensuel;

        public DateTime DateRapportMensuel
        {
            get { return _dateRapportMensuel; }
            set { _dateRapportMensuel = value; OnPropertyChanged(); }
        }


        private DateTime _maxDateMensuel;

        public DateTime MaxDateMensuel
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



        private DateTime SelectLastSunday()
        {
            DateTime currentDate = DateTime.Now;

            // Calcule le nombre de jours qui se sont écoulés depuis dimanche
            int daysSinceSunday = (int)currentDate.DayOfWeek;

            // Calcule la date du dernier dimanche en soustrayant le nombre de jours depuis dimanche
            DateTime lastSunday = currentDate.AddDays(-daysSinceSunday);

            return lastSunday;
        }




        #region Event
        public void ChangeUpdatePage(object? sender, EventArgs e)
        {
            AjouterModifierVueModel tempsVm = new AjouterModifierVueModel(ListProduitVm.SelectedProduct);
            tempsVm.ChangeToListPageUpdate += ChangeToListPageUpdate;
            tempsVm.ChangeToListPageNoUpdate += ChangeToListPageNoUpdate;
            View = new AjouterModifier();
            View.DataContext = tempsVm;
        }

        public void ChangeCreatePage(object? sender, EventArgs e)
        {
            AjouterModifierVueModel tempsVm = new AjouterModifierVueModel();
            tempsVm.ChangeToListPageUpdate += ChangeToListPageUpdate;
            tempsVm.ChangeToListPageNoUpdate += ChangeToListPageNoUpdate;
            View = new AjouterModifier();
            View.DataContext = tempsVm;
        }

        public void ChangeToListPageUpdate(object? sender, EventArgs e)
        {
            View = new ListProduit();
            ListProduitVm.LesProduits = new ObservableCollection<Tblproduit>(BdContext.Tblproduits.ToList());
            BdContext.Tbldepartements.Load();
            View.DataContext = ListProduitVm;
        }
        public void ChangeToListPageNoUpdate(object? sender, EventArgs e)
        {
            View = new ListProduit();
            View.DataContext = ListProduitVm;
        }

        #endregion

    }
}
