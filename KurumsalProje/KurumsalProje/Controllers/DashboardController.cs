using KurumsalProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalProje.Controllers
{

    public class DashboardController : Controller
    {
        KKPBCEEntities db = new KKPBCEEntities();

        public ActionResult Index()
        {
            var totalCompanies = db.BSMGRBCEGEN001.Count(p => !string.IsNullOrEmpty(p.COMCODE));

            var totalMaterial = db.BSMGRBCEMATHEAD.Count(p => !string.IsNullOrEmpty(p.COMCODE));

            var averageNetWeight = Math.Round(db.BSMGRBCEMATHEAD
            .Select(x => x.NETWEIGHT ?? 0)
            .Average(x => (double)x),2);

            var avgworktime = Math.Round(db.BSMGRBCEWCMHEAD
                         .Select(x => x.WORKTIME ?? 0)
                         .Average());

            ViewBag.TotalMaterial = totalMaterial;

            ViewBag.AvgWorkTime = avgworktime;

            ViewBag.TotalCompanies = totalCompanies;

            ViewBag.AvgWeight = averageNetWeight;

            return View();

        }
    }
}