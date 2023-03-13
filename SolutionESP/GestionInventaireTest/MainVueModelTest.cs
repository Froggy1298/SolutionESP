using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionInventaire.VueModel;
using FluentAssertions;

namespace GestionInventaireTest
{
    [TestClass]
    public class MainVueModelTest
    {
        [TestMethod]
        public void TestConstructor() 
        {
            MainVueModel mainVm = new MainVueModel();
            mainVm.Should().NotBeNull();
            //MainVueModel non testable, du au fait que celui-ci demande des composants de l'interface
            //Ceci est l'erreur dont j'ai parlé et que nous arrivons pas à règler
        }

    }
}
