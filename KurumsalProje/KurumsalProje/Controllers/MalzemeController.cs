using KurumsalProje.Models;
using KurumsalProje.ViewModels.BomViewModels;
using KurumsalProje.ViewModels.MaterialViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalProje.Controllers
{
    public class MalzemeController : Controller
    {
        KKPBCEEntities db = new KKPBCEEntities();
        public ActionResult Index()
        {
            var data = (from mathead in db.BSMGRBCEMATHEAD
                        join mattext in db.BSMGRBCEMATTEXT
                        on new { mathead.COMCODE, mathead.MATDOCTYPE, mathead.MATDOCNUM }
                        equals new { mattext.COMCODE, mattext.MATDOCTYPE, mattext.MATDOCNUM }
                        select new MalzemeKayitViewModel
                        {
                            COMCODE = mathead.COMCODE,
                            MATDOCTYPE = mathead.MATDOCTYPE,
                            MATDOCNUM = mathead.MATDOCNUM,
                            MATDOCFROM = mathead.MATDOCFROM,
                            MATDOCUNTIL = mathead.MATDOCUNTIL,
                            SUPPLYTYPE = mathead.SUPPLYTYPE,
                            STUNIT = mathead.STUNIT,
                            NETWEIGHT = mathead.NETWEIGHT,
                            NWUNIT = mathead.NWUNIT,
                            BRUTWEIGHT = mathead.BRUTWEIGHT ?? 0,
                            BWUNIT = mathead.BWUNIT,
                            ISBOM = mathead.ISBOM,
                            BOMDOCTYPE = mathead.BOMDOCTYPE,
                            BOMDOCNUM = mathead.BOMDOCNUM,
                            ISROUTE = mathead.ISROUTE ?? 0,
                            ROTDOCTYPE = mathead.ROTDOCTYPE,
                            ROTDOCNUM = mathead.ROTDOCNUM,
                            ISDELETED = mathead.ISDELETED,
                            ISPASSIVE = mathead.ISPASSIVE,

                            // BSMGRBCEMATTEXT alanları
                            LANCODE = mattext.LANCODE,
                            MATSTEXT = mattext.MATSTEXT,
                            MATLTEXT = mattext.MATLTEXT
                        }).ToList();

            return View(data);
        }

        public ActionResult Detay(string comcode,string matdoctype,string matdocnum)
        {
            var data = (from mathead in db.BSMGRBCEMATHEAD
                        join mattext in db.BSMGRBCEMATTEXT
                        on new { mathead.COMCODE, mathead.MATDOCTYPE, mathead.MATDOCNUM }
                        equals new { mattext.COMCODE, mattext.MATDOCTYPE, mattext.MATDOCNUM }
                        where mathead.COMCODE == comcode &&
                        mathead.MATDOCTYPE == matdoctype &&
                        mathead.MATDOCNUM == matdocnum
                        select new MalzemeKayitViewModel
                        {
                            COMCODE = mathead.COMCODE,
                            MATDOCTYPE = mathead.MATDOCTYPE,
                            MATDOCNUM = mathead.MATDOCNUM,
                            MATDOCFROM = mathead.MATDOCFROM,
                            MATDOCUNTIL = mathead.MATDOCUNTIL,
                            SUPPLYTYPE = mathead.SUPPLYTYPE,
                            STUNIT = mathead.STUNIT,
                            NETWEIGHT = mathead.NETWEIGHT,
                            NWUNIT = mathead.NWUNIT,
                            BRUTWEIGHT = mathead.BRUTWEIGHT ?? 0,
                            BWUNIT = mathead.BWUNIT,
                            ISBOM = mathead.ISBOM,
                            BOMDOCTYPE = mathead.BOMDOCTYPE,
                            BOMDOCNUM = mathead.BOMDOCNUM,
                            ISROUTE = mathead.ISROUTE ?? 0,
                            ROTDOCTYPE = mathead.ROTDOCTYPE,
                            ROTDOCNUM = mathead.ROTDOCNUM,
                            ISDELETED = mathead.ISDELETED,
                            ISPASSIVE = mathead.ISPASSIVE,
                            LANCODE = mattext.LANCODE,
                            MATSTEXT = mattext.MATSTEXT,
                            MATLTEXT = mattext.MATLTEXT
                        }).FirstOrDefault();

            return View(data);
        }

        [HttpGet]
        public ActionResult YeniKayıt()
        {
            ViewBag.COMCODEList = db.BSMGRBCEGEN001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.COMCODE,
                                        Text = x.COMCODE
                                    }).ToList();

            ViewBag.STUNITList = db.BSMGRBCEGEN005
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.UNITCODE,
                                        Text = x.UNITCODE
                                    }).ToList();

            ViewBag.MATDOCTYPEList = db.BSMGRBCEMAT001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
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
            ViewBag.LANCODEList = db.BSMGRBCEGEN002
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.LANCODE,
                                        Text = x.LANCODE
                                    }).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult YeniKayıt(MalzemeKayitViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                    // BSMGRBCEMATHEAD tablosuna kayıt ekleme
                    var mathead = new BSMGRBCEMATHEAD
                    {
                        COMCODE = model.COMCODE,
                        MATDOCTYPE = model.MATDOCTYPE,
                        MATDOCNUM = model.MATDOCNUM,
                        MATDOCFROM = model.MATDOCFROM,
                        MATDOCUNTIL = model.MATDOCUNTIL,
                        SUPPLYTYPE = model.SUPPLYTYPE,
                        STUNIT = model.STUNIT,
                        NETWEIGHT = model.NETWEIGHT,
                        NWUNIT = model.NWUNIT,
                        BRUTWEIGHT = model.BRUTWEIGHT,
                        BWUNIT = model.BWUNIT,
                        ISBOM = model.ISBOM,
                        BOMDOCTYPE = model.BOMDOCTYPE,
                        BOMDOCNUM = model.BOMDOCNUM,
                        ISROUTE = model.ISROUTE,
                        ROTDOCTYPE = model.ROTDOCTYPE,
                        ROTDOCNUM = model.ROTDOCNUM,
                        ISDELETED = model.ISDELETED,
                        ISPASSIVE = model.ISPASSIVE
                    };
                    db.BSMGRBCEMATHEAD.Add(mathead);

                    // BSMGRBCEMATTEXT tablosuna kayıt ekleme
                    var mattext = new BSMGRBCEMATTEXT
                    {
                        COMCODE = model.COMCODE,
                        MATDOCTYPE = model.MATDOCTYPE,
                        MATDOCNUM = model.MATDOCNUM,
                        MATDOCFROM = model.MATDOCFROM,
                        MATDOCUNTIL = model.MATDOCUNTIL,
                        LANCODE = model.LANCODE,
                        MATSTEXT = model.MATSTEXT,
                        MATLTEXT = model.MATLTEXT
                    };
                    db.BSMGRBCEMATTEXT.Add(mattext);

                    // Değişiklikleri kaydet
                    db.SaveChanges();

                    return RedirectToAction("Index");
                
            }
            ViewBag.COMCODEList = db.BSMGRBCEGEN001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.COMCODE,
                                        Text = x.COMCODE
                                    }).ToList();

            ViewBag.STUNITList = db.BSMGRBCEGEN005
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.UNITCODE,
                                        Text = x.UNITCODE
                                    }).ToList();

            ViewBag.MATDOCTYPEList = db.BSMGRBCEMAT001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
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
            ViewBag.LANCODEList = db.BSMGRBCEGEN002
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.LANCODE,
                                        Text = x.LANCODE
                                    }).ToList();

            return View(model);
        }

        public ActionResult MalzemeSil(string COMCODE)
        {
            if (string.IsNullOrEmpty(COMCODE))
            {
                return HttpNotFound("COMCODE parametresi geçerli değil.");
            }

            var head = db.BSMGRBCEMATHEAD.FirstOrDefault(k => k.COMCODE == COMCODE);
            var text = db.BSMGRBCEMATTEXT.FirstOrDefault(k => k.COMCODE == COMCODE);
            if (head == null && text == null)
            {
                return HttpNotFound($"COMCODE: {COMCODE} ile eşleşen kayıt bulunamadı.");
            }

            db.BSMGRBCEMATHEAD.Remove(head);  // Kaydı silme
            db.BSMGRBCEMATTEXT.Remove(text);  // Kaydı silme
            db.SaveChanges();  // Değişiklikleri kaydetme

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult KayıtGuncelle(string comcode,string matdoctype,string matdocnum)
        {
            var head = db.BSMGRBCEMATHEAD.FirstOrDefault(h => h.COMCODE == comcode && h.MATDOCTYPE == matdoctype && h.MATDOCNUM == matdocnum);
            var text = db.BSMGRBCEMATTEXT.FirstOrDefault(t => t.COMCODE == comcode && t.MATDOCTYPE == matdoctype && t.MATDOCNUM == matdocnum);

            if (head == null || text == null)
            {
                return HttpNotFound(); // Kayıt bulunamazsa 404 döndür
            }

            // ViewModel'e verileri aktar
            var model = new MalzemeKayitViewModel
            {
                COMCODE = head.COMCODE,
                MATDOCFROM = head.MATDOCFROM,
                MATDOCUNTIL = head.MATDOCUNTIL,
                SUPPLYTYPE = head.SUPPLYTYPE,
                STUNIT = head.STUNIT,
                NETWEIGHT = head.NETWEIGHT,
                NWUNIT = head.NWUNIT,
                BRUTWEIGHT = head.BRUTWEIGHT ?? 0,
                BWUNIT = head.BWUNIT,
                MATDOCTYPE = head.MATDOCTYPE,
                MATDOCNUM = head.MATDOCNUM,
                ISBOM = head.ISBOM,
                BOMDOCNUM = head.BOMDOCNUM,
                BOMDOCTYPE = head.BOMDOCTYPE,
                ISROUTE = head.ISROUTE ?? 0,
                ROTDOCTYPE = head.ROTDOCTYPE,
                ROTDOCNUM = head.ROTDOCNUM,
                ISDELETED = head.ISDELETED,
                ISPASSIVE = head.ISPASSIVE,
                LANCODE = text.LANCODE,
                MATSTEXT = text.MATSTEXT,
                MATLTEXT = text.MATLTEXT
            };

            ViewBag.COMCODEList = db.BSMGRBCEGEN001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.COMCODE,
                                        Text = x.COMCODE
                                    }).ToList();

            ViewBag.STUNITList = db.BSMGRBCEGEN005
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.UNITCODE,
                                        Text = x.UNITCODE
                                    }).ToList();

            ViewBag.MATDOCTYPEList = db.BSMGRBCEMAT001
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.DOCTYPE,
                                        Text = x.DOCTYPE
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
            ViewBag.LANCODEList = db.BSMGRBCEGEN002
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.LANCODE,
                                        Text = x.LANCODE
                                    }).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult KayıtGuncelle(MalzemeKayitViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Güncellenecek kaydı bul
                var head = db.BSMGRBCEMATHEAD.FirstOrDefault(h => h.COMCODE == model.COMCODE && h.MATDOCTYPE == model.MATDOCTYPE && h.MATDOCNUM == model.MATDOCNUM);
                var text = db.BSMGRBCEMATTEXT.FirstOrDefault(t => t.COMCODE == model.COMCODE && t.MATDOCTYPE == model.MATDOCTYPE && t.MATDOCNUM == model.MATDOCNUM);

                if (head == null || text == null)
                {
                    return HttpNotFound(); // Kayıt bulunamazsa 404 döndür
                }

                // HEAD tablosunu güncelle
                head.MATDOCFROM = model.MATDOCFROM;
                head.MATDOCUNTIL = model.MATDOCUNTIL;
                head.SUPPLYTYPE = model.SUPPLYTYPE;
                head.STUNIT = model.STUNIT;
                head.NETWEIGHT = model.NETWEIGHT;
                head.NWUNIT = model.NWUNIT;
                head.BRUTWEIGHT = model.BRUTWEIGHT;
                head.BWUNIT = model.BWUNIT;
                head.ISBOM = model.ISBOM;
                head.BOMDOCTYPE = model.BOMDOCTYPE;
                head.BOMDOCNUM = model.BOMDOCNUM;
                head.ISROUTE = model.ISROUTE;
                head.ROTDOCTYPE = model.ROTDOCTYPE;
                head.ROTDOCNUM = model.ROTDOCNUM;
                head.ISDELETED = model.ISDELETED;
                head.ISPASSIVE = model.ISPASSIVE;


                // TEXT tablosunu güncelle
                text.LANCODE = model.LANCODE;
                text.MATSTEXT = model.MATSTEXT;
                text.MATLTEXT = model.MATLTEXT;

                // Değişiklikleri kaydet
                db.SaveChanges();

                // Başarılı güncelleme sonrası Index sayfasına yönlendirme
                return RedirectToAction("Index");
            }


            ViewBag.STUNITList = db.BSMGRBCEGEN005
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.UNITCODE,
                                        Text = x.UNITCODE
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
            ViewBag.LANCODEList = db.BSMGRBCEGEN002
                                    .Select(x => new SelectListItem
                                    {
                                        Value = x.LANCODE,
                                        Text = x.LANCODE
                                    }).ToList();

            return View(model);
        }
    }
}