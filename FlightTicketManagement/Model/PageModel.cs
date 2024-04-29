using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManagement.Model
{
    public class PageModel
    {
        public string Username { get; set; }
        public DateOnly Schedule { get; set; }
        public int TicketSale { get; set; }
        public string Flight {  get; set; }
        public string Report { get; set; }
        public string Setting { get; set; }
    }
}
