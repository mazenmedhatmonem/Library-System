using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library_project.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace Library_project.Controllers
{
    [Authorize(Roles = "Employee,Member")]
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        UserManager<ApplicationUser> UM = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        // GET: Books
        public ActionResult Index()
        {
            ViewBag.Role = (UM.GetRoles(Session["UserID"].ToString()))[0];
            return View(db.Book.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ISBN,Title,Category,Author,publisher,PublishingDate,Edition,Pages,NoOfCopy,Available,ShelfNumber,IsNewArrived")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Book.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ISBN,Title,Category,Author,publisher,PublishingDate,Edition,Pages,NoOfCopy,Available,ShelfNumber,IsNewArrived")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Book.Find(id);
            db.Book.Remove(book);
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

        public ActionResult FindBook(string value, int option)
        {
            List<Book> listToSend;

            if (option == 0)
                listToSend = db.Book.Where(a => (a.Title).Contains(value)).ToList();

            else if (option == 1)
            {
                listToSend = db.Book.Where(a => (a.Author).Contains(value)).ToList();

            }

            else
            {
                listToSend = db.Book.Where(a => (a.publisher).Contains(value)).ToList();

            }


            return PartialView(listToSend);
        }



    }
}
