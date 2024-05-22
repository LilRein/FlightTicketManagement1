using FlightTicketManagement.Utilities;
using System.Windows.Input;

namespace FlightTicketManagement.ViewModel
{
    class NavigationVM : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public bool Isloaded = false;

        public ICommand DashboardCommand { get; set; }
        public ICommand ScheduleCommand { get; set; }
        public ICommand TicketSaleCommand { get; set; }
        public ICommand FlightCommand { get; set; }
        public ICommand ReportCommand { get; set; }
        public ICommand SettingCommand { get; set; }

        private void Dashboard(object obj) => CurrentView = new DashboardVM();
        private void Schedule(object obj) => CurrentView = new SchedulesVM();
        private void TicketSale(object obj) => CurrentView = new TicketSaleVM();
        private void Flight(object obj) => CurrentView = new FlightsVM();
        private void Report(object obj) => CurrentView = new ReportsVM();
        private void Setting(object obj) => CurrentView = new SettingVM();

        public NavigationVM()
        {
            DashboardCommand = new RelayCommand(Dashboard);
            ScheduleCommand = new RelayCommand(Schedule);
            TicketSaleCommand = new RelayCommand(TicketSale);
            FlightCommand = new RelayCommand(Flight);
            ReportCommand = new RelayCommand(Report);
            SettingCommand = new RelayCommand(Setting);

            // Khởi tạo LoginVM và đăng ký sự kiện LoginSuccess
            var loginVM = new LoginVM();
            loginVM.LoginSuccess += OnLoginSuccess;
            CurrentView = loginVM;
        }

        private void OnLoginSuccess()
        {
            CurrentView = new DashboardVM();
        }
    }
}
