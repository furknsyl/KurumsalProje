using KurumsalProje.Models;
using KurumsalProje.ViewModels.CostCenterViewModels;
using KurumsalProje.ViewModels.MaterialViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace KurumsalProje.Controllers
{
    public class CostCenterController : Controller
    {
        KKPBCEEntities db = new KKPBCEEntities();

        public ActionResult Index()
        {
            var data = from head in db.BSMGRBCECCMHEAD
                       join text in db.BSMGRBCECCMTEXT
                       on new { head.COMCODE, head.CCMDOCTYPE, head.CCMDOCNUM } equals new { text.COMCODE, text.CCMDOCTYPE, text.CCMDOCNUM }
                       select new CostCenterViewModel
                       {
                           COMCODE = head.COMCODE,
                           CCMDOCTYPE = head.CCMDOCTYPE,
                           CCMDOCNUM = head.CCMDOCNUM,
                           CCMDOCFROM = head.CCMDOCFROM,
                           CCMDOCUNTIL = head.CCMDOCUNTIL,
                           MAINCCMDOCTYPE = head.MAINCCMDOCTYPE,
                           MAINCCMDOCNUM = head.MAINCCMDOCNUM,
                           ISDELETED = head.ISDELETED ?? 0,
                           ISPASSIVE = head.ISPASSIVE ?? 0,
                           LANCODE = text.LANCODE,
                           CCMSTEXT = text.CCMSTEXT,
                           CCMLTEXT = text.CCMLTEXT
                       };

            return View(data.ToList());
        }

        public ActionResult Details(string comcode, string ccmdoctype, string ccmdocnum)
        {
            var data = (from mathead in db.BSMGRBCECCMHEAD
                        join mattext in db.BSMGRBCECCMTEXT
                        on new { mathead.COMCODE, mathead.CCMDOCTYPE, mathead.CCMDOCNUM }
                        equals new { mattext.COMCODE, mattext.CCMDOCTYPE, mattext.CCMDOCNUM }
                        where mathead.COMCODE == comcode &&
                        mathead.CCMDOCTYPE == ccmdoctype &&
                        mathead.CCMDOCNUM == ccmdocnum
                        select new CostCenterViewModel
                        {
                            COMCODE = mathead.COMCODE,
                            CCMDOCTYPE = mathead.CCMDOCTYPE,
                            CCMDOCNUM = mathead.CCMDOCNUM,
                            CCMDOCFROM = mathead.CCMDOCFROM,
                            CCMDOCUNTIL = mathead.CCMDOCUNTIL,
                            MAINCCMDOCTYPE = mathead.MAINCCMDOCTYPE,
                            MAINCCMDOCNUM = mathead.MAINCCMDOCNUM,
                            ISDELETED = mathead.ISDELETED ?? 0,
                            ISPASSIVE = mathead.ISPASSIVE ?? 0,
                            LANCODE = mattext.LANCODE,
                            CCMSTEXT = mattext.CCMSTEXT,
                            CCMLTEXT = mattext.CCMLTEXT
                        }).FirstOrDefault();

            return View(data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            // COMCODE ve COUNTRY listelerini ViewBag'e ekle
            ViewBag.COMCODEList = db.BSMGRBCEGEN001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.COMCODE,
                                        Text = x.COMCODE
                                    }).ToList();

            ViewBag.DOCTYPEList = db.BSMGRBCECCM001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();

            ViewBag.LANCODEList = db.BSMGRBCEGEN002
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.LANCODE,
                                        Text = x.LANCODE
                                    }).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Create(CostCenterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // BSMGRBCECCMHEAD tablosuna veri ekleme
                var head = new BSMGRBCECCMHEAD
                {
                    COMCODE = model.COMCODE,
                    CCMDOCTYPE = model.CCMDOCTYPE,
                    CCMDOCNUM = model.CCMDOCNUM,
                    CCMDOCFROM = model.CCMDOCFROM,
                    CCMDOCUNTIL = model.CCMDOCUNTIL,
                    MAINCCMDOCTYPE = model.MAINCCMDOCTYPE,
                    MAINCCMDOCNUM = model.MAINCCMDOCNUM,
                    ISDELETED = model.ISDELETED,
                    ISPASSIVE = model.ISPASSIVE
                };

                // BSMGRBCECCMTEXT tablosuna veri ekleme
                var text = new BSMGRBCECCMTEXT
                {
                    COMCODE = model.COMCODE,
                    CCMDOCTYPE = model.CCMDOCTYPE,
                    CCMDOCNUM = model.CCMDOCNUM,
                    CCMDOCFROM = model.CCMDOCFROM,
                    CCMDOCUNTIL = model.CCMDOCUNTIL,
                    LANCODE = model.LANCODE,
                    CCMSTEXT = model.CCMSTEXT,
                    CCMLTEXT = model.CCMLTEXT
                };

                // Veritabanına ekleme
                db.BSMGRBCECCMHEAD.Add(head);
                db.BSMGRBCECCMTEXT.Add(text);
                db.SaveChanges();

                // Başarılı ekleme sonrası Index sayfasına yönlendirme
                return RedirectToAction("Index");
            }

            // COMCODE ve COUNTRY listelerini tekrar ViewBag'e ekle
            ViewBag.COMCODEList = db.BSMGRBCEGEN001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.COMCODE,
                                        Text = x.COMCODE
                                    }).ToList();

            ViewBag.DOCTYPEList = db.BSMGRBCECCM001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();
            ViewBag.LANCODEList = db.BSMGRBCEGEN002
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.LANCODE,
                                        Text = x.LANCODE
                                    }).ToList();

            // Model geçerli değilse formu tekrar göster
            return View(model);
        }

        public ActionResult Delete(string COMCODE)
        {
            if (string.IsNullOrEmpty(COMCODE))
            {
                return HttpNotFound("COMCODE parametresi geçerli değil.");
            }

            var head = db.BSMGRBCECCMHEAD.FirstOrDefault(k => k.COMCODE == COMCODE);
            var text = db.BSMGRBCECCMTEXT.FirstOrDefault(k => k.COMCODE == COMCODE);
            if (head == null && text == null)
            {
                return HttpNotFound($"COMCODE: {COMCODE} ile eşleşen kayıt bulunamadı.");
            }

            db.BSMGRBCECCMHEAD.Remove(head);  // Kaydı silme
            db.BSMGRBCECCMTEXT.Remove(text);  // Kaydı silme
            db.SaveChanges();  // Değişiklikleri kaydetme

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(string comcode,string ccmdoctype,string ccmdocnum)
        {
            // Güncellenecek kaydı bul
            var head = db.BSMGRBCECCMHEAD.FirstOrDefault(h => h.COMCODE == comcode && h.CCMDOCTYPE == ccmdoctype && h.CCMDOCNUM == ccmdocnum);
            var text = db.BSMGRBCECCMTEXT.FirstOrDefault(t => t.COMCODE == comcode && t.CCMDOCTYPE == ccmdoctype && t.CCMDOCNUM == ccmdocnum);

            if (head == null || text == null)
            {
                return HttpNotFound(); // Kayıt bulunamazsa 404 döndür
            }

            // ViewModel'e verileri aktar
            var model = new CostCenterViewModel
            {
                COMCODE = head.COMCODE,
                CCMDOCTYPE = head.CCMDOCTYPE,
                CCMDOCNUM = head.CCMDOCNUM,
                CCMDOCFROM = head.CCMDOCFROM,
                CCMDOCUNTIL = head.CCMDOCUNTIL,
                MAINCCMDOCTYPE = head.MAINCCMDOCTYPE,
                MAINCCMDOCNUM = head.MAINCCMDOCNUM,
                ISDELETED = head.ISDELETED ?? 0,
                ISPASSIVE = head.ISPASSIVE ?? 0,
                LANCODE = text.LANCODE,
                CCMSTEXT = text.CCMSTEXT,
                CCMLTEXT = text.CCMLTEXT
            };




            ViewBag.DOCTYPEList = db.BSMGRBCECCM001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();
            ViewBag.LANCODEList = db.BSMGRBCEGEN002
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.LANCODE,
                                        Text = x.LANCODE
                                    }).ToList();

            return View(model); // Güncelleme formunu göster
        }

        [HttpPost]
        public ActionResult Edit(CostCenterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Güncellenecek kaydı bul
                var head = db.BSMGRBCECCMHEAD.FirstOrDefault(h => h.COMCODE == model.COMCODE && h.CCMDOCTYPE == model.CCMDOCTYPE && h.CCMDOCNUM == model.CCMDOCNUM);
                var text = db.BSMGRBCECCMTEXT.FirstOrDefault(t => t.COMCODE == model.COMCODE && t.CCMDOCTYPE == model.CCMDOCTYPE && t.CCMDOCNUM == model.CCMDOCNUM);

                if (head == null || text == null)
                {
                    return HttpNotFound(); // Kayıt bulunamazsa 404 döndür
                }

                // HEAD tablosunu güncelle
                head.CCMDOCFROM = model.CCMDOCFROM;
                head.CCMDOCUNTIL = model.CCMDOCUNTIL;
                head.MAINCCMDOCTYPE = model.MAINCCMDOCTYPE;
                head.MAINCCMDOCNUM = model.MAINCCMDOCNUM;
                head.ISDELETED = model.ISDELETED;
                head.ISPASSIVE = model.ISPASSIVE;

                // TEXT tablosunu güncelle
                text.LANCODE = model.LANCODE;
                text.CCMSTEXT = model.CCMSTEXT;
                text.CCMLTEXT = model.CCMLTEXT;

                // Değişiklikleri kaydet
                db.SaveChanges();

                // Başarılı güncelleme sonrası Index sayfasına yönlendirme
                return RedirectToAction("Index");
            }



            ViewBag.DOCTYPEList = db.BSMGRBCECCM001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();
            ViewBag.LANCODEList = db.BSMGRBCEGEN002
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.LANCODE,
                                        Text = x.LANCODE
                                    }).ToList();

            // Model geçerli değilse formu tekrar göster
            return View(model);
        }
    }
}