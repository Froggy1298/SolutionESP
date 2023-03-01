using ProjectHelper.ViewModel;
using ProjetHelper.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventaire.VueModel
{
    internal class RapportMensuelVueModel : BaseViewModel
    {
        public RapportMensuelVueModel(Tblrapportmensuel tblrapportmensuel)
        {
            RapportMensuel = tblrapportmensuel;
        }

        private Tblrapportmensuel _rapportMensuel;

        public Tblrapportmensuel RapportMensuel
        {
            get { return _rapportMensuel; }
            set { _rapportMensuel = value; }
        }


        public string MonthStringFormat
        {
            get 
            {
                return RapportMensuel.DateRapport.ToString("MMMM");
            }
        }


    }
}
