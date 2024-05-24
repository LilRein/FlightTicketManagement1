using FlightTicketManagement.Model;
using FlightTicketManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        public ICommand TicketOrderCommand { get; set; }

        private void Dashboard(object obj) => CurrentView = new DashboardVM();

        //private void Dashboard(Window) => CurrentView = new DashboardVM();
        private void Schedule(object obj) => CurrentView = new SchedulesVM();
        private void TicketSale(object obj) => CurrentView = new TicketSaleVM();
        private void Flight(object obj) => CurrentView = new FlightsVM();
        private void Report(object obj) => CurrentView = new ReportsVM();
        private void Setting(object obj) => CurrentView = new SettingVM();
        private void TicketOrder(object obj) => CurrentView = new TicketOrderVM();
       

        public NavigationVM()
        {
            //DashboardCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            //{
            //    Isloaded = true;
            //    if (p == null)
            //        return;
            //    p.Hide();
            //    Login loginWindow = new Login();
            //    loginWindow.ShowDialog();

            //    if (loginWindow.DataContext == null)
            //        return;
            //    var loginVM = loginWindow.DataContext as LoginVM;

            //    if (loginVM.IsLogin)
            //    {
            //        p.Show();
            //    }
            //    else
            //    {
            //        p.Close();
            //    }
            //}
            //  );

            DashboardCommand = new RelayCommand(Dashboard);
            ScheduleCommand = new RelayCommand(Schedule);
            TicketSaleCommand = new RelayCommand(TicketSale);
            FlightCommand = new RelayCommand(Flight);
            ReportCommand = new RelayCommand(Report);
            SettingCommand = new RelayCommand(Setting);
            TicketOrderCommand = new RelayCommand(TicketOrder);

            // Startup ????
            CurrentView = new DashboardVM();

            var a = DataProvider.Ins.DB.CHUYENBAYs.ToList();

        }
    }
}
