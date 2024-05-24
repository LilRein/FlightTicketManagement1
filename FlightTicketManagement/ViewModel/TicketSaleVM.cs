using FlightTicketManagement.Model;
using FlightTicketManagement.Utilities;
using FlightTicketManagement.View;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Forms;
using System.Windows.Input;

namespace FlightTicketManagement.ViewModel
{
    class TicketSaleVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public ICommand ExportBtn { get; set; }
        public ICommand CancelBtn { get; set; }



        private ObservableCollection<PHIEUDATCHO> _OrderList;
        public ObservableCollection<PHIEUDATCHO> OrderList
        {
            get { return _OrderList; }
            set
            {
                _OrderList = value;
                OnPropertyChanged(nameof(OrderList));
            }
        }

        private ObservableCollection<VECHUYENBAY> _TicketList;
        public ObservableCollection<VECHUYENBAY> TicketList
        {
            get { return _TicketList; }
            set
            {
                _TicketList = value;
                OnPropertyChanged(nameof(TicketList));
            }
        }

        private PHIEUDATCHO _SelectedOrder;
        public PHIEUDATCHO SelectedOrder
        {
            get => _SelectedOrder;
            set
            {
                _SelectedOrder = value;
                OnPropertyChanged();
                if (SelectedOrder != null)
                {
                    MaHanhKhach = SelectedOrder.MaHanhKhach;
                    MaHangVe = SelectedOrder.MaHangVe;
                    MaChuyenBay = SelectedOrder.MaChuyenBay;
                    MaGhe = SelectedOrder.MaGhe;
                }
            }
        }

        private string _MaHanhKhach;
        public string MaHanhKhach { get => _MaHanhKhach; set { _MaHanhKhach = value; OnPropertyChanged(); } }

        private string _MaHangVe;
        public string MaHangVe { get => _MaHangVe; set { _MaHangVe = value; OnPropertyChanged(); } }

        private string _MaChuyenBay;
        public string MaChuyenBay { get => _MaChuyenBay; set { _MaChuyenBay = value; OnPropertyChanged(); } }

        private string _MaGhe;
        public string MaGhe { get => _MaGhe; set { _MaGhe = value; OnPropertyChanged(); } }

        private string _MaVe;
        public string MaVe { get => _MaVe; set { _MaVe = value; OnPropertyChanged(); } }

        private DateTime _NgayXuatVe;
        public DateTime NgayXuatVe { get => _NgayXuatVe; set { _NgayXuatVe = value; OnPropertyChanged(); } }
        public int FlightSale
        {
            get { return _pageModel.TicketSale; }
            set { _pageModel.TicketSale = value; OnPropertyChanged(); }
        }
        public TicketSaleVM()
        {
            OrderList = new ObservableCollection<PHIEUDATCHO>(DataProvider.Ins.DB.PHIEUDATCHOes);


            ExportBtn = new RelayCommand<TicketSale>((p) => true, (p) => _exportBtn(p));
            CancelBtn = new RelayCommand<TicketSale>((p) => true, (p) => _cancelBtn(p));
            _pageModel = new PageModel();
        }

        private void _exportBtn(TicketSale parameter)
        {
            if (string.IsNullOrEmpty(MaVe) || NgayXuatVe == null ||
                string.IsNullOrEmpty(MaHanhKhach) || string.IsNullOrEmpty(MaHangVe) ||
                string.IsNullOrEmpty(MaChuyenBay) || string.IsNullOrEmpty(MaGhe) )
            {
                MessageBox.Show("Bạn nhập thiếu thông tin!!", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBoxResult addFliNoti = MessageBox.Show("Bạn muốn xuất vé?", "Notification", MessageBoxButton.YesNo);
            if (addFliNoti == MessageBoxResult.Yes)
            {
                var vechuyenbay = new VECHUYENBAY()
                {
                    MaVe = MaVe,
                    NgayXuatVe = NgayXuatVe,
                    MaHanhKhach =MaHanhKhach,
                    MaHangVe = MaHangVe,
                    MaChuyenBay = MaChuyenBay,
                    MaGhe = MaGhe,    
                };

                DataProvider.Ins.DB.VECHUYENBAYs.Add(vechuyenbay);
                DataProvider.Ins.DB.SaveChanges();

                TicketList.Add(vechuyenbay);
                MessageBox.Show("Chuyến bay đã được thêm thành công!");
            }
        }

        private void _cancelBtn(TicketSale parameter)
        {
            MessageBox.Show("Huỷ bỏ");
        }

    }
}
