using KurumsalProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalProje.Controllers
{
    public class CityController : Controller
    {
        // GET: Country
        KKPBCEEntities db = new KKPBCEEntities();
        // GET: Default
        public ActionResult Index()
        {
            var firmalar = db.BSMGRBCEGEN004.ToList();
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
            var countryList = db.BSMGRBCEGEN003
                                .Select(x => new SelectListItem
                                {
                                    Value = x.COUNTRYCODE,
                                    Text = x.COUNTRYCODE
                                }).ToList();
            ViewBag.COUNTRYList = countryList;
            ViewBag.COMCODEList = comcodeList;

            return View(new BSMGRBCEGEN004());
        }

        [HttpPost]
        public ActionResult YeniKayıt(BSMGRBCEGEN004 p)
        {
            if (db.BSMGRBCEGEN004.Any(x => x.COMCODE == p.COMCODE && x.CITYCODE == p.CITYCODE))
            {
                ModelState.AddModelError("", "Bu COMCODE ve CITYCODE kombinasyonu zaten mevcut.");
                return View(p);
            }

            db.BSMGRBCEGEN004.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KayıtSil(string COMCODE)
        {
            if (string.IsNullOrEmpty(COMCODE))
            {
                return HttpNotFound("COMCODE parametresi geçerli değil.");
            }

            var kayitbul = db.BSMGRBCEGEN004.FirstOrDefault(k => k.COMCODE == COMCODE);

            if (kayitbul == null)
            {
                return HttpNotFound($"COMCODE: {COMCODE} ile eşleşen kayıt bulunamadı.");
            }

            db.BSMGRBCEGEN004.Remove(kayitbul);  // Kaydı silme
            db.SaveChanges();  // Değişiklikleri kaydetme

            return RedirectToAction("Index");
        }


        public ActionResult KayıtGetir(string COMCODE)
        {
            var atolye = db.BSMGRBCEGEN004.Find(COMCODE);
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
            var kayit = db.BSMGRBCEGEN004.FirstOrDefault(k => k.COMCODE == COMCODE);

            // Eğer kayıt bulunamazsa hata döndür
            if (kayit == null)
            {
                return HttpNotFound($"COMCODE: {COMCODE} ile eşleşen kayıt bulunamadı.");
            }

            var countrylist = db.BSMGRBCEGEN003
                                .Select(x => new SelectListItem
                                {
                                    Value = x.COUNTRYCODE,
                                    Text = x.COUNTRYCODE
                                }).ToList();
            ViewBag.COUNTRYList = countrylist;
            return View(kayit);
        }

        [HttpPost]
        public ActionResult KayıtGuncelle(BSMGRBCEGEN004 model)
        {
            // Model doğrulama
            if (!ModelState.IsValid)
            {
                ViewBag.COUNTRYList = db.BSMGRBCEGEN003
                                        .Select(x => new SelectListItem
                                        {
                                            Value = x.COUNTRYCODE,
                                            Text = x.COUNTRYCODE
                                        }).ToList();
                return View(model);
            }

            // COMCODE'ye göre kaydı bul
            var kayit = db.BSMGRBCEGEN004.FirstOrDefault(k => k.COMCODE == model.COMCODE);

            // Eğer kayıt bulunamazsa hata döndür
            if (kayit == null)
            {
                return HttpNotFound($"COMCODE: {model.COMCODE} ile eşleşen kayıt bulunamadı.");
            }

            // Alanları güncelle
            kayit.CITYCODE = model.CITYCODE;
            kayit.CITYTEXT = model.CITYTEXT;           
            kayit.COUNTRYCODE = model.COUNTRYCODE;

            // Değişiklikleri kaydet
            db.SaveChanges();

            // Güncelleme işleminden sonra Index sayfasına yönlendir
            return RedirectToAction("Index");
        }
    }
}