using KurumsalProje.Models;
using KurumsalProje.ViewModels.MaterialViewModels;
using KurumsalProje.ViewModels.RouteViewModel;
using KurumsalProje.ViewModels.WorkCenterViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Services.Description;
using static System.Net.Mime.MediaTypeNames;

namespace KurumsalProje.Controllers
{
    public class RoutesController : Controller
    {
        // GET: Routes
        KKPBCEEntities db = new KKPBCEEntities();

        // Index: Tüm Route kayıtlarını listeleme
        public ActionResult Index()
        {
            var data = from head in db.BSMGRBCEROTHEAD
                       join text in db.BSMGRBCEROTBOMCONTENT
                       on new { head.COMCODE, head.ROTDOCTYPE, head.ROTDOCNUM } equals new { text.COMCODE, text.ROTDOCTYPE, text.ROTDOCNUM }
                       join opr in db.BSMGRBCEROTOPRCONTENT
                       on new { head.COMCODE, head.ROTDOCTYPE, head.ROTDOCNUM } equals new { opr.COMCODE, opr.ROTDOCTYPE, opr.ROTDOCNUM }
                       select new RouteViewModel
                       {
                           COMCODE = head.COMCODE,
                           ROTDOCTYPE = head.ROTDOCTYPE,
                           ROTDOCNUM = head.ROTDOCNUM,
                           ROTDOCFROM = head.ROTDOCFROM,
                           ROTDOCUNTIL = head.ROTDOCUNTIL,
                           MATDOCTYPE = head.MATDOCTYPE,
                           MATDOCNUM = head.MATDOCNUM,
                           QUANTITY = head.QUANTITY,
                           ISDELETED = head.ISDELETED ?? 0,
                           ISPASSIVE = head.ISPASSIVE ?? 0,
                           DRAWNUM = head.DRAWNUM,
                           OPRNUM = opr.OPRNUM,
                           WCMDOCTYPE = opr.WCMDOCTYPE,
                           WCMDOCNUM = opr.WCMDOCNUM,
                           OPRDOCTYPE = opr.OPRDOCTYPE,
                           SETUPTIME = opr.SETUPTIME ?? 0,
                           MACHINETIME = opr.MACHINETIME ?? 0,
                           LABOURTIME = opr.LABOURTIME ?? 0,
                           BOMDOCTYPE=text.BOMDOCTYPE,
                           BOMDOCNUM=text.BOMDOCNUM,
                           COMPONENT=text.COMPONENT,
                           CONTENTNUM=text.CONTENTNUM

                       };

            return View(data.ToList());
        }

        public ActionResult Details(string comcode, string rotdoctype, string rotdocnum)
        {
            var data = (from mathead in db.BSMGRBCEROTHEAD
                        join mattext in db.BSMGRBCEROTBOMCONTENT
                        on new { mathead.COMCODE, mathead.ROTDOCTYPE, mathead.ROTDOCNUM } equals new { mattext.COMCODE, mattext.ROTDOCTYPE, mattext.ROTDOCNUM }
                        join opr in db.BSMGRBCEROTOPRCONTENT
                        on new { mathead.COMCODE, mathead.ROTDOCTYPE, mathead.ROTDOCNUM } equals new { opr.COMCODE, opr.ROTDOCTYPE, opr.ROTDOCNUM }
                        where mathead.COMCODE == comcode &&
                        mathead.ROTDOCTYPE == rotdoctype &&
                        mathead.ROTDOCNUM == rotdocnum
                        select new RouteViewModel
                        {
                            COMCODE = mathead.COMCODE,
                            ROTDOCTYPE = mathead.ROTDOCTYPE,
                            ROTDOCNUM = mathead.ROTDOCNUM,
                            ROTDOCFROM = mathead.ROTDOCFROM,
                            ROTDOCUNTIL = mathead.ROTDOCUNTIL,
                            MATDOCTYPE = mathead.MATDOCTYPE,
                            MATDOCNUM = mathead.MATDOCNUM,
                            QUANTITY = mathead.QUANTITY,
                            ISDELETED = mathead.ISDELETED ?? 0,
                            ISPASSIVE = mathead.ISPASSIVE ?? 0,
                            DRAWNUM = mathead.DRAWNUM,
                            OPRNUM = opr.OPRNUM,
                            WCMDOCTYPE = opr.WCMDOCTYPE,
                            WCMDOCNUM = opr.WCMDOCNUM,
                            OPRDOCTYPE = opr.OPRDOCTYPE,
                            SETUPTIME = opr.SETUPTIME ?? 0,
                            MACHINETIME = opr.MACHINETIME ?? 0,
                            LABOURTIME = opr.LABOURTIME ?? 0,
                            BOMDOCTYPE = mattext.BOMDOCTYPE,
                            BOMDOCNUM = mattext.BOMDOCNUM,
                            COMPONENT = mattext.COMPONENT,
                            CONTENTNUM = mattext.CONTENTNUM
                        }).FirstOrDefault();

            return View(data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.COMCODEList = db.BSMGRBCEGEN001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.COMCODE,
                                        Text = x.COMCODE
                                    }).ToList();

