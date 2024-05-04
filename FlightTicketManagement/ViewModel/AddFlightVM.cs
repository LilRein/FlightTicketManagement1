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

        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

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
            ConfirmCommand = new RelayCommand<AddFlight>((p) => true, (p) => _ConfirmCommand(p));
            CancelCommand = new RelayCommand<AddFlight>((p) => true, (p) => _CancelCommand(p));
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

        private void _ConfirmCommand(AddFlight paramater)
        {
            if (paramater.FlightID.Text == "" || string.IsNullOrEmpty(paramater.StartListAirport.Text) || 
                string.IsNullOrEmpty(paramater.EndListAirport.Text) || 
                string.IsNullOrEmpty(paramater.FlightTime.Text)||
                paramater.Seat1.Text == "" || paramater.Seat2.Text == "")
            {
                MessageBox.Show("Có vẻ bạn thiếu thông tin!!", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBoxResult addFliNoti = System.Windows.MessageBox.Show("Bạn muốn thêm chuyến bay ?", "Notification", MessageBoxButton.YesNo);
            if (addFliNoti == MessageBoxResult.Yes)
            {
                //Xử lí logic khi nhấn Yes
                MessageBox.Show("Test thôi không có gì đâu");
            }
        }

        private void _CancelCommand(AddFlight paramater)
        {
            paramater.FlightID.Clear();
            paramater.StartListAirport.SelectedItem = null;
            paramater.EndListAirport.SelectedItem = null;
            paramater.FlightTime.Text = string.Empty;
            paramater.Seat1.Clear();
            paramater.Seat2.Clear();
        }
    }
}
