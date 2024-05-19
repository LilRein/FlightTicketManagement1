using FlightTicketManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using FlightTicketManagement.Utilities;

namespace FlightTicketManagement.ViewModel
{


    internal class ReportsVM : ViewModelBase
    {
        private ObservableCollection<MonthReport> _MonthReportList;
        public ObservableCollection<MonthReport> MonthReportList { get { return _MonthReportList; } set { _MonthReportList = value; OnPropertyChanged(); } }

        public ReportsVM()
        {
                MonthReportList = new ObservableCollection<MonthReport>();

                var MonthList = DataProvider.Ins.DB.CTDOANHTHUTHANGs; 
                
                foreach ( var month in MonthList )
                {
                    MonthReport monthReport = new MonthReport();
                    monthReport.Year = month.Nam;
                    monthReport.Month = month.Thang;
                    monthReport.DoanhThu = month;
                    monthReport.Profit = month.DoanhThuThang;
                    monthReport.Ratio = month.TiLeThang;
                    MonthReportList.Add(monthReport);
                }
            //MonthReportList = new ObservableCollection<MonthReport>();
            //MonthReport monthReports = new MonthReport();
            //monthReports.Year = 2007;
            //monthReports.Month = 8;
            //monthReports.Profit = 100000;
            //monthReports.Ratio = 30;
            //MonthReportList.Add(monthReports);
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
