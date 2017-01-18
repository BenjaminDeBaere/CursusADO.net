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

        public List<Soorten> soortenOb = new List<Soorten>();
        public List<Plant> plantenNaamOb = new List<Plant>();
        private string GeselecteerdeSoortNaam;

        public void ComboBoxInvullen()
        {
            var manager = new TuinCentrumManager();
            ComboboxSoorten.DisplayMemberPath = "SoortNaam";
            ComboboxSoorten.SelectedValuePath = "SoortNr";
            ComboboxSoorten.ItemsSource = manager.GetSoorten();            
        }

    
    

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxInvullen();
            
       
        }

        private void ComboboxSoorten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GeselecteerdeSoortNaam = ((Soorten)ComboboxSoorten.SelectedItem).SoortNaam;
            var manager = new TuinCentrumManager();
            plantenNaamOb = manager.GetPlantNaamPerSoort(Convert.ToInt32(ComboboxSoorten.SelectedValue));
            ListBoxPlanten.ItemsSource = plantenNaamOb;
            ListBoxPlanten.DisplayMemberPath = "PlantNaam";
        }


    }
}
