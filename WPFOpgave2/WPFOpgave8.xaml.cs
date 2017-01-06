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
    /// Interaction logic for WPFOpgave8.xaml
    /// </summary>
    public partial class WPFOpgave8 : Window
    {
        public WPFOpgave8()
        {
            InitializeComponent();
        }

        public List<String> soortenOb = new List<String>();
        public List<String> plantenOb = new List<String>();

        public void ComboBoxInvullen()
        {
            var manager = new TuinCentrumManager();
            soortenOb = manager.GetSoorten();
            foreach(string soort in soortenOb)
            {
                ComboboxSoorten.Items.Add(soort);
            }
        }

        public void VulRoosterIn()
        {
            var manager = new TuinCentrumManager();
            if (ComboboxSoorten.SelectedItem != null)
            {
                plantenOb = manager.GetPlantNaamPerSoort(ComboboxSoorten.SelectedItem.ToString());
            }
            else
            {
                plantenOb = manager.GetPlantNaamPerSoort(string.Empty);        
            }
            ListBoxPlanten.ItemsSource = plantenOb;
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxInvullen();
            VulRoosterIn();
        }

        private void ComboboxSoorten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VulRoosterIn();
        }
    }
}
