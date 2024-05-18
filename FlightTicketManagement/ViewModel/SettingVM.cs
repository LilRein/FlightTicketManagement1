using FlightTicketManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public SettingVM()
        {
            _pageModel = new PageModel();

            HangVeList = new ObservableCollection<HANGVE>(DataProvider.Ins.DB.HANGVEs);

            SayBayList = new ObservableCollection<SANBAY>(DataProvider.Ins.DB.SANBAYs);
        }
    }
}