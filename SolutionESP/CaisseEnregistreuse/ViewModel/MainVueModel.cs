using ProjectHelper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CaisseEnregistreuse.Vue;
using ProjetHelper;
using RelayCommandLibrary;
using System.Security.Policy;
using System.Windows;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using ProjectHelper.Models.ModelsProduitFacture;
using ProjetHelper.Models;
using Microsoft.EntityFrameworkCore;
using ProjectHelper.Models.ModelsProduit;
using System.Diagnostics.Eventing.Reader;

namespace CaisseEnregistreuse.ViewModel
{
    public class MainVueModel : BaseViewModel
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        public MainVueModel()
        {
            BoutonPayer = new RelayCommand(Payer_Execute, Payer_CanExecute);
            PayerBouttonVisibility = Visibility.Visible;

            //Initialisation des Vms et l'event
            PanierVm = new PanierVueModel();
            ChoixPaimentVm = new ChoixPaimentVueModel();
            ChoixPaimentVm.ChangeToThanksPage += PaymentDone;

            View = new Panier();
            View.DataContext = PanierVm;

            BdContext = new A22Sda2031887Context();
            BdContext.Tblproduits.Load();
            BdContext.Tblfactures.Load();
        }



        public A22Sda2031887Context BdContext { get; set; }
        public PanierVueModel PanierVm { get; set; }
        public ChoixPaimentVueModel ChoixPaimentVm { get; set; }

        /// <summary>
        /// Se lance apres que le paiment soit fait
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PaymentDone(object? sender, EventArgs e)
        {
            int newFactureID = CreateAndReturnFactureID();
            foreach (var x in PanierVm.LesProduitsPaniers)
            {
                addProduitFacture(x, newFactureID);
                updateDataBaseQte(x.Product.IdProduit, (decimal)x.NbFoisCommandee);
            }
            BdContext.SaveChanges();
            View = new MerciAvoirMagasiner();


            FactureVueModel newFactureVueModel = new FactureVueModel(newFactureID);
            Facture thisFacture = new Facture();
            thisFacture.DataContext = newFactureVueModel;
            thisFacture.Show();
        }

        /// <summary>
        /// Met à jours la quantité des produits dans la base de donnée
        /// </summary>
        /// <param name="IdproductToUpdate"></param>
        /// <param name="quantityToRemove"></param>
        private async void updateDataBaseQte(int IdproductToUpdate, decimal quantityToRemove)
        {
            Tblproduit tempProduit = await BdContext.Tblproduits.FindAsync(IdproductToUpdate);
            tempProduit.QteInventaire = tempProduit.QteInventaire - quantityToRemove;
            BdContext.Tblproduits.Update(tempProduit);
        }

        /// <summary>
        /// Ajoute un produitFacture valide à la base de donnée
        /// </summary>
        /// <param name="x"></param>
        /// <param name="idFacture"></param>
        private void addProduitFacture(ProduitFacturePanierDTO x, int idFacture)
        {
            BdContext.Tblproduitfactures.Add(ProduitFacturePanierDTO.DTOToObject(x, idFacture));
        }

        /// <summary>
        /// Créer la facture et retourne son Id
        /// </summary>
        /// <returns></returns>
        private int CreateAndReturnFactureID()
        {
            Tblfacture thisFacture = new Tblfacture
            {
                Date = DateTime.Now,
                ModePaiment = ChoixPaimentVm.ChoixPaimentFinal,
                Etat = "Paye",
                Tvqpercent = PanierVueModel.TVQ,
                Tpspercent = PanierVueModel.TPS
            };
            BdContext.Tblfactures.Add(thisFacture);
            BdContext.SaveChanges();

            return thisFacture.IdFacture;
        }


        #region Le bouton qui permet au client de choisir son mode de paiement
        public RelayCommand BoutonPayer { get; set; }
        public void Payer_Execute(object? _)
        {
            PayerBouttonVisibility = Visibility.Collapsed;
            View = new ChoixPaiment();
            View.DataContext = ChoixPaimentVm;
        }
        public bool Payer_CanExecute(object? _)
        {
            return PanierVm.QteProduitPanier != 0;
        }
        #endregion


        private Page view; public Page View
        {
            get { return view; }
            set { view = value; OnPropertyChanged(); }
        }

        private Visibility payerBoutonVisibility; public Visibility PayerBouttonVisibility
        {
            get { return payerBoutonVisibility; }
            set { payerBoutonVisibility = value; OnPropertyChanged(); }
        }



    }
}
