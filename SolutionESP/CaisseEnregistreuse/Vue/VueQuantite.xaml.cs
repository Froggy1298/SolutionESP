using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    /// Logique d'interaction pour VueQuantite.xaml
    /// </summary>
    public partial class VueQuantite : Window
    {
        public VueQuantite(bool isVentePoids)
        {
            InitializeComponent();
            IsVentePoids = isVentePoids;
            QuantityTextBox.Text = "1";
        }

        public decimal QuantityFinal { get; set; }
        public bool IsVentePoids { get; set; }



        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void Button_ClickAddMore(object sender, RoutedEventArgs e)
        {
            decimal tempDouble = GetDoubleValueQuantity();
            if (IsVentePoids)
                tempDouble += 1;
            else
                tempDouble += 5;

            QuantityTextBox.Text = (Math.Round(tempDouble, 1)).ToString();
        }

        private void Button_ClickAdd(object sender, RoutedEventArgs e)
        {
            decimal tempDouble = GetDoubleValueQuantity();
            if (IsVentePoids)
                tempDouble += 0.1m;
            else
                tempDouble += 1;

            QuantityTextBox.Text = (Math.Round(tempDouble, 1)).ToString();
        }


        private void Button_ClickRMMore(object sender, RoutedEventArgs e)
        {
            decimal tempDouble = GetDoubleValueQuantity();

            //Si le produit se vends au poids et ci celui-ci est n'est pas égal à 1
            if (IsVentePoids && !(tempDouble <= 1))
                tempDouble -= 1;
            else if (!(tempDouble <= 5))
                tempDouble -= 5;

            QuantityTextBox.Text = (Math.Round(tempDouble, 1)).ToString();
        }

        private void Button_ClickRM(object sender, RoutedEventArgs e)
        {
            decimal tempDouble = GetDoubleValueQuantity();

            //Si le produit se vends au poids et ci celui-ci est n'est pas égal à 0.10
            if (IsVentePoids && !(tempDouble <= 0.1m))
                tempDouble -= 0.1m;
            else if (!(tempDouble <= 1))
                tempDouble -= 1;

            QuantityTextBox.Text = (Math.Round(tempDouble, 1)).ToString();
        }

       
        private void Button_ClickEnter(object sender, RoutedEventArgs e)
        {
            QuantityFinal = GetDoubleValueQuantity();
            this.Close();
        }

        public decimal GetDoubleValueQuantity()
        {
            decimal tempDouble;
            decimal.TryParse(QuantityTextBox.Text, out tempDouble);
            return tempDouble;
        }

       
    }
}
