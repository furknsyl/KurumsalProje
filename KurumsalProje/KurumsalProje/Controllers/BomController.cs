using KurumsalProje.Models;
using KurumsalProje.ViewModels.BomViewModels;
using KurumsalProje.ViewModels.CostCenterViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace KurumsalProje.Controllers
{
    public class BomController : Controller
    {
        KKPBCEEntities db = new KKPBCEEntities();

        public ActionResult Index()
        {
            var data = from head in db.BSMGRBCEBOMHEAD
                       join text in db.BSMGRBCEBOMCONTENT
                       on new { head.COMCODE, head.BOMDOCTYPE, head.BOMDOCNUM } equals new { text.COMCODE, text.BOMDOCTYPE, text.BOMDOCNUM }
                       select new BomViewModel
                       {
                           COMCODE = head.COMCODE,
                           BOMDOCTYPE = head.BOMDOCTYPE,
                           BOMDOCNUM = head.BOMDOCNUM,
                           BOMDOCFROM = head.BOMDOCFROM,
                           BOMDOCUNTIL = head.BOMDOCUNTIL,
                           ISDELETED = head.ISDELETED ?? 0,
                           ISPASSIVE = head.ISPASSIVE ?? 0,
                           QUANTITY = head.QUANTITY,
                           DRAWNUM= head.DRAWNUM,
                           MATDOCTYPE= head.MATDOCTYPE,
                           MATDOCNUM= head.MATDOCNUM,
                           CONTENTNUM = text.CONTENTNUM,
                           COMPONENT = text.COMPONENT,
                           COMPBOMDOCTYPE = text.COMPBOMDOCTYPE,
                           COMPBOMDOCNUM = text.COMPBOMDOCNUM
                       };

            return View(data.ToList());
        }

        public ActionResult Details(string comcode, string bomdoctype, string bomdocnum)
        {
            var data = (from mathead in db.BSMGRBCEBOMHEAD
                        join mattext in db.BSMGRBCEBOMCONTENT
                        on new { mathead.COMCODE, mathead.BOMDOCTYPE, mathead.BOMDOCNUM }
                        equals new { mattext.COMCODE, mattext.BOMDOCTYPE, mattext.BOMDOCNUM }
                        where mathead.COMCODE == comcode &&
                        mathead.BOMDOCTYPE == bomdoctype &&
                        mathead.BOMDOCNUM == bomdocnum
                        select new BomViewModel
                        {
                            COMCODE = mathead.COMCODE,
                            BOMDOCTYPE = mathead.BOMDOCTYPE,
                            BOMDOCNUM = mathead.BOMDOCNUM,
                            BOMDOCFROM = mathead.BOMDOCFROM,
                            BOMDOCUNTIL = mathead.BOMDOCUNTIL,
                            ISDELETED = mathead.ISDELETED ?? 0,
                            ISPASSIVE = mathead.ISPASSIVE ?? 0,
                            QUANTITY = mathead.QUANTITY,
                            DRAWNUM = mathead.DRAWNUM,
                            MATDOCTYPE = mathead.MATDOCTYPE,
                            MATDOCNUM = mathead.MATDOCNUM,
                            CONTENTNUM = mattext.CONTENTNUM,
                            COMPONENT = mattext.COMPONENT,
                            COMPBOMDOCTYPE = mattext.COMPBOMDOCTYPE,
                            COMPBOMDOCNUM = mattext.COMPBOMDOCNUM
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

            ViewBag.MATDOCTYPEList = db.BSMGRBCEMAT001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();
            ViewBag.COMPBOMDOCTYPEList = db.BSMGRBCEBOM001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();


            return View();
        }

        [HttpPost]
        public ActionResult Create(BomViewModel model)
        {
            if (ModelState.IsValid)
            {
                // BSMGRBCEBOMHEAD tablosuna veri ekleme
                var head = new BSMGRBCEBOMHEAD
                {
                    COMCODE = model.COMCODE,
                    BOMDOCTYPE = model.BOMDOCTYPE,
                    BOMDOCNUM = model.BOMDOCNUM,
                    BOMDOCFROM = model.BOMDOCFROM,
                    BOMDOCUNTIL = model.BOMDOCUNTIL,
                    ISDELETED = model.ISDELETED,
                    ISPASSIVE = model.ISPASSIVE,
                    QUANTITY = model.QUANTITY,
                    DRAWNUM = model.DRAWNUM,
                    MATDOCTYPE = model.MATDOCTYPE,
                    MATDOCNUM = model.MATDOCNUM,
                };

                // BSMGRBCEBOMTEXT tablosuna veri ekleme
                var text = new BSMGRBCEBOMCONTENT
                {
                    COMCODE = model.COMCODE,
                    BOMDOCTYPE = model.BOMDOCTYPE,
                    BOMDOCNUM = model.BOMDOCNUM,
                    BOMDOCFROM = model.BOMDOCFROM,
                    BOMDOCUNTIL = model.BOMDOCUNTIL,
                    MATDOCTYPE = model.MATDOCTYPE,
                    MATDOCNUM = model.MATDOCNUM,
                    CONTENTNUM = model.CONTENTNUM,
                    COMPONENT = model.COMPONENT,
                    COMPBOMDOCTYPE = model.COMPBOMDOCTYPE,
                    COMPBOMDOCNUM = model.COMPBOMDOCNUM,
                    QUANTITY = model.QUANTITY,
                };

                // Veritabanına ekleme
                db.BSMGRBCEBOMHEAD.Add(head);
                db.BSMGRBCEBOMCONTENT.Add(text);
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

            ViewBag.BOMDOCTYPEList = db.BSMGRBCEBOM001
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
            ViewBag.COMPBOMDOCTYPEList = db.BSMGRBCEBOM001
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

            var head = db.BSMGRBCEBOMHEAD.FirstOrDefault(k => k.COMCODE == COMCODE);
            var text = db.BSMGRBCEBOMCONTENT.FirstOrDefault(k => k.COMCODE == COMCODE);
            if (head == null && text == null)
            {
                return HttpNotFound($"COMCODE: {COMCODE} ile eşleşen kayıt bulunamadı.");
            }

            db.BSMGRBCEBOMHEAD.Remove(head);  // Kaydı silme
            db.BSMGRBCEBOMCONTENT.Remove(text);  // Kaydı silme
            db.SaveChanges();  // Değişiklikleri kaydetme

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(string comcode, string bomdoctype, string bomdocnum)
        {
            // Güncellenecek kaydı bul
            var head = db.BSMGRBCEBOMHEAD.FirstOrDefault(h => h.COMCODE == comcode && h.BOMDOCTYPE == bomdoctype && h.BOMDOCNUM == bomdocnum);
            var text = db.BSMGRBCEBOMCONTENT.FirstOrDefault(t => t.COMCODE == comcode && t.BOMDOCTYPE == bomdoctype && t.BOMDOCNUM == bomdocnum);

            if (head == null || text == null)
            {
                return HttpNotFound(); // Kayıt bulunamazsa 404 döndür
            }

            // ViewModel'e verileri aktar
            var model = new BomViewModel
            {
                COMCODE = head.COMCODE,
                BOMDOCTYPE = head.BOMDOCTYPE,
                BOMDOCNUM = head.BOMDOCNUM,
                BOMDOCFROM = head.BOMDOCFROM,
                BOMDOCUNTIL = head.BOMDOCUNTIL,
                ISDELETED = head.ISDELETED ?? 0,
                ISPASSIVE = head.ISPASSIVE ?? 0,
                QUANTITY = head.QUANTITY,
                DRAWNUM = head.DRAWNUM,
                MATDOCTYPE = head.MATDOCTYPE,
                MATDOCNUM = head.MATDOCNUM,
                CONTENTNUM = text.CONTENTNUM,
                COMPONENT = text.COMPONENT,
                COMPBOMDOCTYPE = text.COMPBOMDOCTYPE,
                COMPBOMDOCNUM = text.COMPBOMDOCNUM
            };




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

            ViewBag.MATDOCTYPEList = db.BSMGRBCEMAT001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();
            ViewBag.COMPBOMDOCTYPEList = db.BSMGRBCEBOM001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
                                    }).ToList();

            return View(model); // Güncelleme formunu göster
        }

        [HttpPost]
        public ActionResult Edit(BomViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Güncellenecek kaydı bul
                var head = db.BSMGRBCEBOMHEAD.FirstOrDefault(h => h.COMCODE == model.COMCODE && h.BOMDOCTYPE == model.BOMDOCTYPE && h.BOMDOCNUM == model.BOMDOCNUM);
                var text = db.BSMGRBCEBOMCONTENT.FirstOrDefault(t => t.COMCODE == model.COMCODE && t.BOMDOCTYPE == model.BOMDOCTYPE && t.BOMDOCNUM == model.BOMDOCNUM);

                if (head == null || text == null)
                {
                    return HttpNotFound(); // Kayıt bulunamazsa 404 döndür
                }

                // HEAD tablosunu güncelle
                head.BOMDOCFROM = model.BOMDOCFROM;
                head.BOMDOCUNTIL = model.BOMDOCUNTIL;
                head.MATDOCTYPE = model.MATDOCTYPE;
                head.MATDOCNUM = model.MATDOCNUM;
                head.ISDELETED = model.ISDELETED;
                head.ISPASSIVE = model.ISPASSIVE;
                head.QUANTITY = model.QUANTITY;
                head.DRAWNUM = model.DRAWNUM;

                // TEXT tablosunu güncelle
                text.CONTENTNUM = model.CONTENTNUM;
                text.COMPONENT = model.COMPONENT;
                text.COMPBOMDOCTYPE = model.COMPBOMDOCTYPE;
                text.COMPBOMDOCNUM = model.COMPBOMDOCNUM;

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

            ViewBag.BOMDOCTYPEList = db.BSMGRBCEBOM001
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
            ViewBag.COMPBOMDOCTYPEList = db.BSMGRBCEBOM001
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