﻿using CaisseEnregistreuse.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CaisseEnregistreuse
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Window startWindow = new CaisseEnregistreuse.Vue.Caisse();
            startWindow.DataContext = new MainVueModel();
            startWindow.Show();

            //Window test = new CaisseEnregistreuse.Vue.VueQuantite(true);
            //test.Show();
        }

    }
}
