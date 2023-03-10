using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaisseEnregistreuse.ViewModel;
using FluentAssertions;

namespace CaisseEnregistreuseTest
{
    [TestClass]
    public class ChoixPaimentVueModelTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            ChoixPaimentVueModel choixPaimentVm = new ChoixPaimentVueModel();
            choixPaimentVm.Should().NotBeNull();
        }

        [TestMethod]
        public void ChoixPaiement_Execute()
        {
            ChoixPaimentVueModel choixPaimentVm = new ChoixPaimentVueModel();
            choixPaimentVm.Should().NotBeNull();
            bool eventRaised = false;

            choixPaimentVm.ChangeToThanksPage += (sender, args) =>
            {
                eventRaised = true;
            };

            choixPaimentVm.ChoixPaiement_Execute("Argent");

            eventRaised.Should().BeTrue();
            choixPaimentVm.ChoixPaimentFinal.Should().Be("Argent");
        }


    }
}
