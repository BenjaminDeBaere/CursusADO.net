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
    /// Interaction logic for WPFOpgave5.xaml
    /// </summary>
    public partial class WPFOpgave5 : Window
    {
        public WPFOpgave5()
        {
            InitializeComponent();
        }

        private void GemiddeldeKostprijsButton_Click(object sender, RoutedEventArgs e)
        {
            var manager = new TuinCentrumManager();
            try
            {
                StatusLabel.Content = manager.GemiddeldePrijsBerekenen(TextBoxSoort.Text).ToString();
            }
            catch (Exception ex)
            {
                StatusLabel.Content = ex.Message;
            }
        }
    }
}
