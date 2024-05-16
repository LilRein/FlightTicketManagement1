using FlightTicketManagement.Model;
using FlightTicketManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightTicketManagement.ViewModel
{
    class SettingVM : Utilities.ViewModelBase
    {
        private ObservableCollection<SettingVM> _List;
        public ObservableCollection<SettingVM> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private SettingVM _SelectedItem;
        public SettingVM SelectedItem
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
                    TiLeGiaVe = SelectedItem.TiLeGiaVe;
                }
            }
        }

        private string _MaHangVe;
        public string MaHangVe { get => _MaHangVe; set { _MaHangVe = value; OnPropertyChanged(); } }


        private string _TenHangVe;
        public string TenHangVe { get => _TenHangVe; set { _TenHangVe = value; OnPropertyChanged(); } }


        private double _TiLeGiaVe;
        public double TiLeGiaVe { get => _TiLeGiaVe; set { _TiLeGiaVe = value; OnPropertyChanged(); } }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public SettingVM()
        {
            List = new ObservableCollection<SettingVM>(DataProvider.Ins.DB.HANGVEs);

            //AddCommand = new RelayCommand<object>((p) =>
            //{
            //    return true;

            //}, (p) =>
            //{
            //    var Setting = new SettingVM() { MaHangVe = MaHangVe, TenHangVe = TenHangVe, TiLeGiaVe = TiLeGiaVe };
            //    DataProvider.Ins.DB.HANGVEs.Add(Setting);
            //    DataProvider.Ins.DB.SaveChanges();

            //    List.Add(Setting);
            //});

            //EditCommand = new RelayCommand<object>((p) =>
            //{
            //    if (SelectedItem == null)
            //        return false;

            //    var displayList = DataProvider.Ins.DB.Supliers.Where(x => x.Id == SelectedItem.Id);
            //    if (displayList != null && displayList.Count() != 0)
            //        return true;

            //    return false;

            //}, (p) =>
            //{
            //    var Suplier = DataProvider.Ins.DB.Supliers.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
            //    Suplier.DisplayName = DisplayName;
            //    Suplier.Phone = Phone;
            //    Suplier.Address = Address;
            //    Suplier.Email = Email;
            //    Suplier.ContractDate = ContractDate;
            //    Suplier.MoreInfo = MoreInfo;
            //    DataProvider.Ins.DB.SaveChanges();

            //    SelectedItem.DisplayName = DisplayName;
            //});
        }
    }
}
