using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManagement.Model
{
    public class MonthReport
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public CTDOANHTHUTHANG DoanhThu { get; set; }
        public decimal Profit { get; set; }
        public double Ratio { get; set; }
    }
}
