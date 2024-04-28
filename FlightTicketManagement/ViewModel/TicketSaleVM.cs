using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightTicketManagement.Model;

namespace FlightTicketManagement.ViewModel
{
    class TicketSaleVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public int FlightSale
        {
            get { return _pageModel.TicketSale; }
            set { _pageModel.TicketSale = value; OnPropertyChanged(); }
        }
        public TicketSaleVM()
        {
            _pageModel = new PageModel();
        }
    }
}
