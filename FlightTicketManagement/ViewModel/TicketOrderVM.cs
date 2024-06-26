﻿using FlightTicketManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using FlightTicketManagement.Utilities;
using System.Windows.Controls;
using System.Windows.Input;
using FlightTicketManagement.View;
using System.Windows;
using FlightTicketManagement.View.Components;

namespace FlightTicketManagement.ViewModel
{
    class TicketOrderVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public ICommand ConfirmOrder { get; set; }
        public ICommand CancelOrder { get; set; }

        public ICommand ConfirmInfor { get; set; }
        public ICommand CancelInfor { get; set; }

        private ObservableCollection<PHIEUDATCHO> _OrderFlightList;
        public ObservableCollection<PHIEUDATCHO> OrderFlightList
        {
            get { return _OrderFlightList; }
            set
            {
                _OrderFlightList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<HANGVE> _ClassFlightList;
        public ObservableCollection<HANGVE> ClassFlightList
        {
            get { return _ClassFlightList; }
            set
            {
                _ClassFlightList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CHUYENBAY> _FilteredFlightIDList;
        public ObservableCollection<CHUYENBAY> FilteredFlightIDList
        {
            get { return _FilteredFlightIDList; }
            set
            {
                _FilteredFlightIDList = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<CHUYENBAY> _FlightIDList;
        public ObservableCollection<CHUYENBAY> FlightIDList
        {
            get { return _FlightIDList; }
            set
            {
                _FlightIDList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DANHSACHGHECUAMAYBAY> _SeatIDList;
        public ObservableCollection<DANHSACHGHECUAMAYBAY> SeatIDList
        {
            get { return _SeatIDList; }
            set
            {
                _SeatIDList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _statusList;
        public ObservableCollection<string> StatusList
        {
            get { return _statusList; }
            set
            {
                _statusList = value;
                OnPropertyChanged();
            }
        }

        private HANGVE _SelectedMAHANGVE;
        public HANGVE SelectedMAHANGVE
        {
            get => _SelectedMAHANGVE;
            set
            {
                _SelectedMAHANGVE = value;
                OnPropertyChanged();
            }
        }

        private CHUYENBAY _SelectedMACHUYENBAY;
        public CHUYENBAY SelectedMACHUYENBAY
        {
            get => _SelectedMACHUYENBAY;
            set
            {
                _SelectedMACHUYENBAY = value;
                OnPropertyChanged();
                if (_SelectedMACHUYENBAY != null)
                {

                    // Lọc danh sách mã ghế theo mã chuyến bay đã chọn
                    FilterSeatsByFlight(_SelectedMACHUYENBAY.MaChuyenBay);
                }
            }
        }
        private void FilterSeatsByFlight(string maChuyenBay)
        {
            // Ensure that SeatIDList contains all seats initially
            var allSeats = DataProvider.Ins.DB.DANHSACHGHECUAMAYBAYs.ToList();

            // Get the MaMayBay of the selected flight
            var selectedFlight = FlightIDList.FirstOrDefault(f => f.MaChuyenBay == maChuyenBay);
            if (selectedFlight != null)
            {
                var maMayBay = selectedFlight.MaMayBay;

                // Filter seats by MaMayBay
                var filteredSeats = allSeats.Where(seat => seat.MaMayBay == maMayBay).ToList();
                SeatIDList = new ObservableCollection<DANHSACHGHECUAMAYBAY>(filteredSeats);
            }
        }


        private DANHSACHGHECUAMAYBAY _SelectedMAGHE;
        public DANHSACHGHECUAMAYBAY SelectedMAGHE
        {
            get => _SelectedMAGHE;
            set
            {
                _SelectedMAGHE = value;
                OnPropertyChanged();
            }
        }

        private string _SelectedTINHTRANG;
        public string SelectedTINHTRANG
        {
            get => _SelectedTINHTRANG;
            set
            {
                _SelectedTINHTRANG = value;
                OnPropertyChanged();
            }
        }

        private string _SoPhieuDatCho;
        public string SoPhieuDatCho { get => _SoPhieuDatCho; set { _SoPhieuDatCho = value; OnPropertyChanged(); } }

        private DateTime _NgayDat;
        public DateTime NgayDat { get => _NgayDat; set { _NgayDat = value; OnPropertyChanged(); FilterFlightsByDate(); } }

        private string _MaHanhKhach;
        public string MaHanhKhach { get => _MaHanhKhach; set { _MaHanhKhach = value; OnPropertyChanged(); } }

        private string _MaHangVe;
        public string MaHangVe { get => _MaHangVe; set { _MaHangVe = value; OnPropertyChanged(); } }

        private string _MaChuyenBay;
        public string MaChuyenBay { get => _MaChuyenBay; set { _MaChuyenBay = value; OnPropertyChanged(); } }

        private string _MaGhe;
        public string MaGhe { get => _MaGhe; set { _MaGhe = value; OnPropertyChanged(); } }

        private string _TinhTrang;
        public string TinhTrang { get => _TinhTrang; set { _TinhTrang = value; OnPropertyChanged(); } }



        private ObservableCollection<HANHKHACH> _CustomerList;
        public ObservableCollection<HANHKHACH> CustomerList
        {
            get { return _CustomerList; }
            set
            {
                _CustomerList = value;
                OnPropertyChanged();
            }
        }

        private string _HoTen;
        public string HoTen { get => _HoTen; set { _HoTen = value; OnPropertyChanged(); } }

        private string _CCCD;
        public string CCCD { get => _CCCD; set { _CCCD = value; OnPropertyChanged(); } }

        private string _DienThoai;
        public string DienThoai { get => _DienThoai; set { _DienThoai = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }
        public int OrderID
        {
            get { return _pageModel.TicketOrder; }
            set { _pageModel.TicketOrder = value; OnPropertyChanged(); }
        }

        private int _thoiGianDatVeChamNhat;
        public int ThoiGianDatVeChamNhat
        {
            get { return _thoiGianDatVeChamNhat; }
            set
            {
                _thoiGianDatVeChamNhat = value;
                OnPropertyChanged(nameof(ThoiGianDatVeChamNhat));

            }
        }
        public TicketOrderVM()
        {
            OrderFlightList = new ObservableCollection<PHIEUDATCHO>(DataProvider.Ins.DB.PHIEUDATCHOes);
            ClassFlightList = new ObservableCollection<HANGVE>(DataProvider.Ins.DB.HANGVEs);
            FlightIDList = new ObservableCollection<CHUYENBAY>(DataProvider.Ins.DB.CHUYENBAYs);
            SeatIDList = new ObservableCollection<DANHSACHGHECUAMAYBAY>(DataProvider.Ins.DB.DANHSACHGHECUAMAYBAYs);
            StatusList = new ObservableCollection<string>
            {
                "Đã đặt",
            };
            CustomerList = new ObservableCollection<HANHKHACH>(DataProvider.Ins.DB.HANHKHACHes);

            ConfirmOrder = new RelayCommand<TicketOrder>((p) => true, (p) => _confirmOrder());
            CancelOrder = new RelayCommand<TicketOrder>((p) => true, (p) => _cancelOrder(p));
            ConfirmInfor = new RelayCommand<TicketOrder>((p) => true, (p) => _confirmInfor());
            CancelInfor = new RelayCommand<TicketOrder>((p) => true, (p) => _cancelInfor(p));

            NgayDat = DateTime.Today;
            _pageModel = new PageModel();
            // Initialize FilteredFlightIDList
            FilteredFlightIDList = new ObservableCollection<CHUYENBAY>(_FlightIDList);
            FetchThoiGianDatVeChamNhat();
        }

        private void FetchThoiGianDatVeChamNhat()
        {
            try
            {
                var thoiGian = DataProvider.Ins.DB.THAMSOes.FirstOrDefault()?.ThoiGianDatVeChamNhat;

                if (thoiGian.HasValue)
                {
                    ThoiGianDatVeChamNhat = thoiGian.Value;
                    // Debugging output
                    Console.WriteLine($"Fetched ThoiGianDatVeChamNhat: {ThoiGianDatVeChamNhat}");
                }
                else
                {
                    // Handle the case where the value is not present in the database
                    ThoiGianDatVeChamNhat = 1;
                    Console.WriteLine("ThoiGianDatVeChamNhat value is null in the database. Set to default: 0");
                }
            }
            catch (Exception ex)
            {
                // Handle potential errors
                MessageBox.Show($"Error fetching ThoiGianDatVeChamNhat: {ex.Message}");
                ThoiGianDatVeChamNhat = 1; // Default value in case of error
            }
        }

        private void _cancelInfor(TicketOrder parameter)
        {
            parameter.CustomerIDInput.Clear();
            parameter.NameInput.Clear();
            parameter.IDInput.Clear();
            parameter.PhoneInput.Clear();
            parameter.EmailInput.Clear();
            CustomerList.Clear();
        }

        private void _confirmInfor()
        {
            if (string.IsNullOrEmpty(MaHanhKhach) || string.IsNullOrEmpty(HoTen) ||
                string.IsNullOrEmpty(CCCD) || string.IsNullOrEmpty(DienThoai) || string.IsNullOrEmpty(Email))
            {
                MessageBox.Show("Bạn nhập thiếu thông tin!!", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Kiểm tra xem mã đặt chỗ đã tồn tại hay chưa
            var existingCus = DataProvider.Ins.DB.HANHKHACHes.FirstOrDefault(h => h.MaHanhKhach == MaHanhKhach);
            if (existingCus != null)
            {
                MessageBox.Show("Mã hành khách đã tồn tại", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBoxResult addFliNoti = MessageBox.Show("Bạn muốn thêm hành khách?", "Notification", MessageBoxButton.YesNo);
            if (addFliNoti == MessageBoxResult.Yes)
            {
                var hanhkhach = new HANHKHACH()
                {
                    MaHanhKhach = MaHanhKhach,
                    HoTen = HoTen,
                    CCCD = CCCD,
                    DienThoai = DienThoai,
                    Email = Email
                };

                DataProvider.Ins.DB.HANHKHACHes.Add(hanhkhach);
                DataProvider.Ins.DB.SaveChanges();

                CustomerList.Add(hanhkhach);
                MessageBox.Show("Thông tin hành khách đã được thêm thành công!");
            }
        }

        private void _cancelOrder(TicketOrder parameter)
        {
            parameter.OrderID.Clear();
            //parameter.DateOrderPicker.SelectedDate = null;
            parameter.CustomerID.Clear();
            parameter.ClassFlight.SelectedItem = null;
            parameter.FlightID.SelectedItem = null;
            parameter.SeatID.SelectedItem = null;
            parameter.StatusSeat.SelectedItem = null;
            OrderFlightList.Clear();
        }

        private void _confirmOrder()
        {

            if (string.IsNullOrEmpty(SoPhieuDatCho) || NgayDat == null ||
                string.IsNullOrEmpty(MaHanhKhach) || SelectedMAHANGVE == null ||
                SelectedMACHUYENBAY == null || SelectedMAGHE == null ||
                SelectedTINHTRANG == null)
            {
                MessageBox.Show("Bạn nhập thiếu thông tin!!", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Kiểm tra xem mã đặt chỗ đã tồn tại hay chưa
            var existingOrderSeat = DataProvider.Ins.DB.PHIEUDATCHOes.FirstOrDefault(h => h.SoPhieuDatCho == SoPhieuDatCho);
            if (existingOrderSeat != null)
            {
                MessageBox.Show("Số phiếu đặt chỗ đã tồn tại!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            

            // Kiểm tra số ghế còn lại của hạng vé và chuyến bay đã chọn
            var chiTietHangVe = DataProvider.Ins.DB.CHITIETHANGVEs
                           .FirstOrDefault(c => c.MaHangVe == SelectedMAHANGVE.MaHangVe &&
                                                c.MaChuyenBay == SelectedMACHUYENBAY.MaChuyenBay);
            if (chiTietHangVe == null || chiTietHangVe.SoGheConLai <= 0)
            {
                MessageBox.Show("Hết vé cho hạng vé!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Kiểm tra ngày đặt vé chậm nhất 1 ngày trước ngày bay
            if (NgayDat >= SelectedMACHUYENBAY.NgayBay)
            {
                MessageBox.Show("Bạn chỉ có thể đặt vé chậm nhất 1 ngày trước ngày bay!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Kiểm tra xem mã đặt chỗ đã tồn tại hay chưa
            var existingCus = DataProvider.Ins.DB.HANHKHACHes.FirstOrDefault(h => h.MaHanhKhach == MaHanhKhach);
            if (existingCus != null)
            {
                
            }
            

            // Kiểm tra xem mã khách hàng đã tồn tại hay chưa
            var existingCustomer = DataProvider.Ins.DB.HANHKHACHes.FirstOrDefault(h => h.MaHanhKhach == MaHanhKhach);

            if (existingCustomer != null)
            {
                MessageBoxResult addFliNoti = MessageBox.Show("Bạn muốn thêm phiếu đặt chỗ?", "Notification", MessageBoxButton.YesNo);
                if (addFliNoti == MessageBoxResult.Yes)
                {
                    var phieudatcho = new PHIEUDATCHO()
                    {
                        SoPhieuDatCho = SoPhieuDatCho,
                        NgayDat = NgayDat,
                        MaHanhKhach = MaHanhKhach,
                        MaHangVe = SelectedMAHANGVE.MaHangVe,
                        MaChuyenBay = SelectedMACHUYENBAY.MaChuyenBay,
                        MaGhe = SelectedMAGHE.MaGhe,
                        TinhTrang = SelectedTINHTRANG
                    };

                    DataProvider.Ins.DB.PHIEUDATCHOes.Add(phieudatcho);
                    DataProvider.Ins.DB.SaveChanges();

                    OrderFlightList.Add(phieudatcho);
                    MessageBox.Show("Phiêu đặt chỗ đã được thêm thành công!");
                }
            }
            else
            {
                MessageBox.Show("Mã khách hàng không tồn tại!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FilterFlightsByDate()
        {
            try
            {
                if (FlightIDList == null || !FlightIDList.Any())
                {
                    MessageBox.Show("Flight list is empty or null.");
                    return;
                }

                // Debugging output
                Console.WriteLine($"Filtering flights with ThoiGianDatVeChamNhat: {ThoiGianDatVeChamNhat}");

                var filteredFlights = FlightIDList
                    .Where(f => (f.NgayBay - NgayDat).TotalHours > ThoiGianDatVeChamNhat)
                    .ToList();

                FilteredFlightIDList = new ObservableCollection<CHUYENBAY>(filteredFlights);

                // Debugging output
                Console.WriteLine($"Number of filtered flights: {FilteredFlightIDList.Count}");
            }
            catch (Exception ex)
            {
                // Handle potential errors
                MessageBox.Show($"Error filtering flights by date: {ex.Message}");
            }
        }

    }
}
