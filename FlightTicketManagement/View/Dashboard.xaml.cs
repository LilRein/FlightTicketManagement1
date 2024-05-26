using System.Windows.Controls;
using Microsoft.Maps.MapControl.WPF;
using System.Windows;
using FlightTicketManagement.ViewModel;

namespace FlightTicketManagement.View
{
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();

            // Set the DataContext to the ViewModel
            var viewModel = new DashboardVM();
            this.DataContext = viewModel;

            // Add pushpins to the map
            foreach (var location in viewModel.AirportLocations)
            {
                var pushpin = new Pushpin
                {
                    Location = location,
                    Content = "✈️"
                };
                myMap.Children.Add(pushpin);
            }

            // Add flight routes to the map
            foreach (var route in viewModel.FlightRoutes)
            {
                myMap.Children.Add(route);
            }

            // Add flight arrows to the map
            foreach (var arrow in viewModel.FlightArrows)
            {
                myMap.Children.Add(arrow);
            }
        }
    }
}
