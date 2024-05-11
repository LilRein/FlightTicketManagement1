using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GUI.Model;
using GUI.Utilities;
using GUI.View;

namespace GUI.ViewModel
{
    class TicketSaleVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }


        private string _isAvailableTicket;
        public string IsAvailableTicket
        {
            get { return _isAvailableTicket; }
            set
            {
                _isAvailableTicket = value;
                OnPropertyChanged(nameof(IsAvailableTicket));
            }
        }

        private ObservableCollection<string> _flightItemList;
        public ObservableCollection<string> FlightItemList
        {
            get { return _flightItemList; }
            set
            {
                _flightItemList = value;
                OnPropertyChanged();
            }
        }
        public int FlightSale
        {
            get { return _pageModel.TicketSale; }
            set { _pageModel.TicketSale = value; OnPropertyChanged(); }
        }
        public TicketSaleVM()
        {
            FlightItemList = new ObservableCollection<string>
            {
                // Lấy thông tin mã chuyến bay từ database đưa vào đây

            };
            SaveCommand = new RelayCommand<TicketSale>((p) => true, (p) => _saveCommand());
            CancelCommand = new RelayCommand<TicketSale>((p) => true, (p) => _cancelCommand(p));
            

            _pageModel = new PageModel();
        }

        private void _saveCommand()
        {
            MessageBox.Show("saved");
        }
        private void _cancelCommand(TicketSale paramater)
        {
            paramater.ListFlightID.SelectedItem = null;
            paramater.StartAirport.Clear();
            paramater.EndAirport.Clear();
            paramater.CustomerID.Clear();
            paramater.NameCustomer.Clear();
            paramater.InputInfor.Clear();
            paramater.PhoneNumber.Clear();
            paramater.TicketClass.SelectedItem = null;
            MessageBox.Show("cancel");
        }
    }
}
