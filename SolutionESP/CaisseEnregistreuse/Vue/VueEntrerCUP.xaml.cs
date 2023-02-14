using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CaisseEnregistreuse.Vue
{
    /// <summary>
    /// Logique d'interaction pour VueEntrerCUP.xaml
    /// </summary>
    public partial class VueEntrerCUP : Window
    {
        public VueEntrerCUP()
        {
            InitializeComponent();
        }

        private void AddNumber(string theInput)
        {
            CUPText.Text += theInput;
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            AddNumber("1");
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            AddNumber("2");
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            AddNumber("4");
        }

        private void Button_Click7(object sender, RoutedEventArgs e)
        {
            AddNumber("7");
        }
    }
}
