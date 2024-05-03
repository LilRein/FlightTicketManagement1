﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManagement.Model
{
    public class Schedules
    {
        public string FlightID { get; set; }
        public string StartAirport { get; set; }
        public string EndAirport { get; set; }
        public string StartHour { get; set; }
        public string EndHour { get; set; }

        public string STTMidFlight { get; set; }
        public string MidAirport { get; set; }
        public string TimeBreak { get; set;}
        public string Note { get; set; }
    }
}
