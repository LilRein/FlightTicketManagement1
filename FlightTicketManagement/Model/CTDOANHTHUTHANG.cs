//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlightTicketManagement.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CTDOANHTHUTHANG
    {
        public string MaChuyenBay { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public int SoVe { get; set; }
        public double TiLeThang { get; set; }
        public decimal DoanhThuThang { get; set; }
    
        public virtual CHUYENBAY CHUYENBAY { get; set; }
        public virtual CTDOANHTHUNAM CTDOANHTHUNAM { get; set; }
    }
}
