using FlightTicketManagement.Model;
using FlightTicketManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Data.Entity;

namespace FlightTicketManagement.ViewModel
{
    class FlightsVM : ViewModelBase
    {
        private ObservableCollection<CHUYENBAY> _flights;
        private ObservableCollection<FlightResult> _filteredFlights;
        private ObservableCollection<CHITIETHANGVE> _ticketDetails;


        public FlightsVM()
        {
            LoadData();
            SearchCommand = new RelayCommand(SearchFlightsWithSQL);
            ResetCommand = new RelayCommand(ResetFilters);
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

        public ObservableCollection<CHITIETHANGVE> TicketDetails
        {
            get { return _ticketDetails; }
            set { _ticketDetails = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> FlightIDs => new ObservableCollection<string>(Flights.Select(f => f.MaChuyenBay).Distinct());
        public ObservableCollection<string> FromAirports => new ObservableCollection<string>(Flights.Select(f => f.MaSanBayDi).Distinct());
        public ObservableCollection<string> ToAirports => new ObservableCollection<string>(Flights.Select(f => f.MaSanBayDen).Distinct());
        public ObservableCollection<string> TicketClasses => new ObservableCollection<string>(TicketDetails.Select(td => td.MaHangVe).Distinct());

        public string SelectedFlightID { get; set; }
        public string SelectedFromAirport { get; set; }
        public string SelectedToAirport { get; set; }
        public DateTime? SelectedFlyDate { get; set; }
        public string SelectedTicketClass { get; set; }

        public ICommand ResetCommand { get; set; }
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

        private void LoadData()
        {
            using (var context = new FLIGHTTICKETMANAGEMENTEntities1())
            {
                Flights = new ObservableCollection<CHUYENBAY>(context.CHUYENBAYs.ToList());
                TicketDetails = new ObservableCollection<CHITIETHANGVE>(context.CHITIETHANGVEs.ToList());
            }
        }

        private void SearchFlightsWithSQL(object parameter)
        {
            using (var context = new FLIGHTTICKETMANAGEMENTEntities1())
            {
                var query = from chuyenbay in context.CHUYENBAYs
                            join chitiet in context.CHITIETHANGVEs
                            on chuyenbay.MaChuyenBay equals chitiet.MaChuyenBay
                            where (string.IsNullOrEmpty(SelectedFlightID) || chuyenbay.MaChuyenBay == SelectedFlightID)
                                && (string.IsNullOrEmpty(SelectedFromAirport) || chuyenbay.MaSanBayDi == SelectedFromAirport)
                                && (string.IsNullOrEmpty(SelectedToAirport) || chuyenbay.MaSanBayDen == SelectedToAirport)
                                && (!SelectedFlyDate.HasValue || DbFunctions.TruncateTime(chuyenbay.NgayBay) == DbFunctions.TruncateTime(SelectedFlyDate.Value))
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
        private void ResetFilters(object parameter)
        {
            SelectedFlightID = null;
            SelectedFromAirport = null;
            SelectedToAirport = null;
            SelectedFlyDate = null;
            SelectedTicketClass = null;
            OnPropertyChanged(nameof(SelectedFlightID));
            OnPropertyChanged(nameof(SelectedFromAirport));
            OnPropertyChanged(nameof(SelectedToAirport));
            OnPropertyChanged(nameof(SelectedFlyDate));
            OnPropertyChanged(nameof(SelectedTicketClass));
            // Optional: You can also clear the FilteredFlights if you want to reset the result
            FilteredFlights.Clear();
        }
    }
}
