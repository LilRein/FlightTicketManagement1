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
    
    public partial class DANHSACHGHECUAMAYBAY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DANHSACHGHECUAMAYBAY()
        {
            this.PHIEUDATCHOes = new HashSet<PHIEUDATCHO>();
            this.VECHUYENBAYs = new HashSet<VECHUYENBAY>();
        }
    
        public string MaGhe { get; set; }
        public string SoGhe { get; set; }
        public string MaMayBay { get; set; }
        public string GhiChu { get; set; }
    
        public virtual MAYBAY MAYBAY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUDATCHO> PHIEUDATCHOes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VECHUYENBAY> VECHUYENBAYs { get; set; }
    }
}
