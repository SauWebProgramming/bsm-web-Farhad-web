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
    public class UsersController : Controller
    {
        private CiftliktenEntities db = new CiftliktenEntities();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Authority);
            return View(users.ToList());        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.authorityId = new SelectList(db.Authority, "authorityId", "authorityName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Users users)
        {
            if (ModelState.IsValid)
            {
                users.authorityId = 2;
                db.Users.Add(users);
                db.SaveChanges();
                Session["uyeid"] = users.userId;
                Session["kullaniciadi"] = users.nickname;
                return RedirectToAction("Index", "Home");
            }
            
            return View(users);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.authorityId = new SelectList(db.Authority, "authorityId", "authorityName", users.authorityId);  
            return View(users);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userId,userName,userLastName,email,nickname,password,confirmPassword,userPhoto,authorityId")] Users users)
        {
            if (ModelState.IsValid)
            {
                users.authorityId = 2;
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            ViewBag.authorityId = new SelectList(db.Authority, "authorityId", "authorityName", users.authorityId);
            return View(users);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(Users users)
        {
            var login = db.Users.Where(u => u.email == users.email).SingleOrDefault();
            if (login.email == users.email && login.password == users.password)
            {
                Session["uyeid"] = login.userId;
                Session["kullaniciadi"] = login.nickname;
                Session["yetkiid"] = login.authorityId;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Uyari = "Kullanıcı Adı veya Şifre Hatalı";
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session["uyeid"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
