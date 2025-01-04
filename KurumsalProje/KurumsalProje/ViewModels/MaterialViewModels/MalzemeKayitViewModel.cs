using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KurumsalProje.ViewModels.MaterialViewModels
{
    public class MalzemeKayitViewModel
    {
        // BSMGRBCEMATHEAD tablosu için alanlar
        public string COMCODE { get; set; }
        public string MATDOCTYPE { get; set; }
        public string MATDOCNUM { get; set; }
        public DateTime MATDOCFROM { get; set; }
        public DateTime MATDOCUNTIL { get; set; }
        public int SUPPLYTYPE { get; set; }
        public string STUNIT { get; set; }
        public decimal? NETWEIGHT { get; set; }
        public string NWUNIT { get; set; }
        public decimal? BRUTWEIGHT { get; set; }
        public string BWUNIT { get; set; }
        public int? ISBOM { get; set; }
        public string BOMDOCTYPE { get; set; }
        public string BOMDOCNUM { get; set; }
        public int ISROUTE { get; set; }
        public string ROTDOCTYPE { get; set; }
        public string ROTDOCNUM { get; set; }
        public int? ISDELETED { get; set; }
        public int? ISPASSIVE { get; set; }

        // BSMGRBCEMATTEXT tablosu için alanlar
        public string LANCODE { get; set; }
        public string MATSTEXT { get; set; }
        public string MATLTEXT { get; set; }
    }
}