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
    public class FactureVueModelTest
    {

        [TestMethod]
        public void TestNoExistingFacture()
        {
            FactureVueModel factureVm = new FactureVueModel(0);
            factureVm.Should().NotBeNull();

            factureVm.InfoFacture.Should().BeNull();
            factureVm.LaFacture.Count.Should().Be(0);
        }

        [TestMethod]
        public void TestExistingFacture()
        {
            FactureVueModel factureVm = new FactureVueModel(9);
            factureVm.Should().NotBeNull();

            factureVm.InfoFacture.Should().NotBeNull();
            factureVm.LaFacture.Count().Should().BeGreaterThan(0);
        }
    }
}
