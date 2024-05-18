using FlightTicketManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManagement.ViewModel
{
    class DashboardVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        private string _userName;
        public string Username
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(Username));
            }
        }
    }
}
