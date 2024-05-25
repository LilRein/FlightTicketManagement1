using FlightTicketManagement.Model;
using FlightTicketManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FlightTicketManagement.ViewModel
{
    class FlightsVM : Utilities.ViewModelBase
    {
        
        private ObservableCollection<CHUYENBAY> _flights;
        private ObservableCollection<FlightResult> _filteredFlights;
        private ObservableCollection<CHITIETHANGVE> _ticketDetail;

        public FlightsVM()
        {
            Flights = new ObservableCollection<CHUYENBAY>(DataProvider.Ins.DB.CHUYENBAYs);
            TicketDetail = new ObservableCollection<CHITIETHANGVE>(DataProvider.Ins.DB.CHITIETHANGVEs);
            FilteredFlights = new ObservableCollection<FlightResult>();

            SearchCommand = new RelayCommand(SearchFlightsWithSQL);
        }

        public ObservableCollection<CHUYENBAY> Flights
        {
            get { return _flights; }
            set { _flights = value; OnPropertyChanged(); }
        }

        public ObservableCollection<FlightResult> FilteredFlights
        {
            get { return _filteredFlights; }
            set { _filteredFlights = value; OnPropertyChanged(); }
        }

        public ObservableCollection<CHITIETHANGVE> TicketDetail
        {
            get { return _ticketDetail; }
            set { _ticketDetail = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> FlightIDs => new ObservableCollection<string>(Flights.Select(f => f.MaChuyenBay).Distinct());
        public ObservableCollection<string> FromAirports => new ObservableCollection<string>(Flights.Select(f => f.MaSanBayDi).Distinct());
        public ObservableCollection<string> ToAirports => new ObservableCollection<string>(Flights.Select(f => f.MaSanBayDen).Distinct());
        public ObservableCollection<string> TicketClasses => new ObservableCollection<string>(Flights.SelectMany(f => f.CHITIETHANGVEs.Select(h => h.MaHangVe)).Distinct());

        public string SelectedFlightID { get; set; }
        public string SelectedFromAirport { get; set; }
        public string SelectedToAirport { get; set; }
        public DateTime? SelectedFlyDate { get; set; }
        public string SelectedTicketClass { get; set; }

        public ICommand SearchCommand { get; set; }

        public class FlightResult
        {
            public string MaChuyenBay { get; set; }
            public string MaSanBayDi { get; set; }
            public string MaSanBayDen { get; set; }
            public DateTime NgayBay { get; set; }
            public TimeSpan GioKhoiHanh { get; set; }
            public string TicketClass { get; set; }
            public int SoldTicket { get; set; }
            public int PreOrderedSeat { get; set; }
            public int RemainingSeat { get; set; }
        }


        private void SearchFlightsWithSQL(object parameter)
        {
            using (var context = new FLIGHTTICKETMANAGEMENTEntities())
            {
                var query = from chuyenbay in context.CHUYENBAYs
                            join chitiet in context.CHITIETHANGVEs
                            on chuyenbay.MaChuyenBay equals chitiet.MaChuyenBay
                            where (string.IsNullOrEmpty(SelectedFlightID) || chuyenbay.MaChuyenBay == SelectedFlightID)
                                && (string.IsNullOrEmpty(SelectedFromAirport) || chuyenbay.MaSanBayDi == SelectedFromAirport)
                                && (string.IsNullOrEmpty(SelectedToAirport) || chuyenbay.MaSanBayDen == SelectedToAirport)
                                && (SelectedFlyDate.HasValue
                                    && chuyenbay.NgayBay.Year == SelectedFlyDate.Value.Year
                                    && chuyenbay.NgayBay.Month == SelectedFlyDate.Value.Month
                                    && chuyenbay.NgayBay.Day == SelectedFlyDate.Value.Day)
                                && (string.IsNullOrEmpty(SelectedTicketClass) || chitiet.MaHangVe == SelectedTicketClass)
                            select new FlightResult
                            {
                                MaChuyenBay = chuyenbay.MaChuyenBay,
                                MaSanBayDi = chuyenbay.MaSanBayDi,
                                MaSanBayDen = chuyenbay.MaSanBayDen,
                                NgayBay = chuyenbay.NgayBay,
                                GioKhoiHanh = chuyenbay.GioKhoiHanh,
                                TicketClass = chitiet.MaHangVe,
                                SoldTicket = chitiet.SoVeDaBan,
                                PreOrderedSeat = chitiet.SoGheDat,
                                RemainingSeat = chitiet.SoGheChoHangVe - chitiet.SoGheDat - chitiet.SoVeDaBan
                            };

                FilteredFlights = new ObservableCollection<FlightResult>(query.ToList());
                OnPropertyChanged(nameof(FilteredFlights));
            }
        }

    }




}
