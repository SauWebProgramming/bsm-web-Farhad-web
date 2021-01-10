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
    public class AuthoritiesController : Controller
    {
        private CiftliktenEntities db = new CiftliktenEntities();

        // GET: Authorities
        public ActionResult Index()
        {
            return View(db.Authority.ToList());
        }

        // GET: Authorities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authority authority = db.Authority.Find(id);
            if (authority == null)
            {
                return HttpNotFound();
            }
            return View(authority);
        }

        // GET: Authorities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authorities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "authorityId,authorityName")] Authority authority)
        {
            if (ModelState.IsValid)
            {
                db.Authority.Add(authority);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(authority);
        }

        // GET: Authorities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authority authority = db.Authority.Find(id);
            if (authority == null)
            {
                return HttpNotFound();
            }
            return View(authority);
        }

        // POST: Authorities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "authorityId,authorityName")] Authority authority)
        {
            if (ModelState.IsValid)
            {
                db.Entry(authority).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(authority);
        }

        // GET: Authorities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authority authority = db.Authority.Find(id);
            if (authority == null)
            {
                return HttpNotFound();
            }
            return View(authority);
        }

        // POST: Authorities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Authority authority = db.Authority.Find(id);
            db.Authority.Remove(authority);
            db.SaveChanges();
            return RedirectToAction("Index");
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
