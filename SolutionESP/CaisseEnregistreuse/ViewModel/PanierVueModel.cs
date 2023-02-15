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

namespace CaisseEnregistreuse.ViewModel
{
    class PanierVueModel : BaseViewModel
    {


        public PanierVueModel()
        {
            BoutonEntrerCUP = new RelayCommand(EntrerCUP_Execute, EntrerCUP_CanExecute);
            BdContext = new A22Sda2031887Context();
        }

        public ICollection<Tblproduitfacture> ProduitPanier { get; set; }
        public A22Sda2031887Context BdContext { get; set; }

        public RelayCommand BoutonEntrerCUP { get; set; }
        public void EntrerCUP_Execute(object? _)
        {
            VueEntrerCUP EnterCUP = new VueEntrerCUP();
            EnterCUP.ShowDialog();

            if (EnterCUP.EnterOrCancel == VueEntrerCUP.FinalButtonPressed.Annuler)
                return;

            long intCUP;
            if (long.TryParse(EnterCUP.CUP, out intCUP))
                AddProductToPanierAsync(intCUP);






        }
        public bool EntrerCUP_CanExecute(object? _)
        {
            return true;
        }

        private async Task<bool> AddProductToPanierAsync(long intCUP)
        {
            try
            {
                TblProduitPanierDTO theProduct = TblProduitPanierDTO
                    .IngredientToDTO(await BdContext.Tblproduits.Where(x => x.Cup == intCUP).FirstAsync());

                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Produit entré non trouvé","Non trouvré");
                return false;
            }
        }
    }
}
