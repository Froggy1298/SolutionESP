using GestionInventaire.Vue;
using ProjectHelper.Models.ModelsProduit;
using ProjectHelper.ViewModel;
using ProjetHelper.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GestionInventaire.VueModel
{
    public class MainVueModel : BaseViewModel
    {
        public MainVueModel()
        {
            BdContext = new A22Sda2031887Context();
            View = new ListProduit();
            ListProduitVm = new ListVueModel();
            ListProduitVm.ChangeToUpdatePage += ChangeUpdatePage;
            ListProduitVm.ChangeToCreatePage += ChangeCreatePage;
            View.DataContext = ListProduitVm;
        }

        public A22Sda2031887Context BdContext { get; set; }
        public ListVueModel ListProduitVm { get; set; }

        private Page view; public Page View
        {
            get { return view; }
            set { view = value; OnPropertyChanged(); }
        }

        public void ChangeUpdatePage(object? sender, EventArgs e)
        {
            AjouterModifierVueModel tempsVm = new AjouterModifierVueModel();
            tempsVm.ChangeToListPage += ChangeToListPageNoUpdate;
            View = new AjouterModifier();
            View.DataContext = tempsVm;
        }
        
        public void ChangeCreatePage(object? sender, EventArgs e)
        {
            AjouterModifierVueModel tempsVm = new AjouterModifierVueModel();
            tempsVm.ChangeToListPage += ChangeToListPageNoUpdate;
            View = new AjouterModifier();
            View.DataContext = tempsVm;
        }

        public void ChangeToListPageNoUpdate(object? sender, EventArgs e)
        {
            View = new ListProduit();
            View.DataContext = ListProduitVm;
        }


    }
}
