using FlightTicketManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using FlightTicketManagement.View.Components;
using FlightTicketManagement.Model;

namespace FlightTicketManagement.ViewModel
{
    class AddFlightVM : Utilities.ViewModelBase
    {
        private ObservableCollection<CHUYENBAY> _CHUYENBAYList;
        public ObservableCollection<CHUYENBAY> CHUYENBAYList { get => _CHUYENBAYList; set { _CHUYENBAYList = value; OnPropertyChanged(); } }

        private ObservableCollection<CHITIETHANGVE> _CHITIETHANGVEList;
        public ObservableCollection<CHITIETHANGVE> CHITIETHANGVEList { get => _CHITIETHANGVEList; set { _CHITIETHANGVEList = value; OnPropertyChanged(); } }

        private CHUYENBAY _SelectedCHUYENBAY;
        public CHUYENBAY SelectedCHUYENBAY
        {
            get => _SelectedCHUYENBAY;
            set
            {
                _SelectedCHUYENBAY = value;
                OnPropertyChanged();

                if (SelectedCHUYENBAY != null)
                {
                    MaChuyenBay = SelectedCHUYENBAY.MaChuyenBay;
                    MaSanBayDi = SelectedCHUYENBAY.MaSanBayDi;
                    MaSanBayDen = SelectedCHUYENBAY.MaSanBayDen;
                    NgayBay = SelectedCHUYENBAY.NgayBay;
                    GioKhoiHanh = SelectedCHUYENBAY.GioKhoiHanh;
                    ThoiLuong = SelectedCHUYENBAY.ThoiLuong;
                }
            }
        }

        private CHITIETHANGVE _SelectedCHITIETHANGVE;
        public CHITIETHANGVE SelectedCHITIETHANGVE
        {
            get => _SelectedCHITIETHANGVE;
            set
            {
                _SelectedCHITIETHANGVE = value;
                OnPropertyChanged();

                if (SelectedCHITIETHANGVE != null)
                {
                    SoGheChoHangVe = SelectedCHITIETHANGVE.SoGheChoHangVe;
                }
            }
        }

        private string _MaChuyenBay;
        public string MaChuyenBay { get => _MaChuyenBay; set { _MaChuyenBay = value; OnPropertyChanged(); } }

        private string _MaSanBayDi;
        public string MaSanBayDi { get => _MaSanBayDi; set { _MaSanBayDi = value; OnPropertyChanged(); } }

        private string _MaSanBayDen;
        public string MaSanBayDen { get => _MaSanBayDen; set { _MaSanBayDen = value; OnPropertyChanged(); } }

        private DateTime _NgayBay;
        public DateTime NgayBay { get => _NgayBay; set { _NgayBay = value; OnPropertyChanged(); } }

        private TimeSpan _GioKhoiHanh;
        public TimeSpan GioKhoiHanh { get => _GioKhoiHanh; set { _GioKhoiHanh = value; OnPropertyChanged(); } }

        private double _ThoiLuong;
        public double ThoiLuong { get => _ThoiLuong; set { _ThoiLuong = value; OnPropertyChanged(); } }

        private int _SoGheChoHangVe;
        public int SoGheChoHangVe { get => _SoGheChoHangVe; set { _SoGheChoHangVe = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand CloseAACM { get; set; }

        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public AddFlightVM()
        {
            CHUYENBAYList = new ObservableCollection<CHUYENBAY>();
            CHITIETHANGVEList = new ObservableCollection<CHITIETHANGVE>();



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
                string.IsNullOrEmpty(paramater.FlightTime.Text) ||
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
            paramater.FlightDatePicker.Text = string.Empty;
            paramater.FlightTimePicker.Text = string.Empty;

        }
    }
}
