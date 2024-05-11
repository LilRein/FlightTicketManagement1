using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CTHANGVE
    {
        public string MaHangVe { get; set; }
        public string MaChuyenBay { get; set; }
        public SqlMoney GiaVe { get; set; }
        public int SoGheChoHangVe { get; set; }
        public int SoVeDaBan { get; set; }
        public int SoGheDat { get; set; }
        public int SoGheConLai { get; set; }

    }
}
