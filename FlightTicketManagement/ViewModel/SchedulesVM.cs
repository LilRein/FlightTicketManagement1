using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private ObservableCollection<Schedules> _customerList;

        public ObservableCollection<Schedules> CustomerList
        {
            get { return _customerList; }
            set
            {
                _customerList = value;
                OnPropertyChanged(nameof(CustomerList));
            }
        }

        public SchedulesVM()
        {
            _pageModel = new PageModel();
            Schedules = DateOnly.FromDateTime(DateTime.Now);
        }
    }
}
