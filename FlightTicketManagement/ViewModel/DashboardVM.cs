using FlightTicketManagement.Model;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FlightTicketManagement.ViewModel
{
    class DashboardVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        private string _userName;
        public string Username
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private ObservableCollection<YearReport> _YearRevenue;
        public ObservableCollection<YearReport> YearRevenue { get { return _YearRevenue; } set { _YearRevenue = value; OnPropertyChanged(); } }

        private decimal _totalRevenue2024;
        public decimal TotalRevenue2024
        {
            get { return _totalRevenue2024; }
            set
            {
                _totalRevenue2024 = value;
                OnPropertyChanged(nameof(TotalRevenue2024));
            }
        }

        private int _ticketsSold;
        public int TicketsSold
        {
            get { return _ticketsSold; }
            set
            {
                _ticketsSold = value;
                OnPropertyChanged(nameof(_ticketsSold));
            }
        }

        private int _ticketsNum;
        public int TicketsNum
        {
            get { return _ticketsNum; }
            set
            {
                _ticketsNum = value;
                OnPropertyChanged(nameof(_ticketsNum));
            }
        }

        public ObservableCollection<Location> AirportLocations { get; set; }
        public ObservableCollection<MapPolyline> FlightRoutes { get; set; }
        public ObservableCollection<Pushpin> FlightArrows { get; set; }

        public DashboardVM()
        {
            YearRevenue = new ObservableCollection<YearReport>();
            AirportLocations = new ObservableCollection<Location>();
            FlightRoutes = new ObservableCollection<MapPolyline>();
            FlightArrows = new ObservableCollection<Pushpin>();

            // Retrieve the data for the year 2024
            var year2024Data = DataProvider.Ins.DB.DOANHTHUNAMs
                                        .Where(d => d.NAM == 2024)
                                        .ToList();

            // Convert the data to YearReport and add to YearRevenue
            foreach (var data in year2024Data)
            {
                YearRevenue.Add(new YearReport
                {
                    Year = data.NAM,
                    TotalRevenue = data.TONGDOANHTHUNAM ?? 0
                });
            }

            // Calculate the total revenue for 2024
            TotalRevenue2024 = YearRevenue.Sum(y => y.TotalRevenue);

            TicketsSold = DataProvider.Ins.DB.CTDOANHTHUTHANGs
                                      .Sum(t => t.SoVe); // Adjust this line based on the actual property that represents the number of tickets sold
            TicketsNum = DataProvider.Ins.DB.CHUYENBAYs.Count();

            // Load airports and flight paths
            LoadAirports();
            LoadFlightRoutes();
        }

        private void LoadAirports()
        {
            var airports = DataProvider.Ins.DB.SANBAYs.ToList();
            foreach (var airport in airports)
            {
                if (airport.Latitude.HasValue && airport.Longitude.HasValue)
                {
                    AirportLocations.Add(new Location(airport.Latitude.Value, airport.Longitude.Value));
                }
            }
        }

        private void LoadFlightRoutes()
        {
            var flights = DataProvider.Ins.DB.CHUYENBAYs.ToList();
            foreach (var flight in flights)
            {
                var fromAirport = DataProvider.Ins.DB.SANBAYs.FirstOrDefault(s => s.MaSanBay == flight.MaSanBayDi);
                var toAirport = DataProvider.Ins.DB.SANBAYs.FirstOrDefault(s => s.MaSanBay == flight.MaSanBayDen);

                if (fromAirport != null && toAirport != null && fromAirport.Latitude.HasValue && fromAirport.Longitude.HasValue && toAirport.Latitude.HasValue && toAirport.Longitude.HasValue)
                {
                    var fromLocation = new Location(fromAirport.Latitude.Value, fromAirport.Longitude.Value);
                    var toLocation = new Location(toAirport.Latitude.Value, toAirport.Longitude.Value);

                    var path = new MapPolyline
                    {
                        Locations = new LocationCollection
                        {
                            fromLocation,
                            toLocation
                        },
                        Stroke = new SolidColorBrush(Colors.Red),
                        StrokeThickness = 3,
                        StrokeDashArray = new DoubleCollection { 4, 4 } // Dash pattern for better visibility
                    };

                    FlightRoutes.Add(path);

                    // Add arrow to indicate direction
                    var arrow = new Pushpin
                    {
                        Location = GetMidPoint(fromLocation, toLocation),
                        Content = "➤" // Arrow symbol
                    };
                    FlightArrows.Add(arrow);
                }
            }
        }

        private Location GetMidPoint(Location from, Location to)
        {
            var midLat = (from.Latitude + to.Latitude) / 2;
            var midLong = (from.Longitude + to.Longitude) / 2;
            return new Location(midLat, midLong);
        }
    }
}
