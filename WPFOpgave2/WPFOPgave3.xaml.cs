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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class WPFOpgave3 : Window
    {
        public WPFOpgave3()
        {
            InitializeComponent();
        }

        private void ToevoegButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var manager = new TuinCentrumManager();
                var naam = NaamTextBox.Text.ToString();
                var adres = AdresTextBox.Text.ToString();
                var postcode = PostcodeTextBox.Text.ToString();
                var plaats = PlaatsTextBox.Text.ToString();
                if (manager.LeverancierToevoegen(naam, adres, postcode, plaats))
                {
                    ContentLabel.Content = "Nieuwe leverancier is toegevoegd.";
                }
                else
                {
                    ContentLabel.Content = "Er is een fout opgetreden bij het toevoegen.";
                }
            }
            catch (Exception ex)
            {
                ContentLabel.Content = ex.Message;
            }
        }

        private void EindejaarsKortingButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var manager = new TuinCentrumManager();
                if(manager.EindejaarsKorting())
                {
                    ContentLabel.Content = "Eindejaarskorting toegepast.";

                }
                else
                {
                    ContentLabel.Content = "Er is een fout opgetreden bij de eindejaarskorting";
                }
            }
            catch (Exception ex)
            {
                ContentLabel.Content = ex.Message;
            }

        }

        private void VervangLeverancierButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var manager = new TuinCentrumManager();
                manager.LeverancierVervangen(2, 3);
                ContentLabel.Content = "Leverancier 2 vervangen door leverancier 3";
            }
            catch (Exception ex)
            {
                ContentLabel.Content = ex.Message;
            }
        }
    }
}
