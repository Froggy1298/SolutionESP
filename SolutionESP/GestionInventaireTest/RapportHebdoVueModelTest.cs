using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionInventaire.VueModel;
using FluentAssertions;
using ProjectHelper.Models;

namespace GestionInventaireTest
{
    [TestClass]
    public class RapportHebdoVueModelTest
    {
        [TestMethod]
        public void TestConstructor() 
        {
            RapportHebdoVueModel rapportHebdoVm = new RapportHebdoVueModel(
                default(DayOfTheWeekInfo), default(DayOfTheWeekInfo), default(decimal));
            rapportHebdoVm.Should().NotBeNull();
        }

        
    }
}
