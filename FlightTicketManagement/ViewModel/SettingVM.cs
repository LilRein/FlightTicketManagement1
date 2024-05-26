using FlightTicketManagement.Model;
using FlightTicketManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

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

        private ObservableCollection<THAMSO> _ThamSoList;
        public ObservableCollection<THAMSO> ThamSoList
        {
            get { return _ThamSoList; }
            set
            {
                _ThamSoList = value;
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

        private byte _SoSanBayTrungGianToiDa;
        public byte SoSanBayTrungGianToiDa
        {
            get => _SoSanBayTrungGianToiDa;
            set
            {
                if (_SoSanBayTrungGianToiDa != value)
                {
                    _SoSanBayTrungGianToiDa = value;
                    OnPropertyChanged(nameof(SoSanBayTrungGianToiDa));
                }
            }
        }

        private int _ThoiGianBayToiThieu;
        public int ThoiGianBayToiThieu
        {
            get => _ThoiGianBayToiThieu;
            set
            {
                if (_ThoiGianBayToiThieu != value)
                {
                    _ThoiGianBayToiThieu = value;
                    OnPropertyChanged(nameof(ThoiGianBayToiThieu));
                }
            }
        }

        private int _ThoiGianDungToiDa;
        public int ThoiGianDungToiDa
        {
            get => _ThoiGianDungToiDa;
            set
            {
                if (_ThoiGianDungToiDa != value)
                {
                    _ThoiGianDungToiDa = value;
                    OnPropertyChanged(nameof(ThoiGianDungToiDa));
                }
            }
        }

        private int _ThoiGianDungToiThieu;
        public int ThoiGianDungToiThieu
        {
            get => _ThoiGianDungToiThieu;
            set
            {
                if (_ThoiGianDungToiThieu != value)
                {
                    _ThoiGianDungToiThieu = value;
                    OnPropertyChanged(nameof(ThoiGianDungToiThieu));
                }
            }
        }

        private byte _ThoiGianDatVeChamNhat;
        public byte ThoiGianDatVeChamNhat
        {
            get => _ThoiGianDatVeChamNhat;
            set
            {
                if (_ThoiGianDatVeChamNhat != value)
                {
                    _ThoiGianDatVeChamNhat = value;
                    OnPropertyChanged(nameof(ThoiGianDatVeChamNhat));
                }
            }
        }

        private byte _ThoiGianHuyVeDatVe;
        public byte ThoiGianHuyVeDatVe
        {
            get => _ThoiGianHuyVeDatVe;
            set
            {
                if (_ThoiGianHuyVeDatVe != value)
                {
                    _ThoiGianHuyVeDatVe = value;
                    OnPropertyChanged(nameof(ThoiGianHuyVeDatVe));
                }
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommandFlight { get; set; }
        public ICommand EditCommandFlight { get; set; }
        public ICommand DeleteCommandFlight { get; set; }

        public ICommand SaveCommand { get; }
        public SettingVM()
        {
            _pageModel = new PageModel();

            HangVeList = new ObservableCollection<HANGVE>(DataProvider.Ins.DB.HANGVEs);
            SayBayList = new ObservableCollection<SANBAY>(DataProvider.Ins.DB.SANBAYs);

            var parameter = DataProvider.Ins.DB.THAMSOes.FirstOrDefault();
            if (parameter != null)
            {
                SoSanBayTrungGianToiDa = parameter.SoSanBayTrungGianToiDa;
                ThoiGianBayToiThieu = parameter.ThoiGianBayToiThieu;
                ThoiGianDungToiDa = parameter.ThoiGianDungToiDa;
                ThoiGianDungToiThieu = parameter.ThoiGianDungToiThieu;
                ThoiGianDatVeChamNhat = parameter.ThoiGianDatVeChamNhat;
                ThoiGianHuyVeDatVe = parameter.ThoiGianHuyVeDatVe;
            }

            // Lưu tham số
            SaveCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                var oldThamSo = DataProvider.Ins.DB.THAMSOes.SingleOrDefault();
                if (oldThamSo != null)
                {
                    // Tạo một thực thể mới với khóa chính mới
                    var newThamSo = new THAMSO
                    {
                        SoSanBayTrungGianToiDa = SoSanBayTrungGianToiDa, // Giá trị khóa chính mới
                        ThoiGianBayToiThieu = ThoiGianBayToiThieu,
                        ThoiGianDungToiDa = ThoiGianDungToiDa,
                        ThoiGianDungToiThieu = ThoiGianDungToiThieu,
                        ThoiGianDatVeChamNhat = ThoiGianDatVeChamNhat,
                        ThoiGianHuyVeDatVe = ThoiGianHuyVeDatVe
                    };

                    if (SoSanBayTrungGianToiDa >= 0 && ThoiGianBayToiThieu >= 0 && ThoiGianDungToiDa >= 0 && ThoiGianDungToiThieu >= 0 && ThoiGianDatVeChamNhat >= 0 && ThoiGianHuyVeDatVe >= 0)
                    {
                        // Thêm thực thể mới vào cơ sở dữ liệu
                        DataProvider.Ins.DB.THAMSOes.Add(newThamSo);

                        // Xóa thực thể cũ khỏi cơ sở dữ liệu
                        DataProvider.Ins.DB.THAMSOes.Remove(oldThamSo);

                        // Lưu các thay đổi
                        DataProvider.Ins.DB.SaveChanges();
                        MessageBox.Show("Thay đổi thành công", "Notification", MessageBoxButton.YesNo);
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra", "Error", MessageBoxButton.YesNo);
                    }


                }
            });

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

                MessageBox.Show("Thêm thành công", "Notification", MessageBoxButton.OK);
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

                HangVeList.Remove(SelectedItem);
                HangVeList.Add(hangve);
                SelectedItem = hangve;

                MessageBox.Show("Sửa thành công", "Notification", MessageBoxButton.OK);
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                return true;

            }, (p) =>
            {
                var hangve = DataProvider.Ins.DB.HANGVEs.Where(x => x.MaHangVe == SelectedItem.MaHangVe).SingleOrDefault();

                if (hangve != null)
                {
                    // Kiểm tra các liên kết trước khi xóa
                    bool hasRelatedData = DataProvider.Ins.DB.CHITIETHANGVEs.Any(cthv => cthv.MaHangVe == hangve.MaHangVe);

                    if (hasRelatedData)
                    {
                        MessageBox.Show("Không thể xóa hạng vé vì có dữ liệu liên kết.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    DataProvider.Ins.DB.HANGVEs.Remove(hangve);
                    DataProvider.Ins.DB.SaveChanges();

                    HangVeList.Remove(hangve);
                    MessageBox.Show("Xoá thành công", "Notification", MessageBoxButton.OK);
                }
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

                MessageBox.Show("Thêm thành công", "Notification", MessageBoxButton.OK);
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

                SayBayList.Remove(SelectedSanBay);
                SayBayList.Add(sanbay);
                SelectedSanBay = sanbay;

                MessageBox.Show("Sửa thành công", "Notification", MessageBoxButton.OK);
            });

            DeleteCommandFlight = new RelayCommand<object>((p) =>
            {
                if (SelectedSanBay == null)
                    return false;

                return true;

            }, (p) =>
            {
                var sanbay = DataProvider.Ins.DB.SANBAYs.Where(x => x.MaSanBay == SelectedSanBay.MaSanBay).SingleOrDefault();

                if (sanbay != null)
                {
                    // Kiểm tra các liên kết trước khi xóa
                    bool hasRelatedData = DataProvider.Ins.DB.CHUYENBAYs.Any(cb => cb.MaSanBayDi == sanbay.MaSanBay || cb.MaSanBayDen == sanbay.MaSanBay);

                    if (hasRelatedData)
                    {
                        MessageBox.Show("Không thể xóa sân bay vì có dữ liệu liên kết.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    DataProvider.Ins.DB.SANBAYs.Remove(sanbay);
                    DataProvider.Ins.DB.SaveChanges();

                    SayBayList.Remove(sanbay);

                    MessageBox.Show("Xoá thành công", "Notification", MessageBoxButton.OK);
                }
            });
        }
    }
}