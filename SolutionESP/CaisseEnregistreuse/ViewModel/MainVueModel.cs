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
        private async void updateDataBaseQte(int IdproductToUpdate, decimal quantityToRemove)
        {
            Tblproduit tempProduit = await BdContext.Tblproduits.FindAsync(IdproductToUpdate);
            tempProduit.QteInventaire = tempProduit.QteInventaire - quantityToRemove;
            BdContext.Tblproduits.Update(tempProduit);
        }

        private void addProduitFacture(ProduitFacturePanierDTO x, int idFacture)
        {
            BdContext.Tblproduitfactures.Add(ProduitFacturePanierDTO.DTOToObject(x, idFacture));
        }


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
