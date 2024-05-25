using FlightTicketManagement.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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
                OnPropertyChanged(nameof(TUYENBAY));
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

        private DateTime _NgayBay;
        public DateTime NgayBay { get => _NgayBay; set { _NgayBay = value; OnPropertyChanged(); } }

        private TimeSpan _GioKhoiHanh;
        public TimeSpan GioKhoiHanh { get => _GioKhoiHanh; set { _GioKhoiHanh = value; OnPropertyChanged(); } }

        private double _ThoiLuong;
        public double ThoiLuong { get => _ThoiLuong; set { _ThoiLuong = value; OnPropertyChanged(); } }

        private decimal _DonGia;
        public decimal DonGia { get => _DonGia; set { _DonGia = value; OnPropertyChanged(); } }

        private ObservableCollection<CHITIETHANGVE> _hangVeList;
        public ObservableCollection<CHITIETHANGVE> HangVeList
        {
            get => _hangVeList;
            set
            {
                _hangVeList = value;
                OnPropertyChanged(nameof(HangVeList));
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand CloseAACM { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand EditCommand { get; set; }

        private bool _isEditMode;
        public bool IsEditMode
        {
            get => _isEditMode;
            set
            {
                _isEditMode = value;
                OnPropertyChanged(nameof(IsEditMode));
            }
        }

        public AddFlightVM()
        {
            NgayBay = DateTime.Today;

            // Initialize collections
            CHUYENBAYList = new ObservableCollection<CHUYENBAY>(DataProvider.Ins.DB.CHUYENBAYs);
            SANBAY = new ObservableCollection<SANBAY>(DataProvider.Ins.DB.SANBAYs);
            TUYENBAY = new ObservableCollection<TUYENBAY>(DataProvider.Ins.DB.TUYENBAYs);
            MAYBAY = new ObservableCollection<MAYBAY>(DataProvider.Ins.DB.MAYBAYs);
            HangVeList = new ObservableCollection<CHITIETHANGVE>();

            // Initialize commands
            CloseAACM = new RelayCommand<AddFlight>((p) => true, (p) => _CloseAACM(p));
            ConfirmCommand = new RelayCommand<AddFlight>((p) => !IsEditMode, (p) => _ConfirmCommand(p));
            EditCommand = new RelayCommand<AddFlight>((p) => IsEditMode, (p) => _EditCommand(p));
            CancelCommand = new RelayCommand<AddFlight>((p) => true, (p) => _CancelCommand(p));

            // Khởi tạo danh sách các hạng vé cho trường hợp thêm mới chuyến bay
            InitializeHangVeList();
        }

        private void InitializeHangVeList()
        {
            // Khởi tạo danh sách các hạng vé (dữ liệu mẫu hoặc từ database)
            HangVeList.Clear();
            var hangVeTypes = DataProvider.Ins.DB.HANGVEs.ToList(); // Giả sử có bảng HANGVE lưu các loại hạng vé
            foreach (var hangVe in hangVeTypes)
            {
                HangVeList.Add(new CHITIETHANGVE
                {
                    MaHangVe = hangVe.MaHangVe,
                    SoGheChoHangVe = 0 // Khởi tạo số lượng ghế là 0 hoặc giá trị mặc định khác
                });
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
            // Validate input fields
            if (string.IsNullOrEmpty(MaChuyenBay) || SelectedTUYENBAY == null ||
                SelectedSANBAYDI == null ||
                SelectedSANBAYDEN == null || SelectedMAYBAY == null ||
                ThoiLuong == 0 || DonGia == 0 || !HangVeList.Any())
            {
                MessageBox.Show("Bạn nhập thiếu thông tin!!", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
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

                // Add seat class details
                foreach (var hangVe in HangVeList)
                {
                    hangVe.MaChuyenBay = MaChuyenBay;
                    var existingHangVe = DataProvider.Ins.DB.CHITIETHANGVEs
                        .FirstOrDefault(x => x.MaChuyenBay == hangVe.MaChuyenBay && x.MaHangVe == hangVe.MaHangVe);

                    if (existingHangVe == null)
                    {
                        DataProvider.Ins.DB.CHITIETHANGVEs.Add(hangVe);
                    }
                    else
                    {
                        existingHangVe.SoGheChoHangVe = hangVe.SoGheChoHangVe;
                        DataProvider.Ins.DB.Entry(existingHangVe).State = System.Data.Entity.EntityState.Modified;
                    }
                }

                DataProvider.Ins.DB.SaveChanges();

                CHUYENBAYList.Add(chuyenbay);
                MessageBox.Show("Chuyến bay đã được thêm thành công!");

                // Đóng cửa sổ sau khi thêm thành công
                var window = Window.GetWindow(parameter);
                if (window != null)
                {
                    window.DialogResult = true;
                    window.Close();
                }
            }
        }

        private void _EditCommand(AddFlight parameter)
        {
            // Validate input fields
            if (string.IsNullOrEmpty(MaChuyenBay) || SelectedTUYENBAY == null ||
                SelectedSANBAYDI == null ||
                SelectedSANBAYDEN == null || SelectedMAYBAY == null ||
                ThoiLuong == 0 || DonGia == 0 || !HangVeList.Any())
            {
                MessageBox.Show("Bạn nhập thiếu thông tin!!", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var chuyenbay = DataProvider.Ins.DB.CHUYENBAYs.FirstOrDefault(x => x.MaChuyenBay == MaChuyenBay);
            if (chuyenbay != null)
            {
                chuyenbay.MaTuyenBay = SelectedTUYENBAY.MaTuyenBay;
                chuyenbay.MaSanBayDi = SelectedSANBAYDI.MaSanBay;
                chuyenbay.MaSanBayDen = SelectedSANBAYDEN.MaSanBay;
                chuyenbay.MaMayBay = SelectedMAYBAY.MaMayBay;
                chuyenbay.NgayBay = NgayBay;
                chuyenbay.GioKhoiHanh = GioKhoiHanh;
                chuyenbay.ThoiLuong = ThoiLuong;
                chuyenbay.DonGia = DonGia;

                // Update seat class details
                var existingHangVeList = DataProvider.Ins.DB.CHITIETHANGVEs.Where(x => x.MaChuyenBay == MaChuyenBay).ToList();
                foreach (var hangVe in HangVeList)
                {
                    var existingHangVe = existingHangVeList.FirstOrDefault(x => x.MaHangVe == hangVe.MaHangVe);
                    if (existingHangVe != null)
                    {
                        existingHangVe.SoGheChoHangVe = hangVe.SoGheChoHangVe;
                        DataProvider.Ins.DB.Entry(existingHangVe).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        hangVe.MaChuyenBay = MaChuyenBay;
                        DataProvider.Ins.DB.CHITIETHANGVEs.Add(hangVe);
                    }
                }

                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Chuyến bay đã được cập nhật thành công!");

                // Đóng cửa sổ sau khi chỉnh sửa thành công
                var window = Window.GetWindow(parameter);
                if (window != null)
                {
                    window.DialogResult = true;
                    window.Close();
                }
            }
        }

        public void LoadFlightData(CHUYENBAY flight)
        {
            MaChuyenBay = flight.MaChuyenBay;
            SelectedTUYENBAY = TUYENBAY.FirstOrDefault(x => x.MaTuyenBay == flight.MaTuyenBay);
            SelectedSANBAYDI = SANBAY.FirstOrDefault(x => x.MaSanBay == flight.MaSanBayDi);
            SelectedSANBAYDEN = SANBAY.FirstOrDefault(x => x.MaSanBay == flight.MaSanBayDen);
            SelectedMAYBAY = MAYBAY.FirstOrDefault(x => x.MaMayBay == flight.MaMayBay);
            NgayBay = flight.NgayBay;
            GioKhoiHanh = flight.GioKhoiHanh;
            ThoiLuong = flight.ThoiLuong;
            DonGia = flight.DonGia;

            // Load seat class details
            HangVeList = new ObservableCollection<CHITIETHANGVE>(DataProvider.Ins.DB.CHITIETHANGVEs.Where(x => x.MaChuyenBay == MaChuyenBay));

            // Đặt chế độ chỉnh sửa
            IsEditMode = true;
        }

        private void _CancelCommand(AddFlight parameter)
        {
            parameter.FlightID.Clear();
            parameter.StartListAirport.SelectedItem = null;
            parameter.EndListAirport.SelectedItem = null;
            parameter.PlaneID.SelectedItem = null;
            parameter.Duration.Clear();
            parameter.Price.Clear();
            parameter.FlightDatePicker.SelectedDate = null;
            parameter.FlightTimePicker.Clear();
            parameter.RouteID.SelectedItem = null;
            HangVeList.Clear();
        }
    }
}
