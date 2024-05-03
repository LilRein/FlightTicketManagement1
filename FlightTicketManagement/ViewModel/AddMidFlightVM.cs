using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightTicketManagement.Model;
using FlightTicketManagement.Utilities;
using FlightTicketManagement.View;
using FlightTicketManagement.View.Components;

namespace FlightTicketManagement.ViewModel
{
    class AddMidFlightVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

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

        private string _selectedItem;
        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public AddMidFlightVM()
        {
            FlightItemList = new ObservableCollection<string>
            {
                "Tèo",
                "Đô",
                "Mập"
            };

            SelectedItem = FlightItemList[0];
        }
    }
}
