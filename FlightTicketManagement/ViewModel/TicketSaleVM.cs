using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            

            _pageModel = new PageModel();
        }

        
    }
}
