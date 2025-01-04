using KurumsalProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalProje.Controllers
{
    public class CostCenterTypeController : Controller
    {
        KKPBCEEntities db = new KKPBCEEntities();
        // GET: Default
        public ActionResult Index()
        {
            var firmalar = db.BSMGRBCECCM001.ToList();
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

            return View(new BSMGRBCECCM001());
        }

        [HttpPost]
        public ActionResult YeniKayıt(BSMGRBCECCM001 p)
        {
            if (db.BSMGRBCECCM001.Any(x => x.COMCODE == p.COMCODE && x.DOCTYPE == p.DOCTYPE))
            {
                ModelState.AddModelError("", "Bu COMCODE ve DOCTYPE kombinasyonu zaten mevcut.");
                return View(p);
            }

            db.BSMGRBCECCM001.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KayıtSil(string COMCODE)
        {
            if (string.IsNullOrEmpty(COMCODE))
            {
                return HttpNotFound("COMCODE parametresi geçerli değil.");
            }

            var kayitbul = db.BSMGRBCECCM001.FirstOrDefault(k => k.COMCODE == COMCODE);

            if (kayitbul == null)
            {
                return HttpNotFound($"COMCODE: {COMCODE} ile eşleşen kayıt bulunamadı.");
            }

            db.BSMGRBCECCM001.Remove(kayitbul);  // Kaydı silme
            db.SaveChanges();  // Değişiklikleri kaydetme

            return RedirectToAction("Index");
        }


        public ActionResult KayıtGetir(string COMCODE)
        {
            var atolye = db.BSMGRBCECCM001.Find(COMCODE);
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
            var kayit = db.BSMGRBCECCM001.FirstOrDefault(k => k.COMCODE == COMCODE);

            // Eğer kayıt bulunamazsa hata döndür
            if (kayit == null)
            {
                return HttpNotFound($"COMCODE: {COMCODE} ile eşleşen kayıt bulunamadı.");
            }

            // Kaydı güncelleme formuna gönder
            return View(kayit);
        }

        [HttpPost]
        public ActionResult KayıtGuncelle(BSMGRBCECCM001 model)
        {
            // Model doğrulama
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // COMCODE'ye göre kaydı bul
            var kayit = db.BSMGRBCECCM001.FirstOrDefault(k => k.COMCODE == model.COMCODE);

            // Eğer kayıt bulunamazsa hata döndür
            if (kayit == null)
            {
                return HttpNotFound($"COMCODE: {model.COMCODE} ile eşleşen kayıt bulunamadı.");
            }

            // Alanları güncelle
            kayit.DOCTYPE = model.DOCTYPE;
            kayit.DOCTYPETEXT = model.DOCTYPETEXT;
            kayit.ISPASSIVE = model.ISPASSIVE;

            // Değişiklikleri kaydet
            db.SaveChanges();

            // Güncelleme işleminden sonra Index sayfasına yönlendir
            return RedirectToAction("Index");
        }
    }
}