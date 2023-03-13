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
        /// <summary>
        /// Constructeur
        /// </summary>
        public ChoixPaimentVueModel()
        {
            BoutonChoixPaiement = new RelayCommand(ChoixPaiement_Execute, ChoixPaiement_CanExecute);
        }


        /// <summary>
        /// L'évènement qui permet de au mainVm de changer de page
        /// </summary>
        public event EventHandler<EventArgs> ChangeToThanksPage;

        /// <summary>
        /// Le choix du paiment final
        /// CarteCadeau
        /// Crédit
        /// Débit
        /// Argent
        /// </summary>
        public string ChoixPaimentFinal { get; set; }


        #region Le bouton qui permet de choisir le mode de paiment que le client utilise
        public RelayCommand BoutonChoixPaiement { get; set; }
        public void ChoixPaiement_Execute(object parameter)
        {
            //Le parameter étant le type de paiment que le bouton passe
            ChoixPaimentFinal = parameter.ToString();
            ChangeToThanksPage.Invoke(this, EventArgs.Empty);
        }
        public bool ChoixPaiement_CanExecute(object? _)
        {
            return true;
        }
        #endregion



    }
}
