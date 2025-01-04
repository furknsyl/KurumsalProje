using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KurumsalProje.ViewModels.WorkCenterViewModels
{
    public class WorkCenterViewModel
    {
        public string COMCODE { get; set; }
        public string WCMDOCTYPE { get; set; }
        public string WCMDOCNUM { get; set; }
        public DateTime WCMDOCFROM { get; set; }
        public DateTime WCMDOCUNTIL { get; set; }
        public string MAINWCMDOCTYPE { get; set; }
        public string MAINWCMDOCNUM { get; set; }
        public string CCMDOCTYPE { get; set; }
        public string CCMDOCNUM { get; set; }
        public decimal? WORKTIME { get; set; }
        public int? ISDELETED { get; set; }
        public int? ISPASSIVE { get; set; }
        public string LANCODE { get; set; }
        public string WCMSTEXT { get; set; }
        public string WCMLTEXT { get; set; }
        public string OPRDOCTYPE { get; set; }

    }
}