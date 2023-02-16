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

namespace CaisseEnregistreuse.ViewModel
{
    public class PanierVueModel : BaseViewModel
    {


        public PanierVueModel()
        {
            BoutonEntrerCUP = new RelayCommand(EntrerCUP_Execute, EntrerCUP_CanExecute);
            BoutonRMProduitPanier = new RelayCommand(RMProduitPanier_Execute, RMProduitPanier_CanExecute);
            BdContext = new A22Sda2031887Context();
            LesProduitsPaniers = new ObservableCollection<ProduitFacturePanierDTO>();
        }


        public ObservableCollection<ProduitFacturePanierDTO> LesProduitsPaniers { get; set; }
        public A22Sda2031887Context BdContext { get; set; }
        public int QteProduitPanier
        {
            get
            {
                return LesProduitsPaniers.Count;
            }
        }

        public RelayCommand BoutonRMProduitPanier { get; set; }
        public void RMProduitPanier_Execute(object? parameter)
        {
            LesProduitsPaniers.Remove(LesProduitsPaniers.FirstOrDefault(x => x.Product.IdProduit == (int)parameter));
        }
        public bool RMProduitPanier_CanExecute(object? _)
        {
            return true;
        }


        public RelayCommand BoutonEntrerCUP { get; set; }
        public void EntrerCUP_Execute(object? _)
        {
            VueEntrerCUP enterCUP = new VueEntrerCUP();
            enterCUP.ShowDialog();

            if (enterCUP.EnterOrCancel == VueEntrerCUP.FinalButtonPressed.Annuler)
                return;

            long intCUP;
            if (long.TryParse(enterCUP.CUP, out intCUP))
                AddProductToPanierAsync(intCUP);

        }
        public bool EntrerCUP_CanExecute(object? _)
        {
            return true;
        }

        private Task<Tblproduit?> SearchProduct(long CUP)
        {
           return BdContext.Tblproduits.Where(x => x.Cup == CUP).FirstOrDefaultAsync();
        }

        private async Task<bool> AddProductToPanierAsync(long intCUP)
        {
            try
            {
                //Lacer method recherche produit sans await
                //Afficher truc a qte
                //prend ça
                //await la méthode recherche produit
                //Validate si le produit le produit existe
                //si oui add to panier
                //Si non affficher erreur


    
                Task searchProductTask = SearchProduct(intCUP);


                VueQuantite vueQuantite = new VueQuantite(true);
               
                vueQuantite.ShowDialog();
                decimal quantiteProduit = vueQuantite.QuantityFinal;

                ProduitPanierDTO theProduct = 

                LesProduitsPaniers.Add(new ProduitFacturePanierDTO(theProduct, quantiteProduit, theProduct.Prix));
                OnPropertyChanged(nameof(QteProduitPanier));

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Produit entré non trouvé", "Non trouvé");
                return false;
            }
        }
    }
}
