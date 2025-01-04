using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KurumsalProje.ViewModels.BomViewModels
{
    public class BomViewModel
    {
        public string COMCODE { get; set; }
        public string BOMDOCTYPE { get; set; }
        public string BOMDOCNUM { get; set; }
        public DateTime BOMDOCFROM { get; set; }
        public DateTime BOMDOCUNTIL { get; set; }
        public string MATDOCTYPE { get; set; }
        public string MATDOCNUM { get; set; }
        public decimal QUANTITY { get; set; }
        public string DRAWNUM { get; set; }
        public int CONTENTNUM { get; set; }
        public int ISDELETED { get; set; }
        public int ISPASSIVE { get; set; }
        public string COMPONENT { get; set; }
        public string COMPBOMDOCTYPE { get; set; }
        public string COMPBOMDOCNUM { get; set; }
    }
}