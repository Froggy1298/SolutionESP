using Microsoft.EntityFrameworkCore;
using ProjectHelper.Models.ModelsProduit;
using ProjectHelper.ViewModel;
using ProjetHelper.Models;
using RelayCommandLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace GestionInventaire.VueModel
{
    public class AjouterModifierVueModel : BaseViewModel
    {
        public AjouterModifierVueModel()
        {
            InitializeButton();
            ContentButton = "Ajouter";
            TheProduct = new Tblproduit();
        }

        public AjouterModifierVueModel(Tblproduit productToUpdate)
        {
            InitializeButton();
            ContentButton = "Modifier";
            TheProduct = productToUpdate;
            BdContext.Tbldepartements.Load();

        }

        public void InitializeButton()
        {
            BdContext = new A22Sda2031887Context();
            BoutonAnnuler = new RelayCommand(Annuler_Execute, Annuler_CanExecute);
            BoutonCreerModifier = new RelayCommand(CreerModifier_Execute, CreerModifier_CanExecute);
            LesDepartements = new ObservableCollection<Tbldepartement>(BdContext.Tbldepartements.ToList());
        }

        public event EventHandler<EventArgs> ChangeToListPage;
        public A22Sda2031887Context BdContext { get; set; }


        private ObservableCollection<Tbldepartement> lesDepartements; public ObservableCollection<Tbldepartement> LesDepartements
        {
            get { return lesDepartements; }
            set { lesDepartements = value; OnPropertyChanged(); }
        }

        private string contentButton; public string ContentButton
        {
            get { return contentButton; }
            set { contentButton = value; OnPropertyChanged(); }
        }

        private Tblproduit _theProduct; public Tblproduit TheProduct
        {
            get { return _theProduct; }
            set { _theProduct = value; OnPropertyChanged(); }
        }

        public RelayCommand BoutonCreerModifier { get; set; }
        public void CreerModifier_Execute(object? _)
        {
            //TODO mettre la logique de modifier ajouter
        }
        public bool CreerModifier_CanExecute(object? _)
        {
            if (TheProduct.Cup.Length != 12)
                return false;

            if (!Int64.TryParse(TheProduct.Cup, out long num))
                return false;

            if (TheProduct.Prix == 0)
                return false;

            if (TheProduct.IdDepartementNavigation is null)
                return false;

            if (string.IsNullOrEmpty(TheProduct.Nom))
                return false;

            if (!Convert.ToBoolean(TheProduct.VentePoids))
                if(TheProduct.QteInventaire % 1 != 0)
                    return false;

            return true;
        }


        #region Bouton pour cancel
        public RelayCommand BoutonAnnuler { get; set; }
        public void Annuler_Execute(object? _)
        {
            ChangeToListPage.Invoke(this, new EventArgs());
        }
        public bool Annuler_CanExecute(object? _)
        {
            return true;
        }
        #endregion

    }
}
