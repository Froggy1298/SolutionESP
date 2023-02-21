using GestionInventaire.VueModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GestionInventaire
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Window startWindow = new GestionInventaire.Vue.GestionInventaire();
            startWindow.DataContext = new MainVueModel();
            startWindow.Show();

            //Window test = new CaisseEnregistreuse.Vue.VueQuantite(true);
            //test.Show();
        }


    }
}
