using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjectHelper.Models.ModelsProduit;
using ProjectHelper.ViewModel;
using ProjetHelper.Models;
using RelayCommandLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestionInventaire.VueModel
{
    public class ListVueModel : BaseViewModel
    {
        public ListVueModel()
        {
            BdContext = new A22Sda2031887Context();
            LesProduits = new ObservableCollection<Tblproduit> (BdContext.Tblproduits.ToList());
            BdContext.Tbldepartements.Load();

            BoutonCreer = new RelayCommand(Creer_Execute, Creer_CanExecute);
            BoutonModifier = new RelayCommand(Modifier_Execute, Modifier_CanExecute);
            BoutonSupprimer = new RelayCommand(Supprimer_Execute, Supprimer_CanExecute);
        }

        public event EventHandler<EventArgs> ChangeToCreatePage;
        public event EventHandler<EventArgs> ChangeToUpdatePage;

        #region Bouton Pour créer un produit
        public RelayCommand BoutonCreer { get; set; }
        public void Creer_Execute(object? _)
        {
            ChangeToCreatePage.Invoke(this, new EventArgs());
        }
        public bool Creer_CanExecute(object? _)
        {
            return true;
        }
        #endregion


        #region Bouton Pour modifier un produit
        public RelayCommand BoutonModifier { get; set; }
        public void Modifier_Execute(object? _)
        {
            ChangeToUpdatePage.Invoke(this, new EventArgs());
        }
        public bool Modifier_CanExecute(object? _)
        {
            return SelectedProduct is not null;
        }
        #endregion


        #region Bouton Pour Supprimer un produit
        public RelayCommand BoutonSupprimer { get; set; }
        public void Supprimer_Execute(object? _)
        {
            if (MessageBox.Show("Voulez-vous vraiment effacer: " + SelectedProduct.Nom + "\n de la liste des produits", "Suppression", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            try
            {
                BdContext.Tblproduits.Remove(SelectedProduct);
                BdContext.SaveChanges();
                LesProduits = new ObservableCollection<Tblproduit>(BdContext.Tblproduits.ToList());
            }
            catch (Exception e)
            {
                string temp = e.InnerException is not null ? e.InnerException.Message : "";
                MessageBox.Show(e.Message + "\n" + temp, "Erreur lors de la suppression");
            }
        }
        public bool Supprimer_CanExecute(object? _)
        {
            return SelectedProduct is not null;
        }
        #endregion


        public A22Sda2031887Context BdContext { get; set; }

        private ObservableCollection<Tblproduit> _lesProduits;   public ObservableCollection<Tblproduit> LesProduits
        {
            get { return _lesProduits; }
            set { _lesProduits = value; OnPropertyChanged();}
        }

        private Tblproduit _selectedProduct;public Tblproduit SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged(); }
        }




    }
}
