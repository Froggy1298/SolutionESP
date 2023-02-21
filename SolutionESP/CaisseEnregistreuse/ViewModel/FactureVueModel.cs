using Microsoft.EntityFrameworkCore;
using ProjectHelper.Models.ModelsProduitFacture;
using ProjectHelper.ViewModel;
using ProjetHelper.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;

namespace CaisseEnregistreuse.ViewModel
{
    public class FactureVueModel : BaseViewModel
    {
        public FactureVueModel(int idFacture)
        {
            BdContext = new A22Sda2031887Context();
            LaFacture = new ObservableCollection<Tuple<string, ObservableCollection<Tblproduitfacture>>>();
            BdContext.Tblproduits.Load();
            BdContext.Tblfactures.Load();
            BdContext.Tbldepartements.Load();

            InfoFacture = BdContext.Tblfactures.Find(idFacture);
            var lesProduitsFacture = BdContext.Tblproduitfactures.Where(x => x.IdFacture == idFacture).ToList();
            var lesProduitsFactureTrie = lesProduitsFacture.GroupBy(x => x.IdProduitNavigation.IdDepartement).ToList();

            foreach(var LesProduitFacture  in lesProduitsFactureTrie)
            {
                string nomDep = LesProduitFacture.First().IdProduitNavigation.IdDepartementNavigation.NomDepartement;
                ObservableCollection<Tblproduitfacture> tempProduitFacture = new ObservableCollection<Tblproduitfacture>();
                foreach(var ProduitFacture in LesProduitFacture)
                {
                   tempProduitFacture.Add(ProduitFacture);
                }
                LaFacture.Add(new Tuple<string, ObservableCollection<Tblproduitfacture>>(nomDep, tempProduitFacture));
            }

        }
        public A22Sda2031887Context BdContext { get; set; }


        private ObservableCollection<Tuple<string, ObservableCollection<Tblproduitfacture>>> _laFacture;

        public ObservableCollection<Tuple<string, ObservableCollection<Tblproduitfacture>>> LaFacture
        {
            get { return _laFacture; }
            set { _laFacture = value; }
        }

        private Tblfacture _infoFacture;

        public Tblfacture InfoFacture
        {
            get { return _infoFacture; }
            set { _infoFacture = value; }
        }





    }
}
