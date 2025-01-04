using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KurumsalProje.Models;

namespace KurumsalProje.Controllers
{
    public class DefaultController : Controller
    {
        KKPBCEEntities db=new KKPBCEEntities();
        // GET: Default
        public ActionResult Index()
        {
            var firmalar = db.BSMGRBCEGEN001.ToList();
            return View(firmalar);
        }
        [HttpGet]
        public ActionResult YeniKayıt()
        {
            var countrylist = db.BSMGRBCEGEN003
                                .Select(x => new SelectListItem
                                {
                                    Value = x.COUNTRYCODE,
                                    Text = x.COUNTRYCODE
                                }).ToList();
            var citylist = db.BSMGRBCEGEN004
                                .Select(x => new SelectListItem
                                {
                                    Value = x.CITYCODE,
                                    Text = x.CITYCODE
                                }).ToList();
            ViewBag.CITYList = citylist;
            ViewBag.COUNTRYList = countrylist;
            

            return View(new BSMGRBCEGEN001());
        }

        [HttpPost]
        public ActionResult YeniKayıt(BSMGRBCEGEN001 p)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKayıt");
            }
            
            db.BSMGRBCEGEN001.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KayıtSil(string COMCODE)
        {
            if (string.IsNullOrEmpty(COMCODE))
            {
                return HttpNotFound("COMCODE parametresi geçerli değil.");
            }

            var kayitbul = db.BSMGRBCEGEN001.FirstOrDefault(k => k.COMCODE == COMCODE);

            if (kayitbul == null)
            {
                return HttpNotFound($"COMCODE: {COMCODE} ile eşleşen kayıt bulunamadı.");
            }

            db.BSMGRBCEGEN001.Remove(kayitbul);  // Kaydı silme
            db.SaveChanges();  // Değişiklikleri kaydetme

            return RedirectToAction("Index");
        }


        public ActionResult KayıtGetir(string COMCODE)
        {
            var atolye = db.BSMGRBCEGEN001.Find(COMCODE);
            return View("KayıtGetir", atolye);
        }

        [HttpGet]
        public ActionResult KayıtGuncelle(string COMCODE)
        {
            if (string.IsNullOrEmpty(COMCODE))
            {
                return HttpNotFound("Geçersiz veya eksik COMCODE değeri.");
            }

            // COMCODE'ye göre kaydı bul
            var kayit = db.BSMGRBCEGEN001.FirstOrDefault(k => k.COMCODE == COMCODE);

            if (kayit == null)
            {
                return HttpNotFound($"COMCODE: {COMCODE} ile eşleşen kayıt bulunamadı.");
            }

            // COUNTRYList'i ViewBag'e ekle
            var countrylist = db.BSMGRBCEGEN003
                                .Select(x => new SelectListItem
                                {
                                    Value = x.COUNTRYCODE,
                                    Text = x.COUNTRYCODE
                                }).ToList();
            var citylist = db.BSMGRBCEGEN004
                                .Select(x => new SelectListItem
                                {
                                    Value = x.CITYCODE,
                                    Text = x.CITYCODE
                                }).ToList();
            ViewBag.CITYList = citylist;
            ViewBag.COUNTRYList = countrylist;

            return View(kayit);
        }


        [HttpPost]
        public ActionResult KayıtGuncelle(BSMGRBCEGEN001 model)
        {
            if (!ModelState.IsValid)
            {
                // COUNTRYList'i tekrar yükle, yoksa sayfa hata verir
                ViewBag.COUNTRYList = db.BSMGRBCEGEN003
                                        .Select(x => new SelectListItem
                                        {
                                            Value = x.COUNTRYCODE,
                                            Text = x.COUNTRYCODE
                                        }).ToList();
                ViewBag.CITYList = db.BSMGRBCEGEN004
                                .Select(x => new SelectListItem
                                {
                                    Value = x.CITYCODE,
                                    Text = x.CITYCODE
                                }).ToList();
                
                return View(model);
            }

            var kayit = db.BSMGRBCEGEN001.FirstOrDefault(k => k.COMCODE == model.COMCODE);

            if (kayit == null)
            {
                return HttpNotFound($"COMCODE: {model.COMCODE} ile eşleşen kayıt bulunamadı.");
            }

            // Alanları güncelle
            kayit.COMTEXT = model.COMTEXT;
            kayit.ADDRESS1 = model.ADDRESS1;
            kayit.ADDRESS2 = model.ADDRESS2;
            kayit.CITYCODE = model.CITYCODE;
            kayit.COUNTRYCODE = model.COUNTRYCODE;

            // Veritabanına kaydet
            db.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}