            ViewBag.BOMDOCTYPEList = db.BSMGRBCEBOM001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();

            ViewBag.ROTDOCTYPEList = db.BSMGRBCEROT001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();

            ViewBag.MATDOCTYPEList = db.BSMGRBCEMAT001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();
            ViewBag.WCMDOCTYPEList = db.BSMGRBCEWCM001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();
            ViewBag.OPRDOCTYPEList = db.BSMGRBCEOPR001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();


            return View();
        }

        // Create: Yeni Route ekleme (POST)
        [HttpPost]
        public ActionResult Create(RouteViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Route Head tablosuna veri ekleme
                var head = new BSMGRBCEROTHEAD
                {
                    COMCODE = model.COMCODE,
                    ROTDOCTYPE = model.ROTDOCTYPE,
                    ROTDOCNUM = model.ROTDOCNUM,
                    ROTDOCFROM = model.ROTDOCFROM,
                    ROTDOCUNTIL = model.ROTDOCUNTIL,
                    MATDOCTYPE = model.MATDOCTYPE,
                    MATDOCNUM = model.MATDOCNUM,
                    QUANTITY = model.QUANTITY,
                    ISDELETED = model.ISDELETED,
                    ISPASSIVE = model.ISPASSIVE,
                    DRAWNUM = model.DRAWNUM
                };

                // Route Operation tablosuna veri ekleme
                var operation = new BSMGRBCEROTOPRCONTENT
                {
                    COMCODE = model.COMCODE,
                    ROTDOCTYPE = model.ROTDOCTYPE,
                    ROTDOCNUM = model.ROTDOCNUM,
                    ROTDOCFROM = model.ROTDOCFROM,
                    ROTDOCUNTIL = model.ROTDOCUNTIL,
                    MATDOCTYPE = model.MATDOCTYPE,
                    MATDOCNUM = model.MATDOCNUM,
                    OPRNUM = model.OPRNUM,
                    WCMDOCTYPE = model.WCMDOCTYPE,
                    WCMDOCNUM = model.WCMDOCNUM,
                    OPRDOCTYPE = model.OPRDOCTYPE,
                    SETUPTIME = model.SETUPTIME,
                    MACHINETIME = model.MACHINETIME,
                    LABOURTIME = model.LABOURTIME
                };
                var text = new BSMGRBCEROTBOMCONTENT
                {
                    COMCODE = model.COMCODE,
                    ROTDOCTYPE = model.ROTDOCTYPE,
                    ROTDOCNUM = model.ROTDOCNUM,
                    ROTDOCFROM = model.ROTDOCFROM,
                    ROTDOCUNTIL = model.ROTDOCUNTIL,
                    MATDOCTYPE = model.MATDOCTYPE,
                    MATDOCNUM = model.MATDOCNUM,
                    OPRNUM = model.OPRNUM,
                    BOMDOCTYPE = model.BOMDOCTYPE,
                    BOMDOCNUM = model.BOMDOCNUM,
                    CONTENTNUM = model.CONTENTNUM,
                    COMPONENT = model.COMPONENT,
                    QUANTITY = model.QUANTITY,
                };

                // Veritabanına ekleme
                db.BSMGRBCEROTHEAD.Add(head);
                db.BSMGRBCEROTOPRCONTENT.Add(operation);
                db.BSMGRBCEROTBOMCONTENT.Add(text);

                db.SaveChanges();

                // Başarılı ekleme sonrası Index sayfasına yönlendirme
                return RedirectToAction("Index");
            }

