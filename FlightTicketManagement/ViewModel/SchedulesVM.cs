using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FlightTicketManagement.Model;
using FlightTicketManagement.Utilities;
using FlightTicketManagement.View;
using FlightTicketManagement.View.Components;

namespace FlightTicketManagement.ViewModel
{
    class SchedulesVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
        public string Schedules
        {
            get { return _pageModel.Schedule; }
            set { _pageModel.Schedule = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Model.Schedules> _flightList;

        public ObservableCollection<Model.Schedules> FlightList
        {
            get { return _flightList; }
            set
            {
                _flightList = value;
                OnPropertyChanged(nameof(FlightList));
            }
        }

        

        private ObservableCollection<Model.MidAirport> _midflightList;

        public ObservableCollection<Model.MidAirport> MidFlightList
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

            

            InitalizeCommand();
        }
    }
}
