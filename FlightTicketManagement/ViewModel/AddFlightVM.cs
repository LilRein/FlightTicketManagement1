using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using FlightTicketManagement.Utilities;
using FlightTicketManagement.View.Components;
using System.Collections.ObjectModel;

namespace FlightTicketManagement.ViewModel
{
    class AddFlightVM : Utilities.ViewModelBase
    {
        public ICommand AddCommand { get; set; }
        public ICommand CloseAACM { get; set; }

        public AddFlightVM() 
        {
            FlightItemList = new ObservableCollection<string>
            {
                "Sân bay quốc tế Tân Sơn Nhất",
                "Sân bay quốc tế Vân Đồn",
                "Sân bay quốc tế Nội Bài",
                "Sân bay quốc tế Cát Bi",

            };
            CloseAACM = new RelayCommand<AddFlight>((p) => true, (p) => _CloseAACM(p));
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

        private void _CloseAACM(AddFlight paramater)
        {
            var window = Window.GetWindow(paramater);
            if (window != null)
            {
                window.Close();
            }
        }

       
    }
}
