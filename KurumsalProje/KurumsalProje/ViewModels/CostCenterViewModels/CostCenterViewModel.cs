using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KurumsalProje.ViewModels.CostCenterViewModels
{
    public class CostCenterViewModel
    {
        // Ortak Alanlar
        public string COMCODE { get; set; } // Firma Kodu
        public string CCMDOCTYPE { get; set; } // Maliyet Merkezi Tipi
        public string CCMDOCNUM { get; set; } // Maliyet Merkezi Kodu
        public DateTime CCMDOCFROM { get; set; } // Geçerlilik Başlangıç
        public DateTime CCMDOCUNTIL { get; set; } // Geçerlilik Bitiş

        // BSMGROCCMHEAD Tablosuna Özel Alanlar
        public string MAINCCMDOCTYPE { get; set; } // Ana Maliyet Merkezi Tipi
        public string MAINCCMDOCNUM { get; set; } // Ana Maliyet Merkezi Kodu
        public int ISDELETED { get; set; } // Silindi?
        public int ISPASSIVE { get; set; } // Pasif mi?

        // BSMGROCCMTEXT Tablosuna Özel Alanlar
        public string LANCODE { get; set; } // Dil Kodu
        public string CCMSTEXT { get; set; } // Maliyet Merkezi Kısa Açıklaması
        public string CCMLTEXT { get; set; } // Maliyet Merkezi Uzun Açıklaması
    }
}