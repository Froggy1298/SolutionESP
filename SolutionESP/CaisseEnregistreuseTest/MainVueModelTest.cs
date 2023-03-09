using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CaisseEnregistreuse.ViewModel;
using FluentAssertions;

namespace CaisseEnregistreuseTest
{
    [TestClass]
    public class MainVueModelTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            MainVueModel mainVm = new MainVueModel();
            mainVm.Should().NotBeNull();
        }

        public void Payer_CanExecute()
        {
            MainVueModel mainVm = new MainVueModel();
            mainVm.Should().NotBeNull();

            mainVm.Payer_CanExecute(null).Should().BeFalse();
        }

    }
}
