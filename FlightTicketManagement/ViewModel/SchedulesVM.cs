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

        private ObservableCollection<SANBAY> _airportLocations;
        public ObservableCollection<SANBAY> AirportLocations
        {
            get { return _airportLocations; }
            set
            {
                _airportLocations = value;
                OnPropertyChanged(nameof(AirportLocations));
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

            window.ShowDialog();
            LoadData();
        }

        private void _DeleteFlight(CHUYENBAY flight)
        {
            if (flight == null)
                return;

            // Kiểm tra liên kết dữ liệu
            bool hasRelatedData = DataProvider.Ins.DB.CTSANBAYTRUNGGIANs.Any(mf => mf.MaChuyenBay == flight.MaChuyenBay) ||
                                  DataProvider.Ins.DB.VECHUYENBAYs.Any(vc => vc.MaChuyenBay == flight.MaChuyenBay) ||
                                  DataProvider.Ins.DB.PHIEUDATCHOes.Any(pd => pd.MaChuyenBay == flight.MaChuyenBay) ||
                                  DataProvider.Ins.DB.CHITIETHANGVEs.Any(hv => hv.MaChuyenBay == flight.MaChuyenBay) ||
                                  DataProvider.Ins.DB.CTDOANHTHUTHANGs.Any(dt => dt.MaChuyenBay == flight.MaChuyenBay);

            if (hasRelatedData)
            {
                MessageBox.Show("Không thể xóa chuyến bay vì có dữ liệu liên kết.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Bạn có thực sự muốn xóa chuyến bay này?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                DataProvider.Ins.DB.CHUYENBAYs.Remove(flight);
                DataProvider.Ins.DB.SaveChanges();
                FlightList.Remove(flight);
            }
        }

        public void LoadData()
        {
            FlightList = new ObservableCollection<CHUYENBAY>(DataProvider.Ins.DB.CHUYENBAYs.ToList());
            MidFlightList = new ObservableCollection<CTSANBAYTRUNGGIAN>(DataProvider.Ins.DB.CTSANBAYTRUNGGIANs.ToList());
            AirportLocations = new ObservableCollection<SANBAY>(DataProvider.Ins.DB.SANBAYs.ToList());
        }

        public SchedulesVM()
        {
            _pageModel = new PageModel();
            FlightList = new ObservableCollection<CHUYENBAY>();
            MidFlightList = new ObservableCollection<CTSANBAYTRUNGGIAN>();

            LoadData();
            InitializeCommand();
        }
    }
}
