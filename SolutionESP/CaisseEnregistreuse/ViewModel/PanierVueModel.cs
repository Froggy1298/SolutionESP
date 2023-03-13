using CaisseEnregistreuse.Vue;
using ProjectHelper.ViewModel;
using ProjetHelper.Models;
using RelayCommandLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ProjectHelper.Models.ModelsProduit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjectHelper.Models.ModelsProduitFacture;
using System.Collections.ObjectModel;
using System.Security.Policy;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.Internal;

namespace CaisseEnregistreuse.ViewModel
{
    public class PanierVueModel : BaseViewModel
    {

        public const decimal TVQ = 0.09975m;
        public const decimal TPS = 0.05m;


        public PanierVueModel()
        {
            BoutonEntrerCUP = new RelayCommand(EntrerCUP_Execute, EntrerCUP_CanExecute);
            BoutonRMProduitPanier = new RelayCommand(RMProduitPanier_Execute, RMProduitPanier_CanExecute);

            BdContext = new A22Sda2031887Context();
            BdContext.Tblproduits.Load();

            LesProduitsPaniers = new ObservableCollection<ProduitFacturePanierDTO>();
        }

        public ObservableCollection<ProduitFacturePanierDTO> LesProduitsPaniers { get; set; }
        public A22Sda2031887Context BdContext { get; set; }

       #region Pour les prix à droite
        public int QteProduitPanier
        {
            get
            {
                int sumVentePoidT = LesProduitsPaniers.Where(x => x.Product.VentePoids == true).Count();
                int sumVentePoidF = LesProduitsPaniers.Where(x => x.Product.VentePoids == false).Sum(x => (int)x.NbFoisCommandee);
                return sumVentePoidF + sumVentePoidT;
            }
        }
        public decimal TotalPartiel
        {
            get
            {
                return Math.Round(LesProduitsPaniers.Sum(x => x.Product.Prix * (decimal)x.NbFoisCommandee), 2);
            }
        }
        public decimal TotalTVQ
        {
            get
            {
                return Math.Round((LesProduitsPaniers.Where(x => x.Product.Tvq == true)).Sum(x => (x.Product.Prix * (decimal)x.NbFoisCommandee) * TVQ), 2);
            }
        }
        public decimal TotalTPS
        {
            get
            {
                return Math.Round((LesProduitsPaniers.Where(x => x.Product.Tps == true)).Sum(x => (x.Product.Prix * (decimal)x.NbFoisCommandee) * TPS), 2);
            }
        }
        public decimal Total
        {
            get
            {
                return Math.Round(TotalPartiel + TotalTPS + TotalTVQ, 2);
            }
        }
        #endregion



        #region Le bouton pour enlever un produit du panier
        public RelayCommand BoutonRMProduitPanier { get; set; }
        public void RMProduitPanier_Execute(object? parameter)
        {
            var tempProduitPanier = LesProduitsPaniers.FirstOrDefault(x => x.Product.IdProduit == (int)parameter);
            if (tempProduitPanier.Product.VentePoids)
                LesProduitsPaniers.Remove(tempProduitPanier);
            else
            {
                if (tempProduitPanier.NbFoisCommandee == 1)
                    LesProduitsPaniers.Remove(tempProduitPanier);
                else
                    tempProduitPanier.NbFoisCommandee -= 1;
            }
            UpdateAllPrice();
        }
        public bool RMProduitPanier_CanExecute(object? _)
        {
            return true;
        }
        #endregion

        #region Le bouton qui va ouvrir l'interface pour demander le CUP
        public RelayCommand BoutonEntrerCUP { get; set; }
        public void EntrerCUP_Execute(object? _)
        {
            VueEntrerCUP enterCUP = new VueEntrerCUP();
            enterCUP.ShowDialog();

            if (enterCUP.EnterOrCancel == VueEntrerCUP.FinalButtonPressed.Annuler)
                return;

            AddProductToPanierAsync(enterCUP.CUP);

        }
        public bool EntrerCUP_CanExecute(object? _)
        {
            return true;
        }
        #endregion

        /// <summary>
        /// Permet de mettre à jours tous les propriétés des prix à droite
        /// </summary>
        private void UpdateAllPrice()
        {
            OnPropertyChanged(nameof(QteProduitPanier));
            OnPropertyChanged(nameof(TotalPartiel));
            OnPropertyChanged(nameof(TotalTVQ));
            OnPropertyChanged(nameof(TotalTPS));
            OnPropertyChanged(nameof(Total));
        }

        /// <summary>
        /// Tous le procésus d'ajout d'un produit au panier
        /// </summary>
        /// <param name="CUPProduit"></param>
        /// <returns></returns>
        private async Task<bool> AddProductToPanierAsync(string CUPProduit)
        {
            try
            {
                //Cherche le produit dans la base de données
                Tblproduit produit = await BdContext.Tblproduits.FirstOrDefaultAsync(x => x.Cup == CUPProduit);

                if (produit is null) //Si le produit existe
                {
                    MessageBox.Show("Produit entré non trouvé", "Non trouvé");
                    return false;
                }

                if (produit.QteInventaire == 0) //Si la quantité du produit est suffisante
                {
                    MessageBox.Show("Quantité inventaire insufisante", "Quantité insufisante");
                    return false;
                }


                //Fait apparaitre l'interface pour demander la quantité
                VueQuantite vueQuantite = new VueQuantite(Convert.ToBoolean(produit.VentePoids));
                vueQuantite.ShowDialog();
                decimal quantiteProduit = vueQuantite.QuantityFinal;

                ProduitFacturePanierDTO tempsProduitFacture = LesProduitsPaniers.Where(x => x.Product.Cup == produit.Cup).FirstOrDefault();

                if (tempsProduitFacture is not null) //Se le produit se trouve déja dans le panier
                {
                    decimal newQuantity = quantiteProduit + (decimal)tempsProduitFacture.NbFoisCommandee;
                    //Vérifie si la nouvelle quantité est assez pour l'inventaire actuel
                    if (produit.QteInventaire < newQuantity) //Vérifie la qte inventaire
                    {
                        MessageBox.Show("Quantité inventaire insuffisante", "Erreur quantité");
                        return false;
                    }
                    tempsProduitFacture.NbFoisCommandee = newQuantity;
                    UpdateAllPrice();
                    return true;
                }
                else //Si le produit ne se trouve pas dans le panier
                {
                    if (produit.QteInventaire < quantiteProduit) //Vérifie la qte inventaire
                    {
                        MessageBox.Show("Quantité inventaire insuffisante", "Erreur quantité");
                        return false;
                    }
                    ProduitPanierDTO dtoProduitFacture = ProduitPanierDTO.ProduitToDTO(produit);

                    LesProduitsPaniers.Add(new ProduitFacturePanierDTO(dtoProduitFacture, quantiteProduit, produit.Prix));
                    UpdateAllPrice();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Produit entré non trouvé", "Non trouvé");
                return false;
            }
        }
    }
}
