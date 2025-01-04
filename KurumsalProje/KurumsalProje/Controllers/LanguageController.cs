using KurumsalProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalProje.Controllers
{
    public class LanguageController : Controller
    {
        KKPBCEEntities db = new KKPBCEEntities();
        // GET: Default
        public ActionResult Index()
        {
            var firmalar = db.BSMGRBCEGEN002.ToList();
            return View(firmalar);
        }
        [HttpGet]
        public ActionResult YeniKayıt()
        {
            var comcodeList = db.BSMGRBCEGEN001
                                .Select(x => new SelectListItem
                                {
                                    Value = x.COMCODE,
                                    Text = x.COMCODE
                                }).ToList();

            ViewBag.COMCODEList = comcodeList;

            return View(new BSMGRBCEGEN002());
        }

        [HttpPost]
        public ActionResult YeniKayıt(BSMGRBCEGEN002 p)
        {
            if (db.BSMGRBCEGEN002.Any(x => x.COMCODE == p.COMCODE && x.LANCODE == p.LANCODE))
            {
                ModelState.AddModelError("", "Bu COMCODE ve LANCODE kombinasyonu zaten mevcut.");
                return View(p);
            }

            db.BSMGRBCEGEN002.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KayıtSil(string COMCODE)
        {
            if (string.IsNullOrEmpty(COMCODE))
            {
                return HttpNotFound("COMCODE parametresi geçerli değil.");
            }

            var kayitbul = db.BSMGRBCEGEN002.FirstOrDefault(k => k.COMCODE == COMCODE);

            if (kayitbul == null)
            {
                return HttpNotFound($"COMCODE: {COMCODE} ile eşleşen kayıt bulunamadı.");
            }

            db.BSMGRBCEGEN002.Remove(kayitbul);  // Kaydı silme
            db.SaveChanges();  // Değişiklikleri kaydetme

            return RedirectToAction("Index");
        }


        public ActionResult KayıtGetir(string COMCODE)
        {
            var atolye = db.BSMGRBCEGEN002.Find(COMCODE);
            return View("KayıtGetir", atolye);
        }

        public ActionResult KayıtGuncelle(string COMCODE)
        {
            // COMCODE boş veya null ise hata döndür
            if (string.IsNullOrEmpty(COMCODE))
            {
                return HttpNotFound("Geçersiz veya eksik COMCODE değeri.");
            }

            // COMCODE'ye göre kaydı bul
            var kayit = db.BSMGRBCEGEN002.FirstOrDefault(k => k.COMCODE == COMCODE);

            // Eğer kayıt bulunamazsa hata döndür
            if (kayit == null)
            {
                return HttpNotFound($"COMCODE: {COMCODE} ile eşleşen kayıt bulunamadı.");
            }

            // Kaydı güncelleme formuna gönder
            return View(kayit);
        }

        [HttpPost]
        public ActionResult KayıtGuncelle(BSMGRBCEGEN002 model)
        {
            // Model doğrulama
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // COMCODE'ye göre kaydı bul
            var kayit = db.BSMGRBCEGEN002.FirstOrDefault(k => k.COMCODE == model.COMCODE);

            // Eğer kayıt bulunamazsa hata döndür
            if (kayit == null)
            {
                return HttpNotFound($"COMCODE: {model.COMCODE} ile eşleşen kayıt bulunamadı.");
            }

            // Alanları güncelle
            kayit.LANCODE = model.LANCODE;
            kayit.LANTEXT = model.LANTEXT;

            // Değişiklikleri kaydet
            db.SaveChanges();

            // Güncelleme işleminden sonra Index sayfasına yönlendir
            return RedirectToAction("Index");
        }
    }
}