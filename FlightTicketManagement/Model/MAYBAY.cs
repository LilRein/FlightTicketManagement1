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
    
    public partial class MAYBAY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MAYBAY()
        {
            this.CHUYENBAYs = new HashSet<CHUYENBAY>();
            this.DANHSACHGHECUAMAYBAYs = new HashSet<DANHSACHGHECUAMAYBAY>();
        }
    
        public string MaMayBay { get; set; }
        public string TenMayBay { get; set; }
        public string MaHangMayBay { get; set; }
        public int SoLuongGhe { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHUYENBAY> CHUYENBAYs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DANHSACHGHECUAMAYBAY> DANHSACHGHECUAMAYBAYs { get; set; }
        public virtual HANGMAYBAY HANGMAYBAY { get; set; }
    }
}
