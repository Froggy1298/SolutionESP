﻿using ProjectHelper.ViewModel;
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
    class MainVueModel : BaseViewModel
    {
        public MainVueModel()
        {
            BoutonPayer = new RelayCommand(Payer_Execute, Payer_CanExecute);
            View = new Panier();
            panierVm = new PanierVueModel();
            View.DataContext = panierVm;
        }

        public PanierVueModel panierVm;
        public RelayCommand BoutonPayer { get; set; }
        public void Payer_Execute(object? _)
        {
            MessageBox.Show(panierVm.alloa);
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
