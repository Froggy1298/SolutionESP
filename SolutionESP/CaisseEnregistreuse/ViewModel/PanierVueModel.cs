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
    class PanierVueModel : BaseViewModel
    {
        

        public PanierVueModel()
        {
            BoutonEntrerCUP = new RelayCommand(EntrerCUP_Execute, EntrerCUP_CanExecute);
        }

        public string alloa;

        public RelayCommand BoutonEntrerCUP { get; set; }
        public void EntrerCUP_Execute(object? _)
        {
            MessageBox.Show("Test2");
            alloa = "CALIS";
        }
        public bool EntrerCUP_CanExecute(object? _)
        {
            return true;
        }
    }
}
