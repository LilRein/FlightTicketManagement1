﻿using FlightTicketManagement.Utilities;
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

        private ObservableCollection<CHITIETHANGVE> _CHITIETHANGVEList;
        public ObservableCollection<CHITIETHANGVE> CHITIETHANGVEList
        {
            get { return _CHITIETHANGVEList; }
            set
            {
                _CHITIETHANGVEList = value;
                OnPropertyChanged(nameof(CHITIETHANGVEList));
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

        private string _MaTuyenBay;
        public string MaTuyenBay { get => _MaTuyenBay; set { _MaTuyenBay = value; OnPropertyChanged(); } }

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
            CHUYENBAYList = new ObservableCollection<CHUYENBAY>(DataProvider.Ins.DB.CHUYENBAYs);
            CHITIETHANGVEList = new ObservableCollection<CHITIETHANGVE>(DataProvider.Ins.DB.CHITIETHANGVEs);

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

        private ObservableCollection<string> _routeIDList;
        public ObservableCollection<string> RouteIDList
        {
            get { return _routeIDList; }
            set
            {
                _routeIDList = value;
                OnPropertyChanged();
            }
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
            if (string.IsNullOrEmpty(parameter.FlightID.Text) || parameter.StartListAirport.SelectedItem == null ||
                parameter.EndListAirport.SelectedItem == null ||
                parameter.FlightTimePicker.SelectedTime == null ||
                string.IsNullOrEmpty(parameter.SeatCount.Text) ||
                string.IsNullOrEmpty(parameter.Price.Text))
            {
                MessageBox.Show("Có vẻ bạn thiếu thông tin!!", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBoxResult addFliNoti = MessageBox.Show("Bạn muốn thêm chuyến bay?", "Notification", MessageBoxButton.YesNo);
            if (addFliNoti == MessageBoxResult.Yes)
            {
                // Xử lý logic khi nhấn Yes
                MessageBox.Show("Chuyến bay đã được thêm thành công!");
            }
        }

        private void _CancelCommand(AddFlight parameter)
        {
            parameter.FlightID.Clear();
            parameter.StartListAirport.SelectedItem = null;
            parameter.EndListAirport.SelectedItem = null;
            parameter.PlaneID.Clear();
            parameter.Duration.Clear();
            parameter.Price.Clear();
            parameter.SeatCount.Clear();
            parameter.FlightDatePicker.SelectedDate = null;
            parameter.FlightTimePicker.SelectedTime = null;
        }
    }
}
