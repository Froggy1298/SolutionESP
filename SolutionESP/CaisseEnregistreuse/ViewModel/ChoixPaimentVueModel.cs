using CaisseEnregistreuse.Event;
using CaisseEnregistreuse.Vue;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjectHelper.ViewModel;
using RelayCommandLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CaisseEnregistreuse.ViewModel
{
    public class ChoixPaimentVueModel : BaseViewModel
    {
        public ChoixPaimentVueModel()
        {
            BoutonChoixPaiement = new RelayCommand(ChoixPaiement_Execute, ChoixPaiement_CanExecute);
        }


        public event EventHandler<EventArgs> ChangeToThanksPage;
        public string ChoixPaimentFinal { get; set; }


        public RelayCommand BoutonChoixPaiement { get; set; }
        public void ChoixPaiement_Execute(object parameter)
        {
            ChoixPaimentFinal = parameter.ToString();
            ChangeToThanksPage.Invoke(this, EventArgs.Empty);
        }
        public bool ChoixPaiement_CanExecute(object? _)
        {
            return true;
        }




    }
}
