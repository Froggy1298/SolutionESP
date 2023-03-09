using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaisseEnregistreuse.ViewModel;
using FluentAssertions;
using ProjectHelper.Models.ModelsProduit;
using ProjectHelper.Models.ModelsProduitFacture;

namespace CaisseEnregistreuseTest
{
    [TestClass]
    public class PanierVueModelTest
    {

        [TestMethod]
        public void TestConstructor()
        {
            PanierVueModel PanierVm = new PanierVueModel();
            PanierVm.Should().NotBeNull();
        }

        [TestMethod]
        public void TestPrixDroiteOnlyTaxe()
        {
            PanierVueModel PanierVm = new PanierVueModel();
            PanierVm.Should().NotBeNull();

            PanierVm.LesProduitsPaniers = CreateFakePanier();

            PanierVm.QteProduitPanier.Should().Be(8);
            PanierVm.TotalPartiel.Should().Be(91.88m);
            PanierVm.TotalTVQ.Should().Be(4.58m);
            PanierVm.TotalTPS.Should().Be(2.30m);
            PanierVm.Total.Should().Be(98.76m);
        }

        [TestMethod]
        public void RemoveProduitSellByWeight()
        {
            PanierVueModel PanierVm = new PanierVueModel();
            PanierVm.Should().NotBeNull();

            PanierVm.LesProduitsPaniers = CreateFakePanier();
            int longueurAvant = PanierVm.LesProduitsPaniers.Count();

            PanierVm.RMProduitPanier_Execute(PanierVm.LesProduitsPaniers.Where(x => x.Product.IdProduit == 1).First().Product.IdProduit);

            PanierVm.LesProduitsPaniers.Count().Should().Be(longueurAvant - 1);
        }

        [TestMethod]
        public void RemoveProduitSellByUnit()
        {
            PanierVueModel PanierVm = new PanierVueModel();
            PanierVm.Should().NotBeNull();

            PanierVm.LesProduitsPaniers = CreateFakePanier();
            decimal? qteAcheteAvant = PanierVm.LesProduitsPaniers.Where(x => x.Product.IdProduit == 2).First().NbFoisCommandee;

            PanierVm.RMProduitPanier_Execute(PanierVm.LesProduitsPaniers.Where(x => x.Product.IdProduit == 2).First().Product.IdProduit);

            PanierVm.LesProduitsPaniers.Where(x => x.Product.IdProduit == 2).First().NbFoisCommandee.Should().Be(qteAcheteAvant - 1);
        }

        public ObservableCollection<ProduitFacturePanierDTO> CreateFakePanier()
        {
            ProduitPanierDTO pdt1 = new ProduitPanierDTO
            {
                IdProduit = 1,
                Prix = 5.99m,
                Tps = true,
                Tvq = true,
                VentePoids = true
            };
            ProduitPanierDTO pdt2 = new ProduitPanierDTO
            {
                IdProduit = 2,
                Prix = 5.99m,
                Tps = false,
                Tvq = false,
                VentePoids = false
            };
            ProduitPanierDTO pdt3 = new ProduitPanierDTO
            {
                IdProduit= 3,
                Prix = 10.99m,
                Tps = true,
                Tvq = true,
                VentePoids = false
            };
            ProduitPanierDTO pdt4 = new ProduitPanierDTO
            {
                IdProduit = 4,
                Prix = 10.99m,
                Tps = false,
                Tvq = false,
                VentePoids = true
            };

            return new ObservableCollection<ProduitFacturePanierDTO>(new ProduitFacturePanierDTO[]
            {
                new ProduitFacturePanierDTO(pdt1, 4, pdt1.Prix),
                new ProduitFacturePanierDTO(pdt2, 4, pdt2.Prix),
                new ProduitFacturePanierDTO(pdt3, 2, pdt3.Prix),
                new ProduitFacturePanierDTO(pdt4, 2, pdt4.Prix),
            });
        }
    }
}
