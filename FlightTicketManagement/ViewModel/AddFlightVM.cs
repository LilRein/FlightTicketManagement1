using FlightTicketManagement.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using FlightTicketManagement.Model;
using FlightTicketManagement.View.Components;

namespace FlightTicketManagement.ViewModel
{
    class AddFlightVM : Utilities.ViewModelBase
    {
        private ObservableCollection<CHUYENBAY> _CHUYENBAYList;
        public ObservableCollection<CHUYENBAY> CHUYENBAYList
        {
            get { return _CHUYENBAYList; }
            set
            {
                _CHUYENBAYList = value;
                OnPropertyChanged(nameof(CHUYENBAYList));
            }
        }

        private ObservableCollection<TUYENBAY> _TUYENBAY;
        public ObservableCollection<TUYENBAY> TUYENBAY
        {
            get { return _TUYENBAY; }
            set
            {
                _TUYENBAY = value;
                OnPropertyChanged(nameof(_TUYENBAY));
            }
        }

        private ObservableCollection<SANBAY> _SANBAY;
        public ObservableCollection<SANBAY> SANBAY
        {
            get { return _SANBAY; }
            set
            {
                _SANBAY = value;
                OnPropertyChanged(nameof(SANBAY));
            }
        }

        private ObservableCollection<MAYBAY> _MAYBAY;
        public ObservableCollection<MAYBAY> MAYBAY
        {
            get { return _MAYBAY; }
            set
            {
                _MAYBAY = value;
                OnPropertyChanged(nameof(MAYBAY));
            }
        }

        private SANBAY _SelectedSANBAYDI;
        public SANBAY SelectedSANBAYDI
        {
            get => _SelectedSANBAYDI;
            set
            {
                _SelectedSANBAYDI = value;
                OnPropertyChanged();
            }
        }

        private SANBAY _SelectedSANBAYDEN;
        public SANBAY SelectedSANBAYDEN
        {
            get => _SelectedSANBAYDEN;
            set
            {
                _SelectedSANBAYDEN = value;
                OnPropertyChanged();
            }
        }

        private MAYBAY _SelectedMAYBAY;
        public MAYBAY SelectedMAYBAY
        {
            get => _SelectedMAYBAY;
            set
            {
                _SelectedMAYBAY = value;
                OnPropertyChanged();
            }
        }

        private TUYENBAY _SelectedTUYENBAY;
        public TUYENBAY SelectedTUYENBAY
        {
            get => _SelectedTUYENBAY;
            set
            {
                _SelectedTUYENBAY = value;
                OnPropertyChanged();
            }
        }


        private string _MaChuyenBay;
        public string MaChuyenBay { get => _MaChuyenBay; set { _MaChuyenBay = value; OnPropertyChanged(); } }

        private string _MaTuyenBay;
        public string MaTuyenBay { get => _MaTuyenBay; set { _MaTuyenBay = value; OnPropertyChanged(); } }

        private string _MaSanBayDi;
        public string MaSanBayDi { get => _MaSanBayDi; set { _MaSanBayDi = value; OnPropertyChanged(); } }

        private string _MaSanBayDen;
        public string MaSanBayDen { get => _MaSanBayDen; set { _MaSanBayDen = value; OnPropertyChanged(); } }

        private string _MaMayBay;
        public string MaMayBay { get => _MaMayBay; set { _MaMayBay = value; OnPropertyChanged(); } }

        private DateTime _NgayBay;
        public DateTime NgayBay { get => _NgayBay; set { _NgayBay = value; OnPropertyChanged(); } }

        private TimeSpan _GioKhoiHanh;
        public TimeSpan GioKhoiHanh { get => _GioKhoiHanh; set { _GioKhoiHanh = value; OnPropertyChanged(); } }

        private double _ThoiLuong;
        public double ThoiLuong { get => _ThoiLuong; set { _ThoiLuong = value; OnPropertyChanged(); } }

        private decimal _DonGia;
        public decimal DonGia { get => _DonGia; set { _DonGia = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand CloseAACM { get; set; }

        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public AddFlightVM()
        {
            CHUYENBAYList = new ObservableCollection<CHUYENBAY>(DataProvider.Ins.DB.CHUYENBAYs);
            SANBAY = new ObservableCollection<SANBAY>(DataProvider.Ins.DB.SANBAYs);
            TUYENBAY = new ObservableCollection<TUYENBAY>(DataProvider.Ins.DB.TUYENBAYs);
            MAYBAY = new ObservableCollection<MAYBAY>(DataProvider.Ins.DB.MAYBAYs);

            CloseAACM = new RelayCommand<AddFlight>((p) => true, (p) => _CloseAACM(p));
            ConfirmCommand = new RelayCommand<AddFlight>((p) => true, (p) => _ConfirmCommand(p));
            //CancelCommand = new RelayCommand<AddFlight>((p) => true, (p) => _CancelCommand(p));
        }

        private void _CloseAACM(AddFlight parameter)
        {
            var window = Window.GetWindow(parameter);
            if (window != null)
            {
                window.Close();
            }
        }

        private void _ConfirmCommand(AddFlight parameter)
        {
            if (string.IsNullOrEmpty(MaChuyenBay) || SelectedTUYENBAY == null ||
                SelectedSANBAYDI == null ||
                SelectedSANBAYDEN == null || SelectedMAYBAY == null ||
                ThoiLuong == 0 || DonGia == 0)

            {
                MessageBox.Show("Có vẻ bạn thiếu thông tin!!", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBoxResult addFliNoti = MessageBox.Show("Bạn muốn thêm chuyến bay?", "Notification", MessageBoxButton.YesNo);
            if (addFliNoti == MessageBoxResult.Yes)
            {
                var chuyenbay = new CHUYENBAY()
                {
                    MaChuyenBay = MaChuyenBay,
                    MaTuyenBay = SelectedTUYENBAY.MaTuyenBay,
                    MaSanBayDi = SelectedSANBAYDI.MaSanBay,
                    MaSanBayDen = SelectedSANBAYDEN.MaSanBay,
                    MaMayBay = SelectedMAYBAY.MaMayBay,
                    NgayBay = NgayBay,
                    GioKhoiHanh = GioKhoiHanh,
                    ThoiLuong = ThoiLuong,
                    DonGia = DonGia
                };

                DataProvider.Ins.DB.CHUYENBAYs.Add(chuyenbay);
                DataProvider.Ins.DB.SaveChanges();

                CHUYENBAYList.Add(chuyenbay);
                MessageBox.Show("Chuyến bay đã được thêm thành công!");
            }
        }

        //private void _CancelCommand(AddFlight parameter)
        //{
        //    parameter.FlightID.Clear();
        //    parameter.StartListAirport.SelectedItem = null;
        //    parameter.EndListAirport.SelectedItem = null;
        //    parameter.PlaneID.Clear();
        //    parameter.Duration.Clear();
        //    parameter.Price.Clear();
        //    parameter.SeatCount.Clear();
        //    parameter.FlightDatePicker.SelectedDate = null;
        //    parameter.FlightTimePicker.SelectedTime = null;
        //}
    }
}
