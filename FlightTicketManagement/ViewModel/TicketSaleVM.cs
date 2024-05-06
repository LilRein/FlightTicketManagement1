using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FlightTicketManagement.Model;
using FlightTicketManagement.Utilities;
using FlightTicketManagement.View;

namespace FlightTicketManagement.ViewModel
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
            CancelCommand = new RelayCommand<TicketSale>((p) => true, (p) => _cancelCommand());
            

            _pageModel = new PageModel();
        }

        private void _saveCommand()
        {
            MessageBox.Show("hi");
        }
        private void _cancelCommand()
        {
            MessageBox.Show("cancel");
        }
    }
}
