using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightTicketManagement.Utilities;

namespace FlightTicketManagement.ViewModel
{
    class AddFlightVM : Utilities.ViewModelBase
    {
        public ICommand AddCommand { get; set; }

        public AddFlightVM() 
        {
            
        }
    }
}
