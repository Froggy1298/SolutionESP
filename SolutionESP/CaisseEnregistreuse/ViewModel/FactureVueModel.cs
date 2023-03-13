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
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="idFacture">L'id de la facture que nous vonlons faire apparaître</param>
        public FactureVueModel(int idFacture)
        {
            BdContext = new A22Sda2031887Context();
            LaFacture = new ObservableCollection<Tuple<string, ObservableCollection<Tblproduitfacture>>>();
            BdContext.Tblproduits.Load();
            BdContext.Tblfactures.Load();
            BdContext.Tbldepartements.Load();

            InfoFacture = BdContext.Tblfactures.Find(idFacture); //Trouve la facture
            var lesProduitsFacture = BdContext.Tblproduitfactures.Where(x => x.IdFacture == idFacture).ToList(); //Trouve les produits factures
            var lesProduitsFactureTrie = lesProduitsFacture.GroupBy(x => x.IdProduitNavigation.IdDepartement).ToList(); //Filtre les produitfactures en departement

            foreach(var LesProduitFacture  in lesProduitsFactureTrie)
            {
                //En gros pour chaque départment, on va créer la liste des produitsfactures qui va être relier avec un departement
                string nomDep = LesProduitFacture.First().IdProduitNavigation.IdDepartementNavigation.NomDepartement;
                ObservableCollection<Tblproduitfacture> tempProduitFacture = new ObservableCollection<Tblproduitfacture>();
                foreach(var ProduitFacture in LesProduitFacture)
                {
                   tempProduitFacture.Add(ProduitFacture);
                }
                LaFacture.Add(new Tuple<string, ObservableCollection<Tblproduitfacture>>(nomDep, tempProduitFacture));
            }

        }

        /// <summary>
        /// Le context de notre base de donnée
        /// </summary>
        public A22Sda2031887Context BdContext { get; set; }

        /// <summary>
        /// Une liste contenant tuble qui contient le nom du département et la liste de produit facture relié à ce départment
        /// </summary>
        private ObservableCollection<Tuple<string, ObservableCollection<Tblproduitfacture>>> _laFacture;

        public ObservableCollection<Tuple<string, ObservableCollection<Tblproduitfacture>>> LaFacture
        {
            get { return _laFacture; }
            set { _laFacture = value; }
        }

        /// <summary>
        /// Les informations générale de la facture
        /// </summary>
        private Tblfacture _infoFacture;public Tblfacture InfoFacture
        {
            get { return _infoFacture; }
            set { _infoFacture = value; }
        }





    }
}
