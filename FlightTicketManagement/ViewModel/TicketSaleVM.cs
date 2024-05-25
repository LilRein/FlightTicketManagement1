using FlightTicketManagement.Model;
using FlightTicketManagement.Utilities;
using FlightTicketManagement.View;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
        public ICommand SearchBtn { get; set; }
        public ICommand CancelGridBtn { get; set; }


        private ObservableCollection<PHIEUDATCHO> _FilteredOrderList;
        public ObservableCollection<PHIEUDATCHO> FilteredOrderList
        {
            get { return _FilteredOrderList; }
            set
            {
                _FilteredOrderList = value;
                OnPropertyChanged();
            }
        }



        private ObservableCollection<PHIEUDATCHO> _OrderList;
        public ObservableCollection<PHIEUDATCHO> OrderList
        {
            get { return _OrderList; }
            set
            {
                _OrderList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<VECHUYENBAY> _TicketList;
        public ObservableCollection<VECHUYENBAY> TicketList
        {
            get { return _TicketList; }
            set
            {
                _TicketList = value;
                OnPropertyChanged();
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



        private string _FilterCusID;
        public string FilterCusID
        {
            get => _FilterCusID;
            set
            {
                _FilterCusID = value;
                OnPropertyChanged();
                _searchCusID();
            }
        }

        public int FlightSale
        {
            get { return _pageModel.TicketSale; }
            set { _pageModel.TicketSale = value; OnPropertyChanged(); }
        }
        public TicketSaleVM()
        {
            NgayXuatVe = DateTime.Today;


            OrderList = new ObservableCollection<PHIEUDATCHO>(DataProvider.Ins.DB.PHIEUDATCHOes);

            ExportBtn = new RelayCommand<TicketSale>((p) => true, (p) => _exportBtn(p));
            CancelBtn = new RelayCommand<TicketSale>((p) => true, (p) => _cancelBtn(p));
            SearchBtn = new RelayCommand<TicketSale>((p) => true, (p) => _searchCusID());
            CancelGridBtn = new RelayCommand<TicketSale>((p) => true, (p) => _cancelGridBtn());
            _pageModel = new PageModel();
        }

        private void _cancelGridBtn()
        {
            if (SelectedOrder == null)
            {
                MessageBox.Show("Vui lòng chọn một phiếu đặt chỗ để huỷ", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult cancelConfirm = MessageBox.Show("Bạn có chắc chắn muốn huỷ phiếu đặt chỗ này?", "Notification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (cancelConfirm == MessageBoxResult.Yes)
            {
                // Update the status of the selected order to "Đã huỷ"
                SelectedOrder.TinhTrang = "Đã huỷ";

                // Save changes to the database
                try
                {
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Phiếu đặt chỗ đã được huỷ thành công!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // Refresh the OrderList to reflect changes
                OrderList = new ObservableCollection<PHIEUDATCHO>(DataProvider.Ins.DB.PHIEUDATCHOes);
            }
        }

        private void _searchCusID()
        {
            if (!string.IsNullOrEmpty(FilterCusID))
            {
                string filter = FilterCusID;
                var filteredList = DataProvider.Ins.DB.PHIEUDATCHOes.Where(p => p.MaHanhKhach.Contains(filter)).ToList();
                OrderList = new ObservableCollection<PHIEUDATCHO>(filteredList);

            }
            else
            {
                OrderList = new ObservableCollection<PHIEUDATCHO>(DataProvider.Ins.DB.PHIEUDATCHOes);

            }
        }

        private void _exportBtn(TicketSale parameter)
        {
            if (string.IsNullOrEmpty(MaVe) || NgayXuatVe == null ||
                string.IsNullOrEmpty(MaHanhKhach) || string.IsNullOrEmpty(MaHangVe) ||
                string.IsNullOrEmpty(MaChuyenBay) || string.IsNullOrEmpty(MaGhe))
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
                    MaChuyenBay = MaChuyenBay,
                    MaHangVe = MaHangVe,
                    MaHanhKhach = MaHanhKhach,
                    MaGhe = MaGhe,
                };

                DataProvider.Ins.DB.VECHUYENBAYs.Add(vechuyenbay);
                DataProvider.Ins.DB.SaveChanges();

                //TicketList.Add(vechuyenbay);
                MessageBox.Show("Vé chuyến bay đã được xuất thành công!");
            }
        }

        private void _cancelBtn(TicketSale parameter)
        {
            parameter.TicketID.Clear();
            parameter.FlightID.Clear();
            parameter.ClassFlightID.Clear();
            parameter.CustomerID.Clear();
            parameter.SeatID.Clear();
            //TicketList.Clear();
        }

    }
}
