using FlightTicketManagement.Model;
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

namespace FlightTicketManagement.ViewModel
{
    class AddMidFlightVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
        private readonly SchedulesVM _schedulesVM;

        public ICommand CloseAMACM { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private ObservableCollection<CTSANBAYTRUNGGIAN> _SANBAYTRUNGGIANList;
        public ObservableCollection<CTSANBAYTRUNGGIAN> SANBAYTRUNGGIANList
        {
            get { return _SANBAYTRUNGGIANList; }
            set
            {
                _SANBAYTRUNGGIANList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CHUYENBAY> _CHUYENBAY;
        public ObservableCollection<CHUYENBAY> CHUYENBAY
        {
            get { return _CHUYENBAY; }
            set
            {
                _CHUYENBAY = value;
                OnPropertyChanged(nameof(CHUYENBAY));
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


        private SANBAY _SelectedSANBAY;
        public SANBAY SelectedSANBAY
        {
            get => _SelectedSANBAY;
            set
            {
                _SelectedSANBAY = value;
                OnPropertyChanged();
            }
        }

        private CHUYENBAY _SelectedCHUYENBAY;
        public CHUYENBAY SelectedCHUYENBAY
        {
            get => _SelectedCHUYENBAY;
            set
            {
                _SelectedCHUYENBAY = value;
                OnPropertyChanged();
            }
        }

        private string _MaChuyenBay;
        public string MaChuyenBay { get => _MaChuyenBay; set { _MaChuyenBay = value; OnPropertyChanged(); } }

        private string _MaSanBayTrungGian;
        public string MaSanBayTrungGian { get => _MaSanBayTrungGian; set { _MaSanBayTrungGian = value; OnPropertyChanged(); } }

        private int _ThoiGianDung;
        public int ThoiGianDung { get => _ThoiGianDung; set { _ThoiGianDung = value; OnPropertyChanged(); } }

        private string _GhiChu;
        public string GhiChu { get => _GhiChu; set { _GhiChu = value; OnPropertyChanged(); } }


        public AddMidFlightVM()
        {
            SANBAYTRUNGGIANList = new ObservableCollection<CTSANBAYTRUNGGIAN>(DataProvider.Ins.DB.CTSANBAYTRUNGGIANs);
            CHUYENBAY = new ObservableCollection<CHUYENBAY>(DataProvider.Ins.DB.CHUYENBAYs);
            SANBAY = new ObservableCollection<SANBAY>(DataProvider.Ins.DB.SANBAYs);

            CloseAMACM = new RelayCommand<AddMidFlight>((p) => true, (p) => _CloseAM(p));
            ConfirmCommand = new RelayCommand<AddMidFlight>((p) => true, (p) => _ConfirmCommand(p));
            //CancelCommand = new RelayCommand<AddMidFlight>((p) => true, (p) => _CancelCommand(p));

        }

        private void _CloseAM(AddMidFlight paramater)
        {
            var window = Window.GetWindow(paramater);
            if (window != null)
            {
                window.Close();
            }
        }

        //void _CancelCommand(AddMidFlight paramater)
        //{
        //    paramater.ListAirport.SelectedItem = null;
        //    paramater.WaitTime.Text = string.Empty;
        //    paramater.InputNote.Clear();
        //}
        void _ConfirmCommand(AddMidFlight paramater)
        {
            if (SelectedCHUYENBAY == null || SelectedSANBAY == null || ThoiGianDung == 0)
            {
                MessageBox.Show("Có vẻ bạn thiếu thông tin!!", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBoxResult addFliNoti = System.Windows.MessageBox.Show("Bạn muốn thêm sân bay trung gian ?", "Notification", MessageBoxButton.YesNo);
            if (addFliNoti == MessageBoxResult.Yes)
            {
                var sanbaytrunggian = new CTSANBAYTRUNGGIAN()
                {
                    MaChuyenBay = SelectedCHUYENBAY.MaChuyenBay,
                    MaSanBayTrungGian = SelectedSANBAY.MaSanBay,
                    ThoiGianDung = ThoiGianDung,
                    GhiChu = GhiChu

                };

                DataProvider.Ins.DB.CTSANBAYTRUNGGIANs.Add(sanbaytrunggian);
                DataProvider.Ins.DB.SaveChanges();

                SANBAYTRUNGGIANList.Add(sanbaytrunggian);
                MessageBox.Show("Sân bay trung gian đã được thêm thành công!");
            }
        }
    }
}
