using FlightTicketManagement.Model;
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

namespace FlightTicketManagement.ViewModel
{
    class TicketOrderVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public ICommand ConfirmOrder {  get; set; }
        public ICommand CancelOrder {  get; set; }

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

        private ObservableCollection<string> _Status;
        public ObservableCollection<string> Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
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

        private string _SoPhieuDatCho;
        public string SoPhieuDatCho { get => _SoPhieuDatCho; set { _SoPhieuDatCho = value; OnPropertyChanged(); } }

        private DateTime _NgayDat;
        public DateTime NgayDat { get => _NgayDat; set { _NgayDat = value; OnPropertyChanged(); } }

        private string _MaHanhKhach;
        public string MaHanhKhach { get => _MaHanhKhach; set { _MaHanhKhach = value; OnPropertyChanged(); } }

        private string _MaChuyenBay;
        public string MaChuyenBay { get => _MaChuyenBay; set { _MaChuyenBay = value; OnPropertyChanged(); } }

        private string _MaGhe;
        public string MaGhe { get => _MaGhe; set { _MaGhe = value; OnPropertyChanged(); } }

        private string _TinhTrang;
        public string TinhTrang { get => _TinhTrang; set { _TinhTrang = value; OnPropertyChanged(); } }

        public int OrderID 
        {
            get { return _pageModel.TicketOrder; }
            set {  _pageModel.TicketOrder = value; OnPropertyChanged(); }
        }
        public TicketOrderVM()
        {
            OrderFlightList = new ObservableCollection<PHIEUDATCHO>(DataProvider.Ins.DB.PHIEUDATCHOes);
            ClassFlightList = new ObservableCollection<HANGVE>(DataProvider.Ins.DB.HANGVEs);
            FlightIDList = new ObservableCollection<CHUYENBAY>(DataProvider.Ins.DB.CHUYENBAYs);
            SeatIDList = new ObservableCollection<DANHSACHGHECUAMAYBAY>(DataProvider.Ins.DB.DANHSACHGHECUAMAYBAYs);
            Status = new ObservableCollection<string>
            {
                //Lấy tình trạng ghế
            };

            ConfirmOrder = new RelayCommand<TicketOrder>((p) => true, (p) => _confirmOrder());
            CancelOrder = new RelayCommand<TicketOrder>((p) => true, (p) => _cancelOrder());
            ConfirmInfor = new RelayCommand<TicketOrder>((p) => true, (p) => _confirmInfor());
            CancelInfor = new RelayCommand<TicketOrder>((p) => true, (p) => _cancelInfor());

            _pageModel = new PageModel();
        }

        private void _cancelInfor()
        {
            
        }

        private void _confirmInfor()
        {
            
        }

        private void _cancelOrder()
        {
            MessageBox.Show("Canceled");
        }

        private void _confirmOrder()
        {
            //if (string.IsNullOrEmpty(MaChuyenBay) || SelectedTUYENBAY == null ||
            //    SelectedSANBAYDI == null ||
            //    SelectedSANBAYDEN == null || SelectedMAYBAY == null ||
            //    ThoiLuong == 0 || DonGia == 0)

            //{
            //    MessageBox.Show("Có vẻ bạn thiếu thông tin!!", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            MessageBoxResult addFliNoti = MessageBox.Show("Bạn muốn thêm chuyến bay?", "Notification", MessageBoxButton.YesNo);
            if (addFliNoti == MessageBoxResult.Yes)
            {
                var phieudatcho = new PHIEUDATCHO()
                {
                    SoPhieuDatCho = SoPhieuDatCho,
                    NgayDat = NgayDat,
                    MaHangVe = SelectedMAHANGVE.MaHangVe,
                    MaChuyenBay = SelectedMACHUYENBAY.MaChuyenBay,
                    MaGhe = SelectedMAGHE.MaGhe,
                    TinhTrang = TinhTrang
                };

                DataProvider.Ins.DB.PHIEUDATCHOes.Add(phieudatcho);
                DataProvider.Ins.DB.SaveChanges();

                OrderFlightList.Add(phieudatcho);
                MessageBox.Show("Chuyến bay đã được thêm thành công!");
            }
        }
    }
}
