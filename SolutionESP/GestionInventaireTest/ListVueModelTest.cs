using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionInventaire.VueModel;
using FluentAssertions;
using System.Security.Cryptography.X509Certificates;
using GestionInventaire.Vue;
using ProjectHelper.Models.ModelsProduit;
using ProjetHelper.Models;

namespace GestionInventaireTest
{
    [TestClass]
    public class ListVueModelTest
    {
        [TestMethod]
        public void TestConstructor() 
        { 
            ListVueModel listVm= new ListVueModel();    
            listVm.Should().NotBeNull(); 
        }

        [TestMethod]
        public void Creer_ExecuteTest()
        {
            ListVueModel listVm = new ListVueModel();
            listVm.Should().NotBeNull();
            bool eventRaised = false;

            listVm.ChangeToCreatePage += (sender, args) =>
            {
                eventRaised = true;
            };

            listVm.Creer_Execute(null);
            eventRaised.Should().BeTrue();
        }

        [TestMethod]
        public void Modifier_ExecuteTest()
        {
            ListVueModel listVm = new ListVueModel();
            listVm.Should().NotBeNull();
            bool eventRaised = false;

            listVm.ChangeToUpdatePage += (sender, args) =>
            {
                eventRaised = true;
            };

            listVm.Modifier_Execute(null);
            eventRaised.Should().BeTrue();
        }

        [TestMethod]
        public void Modifier_CanExecuteTest()
        {
            ListVueModel listVm = new ListVueModel();
            listVm.Should().NotBeNull();

            listVm.Modifier_CanExecute(null).Should().BeFalse();
            
            listVm.SelectedProduct =  CreateFakeValidProduct();

            listVm.Modifier_CanExecute(null).Should().BeTrue();
        }


        [TestMethod]
        public void Supprimer_CanExecuteTest()
        {
            ListVueModel listVm = new ListVueModel();
            listVm.Should().NotBeNull();

            listVm.Supprimer_CanExecute(null).Should().BeFalse();

            listVm.SelectedProduct = CreateFakeValidProduct();

            listVm.Supprimer_CanExecute(null).Should().BeTrue();
        }




        public Tblproduit CreateFakeValidProduct()
        {
            return new Tblproduit
            {
                IdDepartementNavigation = new Tbldepartement { NomDepartement = "TestDepartement" },
                Cup = "123456789123",
                Nom = "ProduitTest",
                QteInventaire = 5,
                Prix = 12.99m,
                VentePoids = 1,
                Tps = 0,
                Tvq = 0,
                Description = "Je suis un produit valide"
            };
        }

    }
}
