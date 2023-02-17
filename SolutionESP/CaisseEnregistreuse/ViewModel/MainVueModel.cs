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
using System.Xml.Serialization;

namespace CaisseEnregistreuse.ViewModel
{
    public class MainVueModel : BaseViewModel
    {
        public MainVueModel()
        {
            BoutonPayer = new RelayCommand(Payer_Execute, Payer_CanExecute);
            PayerBouttonVisibility = Visibility.Visible;

            View = new Panier();

            PanierVm = new PanierVueModel();
            ChoixPaimentVm = new ChoixPaimentVueModel();

            View.DataContext = PanierVm;
        }



        public PanierVueModel PanierVm { get; set; }
        public ChoixPaimentVueModel ChoixPaimentVm { get; set; }

        public RelayCommand BoutonPayer { get; set; }
        public void Payer_Execute(object? _)
        {
            PayerBouttonVisibility = Visibility.Collapsed;
            View = new ChoixPaiment();
            View.DataContext = ChoixPaimentVm;
        }
        public bool Payer_CanExecute(object? _)
        {
            return PanierVm.QteProduitPanier != 0;
        }



        private Page view; public Page View
        {
            get { return view; }
            set { view = value; OnPropertyChanged(); }
        }

        private Visibility payerBoutonVisibility;public Visibility PayerBouttonVisibility
        {
            get { return payerBoutonVisibility; }
            set { payerBoutonVisibility = value; OnPropertyChanged(); }
        }



    }
}
