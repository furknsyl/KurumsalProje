//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KurumsalProje.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BSMGRBCEMATHEAD
    {
        public string COMCODE { get; set; }
        public string MATDOCTYPE { get; set; }
        public string MATDOCNUM { get; set; }
        public System.DateTime MATDOCFROM { get; set; }
        public System.DateTime MATDOCUNTIL { get; set; }
        public int SUPPLYTYPE { get; set; }
        public string STUNIT { get; set; }
        public Nullable<decimal> NETWEIGHT { get; set; }
        public string NWUNIT { get; set; }
        public Nullable<decimal> BRUTWEIGHT { get; set; }
        public string BWUNIT { get; set; }
        public Nullable<int> ISBOM { get; set; }
        public string BOMDOCTYPE { get; set; }
        public string BOMDOCNUM { get; set; }
        public Nullable<int> ISROUTE { get; set; }
        public string ROTDOCTYPE { get; set; }
        public string ROTDOCNUM { get; set; }
        public Nullable<int> ISDELETED { get; set; }
        public Nullable<int> ISPASSIVE { get; set; }
    
        public virtual BSMGRBCEMATTEXT BSMGRBCEMATTEXT { get; set; }
    }
}
