using FlightTicketManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManagement.ViewModel
{
    class DashboardVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        private string _userName;
        public string Username
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private ObservableCollection<YearReport> _YearRevenue;
        public ObservableCollection<YearReport> YearRevenue { get { return _YearRevenue; } set { _YearRevenue = value; OnPropertyChanged(); } }

        private decimal _totalRevenue2024;
        public decimal TotalRevenue2024
        {
            get { return _totalRevenue2024; }
            set
            {
                _totalRevenue2024 = value;
                OnPropertyChanged(nameof(TotalRevenue2024));
            }
        }

        private int _ticketsSold;
        public int TicketsSold
        {
            get { return _ticketsSold; }
            set
            {
                _ticketsSold = value;
                OnPropertyChanged(nameof(_ticketsSold));
            }
        }

        public DashboardVM() 
        {
            YearRevenue = new ObservableCollection<YearReport>();

            // Retrieve the data for the year 2024
            var year2024Data = DataProvider.Ins.DB.DOANHTHUNAMs
                                        .Where(d => d.NAM == 2024)
                                        .ToList();

            // Convert the data to YearReport and add to YearRevenue
            foreach (var data in year2024Data)
            {
                YearRevenue.Add(new YearReport
                {
                    Year = data.NAM,
                    TotalRevenue = data.TONGDOANHTHUNAM ?? 0
                });
            }

            // Calculate the total revenue for 2024
            TotalRevenue2024 = YearRevenue.Sum(y => y.TotalRevenue);

            TicketsSold = DataProvider.Ins.DB.CTDOANHTHUTHANGs
                                      .Sum(t => t.SoVe); // Adjust this line based on the actual property that represents the number of tickets sold

        }

    }
}
