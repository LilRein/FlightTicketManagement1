using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightTicketManagement.Model;

namespace FlightTicketManagement.ViewModel
{
    class SchedulesVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
        public DateOnly Schedules
        {
            get { return _pageModel.Schedule; }
            set { _pageModel.Schedule = value; OnPropertyChanged(); }
        }

        public SchedulesVM()
        {
            _pageModel = new PageModel();
            Schedules = DateOnly.FromDateTime(DateTime.Now);
        }
    }
}
