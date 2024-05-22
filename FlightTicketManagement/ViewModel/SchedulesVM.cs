using FlightTicketManagement.Model;
using FlightTicketManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        public ICommand AddMidFlightCM { get; set; }
        public ICommand AddFlightCM { get; set; }

        private void InitalizeCommand()
        {
            AddMidFlightCM = new RelayCommand<View.Schedules>((p) => true, (p) => _AddMidFlight());
            AddFlightCM = new RelayCommand<View.Schedules>((p) => true, (p) => _AddMidFlight());
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
        }

        public SchedulesVM()
        {
            _pageModel = new PageModel();

            FlightList = new ObservableCollection<CHUYENBAY>(DataProvider.Ins.DB.CHUYENBAYs);

            MidFlightList = new ObservableCollection<CTSANBAYTRUNGGIAN>(DataProvider.Ins.DB.CTSANBAYTRUNGGIANs);

            InitalizeCommand();
        }
    }
}
