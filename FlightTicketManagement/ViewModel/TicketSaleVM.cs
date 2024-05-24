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
using System.Windows.Forms;
using System.Windows.Input;

namespace FlightTicketManagement.ViewModel
{
    class TicketSaleVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public ICommand ExportBtn { get; set; }
        public ICommand CancelBtn { get; set; }



        private ObservableCollection<PHIEUDATCHO> _orderList;
        public ObservableCollection<PHIEUDATCHO> OrderList
        {
            get { return _orderList; }
            set
            {
                _orderList = value;
                OnPropertyChanged(nameof(OrderList));
            }
        }


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
            MessageBox.Show("Xuất vé");
        }

        private void _cancelBtn(TicketSale parameter)
        {
            MessageBox.Show("Huỷ bỏ");
        }

    }
}
