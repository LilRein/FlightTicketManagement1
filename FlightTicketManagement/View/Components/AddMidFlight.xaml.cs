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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightTicketManagement.View.Components
{
    /// <summary>
    /// Interaction logic for AddMidFlight.xaml
    /// </summary>
    public partial class AddMidFlight : UserControl
    {
        public AddMidFlight()
        {
            InitializeComponent();
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            string content = WaitTime.Text;
            if (string.IsNullOrEmpty(content))
            {
                MessageBox.Show("Error");
            }
            else
            {
                MessageBox.Show(content);
            }

        }
    }
}
