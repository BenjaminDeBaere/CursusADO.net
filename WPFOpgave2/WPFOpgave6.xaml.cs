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
    /// Interaction logic for WPFOpgave6.xaml
    /// </summary>
    public partial class WPFOpgave6 : Window
    {
        public WPFOpgave6()
        {
            InitializeComponent();
        }

        private void OpzoekenButton_Click(object sender, RoutedEventArgs e)
        {
            int plantnummer;
            if (int.TryParse(PlantNummerTextBox.Text, out plantnummer))
            {
                try
                {
                    var manager = new TuinCentrumManager();
                    var info = manager.plantInfoRaadplegen(Convert.ToInt16(PlantNummerTextBox.Text));
                    NaamLabel.Content = info.PlantNaam;
                    SoortLabel.Content = info.PlantSoort;
                    LevernacierLabel.Content = info.LeverancierNaam;
                    KleurLabel.Content = "empty";
                    KleurLabel.Content = info.Kleur;
                    KostprijsLabel.Content = "€ " + info.Kostprijs;
                    StatusLabel.Content = String.Empty;

                }
                catch (Exception ex)
                {
                    StatusLabel.Content = ex.Message;
                    NaamLabel.Content = String.Empty;
                    SoortLabel.Content = String.Empty;
                    LevernacierLabel.Content = String.Empty;
                    KleurLabel.Content = String.Empty;
                    KostprijsLabel.Content = String.Empty;
                }
            }
        }
    }
}
