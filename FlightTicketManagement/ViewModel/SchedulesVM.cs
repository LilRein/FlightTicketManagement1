using FlightTicketManagement.Model;
using FlightTicketManagement.Utilities;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using FlightTicketManagement.View.Components;

namespace FlightTicketManagement.ViewModel
{
    class SchedulesVM : ViewModelBase
    {
        private readonly PageModel _pageModel;
        public string Schedules
        {
            get { return _pageModel.Schedule; }
            set { _pageModel.Schedule = value; OnPropertyChanged(); }
        }

        private ObservableCollection<CHUYENBAY> _flightList;
        public ObservableCollection<CHUYENBAY> FlightList
        {
            get { return _flightList; }
            set
            {
                _flightList = value;
                OnPropertyChanged(nameof(FlightList));
            }
        }

        private ObservableCollection<CTSANBAYTRUNGGIAN> _midflightList;
        public ObservableCollection<CTSANBAYTRUNGGIAN> MidFlightList
        {
            get { return _midflightList; }
            set
            {
                _midflightList = value;
                OnPropertyChanged(nameof(MidFlightList));
            }
        }

        public ICommand EditFlightCommand { get; set; }
        public ICommand AddMidFlightCM { get; set; }
        public ICommand AddFlightCM { get; set; }
        public ICommand DeleteFlightCommand { get; set; }

        private void InitializeCommand()
        {
            AddMidFlightCM = new RelayCommand<View.Schedules>((p) => true, (p) => _AddMidFlight());
            AddFlightCM = new RelayCommand<View.Schedules>((p) => true, (p) => _AddFlight());
            EditFlightCommand = new RelayCommand<CHUYENBAY>((flight) => flight != null, (flight) => _EditFlight(flight));
            DeleteFlightCommand = new RelayCommand<CHUYENBAY>((flight) => flight != null, (flight) => _DeleteFlight(flight));
        }

        private void _EditFlight(CHUYENBAY flight)
        {
            if (flight == null)
                return;

            var addFlightVM = new AddFlightVM();
            addFlightVM.LoadFlightData(flight);

            var addFlightWindow = new Window
            {
                Content = new AddFlight { DataContext = addFlightVM },
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStyle = WindowStyle.None,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Title = "Sửa chuyến bay"
            };

            bool? result = addFlightWindow.ShowDialog();
            if (result == true)
            {
                LoadData();
            }
        }

        private void _AddMidFlight()
        {
            AddMidFlight addMidFlight = new AddMidFlight();

            var window = new Window
            {
                Content = addMidFlight,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStyle = WindowStyle.None,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            window.ShowDialog();
            LoadData();
        }

        private void _AddFlight()
        {
            AddFlight addFlight = new AddFlight();
            var window = new Window
            {
                Content = addFlight,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStyle = WindowStyle.None,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            bool? result = window.ShowDialog();
            if (result == true)
            {
                LoadData();
            }
        }

        private void _DeleteFlight(CHUYENBAY flight)
        {
            if (flight == null)
                return;

            MessageBoxResult result = MessageBox.Show("Bạn có thực sự muốn xóa chuyến bay này?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                // Xóa dữ liệu liên quan trong các bảng khác trước
                var relatedMidFlights = DataProvider.Ins.DB.CTSANBAYTRUNGGIANs.Where(mf => mf.MaChuyenBay == flight.MaChuyenBay).ToList();
                foreach (var midFlight in relatedMidFlights)
                {
                    DataProvider.Ins.DB.CTSANBAYTRUNGGIANs.Remove(midFlight);
                }

                var relatedTickets = DataProvider.Ins.DB.VECHUYENBAYs.Where(vc => vc.MaChuyenBay == flight.MaChuyenBay).ToList();
                foreach (var ticket in relatedTickets)
                {
                    DataProvider.Ins.DB.VECHUYENBAYs.Remove(ticket);
                }

                var relatedReservations = DataProvider.Ins.DB.PHIEUDATCHOes.Where(pd => pd.MaChuyenBay == flight.MaChuyenBay).ToList();
                foreach (var reservation in relatedReservations)
                {
                    DataProvider.Ins.DB.PHIEUDATCHOes.Remove(reservation);
                }

                var relatedHangVes = DataProvider.Ins.DB.CHITIETHANGVEs.Where(hv => hv.MaChuyenBay == flight.MaChuyenBay).ToList();
                foreach (var hangVe in relatedHangVes)
                {
                    DataProvider.Ins.DB.CHITIETHANGVEs.Remove(hangVe);
                }

                var relatedDoanhThuThangs = DataProvider.Ins.DB.CTDOANHTHUTHANGs.Where(dt => dt.MaChuyenBay == flight.MaChuyenBay).ToList();
                foreach (var doanhThuThang in relatedDoanhThuThangs)
                {
                    DataProvider.Ins.DB.CTDOANHTHUTHANGs.Remove(doanhThuThang);
                }

                // Xóa chuyến bay
                _flightList.Remove(flight);
                DataProvider.Ins.DB.CHUYENBAYs.Remove(flight);
                DataProvider.Ins.DB.SaveChanges();
                LoadData();
            }
        }

        public void LoadData()
        {
            FlightList = new ObservableCollection<CHUYENBAY>(DataProvider.Ins.DB.CHUYENBAYs.ToList());
            MidFlightList = new ObservableCollection<CTSANBAYTRUNGGIAN>(DataProvider.Ins.DB.CTSANBAYTRUNGGIANs.ToList());
        }

        public SchedulesVM()
        {
            _pageModel = new PageModel();

            LoadData();
            InitializeCommand();
        }
    }
}
