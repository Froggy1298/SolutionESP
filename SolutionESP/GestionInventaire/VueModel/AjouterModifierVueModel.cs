using ProjectHelper.Models.ModelsProduit;
using ProjectHelper.ViewModel;
using RelayCommandLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventaire.VueModel
{
    public class AjouterModifierVueModel : BaseViewModel
    {
        public AjouterModifierVueModel()
        {
            InitializeButton();
        }

        public AjouterModifierVueModel(Tblproduit productToUpdate)
        {
            InitializeButton();
        }

        public void InitializeButton()
        {
            BoutonAnnuler = new RelayCommand(Annuler_Execute, Annuler_CanExecute);
        }

        public event EventHandler<EventArgs> ChangeToListPage;

        public RelayCommand BoutonAnnuler { get; set; }
        public void Annuler_Execute(object? _)
        {
            ChangeToListPage.Invoke(this, new EventArgs());
        }
        public bool Annuler_CanExecute(object? _)
        {
            return true;
        }

    }
}
