using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ciftlikten.Models;

namespace Ciftlikten.Controllers
{
    public class HomeController : Controller
    {
        private CiftliktenEntities db = new CiftliktenEntities();
        public ActionResult Index()
        {
            var urun = db.urun.Include(b => b.Categories);
            return View(urun.ToList());
        }
        public ActionResult KategoriDetay(int id)
        {
            var urun = db.urun.Where(x => x.categoryId == id).ToList();
            return View(urun);
        }
        public ActionResult KetegoriPartial()
        {
            var kategoriler = db.Categories.ToList();
            return View(kategoriler);
        }

        public ActionResult urunDetay(int id)
        {
            return View(db.urun.Find(id));
        }

        public JsonResult Yorum(string yorum, int urunid)
        {
            var üyeid = Session["uyeid"];
            if (yorum == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            db.Comments.Add(new Comments { userId = Convert.ToInt32(üyeid), urunId = urunid, commentContent = yorum });
            db.SaveChanges();
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}