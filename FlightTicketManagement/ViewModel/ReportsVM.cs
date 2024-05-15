using FlightTicketManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightTicketManagement.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace FlightTicketManagement.ViewModel
{
    internal class ReportsVM : Utilities.ViewModelBase
    {
        private ObservableCollection<Report>? _reports;

        public ObservableCollection<Report> Reports
        {
            get{ return _reports;}

            set { _reports = value; }
        }

        public ReportsVM() 
        {
            _reports = new ObservableCollection<Report>();
            Reports.Add(new Report() { Rank = "1", FullName = "VN001", Profit = "100000", Ratio = "105", TicketNum = "2"});
        }
        //private readonly PageModel _pageModel;

        //public string Report
        //{
        //    get { return _pageModel.Report; } 
        //    set { _pageModel.Report = value; OnPropertyChanged(); }
        //}
        //public ReportsVM()
        //{
        //    _pageModel = new PageModel();
        //    Report = "hihi đây là test báo cáo";
        //}
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
