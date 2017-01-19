using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

        public ObservableCollection<Leverancier> leveranciersOb = new ObservableCollection<Leverancier>();
        public List<Leverancier> oudeLeveranciers, nieuweLeveranciers = new List<Leverancier>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            VulDeGrid();
            VulPostcodeComboBox();
        }
        

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.OldItems != null)
            {
                foreach(Leverancier leverancier in e.OldItems)
                {
                    oudeLeveranciers.Add(leverancier);
                }
            }
            if (e.NewItems !=null)
            {
                foreach(Leverancier leverancier in e.NewItems)
                {
                    nieuweLeveranciers.Add(leverancier);
                }
            }
           
        }

        private void PostcodeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(PostcodeComboBox.SelectedIndex ==0)
            {
                leverancierDataGrid.Items.Filter = null;
            }
            else
            {
                leverancierDataGrid.Items.Filter = new Predicate<Object>(postcodeFilter);
            }
            VulDeGrid();
        }

        public bool postcodeFilter(object lev)
        {
            Leverancier l = lev as Leverancier;
            bool result = (l.Postcode == PostcodeComboBox.SelectedItem.ToString());
            return result;
        }

        private void VulDeGrid()
        {
    

            CollectionViewSource leverancierViewSource = ((CollectionViewSource)(this.FindResource("leverancierViewSource")));
            var manager = new TuinCentrumManager();
            leveranciersOb = manager.GetLeveranciers(String.Empty);
            leverancierViewSource.Source = leveranciersOb;
            leveranciersOb.CollectionChanged += this.OnCollectionChanged;

        }

        private void VulPostcodeComboBox()
        {
            var nummers = (from l in leveranciersOb orderby l.Postcode select l.Postcode.ToString()).Distinct().ToList();
            nummers.Insert(0, "alles");
            PostcodeComboBox.ItemsSource = nummers;
            PostcodeComboBox.SelectedIndex = 0;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var manager = new TuinCentrumManager();
            if (MessageBox.Show("Wilt u alles wegschrijven naar de database?", "Opslaan", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                StringBuilder boodschap = new StringBuilder();
                List<Leverancier> resultaatLeveranciers = new List<Leverancier>();
                int verwijderdeLeveranciers;
                int toegevoegdeLeveranciers;
                if (oudeLeveranciers.Count() != 0)
                {
                    resultaatLeveranciers = manager.WriteDeletedLeveranciers(oudeLeveranciers);
                    boodschap.Append("Niet verwijderd: \n");
                    foreach(Leverancier leverancier in resultaatLeveranciers)
                    {
                        boodschap.Append(leverancier.LeverancierNr.ToString() + " " + leverancier.Naam.ToString() + " niet \n");
                     
                    }
                    verwijderdeLeveranciers = oudeLeveranciers.Count() - resultaatLeveranciers.Count();
                    boodschap.Append(verwijderdeLeveranciers.ToString() + " leveranciers verwijderd uit de database.");
                    for (var i = 0; i <= 5; i++)
                        boodschap.Append("\n");

                }
                resultaatLeveranciers.Clear();
                if(nieuweLeveranciers.Count()!=0)
                {
                    resultaatLeveranciers = manager.WriteNewLeveranciers(nieuweLeveranciers);
                    boodschap.Append("Niet toegevoegd: \n");
                    foreach(Leverancier leverancier in resultaatLeveranciers)
                    {
                        boodschap.Append(leverancier.LeverancierNr.ToString() + " " + leverancier.Naam.ToString() + " niet \n");                                              

                    }
                    toegevoegdeLeveranciers = nieuweLeveranciers.Count() - resultaatLeveranciers.Count();
                    boodschap.Append(toegevoegdeLeveranciers.ToString() + " leveranciers toegevoegd in de database.");           
                }


                
                
            }
        }
    }
}
