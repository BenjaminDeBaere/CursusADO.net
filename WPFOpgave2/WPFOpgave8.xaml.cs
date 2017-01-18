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
        public List<Plant> gewijzigdePlanten = new List<Plant>();
        private string GeselecteerdeSoortNaam;
        private bool HeeftFouten()
        {
            bool foutGevonden = false;
            if (Validation.GetHasError(TextBoxKleur)) foutGevonden = true;
            if (Validation.GetHasError(TextBoxPrijs)) foutGevonden = true;
            return foutGevonden;
        }

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

        private void ButtonOpslaan_Click(object sender, RoutedEventArgs e)
        {
            if (!HeeftFouten())
            {
                var manager = new TuinCentrumManager();
                foreach (Plant p in plantenNaamOb)
                {
                    if (p.HasChanged)
                    {
                        gewijzigdePlanten.Add(p);
                        p.HasChanged = false;
                    }
                }
                if (gewijzigdePlanten.Count() != 0)
                {
                    string boodschap = "Gewijzigde planten van soort " + ComboboxSoorten.SelectedItem.ToString() + " opslaan?";
                    if (MessageBox.Show(boodschap, "opslaan", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        manager.VeranderPlanten(gewijzigdePlanten, Convert.ToInt16(ComboboxSoorten.SelectedValue));
                    }
                    MessageBox.Show(gewijzigdePlanten.Count + " plant(en) gewijzigd", "Opslaan", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }

           
        }

        private void ListBoxPlanten_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (HeeftFouten()) e.Handled = true;
        }  

        private void ListBoxPlanten_KeyUp(object sender, KeyEventArgs e)
        {
            if (HeeftFouten()) e.Handled = true;
        }

        private void ComboboxSoorten_KeyUp(object sender, KeyEventArgs e)
        {
            if (HeeftFouten()) e.Handled = true;
        }

        private void ComboboxSoorten_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (HeeftFouten()) e.Handled = true;
        }

  
    }
}
