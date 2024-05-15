using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManagement.Model
{
    public class Report
    {
        private string rank;
        public string Rank {
            get { return rank; }
            set { rank = value; }
        }

        private string fullname;
        public string FullName
        {
            get { return fullname; }
            set { fullname = value; }
        }

        private string ticketnum;
        public string TicketNum
        {
            get { return ticketnum; }
            set { ticketnum = value; }
        }

        private string profit;
        public string Profit
        {
            get { return profit; }
            set { profit = value; }
        }

        private string ratio;
        public string Ratio
        {
            get { return ratio; }
            set { ratio = value; }
        }
    }
}
