using KurumsalProje.Models;
using KurumsalProje.ViewModels.CostCenterViewModels;
using KurumsalProje.ViewModels.WorkCenterViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace KurumsalProje.Controllers
{
    public class WorkCenterController : Controller
    {
        KKPBCEEntities db = new KKPBCEEntities();

        public ActionResult Index()
        {
            var data = from head in db.BSMGRBCEWCMHEAD
                       join text in db.BSMGRBCEWCMTEXT
                       on new { head.COMCODE, head.WCMDOCTYPE, head.WCMDOCNUM } equals new { text.COMCODE, text.WCMDOCTYPE, text.WCMDOCNUM }
                       join opr in db.BSMGRBCEWCMOPR
                       on new { head.COMCODE, head.WCMDOCTYPE, head.WCMDOCNUM } equals new { opr.COMCODE, opr.WCMDOCTYPE, opr.WCMDOCNUM }
                       select new WorkCenterViewModel
                       {
                           COMCODE = head.COMCODE,
                           WCMDOCTYPE = head.WCMDOCTYPE,
                           WCMDOCNUM = head.WCMDOCNUM,
                           WCMDOCFROM = head.WCMDOCFROM,
                           WCMDOCUNTIL = head.WCMDOCUNTIL,
                           MAINWCMDOCTYPE = head.MAINWCMDOCTYPE,
                           MAINWCMDOCNUM = head.MAINWCMDOCNUM,
                           CCMDOCTYPE = head.CCMDOCTYPE,
                           CCMDOCNUM = head.CCMDOCNUM,
                           WORKTIME = head.WORKTIME,
                           ISDELETED = head.ISDELETED ?? 0,
                           ISPASSIVE = head.ISPASSIVE ?? 0,
                           LANCODE = text.LANCODE,
                           WCMSTEXT = text.WCMSTEXT,
                           WCMLTEXT = text.WCMLTEXT,
                           OPRDOCTYPE = opr.OPRDOCTYPE
                       };

            return View(data.ToList());
        }

        public ActionResult Details(string comcode, string wcmdoctype, string wcmdocnum)
        {
            var data = (from mathead in db.BSMGRBCEWCMHEAD
                        join mattext in db.BSMGRBCEWCMTEXT
                        on new { mathead.COMCODE, mathead.WCMDOCTYPE, mathead.WCMDOCNUM } equals new { mattext.COMCODE, mattext.WCMDOCTYPE, mattext.WCMDOCNUM }
                        join opr in db.BSMGRBCEWCMOPR
                        on new { mathead.COMCODE, mathead.WCMDOCTYPE, mathead.WCMDOCNUM } equals new { opr.COMCODE, opr.WCMDOCTYPE, opr.WCMDOCNUM }
                        where mathead.COMCODE == comcode &&
                        mathead.WCMDOCTYPE == wcmdoctype &&
                        mathead.WCMDOCNUM == wcmdocnum
                        select new WorkCenterViewModel
                        {
                            COMCODE = mathead.COMCODE,
                            WCMDOCTYPE = mathead.WCMDOCTYPE,
                            WCMDOCNUM = mathead.WCMDOCNUM,
                            WCMDOCFROM = mathead.WCMDOCFROM,
                            WCMDOCUNTIL = mathead.WCMDOCUNTIL,
                            MAINWCMDOCTYPE = mathead.MAINWCMDOCTYPE,
                            MAINWCMDOCNUM = mathead.MAINWCMDOCNUM,
                            CCMDOCTYPE = mathead.CCMDOCTYPE,
                            CCMDOCNUM = mathead.CCMDOCNUM,
                            WORKTIME = mathead.WORKTIME,
                            ISDELETED = mathead.ISDELETED ?? 0,
                            ISPASSIVE = mathead.ISPASSIVE ?? 0,
                            LANCODE = mattext.LANCODE,
                            WCMSTEXT = mattext.WCMSTEXT,
                            WCMLTEXT = mattext.WCMLTEXT,
                            OPRDOCTYPE = opr.OPRDOCTYPE
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

            ViewBag.WCMDOCTYPEList = db.BSMGRBCEWCM001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();

            ViewBag.CCMDOCTYPEList = db.BSMGRBCECCM001
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
            ViewBag.OPRDOCTYPEList = db.BSMGRBCEOPR001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Create(WorkCenterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var head = new BSMGRBCEWCMHEAD
                {
                    COMCODE = model.COMCODE,
                    WCMDOCTYPE = model.WCMDOCTYPE,
                    WCMDOCNUM = model.WCMDOCNUM,
                    WCMDOCFROM = model.WCMDOCFROM,
                    WCMDOCUNTIL = model.WCMDOCUNTIL,
                    MAINWCMDOCTYPE = model.MAINWCMDOCTYPE,
                    MAINWCMDOCNUM = model.MAINWCMDOCNUM,
                    CCMDOCTYPE = model.CCMDOCTYPE,
                    CCMDOCNUM = model.CCMDOCNUM,
                    WORKTIME = model.WORKTIME,
                    ISDELETED = model.ISDELETED ?? 0,
                    ISPASSIVE = model.ISPASSIVE
                };

                // BSMGRBCECCMTEXT tablosuna veri ekleme
                var text = new BSMGRBCEWCMTEXT
                {
                    COMCODE = model.COMCODE,
                    WCMDOCTYPE = model.WCMDOCTYPE,
                    WCMDOCNUM = model.WCMDOCNUM,
                    WCMDOCFROM = model.WCMDOCFROM,
                    WCMDOCUNTIL = model.WCMDOCUNTIL,
                    LANCODE = model.LANCODE,
                    WCMSTEXT = model.WCMSTEXT,
                    WCMLTEXT = model.WCMLTEXT
                };
                var opr = new BSMGRBCEWCMOPR
                {
                    COMCODE = model.COMCODE,
                    WCMDOCTYPE = model.WCMDOCTYPE,
                    WCMDOCNUM = model.WCMDOCNUM,
                    WCMDOCFROM = model.WCMDOCFROM,
                    WCMDOCUNTIL = model.WCMDOCUNTIL,
                    OPRDOCTYPE = model.OPRDOCTYPE
                };

                db.BSMGRBCEWCMHEAD.Add(head);
                db.BSMGRBCEWCMTEXT.Add(text);
                db.BSMGRBCEWCMOPR.Add(opr);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.COMCODEList = db.BSMGRBCEGEN001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.COMCODE,
                                        Text = x.COMCODE
                                    }).ToList();

            ViewBag.WCMDOCTYPEList = db.BSMGRBCEWCM001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();

            ViewBag.CCMDOCTYPEList = db.BSMGRBCECCM001
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
            ViewBag.OPRDOCTYPEList = db.BSMGRBCEOPR001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
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

            var head = db.BSMGRBCEWCMHEAD.FirstOrDefault(k => k.COMCODE == COMCODE);
            var text = db.BSMGRBCEWCMTEXT.FirstOrDefault(k => k.COMCODE == COMCODE);
            var opr = db.BSMGRBCEWCMOPR.FirstOrDefault(k => k.COMCODE == COMCODE);
            if (head == null && text == null && opr==null)
            {
                return HttpNotFound($"COMCODE: {COMCODE} ile eşleşen kayıt bulunamadı.");
            }

            db.BSMGRBCEWCMHEAD.Remove(head);  // Kaydı silme
            db.BSMGRBCEWCMTEXT.Remove(text);  // Kaydı silme
            db.BSMGRBCEWCMOPR.Remove(opr);  // Kaydı silme
            db.SaveChanges();  // Değişiklikleri kaydetme

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(string comcode, string wcmdoctype, string wcmdocnum)
        {
            // Güncellenecek kaydı bul
            var head = db.BSMGRBCEWCMHEAD.FirstOrDefault(h => h.COMCODE == comcode && h.WCMDOCTYPE == wcmdoctype && h.WCMDOCNUM == wcmdocnum);
            var text = db.BSMGRBCEWCMTEXT.FirstOrDefault(t => t.COMCODE == comcode && t.WCMDOCTYPE == wcmdoctype && t.WCMDOCNUM == wcmdocnum);
            var opr = db.BSMGRBCEWCMOPR.FirstOrDefault(k => k.COMCODE == comcode && k.WCMDOCTYPE == wcmdoctype && k.WCMDOCNUM == wcmdocnum); 

            if (head == null || text == null)
            {
                return HttpNotFound(); // Kayıt bulunamazsa 404 döndür
            }

            // ViewModel'e verileri aktar
            var model = new WorkCenterViewModel
            {
                COMCODE = head.COMCODE,
                WCMDOCTYPE = head.WCMDOCTYPE,
                WCMDOCNUM = head.WCMDOCNUM,
                WCMDOCFROM = head.WCMDOCFROM,
                WCMDOCUNTIL = head.WCMDOCUNTIL,
                MAINWCMDOCTYPE = head.MAINWCMDOCTYPE,
                MAINWCMDOCNUM = head.MAINWCMDOCNUM,
                CCMDOCTYPE = head.CCMDOCTYPE,
                CCMDOCNUM = head.CCMDOCNUM,
                WORKTIME = head.WORKTIME,
                ISDELETED = head.ISDELETED ?? 0,
                ISPASSIVE = head.ISPASSIVE ?? 0,
                LANCODE = text.LANCODE,
                WCMSTEXT = text.WCMSTEXT,
                WCMLTEXT = text.WCMLTEXT,
                OPRDOCTYPE = opr.OPRDOCTYPE
            };



            ViewBag.COMCODEList = db.BSMGRBCEGEN001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.COMCODE,
                                        Text = x.COMCODE
                                    }).ToList();

            ViewBag.WCMDOCTYPEList = db.BSMGRBCEWCM001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();

            ViewBag.CCMDOCTYPEList = db.BSMGRBCECCM001
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

            ViewBag.OPRDOCTYPEList = db.BSMGRBCEOPR001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();
            return View(model); // Güncelleme formunu göster
        }

        [HttpPost]
        public ActionResult Edit(WorkCenterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Güncellenecek kaydı bul
                var head = db.BSMGRBCEWCMHEAD.FirstOrDefault(h => h.COMCODE == model.COMCODE && h.WCMDOCTYPE == model.WCMDOCTYPE && h.WCMDOCNUM == model.WCMDOCNUM);
                var text = db.BSMGRBCEWCMTEXT.FirstOrDefault(t => t.COMCODE == model.COMCODE && t.WCMDOCTYPE == model.WCMDOCTYPE && t.WCMDOCNUM == model.WCMDOCNUM);
                var opr = db.BSMGRBCEWCMOPR.FirstOrDefault(k => k.COMCODE == model.COMCODE && k.WCMDOCTYPE == model.WCMDOCTYPE && k.WCMDOCNUM == model.WCMDOCNUM);

                if (head == null || text == null || opr==null)
                {
                    return HttpNotFound(); // Kayıt bulunamazsa 404 döndür
                }

                // HEAD tablosunu güncelle
                head.WCMDOCFROM = model.WCMDOCFROM;
                head.WCMDOCUNTIL = model.WCMDOCUNTIL;
                head.MAINWCMDOCTYPE = model.MAINWCMDOCTYPE;
                head.MAINWCMDOCNUM = model.MAINWCMDOCNUM;
                head.WORKTIME = model.WORKTIME;
                head.ISDELETED = model.ISDELETED;
                head.ISPASSIVE = model.ISPASSIVE;

                // TEXT tablosunu güncelle
                text.LANCODE = model.LANCODE;
                text.WCMSTEXT = model.WCMSTEXT;
                text.WCMLTEXT = model.WCMLTEXT;

                // OPERASYON tablosunu güncelle
                opr.WCMDOCFROM = model.WCMDOCFROM;
                opr.WCMDOCUNTIL = model.WCMDOCUNTIL;
                opr.OPRDOCTYPE = model.OPRDOCTYPE;

                // Değişiklikleri kaydet
                db.SaveChanges();

                // Başarılı güncelleme sonrası Index sayfasına yönlendirme
                return RedirectToAction("Index");
            }



            ViewBag.COMCODEList = db.BSMGRBCEGEN001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.COMCODE,
                                        Text = x.COMCODE
                                    }).ToList();

            ViewBag.WCMDOCTYPEList = db.BSMGRBCEWCM001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();

            ViewBag.CCMDOCTYPEList = db.BSMGRBCECCM001
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
            ViewBag.OPRDOCTYPEList = db.BSMGRBCEOPR001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();

            // Model geçerli değilse formu tekrar göster
            return View(model);
        }
    }
}