using ProjectHelper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CaisseEnregistreuse.Vue;
using ProjetHelper;
using RelayCommandLibrary;
using System.Security.Policy;
using System.Windows;

namespace CaisseEnregistreuse.ViewModel
{
    public class MainVueModel : BaseViewModel
    {
        public MainVueModel()
        {
            BoutonPayer = new RelayCommand(Payer_Execute, Payer_CanExecute);
            View = new Panier();
            PanierVm = new PanierVueModel();
            View.DataContext = PanierVm;
        }



        public PanierVueModel PanierVm { get; set; }
        
        public RelayCommand BoutonPayer { get; set; }
        public void Payer_Execute(object? _)
        {

        }
        public bool Payer_CanExecute(object? _)
        {
            return true;
        }

        private Page view; public Page View
        {
            get { return view; }
            set { view = value; OnPropertyChanged(); }
        }

       


    }
}
