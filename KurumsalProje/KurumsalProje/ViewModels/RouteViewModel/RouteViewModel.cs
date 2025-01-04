using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KurumsalProje.ViewModels.RouteViewModel
{
    public class RouteViewModel
    {
        public string COMCODE { get; set; } // Firma Kodu
        public string ROTDOCTYPE { get; set; } // Rota Tipi
        public string ROTDOCNUM { get; set; } // Rota Numarası
        public DateTime ROTDOCFROM { get; set; } // Geçerlilik Başlangıç Tarihi
        public DateTime ROTDOCUNTIL { get; set; } // Geçerlilik Bitiş Tarihi
        public string MATDOCTYPE { get; set; } // Malzeme Tipi
        public string MATDOCNUM { get; set; } // Malzeme Numarası
        public decimal QUANTITY { get; set; } // Miktar
        public int ISDELETED { get; set; } // Silindi mi?
        public int ISPASSIVE { get; set; } // Pasif mi?
        public string DRAWNUM { get; set; } // Çizim Numarası

        // İkinci grup kolonlar
        public int OPRNUM { get; set; } // Operasyon Numarası
        public string WCMDOCTYPE { get; set; } // İş Merkezi Tipi
        public string WCMDOCNUM { get; set; } // İş Merkezi Numarası
        public string OPRDOCTYPE { get; set; } // Operasyon Tipi
        public decimal SETUPTIME { get; set; } // Kurulum Süresi
        public decimal MACHINETIME { get; set; } // Makine Süresi
        public decimal LABOURTIME { get; set; } // İşçilik Süresi

        // Üçüncü grup kolonlar
        public string BOMDOCTYPE { get; set; } // Ürün Ağacı Tipi
        public string BOMDOCNUM { get; set; } // Ürün Ağacı Numarası
        public string CONTENTNUM { get; set; } // İçerik Numarası
        public string COMPONENT { get; set; } // Bileşen
    }
}