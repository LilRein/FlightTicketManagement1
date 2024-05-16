using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManagement.Model
{
    public class DataProvider
    {
        //Dùng semaphore để chỉ tạo 1 db trong 1 chương trình
        private static DataProvider _ins;

        public static DataProvider Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new DataProvider();
                }

                return _ins;
            }

            set
            {
                _ins = value;
            }
        }

        public QUANLYBANVECHUYENBAYEntities DB { get; set; }

        private DataProvider()
        {
            DB = new QUANLYBANVECHUYENBAYEntities();
        }
    }
}