            // ViewBag verilerini tekrar yükle
            ViewBag.COMCODEList = db.BSMGRBCEGEN001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.COMCODE,
                                        Text = x.COMCODE
                                    }).ToList();

            ViewBag.BOMDOCTYPEList = db.BSMGRBCEBOM001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();

            ViewBag.ROTDOCTYPEList = db.BSMGRBCEROT001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();

            ViewBag.MATDOCTYPEList = db.BSMGRBCEMAT001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();
            ViewBag.WCMDOCTYPEList = db.BSMGRBCEWCM001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
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

        // Delete: Route silme
        public ActionResult Delete(string COMCODE)
        {
            if (string.IsNullOrEmpty(COMCODE))
            {
                return HttpNotFound("Geçersiz parametreler.");
            }

            var head = db.BSMGRBCEROTHEAD.FirstOrDefault(h => h.COMCODE == COMCODE);
            var operation = db.BSMGRBCEROTOPRCONTENT.FirstOrDefault(o => o.COMCODE == COMCODE);
            var text = db.BSMGRBCEROTBOMCONTENT.FirstOrDefault(k => k.COMCODE == COMCODE);

            if (head == null && operation == null&& text==null)
            {
                return HttpNotFound("Kayıt bulunamadı.");
            }

            db.BSMGRBCEROTHEAD.Remove(head);
            db.BSMGRBCEROTOPRCONTENT.Remove(operation);
            db.BSMGRBCEROTBOMCONTENT.Remove(text);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // Edit: Route güncelleme (GET)
        [HttpGet]
        public ActionResult Edit(string comcode, string rotdoctype, string rotdocnum)
        {
            var head = db.BSMGRBCEROTHEAD.FirstOrDefault(h => h.COMCODE == comcode && h.ROTDOCTYPE == rotdoctype && h.ROTDOCNUM == rotdocnum);
            var text = db.BSMGRBCEROTBOMCONTENT.FirstOrDefault(t => t.COMCODE == comcode && t.ROTDOCTYPE == rotdoctype && t.ROTDOCNUM == rotdocnum);
            var opr = db.BSMGRBCEROTOPRCONTENT.FirstOrDefault(k => k.COMCODE == comcode && k.ROTDOCTYPE == rotdoctype && k.ROTDOCNUM == rotdocnum);

            if (head == null || opr == null || text==null)
            {
                return HttpNotFound("Kayıt bulunamadı.");
            }

            var model = new RouteViewModel
            {
                COMCODE = head.COMCODE,
                ROTDOCTYPE = head.ROTDOCTYPE,
                ROTDOCNUM = head.ROTDOCNUM,
                ROTDOCFROM = head.ROTDOCFROM,
                ROTDOCUNTIL = head.ROTDOCUNTIL,
                MATDOCTYPE = head.MATDOCTYPE,
                MATDOCNUM = head.MATDOCNUM,
                QUANTITY = head.QUANTITY,
                ISDELETED = head.ISDELETED ?? 0,
                ISPASSIVE = head.ISPASSIVE ?? 0,
                DRAWNUM = head.DRAWNUM,
                OPRNUM = opr.OPRNUM,
                WCMDOCTYPE = opr.WCMDOCTYPE,
                WCMDOCNUM = opr.WCMDOCNUM,
                OPRDOCTYPE = opr.OPRDOCTYPE,
                SETUPTIME = opr.SETUPTIME ?? 0,
                MACHINETIME = opr.MACHINETIME ?? 0,
                LABOURTIME = opr.LABOURTIME ?? 0,
                BOMDOCTYPE = text.BOMDOCTYPE,
                BOMDOCNUM = text.BOMDOCNUM,
                CONTENTNUM = text.CONTENTNUM,
                COMPONENT = text.COMPONENT
            };

            // ViewBag verilerini tekrar yükle
            ViewBag.COMCODEList = db.BSMGRBCEGEN001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.COMCODE,
                                        Text = x.COMCODE
                                    }).ToList();

            ViewBag.BOMDOCTYPEList = db.BSMGRBCEBOM001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();

            ViewBag.ROTDOCTYPEList = db.BSMGRBCEROT001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();

            ViewBag.MATDOCTYPEList = db.BSMGRBCEMAT001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();
            ViewBag.WCMDOCTYPEList = db.BSMGRBCEWCM001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();
            ViewBag.OPRDOCTYPEList = db.BSMGRBCEOPR001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();
            return View(model);
        }

        // Edit: Route güncelleme (POST)
        [HttpPost]
        public ActionResult Edit(RouteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var head = db.BSMGRBCEROTHEAD.FirstOrDefault(h => h.COMCODE == model.COMCODE && h.ROTDOCTYPE == model.ROTDOCTYPE && h.ROTDOCNUM == model.ROTDOCNUM);
                var operation = db.BSMGRBCEROTOPRCONTENT.FirstOrDefault(o => o.COMCODE == model.COMCODE && o.ROTDOCTYPE == model.ROTDOCTYPE && o.ROTDOCNUM == model.ROTDOCNUM);
                var text = db.BSMGRBCEROTBOMCONTENT.FirstOrDefault(k => k.COMCODE == model.COMCODE && k.ROTDOCTYPE == model.ROTDOCTYPE && k.ROTDOCNUM == model.ROTDOCNUM);

                if (head == null || operation == null || text==null)
                {
                    return HttpNotFound("Kayıt bulunamadı.");
                }

                // Head güncelleme
                head.ROTDOCFROM = model.ROTDOCFROM;
                head.ROTDOCUNTIL = model.ROTDOCUNTIL;
                head.MATDOCTYPE = model.MATDOCTYPE;
                head.MATDOCNUM = model.MATDOCNUM;
                head.QUANTITY = model.QUANTITY;
                head.ISDELETED = model.ISDELETED;
                head.ISPASSIVE = model.ISPASSIVE;
                head.DRAWNUM = model.DRAWNUM;

                // Operation güncelleme
                operation.OPRNUM = model.OPRNUM;
                operation.WCMDOCTYPE = model.WCMDOCTYPE;
                operation.WCMDOCNUM = model.WCMDOCNUM;
                operation.OPRDOCTYPE = model.OPRDOCTYPE;
                operation.SETUPTIME = model.SETUPTIME;
                operation.MACHINETIME = model.MACHINETIME;
                operation.LABOURTIME = model.LABOURTIME;

                //text güncelleme

                text.BOMDOCTYPE = model.BOMDOCTYPE;
                text.BOMDOCNUM = model.BOMDOCNUM;
                text.COMPONENT=model.COMPONENT;
                text.CONTENTNUM = model.CONTENTNUM;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            // ViewBag verilerini tekrar yükle
            ViewBag.COMCODEList = db.BSMGRBCEGEN001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.COMCODE,
                                        Text = x.COMCODE
                                    }).ToList();

            ViewBag.BOMDOCTYPEList = db.BSMGRBCEBOM001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();

            ViewBag.ROTDOCTYPEList = db.BSMGRBCEROT001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();

            ViewBag.MATDOCTYPEList = db.BSMGRBCEMAT001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();
            ViewBag.WCMDOCTYPEList = db.BSMGRBCEWCM001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();
            ViewBag.OPRDOCTYPEList = db.BSMGRBCEOPR001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();
            return View(model);
        }
    }
}
