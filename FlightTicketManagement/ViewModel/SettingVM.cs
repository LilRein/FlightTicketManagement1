using FlightTicketManagement.Model;
using FlightTicketManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace FlightTicketManagement.ViewModel
{
    class SettingVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        private ObservableCollection<HANGVE> _HangVeList;
        public ObservableCollection<HANGVE> HangVeList
        {
            get { return _HangVeList; }
            set
            {
                _HangVeList = value;
                OnPropertyChanged(nameof(HangVeList));
            }
        }

        private HANGVE _SelectedItem;
        public HANGVE SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    MaHangVe = SelectedItem.MaHangVe;
                    TenHangVe = SelectedItem.TenHangVe;
                    TiLeTinhDonGia = SelectedItem.TiLeTinhDonGia;
                }
            }
        }

        private string _MaHangVe;
        public string MaHangVe { get => _MaHangVe; set { _MaHangVe = value; OnPropertyChanged(); } }


        private string _TenHangVe;
        public string TenHangVe { get => _TenHangVe; set { _TenHangVe = value; OnPropertyChanged(); } }


        private double _TiLeTinhDonGia;
        public double TiLeTinhDonGia { get => _TiLeTinhDonGia; set { _TiLeTinhDonGia = value; OnPropertyChanged(); } }

        private ObservableCollection<SANBAY> _SayBayList;
        public ObservableCollection<SANBAY> SayBayList
        {
            get { return _SayBayList; }
            set
            {
                _SayBayList = value;
                OnPropertyChanged(nameof(SayBayList));
            }
        }

        private SANBAY _SelectedSanBay;
        public SANBAY SelectedSanBay
        {
            get => _SelectedSanBay;
            set
            {
                _SelectedSanBay = value;
                OnPropertyChanged();
                if (SelectedSanBay != null)
                {
                    MaSanBay = SelectedSanBay.MaSanBay;
                    TenSanBay = SelectedSanBay.TenSanBay;
                    DiaChi = SelectedSanBay.DiaChi;
                    MaQuocGia = SelectedSanBay.MaQuocGia;
                }
            }
        }

        private string _MaSanBay;
        public string MaSanBay { get => _MaSanBay; set { _MaSanBay = value; OnPropertyChanged(); } }

        private string _TenSanBay;
        public string TenSanBay { get => _TenSanBay; set { _TenSanBay = value; OnPropertyChanged(); } }

        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }

        private string _MaQuocGia;
        public string MaQuocGia { get => _MaQuocGia; set { _MaQuocGia = value; OnPropertyChanged(); } }

        private ObservableCollection<THAMSO> _ThamSoList;
        public ObservableCollection<THAMSO> ThamSoList
        {
            get { return _ThamSoList; }
            set
            {
                _ThamSoList = value;
                OnPropertyChanged(nameof(ThamSoList));
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public ICommand AddCommandFlight { get; set; }
        public ICommand EditCommandFlight { get; set; }

        public ICommand EditCommandThamSo { get; set; }

        public SettingVM()
        {
            _pageModel = new PageModel();

            HangVeList = new ObservableCollection<HANGVE>(DataProvider.Ins.DB.HANGVEs);

            SayBayList = new ObservableCollection<SANBAY>(DataProvider.Ins.DB.SANBAYs);

            ThamSoList = new ObservableCollection<THAMSO>(DataProvider.Ins.DB.THAMSOes);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(MaHangVe))
                    return false;

                var displayList = DataProvider.Ins.DB.HANGVEs.Where(x => x.MaHangVe == MaHangVe);
                if (displayList == null || displayList.Count() != 0)
                    return false;

                return true;

            }, (p) =>
            {
                var hangve = new HANGVE() { MaHangVe = MaHangVe, TenHangVe = TenHangVe, TiLeTinhDonGia = TiLeTinhDonGia };

                DataProvider.Ins.DB.HANGVEs.Add(hangve);
                DataProvider.Ins.DB.SaveChanges();

                HangVeList.Add(hangve);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                var displayList = DataProvider.Ins.DB.HANGVEs.Where(x => x.MaHangVe == SelectedItem.MaHangVe);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                return false;

            }, (p) =>
            {
                var hangve = DataProvider.Ins.DB.HANGVEs.Where(x => x.MaHangVe == SelectedItem.MaHangVe).SingleOrDefault();
                hangve.TenHangVe = TenHangVe;
                hangve.TiLeTinhDonGia = TiLeTinhDonGia;
                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.MaHangVe = MaHangVe;
            });

            AddCommandFlight = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(MaSanBay))
                    return false;

                var displayList = DataProvider.Ins.DB.SANBAYs.Where(x => x.MaSanBay == MaSanBay);
                if (displayList == null || displayList.Count() != 0)
                    return false;

                return true;

            }, (p) =>
            {
                var sanbay = new SANBAY() { MaSanBay = MaSanBay, TenSanBay = TenSanBay, DiaChi = DiaChi, MaQuocGia = MaQuocGia };

                DataProvider.Ins.DB.SANBAYs.Add(sanbay);
                DataProvider.Ins.DB.SaveChanges();

                SayBayList.Add(sanbay);
            });

            EditCommandFlight = new RelayCommand<object>((p) =>
            {
                if (SelectedSanBay == null)
                    return false;

                var displayList = DataProvider.Ins.DB.SANBAYs.Where(x => x.MaSanBay == SelectedSanBay.MaSanBay);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                return false;

            }, (p) =>
            {
                var sanbay = DataProvider.Ins.DB.SANBAYs.Where(x => x.MaSanBay == SelectedSanBay.MaSanBay).SingleOrDefault();
                sanbay.TenSanBay = TenSanBay;
                sanbay.DiaChi = DiaChi;
                DataProvider.Ins.DB.SaveChanges();

                SelectedSanBay.MaSanBay = MaSanBay;
            });

            //EditCommandThamSo = new RelayCommand<object>((p) =>
            //{
            //    if (SelectedSanBay == null)
            //        return false;

            //    var displayList = DataProvider.Ins.DB.SANBAYs.Where(x => x.MaSanBay == SelectedSanBay.MaSanBay);
            //    if (displayList != null && displayList.Count() != 0)
            //        return true;

            //    return false;

            //}, (p) =>
            //{
            //    var thamso = DataProvider.Ins.DB.THAMSOes.Where();
            //    thamso.SoSanBayTrungGianToiDa = SoSanBayTrungGianToiDa;
            //    DataProvider.Ins.DB.SaveChanges();

            //    SelectedSanBay.MaSanBay = MaSanBay;
            //});
        }
    }
}