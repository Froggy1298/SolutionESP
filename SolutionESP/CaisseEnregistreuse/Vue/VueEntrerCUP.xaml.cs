using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
        public enum FinalButtonPressed
        {
            None,
            Entrer,
            Annuler
        }

        public string CUP { get; set; }
        public FinalButtonPressed EnterOrCancel { get; set; }

        public VueEntrerCUP()
        {
            InitializeComponent();
            this.Topmost = true;
        }

        private void AddNumber(string theInput)
        {
            CUPTextBlock.Text += theInput;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CUPTextBlock.Text.Length < 12)
                AddNumber(((Button)sender).Tag.ToString());
        }

        private void Button_ClickRM(object sender, RoutedEventArgs e)
        {
            if (CUPTextBlock.Text.Length != 0)
                CUPTextBlock.Text = CUPTextBlock.Text.Remove(CUPTextBlock.Text.Length - 1);
        }

        private void Button_ClickEnter(object sender, RoutedEventArgs e)
        {
            if (CUPTextBlock.Text.Length == 12)
            {
                CUP = CUPTextBlock.Text;
                EnterOrCancel = FinalButtonPressed.Entrer;
                this.Close();
            }
            else
            {
                MessageBox.Show("CUP non valide", "Erreur");
            }
        }

        private void Button_ClickAnnuler(object sender, RoutedEventArgs e)
        {
            EnterOrCancel = FinalButtonPressed.Annuler;
            this.Close();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
