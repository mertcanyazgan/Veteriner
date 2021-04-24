using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using veteriner.Models;

namespace veteriner.Controllers
{
    public class hastaController : Controller
    {
        // GET: hasta
        hasta_takip_sistemiEntities db = new hasta_takip_sistemiEntities();
        [Authorize]
        public ActionResult Index(string ara)
        {
            var list = db.detay.ToList();
            if (!string.IsNullOrEmpty(ara))
            {
                list = list.Where(x => x.Hasta_ad.Contains(ara)|| x.Sahip_ad.Contains(ara)||x.Tani.Contains(ara)|| x.Cins.Contains(ara)).ToList();
            }
            return View(list);
        }

        public ActionResult Index()
            
        {   var list = db.detay.ToList();
            return View(list);
        }
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(detay Data, HttpPostedFileBase File)
        {
            string path = Path.Combine("~/Content/image" + File.FileName);
            File.SaveAs(Server.MapPath(path));
            Data.foto = File.FileName.ToString();
            db.detay.Add(Data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var hasta = db.detay.Where(x => x.id == id).FirstOrDefault();
            db.detay.Remove(hasta);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Guncelle(int id)
        {
            var guncelle = db.detay.Where(x => x.id == id).FirstOrDefault();
            return View(guncelle);
        }
        public ActionResult Guncelle(detay model ,HttpPostedFileBase File)
        {
            var detay = db.detay.Find(model.id);
            detay.Hasta_ad = model.Hasta_ad;
            detay.foto = File.FileName.ToString();
            detay.Sahip_ad = model.Sahip_ad;
            detay.Cins = model.Cins;
            detay.Tani = model.Tani;

            return View(guncelle);
        }
    }

}