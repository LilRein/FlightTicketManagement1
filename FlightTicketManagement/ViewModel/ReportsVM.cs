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

        private ObservableCollection<YearReport> _YearReportList;
        public ObservableCollection<YearReport> YearReportList { get { return _YearReportList; } set { _YearReportList = value; OnPropertyChanged(); } }

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
            
                YearReportList = new ObservableCollection<YearReport>();

                var YearList = DataProvider.Ins.DB.CTDOANHTHUNAMs;

                foreach ( var year in YearList )
                {
                    YearReport yearReport = new YearReport();
                    yearReport.Month = year.Thang;
                    yearReport.Year = year.Nam;
                    yearReport.TicketNum = year;
                    yearReport.Profit = year.DoanhThuNam;
                    yearReport.Ratio = year.TiLeNam;
                    YearReportList.Add(yearReport);
                }
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
