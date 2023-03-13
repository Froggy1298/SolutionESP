using ProjetHelper.Models;
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
    public class RapportMensuelVueModelTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            RapportMensuelVueModel rapportMensuelVm = new RapportMensuelVueModel(CreateValidFakeRapportMensuel());
            rapportMensuelVm.Should().NotBeNull();
        }

        [TestMethod]
        public void TestMonthStringFormat()
        {
            RapportMensuelVueModel rapportMensuelVm = new RapportMensuelVueModel(CreateValidFakeRapportMensuel());
            rapportMensuelVm.Should().NotBeNull();

            rapportMensuelVm.MonthStringFormat.Should().Be("mars");
        }

        public Tblrapportmensuel CreateValidFakeRapportMensuel()
        {
            return new Tblrapportmensuel
            {
                DateRapport = new DateTime(2023, 03, 12),
                SommeVente = 1000.12m,
                NbTransactionTotal = 45,
                ValeurMoyTransaction = 105.56m
            };
        }

    }
}
