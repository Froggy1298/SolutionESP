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
    public class PanierVueModelTest
    {

        [TestMethod]
        public void TestConstructor()
        {
            PanierVueModel PanierVm = new PanierVueModel();
            PanierVm.Should().NotBeNull();
        }

        [TestMethod]
        public void Alloa()
        {
            true.Should().BeTrue();
        }
    }
}
