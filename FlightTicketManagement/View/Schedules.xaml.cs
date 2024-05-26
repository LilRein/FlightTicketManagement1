using FlightTicketManagement.View.Components;
using FlightTicketManagement.ViewModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Maps.MapControl.WPF;
using FlightTicketManagement.Model;

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
            
        }

        private void AddMidFlight(object sender, RoutedEventArgs e)
        {
            
        }

        private void DataGridCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedFlight = (sender as DataGrid).SelectedItem as CHUYENBAY;
            if (selectedFlight != null)
            {
                var viewModel = this.DataContext as SchedulesVM;
                if (viewModel != null)
                {
                    // Clear previous routes
                    myMap.Children.Clear();

                    // Get airport locations
                    var airportDi = viewModel.AirportLocations.FirstOrDefault(a => a.MaSanBay == selectedFlight.MaSanBayDi);
                    var airportDen = viewModel.AirportLocations.FirstOrDefault(a => a.MaSanBay == selectedFlight.MaSanBayDen);

                    if (airportDi != null && airportDen != null)
                    {
                        // Add pushpins for airports
                        var pushpinDi = new Pushpin
                        {
                            Location = new Location(airportDi.Latitude ?? 0, airportDi.Longitude ?? 0),
                            Content = "🛫"
                        };
                        var pushpinDen = new Pushpin
                        {
                            Location = new Location(airportDen.Latitude ?? 0, airportDen.Longitude ?? 0),
                            Content = "🛬"
                        };
                        myMap.Children.Add(pushpinDi);
                        myMap.Children.Add(pushpinDen);

                        // Draw route
                        var routeLine = new MapPolyline
                        {
                            Locations = new LocationCollection
                            {
                                new Location(airportDi.Latitude ?? 0, airportDi.Longitude ?? 0),
                                new Location(airportDen.Latitude ?? 0, airportDen.Longitude ?? 0)
                            },
                            Stroke = new SolidColorBrush(Colors.Blue),
                            StrokeThickness = 5
                        };
                        myMap.Children.Add(routeLine);

                        // Add arrowhead at destination
                        var arrowhead = new Pushpin
                        {
                            Location = new Location(airportDen.Latitude ?? 0, airportDen.Longitude ?? 0),
                            Template = Resources["ArrowHeadTemplate"] as ControlTemplate
                        };
                        myMap.Children.Add(arrowhead);

                        // Show the map
                        MapGrid.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void CloseMap_Click(object sender, RoutedEventArgs e)
        {
            // Hide the map
            MapGrid.Visibility = Visibility.Collapsed;
        }
    }
}
