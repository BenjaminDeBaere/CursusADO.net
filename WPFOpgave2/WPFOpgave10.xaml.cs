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
using TuinCommunity;

namespace WPFOpgave2
{
    /// <summary>
    /// Interaction logic for WPFOpgave10.xaml
    /// </summary>
    public partial class WPFOpgave10 : Window
    {
        public WPFOpgave10()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            CollectionViewSource leverancierViewSource = ((CollectionViewSource)(this.FindResource("leverancierViewSource")));
            var manager = new TuinCentrumManager();
            List<Leverancier> leveranciersOb = new List<Leverancier>();
            leveranciersOb = manager.GetLeveranciers(0);
            leverancierViewSource.Source = leveranciersOb;

            
        }
    }
}
