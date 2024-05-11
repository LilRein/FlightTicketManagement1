using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CHUYENBAY
    {
        public string MaChuyenBay { get; set; }
        public string MaTuyenBay { get; set; }
        public string MaSanBayDi { get; set; }
        public string MaSanBayDen { get; set; }
        public string MaMayBay { get; set; }
        public string NgayBay { get; set; }
        public string GioKhoiHanh { get; set; }
        public float ThoiLuong { get; set; }
        public int DonGia { get; set; }

    }
}
