using FlightTicketManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using FlightTicketManagement.Utilities;
using System.Windows.Controls;
using System.Windows.Input;
using FlightTicketManagement.View;
using System.Windows;

namespace FlightTicketManagement.ViewModel
{
    class TicketOrderVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public ICommand ConfirmOrder {  get; set; }
        public ICommand CancelOrder {  get; set; }

        public ICommand ConfirmInfor { get; set; }
        public ICommand CancelInfor { get; set; }


        private ObservableCollection<string> _classFlightList;
        public ObservableCollection<string> ClassFlightList
        {
            get { return _classFlightList; }
            set 
            { 
                _classFlightList = value; 
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _flightIDList;
        public ObservableCollection<string> FlightIDList
        {
            get { return _flightIDList; }
            set
            {
                _flightIDList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _seatIDList;
        public ObservableCollection<string> SeatIDList
        {
            get { return _seatIDList; }
            set
            {
                _seatIDList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _status;
        public ObservableCollection<string> Status
        {
            get { return _seatIDList; }
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }



        public int OrderID 
        {
            get { return _pageModel.TicketOrder; }
            set {  _pageModel.TicketOrder = value; OnPropertyChanged(); }
        }
        public TicketOrderVM()
        {
            ClassFlightList = new ObservableCollection<string>
            {
                //Lấy mã hạng vé từ dtb
            };
            FlightIDList = new ObservableCollection<string>
            {
                //Lấy mã chuyến bay từ dtb
            };
            SeatIDList = new ObservableCollection<string>
            {
                //Lấy dữ liệu từ dtb
            };
            Status = new ObservableCollection<string>
            {
                //Lấy tình trạng ghế
            };

            ConfirmOrder = new RelayCommand<TicketOrder>((p) => true, (p) => _confirmOrder());
            CancelOrder = new RelayCommand<TicketOrder>((p) => true, (p) => _cancelOrder());
            ConfirmInfor = new RelayCommand<TicketOrder>((p) => true, (p) => _confirmInfor());
            CancelInfor = new RelayCommand<TicketOrder>((p) => true, (p) => _cancelInfor());

            _pageModel = new PageModel();
        }

        private void _cancelInfor()
        {
            
        }

        private void _confirmInfor()
        {
            
        }

        private void _cancelOrder()
        {
            MessageBox.Show("Canceled");
        }

        private void _confirmOrder()
        {
            MessageBox.Show("Clicked");
        }
    }
}
