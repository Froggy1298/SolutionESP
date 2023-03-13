using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionInventaire.VueModel;
using FluentAssertions;
using ProjetHelper.Models;
using ProjectHelper.Models.ModelsProduit;
using Microsoft.Windows.Themes;

namespace GestionInventaireTest
{
    [TestClass]
    public class AjouterModifierVueModelTest
    {

        [TestMethod]
        public void TestConstructorAdd()
        {
            AjouterModifierVueModel ajouterModifierVm = new AjouterModifierVueModel();
            ajouterModifierVm.Should().NotBeNull();
            ajouterModifierVm.ContentButton.Should().Be(AjouterModifierVueModel.CRUDAction.Ajouter.ToString());
        }

        [TestMethod]
        public void TestConstructorUpdate()
        {
            AjouterModifierVueModel ajouterModifierVm = new AjouterModifierVueModel(CreateFakeValidProduct());
            ajouterModifierVm.Should().NotBeNull();
            ajouterModifierVm.ContentButton.Should().Be(AjouterModifierVueModel.CRUDAction.Modifier.ToString());
        }

        [TestMethod]
        public void CreerModifier_CanExecuteTestTrue()
        {
            AjouterModifierVueModel ajouterModifierVm = new AjouterModifierVueModel();
            ajouterModifierVm.Should().NotBeNull();

            ajouterModifierVm.TheProduct = CreateFakeValidProduct();
            ajouterModifierVm.CreerModifier_CanExecute(null).Should().BeTrue();
        }

        [TestMethod]
        public void CreerModifier_CanExecuteTestFalse1()
        {
            AjouterModifierVueModel ajouterModifierVm = new AjouterModifierVueModel();
            ajouterModifierVm.Should().NotBeNull();

            ajouterModifierVm.TheProduct = CreateFakeNotValidProduct1();
            ajouterModifierVm.CreerModifier_CanExecute(null).Should().BeFalse();
        }

        [TestMethod]
        public void CreerModifier_CanExecuteTestFalse2()
        {
            AjouterModifierVueModel ajouterModifierVm = new AjouterModifierVueModel();
            ajouterModifierVm.Should().NotBeNull();

            ajouterModifierVm.TheProduct = CreateFakeNotValidProduct2();
            ajouterModifierVm.CreerModifier_CanExecute(null).Should().BeFalse();
        }

        [TestMethod]
        public void Annuler_ExecuteTest()
        {
            AjouterModifierVueModel ajouterModifierVm = new AjouterModifierVueModel();
            ajouterModifierVm.Should().NotBeNull();
            bool eventRaised = false;

            ajouterModifierVm.ChangeToListPageNoUpdate += (sender, args) =>
            {
                eventRaised = true;
            };

            ajouterModifierVm.Annuler_Execute(null);
            eventRaised.Should().BeTrue();
        }

        public Tblproduit CreateFakeValidProduct()
        {
            return new Tblproduit
            {
                IdDepartementNavigation = new Tbldepartement { NomDepartement = "TestDepartement"},
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

        public Tblproduit CreateFakeNotValidProduct1()
        {
            return new Tblproduit
            {
                IdDepartementNavigation = null, //Département null
                Cup = "12345678912", //Le cup contient 11 chiffre
                Nom = "ProduitTest",
                QteInventaire = 5,
                Prix = 12.99m,
                VentePoids = 0,
                Tps = 0,
                Tvq = 0,
                Description = "Je suis un produit valide"
            };
        }
        public Tblproduit CreateFakeNotValidProduct2()
        {
            return new Tblproduit
            {
                IdDepartementNavigation = new Tbldepartement { NomDepartement = "TestDepartement" },
                Cup = "123456789123", 
                Nom = "ProduitTest",
                QteInventaire = 5.99m, //Produit ne se vend pas au poid mais à une quantité à virgule
                Prix = 12.99m,
                VentePoids = 0,
                Tps = 0,
                Tvq = 0,
                Description = "Je suis un produit valide"
            };
        }
    }
}
