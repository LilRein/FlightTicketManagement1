using FlightTicketManagement.View.Components;
using FlightTicketManagement.ViewModel;
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

namespace FlightTicketManagement.View
{
    /// <summary>
    /// Interaction logic for Schedules.xaml
    /// </summary>
    public partial class Schedules : UserControl
    {
        public Schedules()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddFlight addFlight = new AddFlight();
            var window = new Window
            {
                Content = addFlight,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStyle = WindowStyle.None,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            window.ShowDialog();
        }

        private void AddMidFlight(object sender, RoutedEventArgs e)
        {

        }

        private void DataGridCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
