using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Model
{
    public class Schedules
    {
        public string FlightID { get; set; }
        public string StartAirport { get; set; }
        public string EndAirport { get; set; }
        public string StartHour { get; set; }
        public string EndHour { get; set; }
    }
}
