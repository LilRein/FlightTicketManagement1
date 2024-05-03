using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using FlightTicketManagement.Model;
using FlightTicketManagement.Utilities;
using FlightTicketManagement.View;
using FlightTicketManagement.View.Components;

namespace FlightTicketManagement.ViewModel
{
    class AddMidFlightVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public ICommand CloseAMACM { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

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
                "Mập",
                "Béo",
                
            };

            CloseAMACM = new RelayCommand<AddMidFlight>((p) => true, (p) => _CloseAM(p));
            ConfirmCommand = new RelayCommand<AddMidFlight>((p) => true, (p) => _ConfirmCommand(p));
            CancelCommand = new RelayCommand<AddMidFlight>((p) => true, (p) => _CancelCommand(p));

            SelectedItem = FlightItemList[0];
        }

        private void _CloseAM(AddMidFlight paramater)
        {
            var window = Window.GetWindow(paramater);
            if(window != null)
            {
                window.Close();
            }
        }

        void _CancelCommand(AddMidFlight paramater)
        {
            paramater.WaitTime.Text = string.Empty;
            paramater.InputNote.Clear();
        }
        void _ConfirmCommand(AddMidFlight paramater)
        {
            if (string.IsNullOrEmpty(paramater.WaitTime.Text))
            {
                MessageBox.Show("Có vẻ bạn thiếu thông tin!!", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBoxResult addFliNoti = System.Windows.MessageBox.Show("Bạn muốn thêm chuyến bay ?", "Notification", MessageBoxButton.YesNo);
            if (addFliNoti == MessageBoxResult.Yes)
            {
                AddMidFlight(paramater);
                var window = Window.GetWindow(paramater);
                if (window != null)
                {
                    window.Close();
                }
            }
        }

        private void AddMidFlight(AddMidFlight paramater)
        {

        }
    }
}
