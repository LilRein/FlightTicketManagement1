using FlightTicketManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using FlightTicketManagement.Utilities;
using System.Windows.Controls;


namespace FlightTicketManagement.ViewModel
{


    internal class ReportsVM : ViewModelBase
    {
        private ObservableCollection<MonthReport> _MonthReportList;
        public ObservableCollection<MonthReport> MonthReportList { get { return _MonthReportList; } set { _MonthReportList = value; OnPropertyChanged(); } }

        private ObservableCollection<YearReport> _YearReportList;
        public ObservableCollection<YearReport> YearReportList { get { return _YearReportList; } set { _YearReportList = value; OnPropertyChanged(); } }

        private ObservableCollection<int> _SelectedYear;
        public ObservableCollection<int> SelectedYear { get { return _SelectedYear; } set { _SelectedYear = value; OnPropertyChanged(); } }

        private int? _selectedYearItem;
        public int? SelectedYearItem
        {
            get { return _selectedYearItem; }
            set
            {
                _selectedYearItem = value;
                OnPropertyChanged();
                FilterReports(); // Call method to update the report based on selected year
            }
        }
        public ReportsVM()
        {
            MonthReportList = new ObservableCollection<MonthReport>();
            YearReportList = new ObservableCollection<YearReport>();
            
            LoadReports();

            FilterReports();
        }

        private void LoadReports()
        {
            MonthReportList = new ObservableCollection<MonthReport>();
            var MonthList = DataProvider.Ins.DB.CTDOANHTHUTHANGs;

            foreach (var month in MonthList)
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
            SelectedYear = new ObservableCollection<int>();

            var YearList = DataProvider.Ins.DB.CTDOANHTHUNAMs;

            foreach (var year in YearList)
            {
                YearReport yearReport = new YearReport();
                yearReport.Month = year.Thang;
                yearReport.Year = year.Nam;
                yearReport.TicketNum = year;
                yearReport.Profit = year.DoanhThuNam;
                yearReport.Ratio = year.TiLeNam;
                YearReportList.Add(yearReport);
                if (!SelectedYear.Contains(year.Nam))
                {
                    SelectedYear.Add(year.Nam);
                }
            }
        }

        private void FilterReports()
        {
            if (SelectedYearItem.HasValue)
            {
                YearReportList = new ObservableCollection<YearReport>(
                   DataProvider.Ins.DB.CTDOANHTHUNAMs
                    .Where(y => y.Nam == SelectedYearItem.Value)
                .Select(y => new YearReport
                {
                    Year = y.Nam,
                    Month = y.Thang,
                    TicketNum = y,
                    Profit = y.DoanhThuNam,
                    Ratio = y.TiLeNam
                }));

                MonthReportList = new ObservableCollection<MonthReport>(
                    DataProvider.Ins.DB.CTDOANHTHUTHANGs
                        .Where(month => month.Nam == SelectedYearItem.Value)
                        .Select(month => new MonthReport
                        {
                            Year = month.Nam,
                            Month = month.Thang,
                            DoanhThu = month,
                            Profit = month.DoanhThuThang,
                            Ratio = month.TiLeThang
                        }));
            }
            else
            {
                YearReportList = new ObservableCollection<YearReport>(
                    DataProvider.Ins.DB.CTDOANHTHUNAMs.Select(year => new YearReport
                    {
                        Year = year.Nam,
                        Month = year.Thang,
                        TicketNum = year,
                        Profit = year.DoanhThuNam,
                        Ratio = year.TiLeNam
                    }));

                MonthReportList = new ObservableCollection<MonthReport>(
                    DataProvider.Ins.DB.CTDOANHTHUTHANGs.Select(month => new MonthReport
                    {
                        Year = month.Nam,
                        Month = month.Thang,
                        DoanhThu = month,
                        Profit = month.DoanhThuThang,
                        Ratio = month.TiLeThang
                    }));
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
