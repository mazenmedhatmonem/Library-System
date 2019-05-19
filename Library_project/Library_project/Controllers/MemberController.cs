using Library_project.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_project.Controllers
{
    [Authorize(Roles = "Member,Admin,BasicAdmin")]

    public class MemberController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Member
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowProfile()
        {

            var UserID = Session["UserID"].ToString();
            ViewBag.UserID = UserID;
            return View(db.Member.Where(a => a.MemberID == UserID).FirstOrDefault());
        }



        public ActionResult UpdateProfile(string id)
        {
            return View(db.Member.Where(a => a.MemberID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult UpdateProfile(Member member, string id)
        {

            Member OldMember = db.Member.Where(a => a.MemberID == id).Single();
            OldMember.FirstName = member.FirstName;
            OldMember.LastName = member.LastName;
            OldMember.Birthdate = member.Birthdate;
            OldMember.Phone = member.Phone;
            OldMember.Photo = member.Photo;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ShowBooks()
        {
            return View(db.Book.ToList());
        }

        public ActionResult ListReadingBooks()
        {
            List<Book> RBooks = new List<Book>();
            var UserID = Session["UserID"].ToString();
            var ReadingBooks = db.UserBook.Where(a => a.isRead == true && a.UserID == UserID && a.Date.Month == DateTime.Now.Month && a.IsReturned == false).ToList();
            foreach (var item in ReadingBooks)
            {
                RBooks.Add(db.Book.Where(a => a.ISBN == item.ISBN).SingleOrDefault());
            }
            return View(RBooks);
        }
        public ActionResult ListBorrowedBooks()
        {
            List<Book> BorBooks = new List<Book>();
            var UserID = Session["UserID"].ToString();
            var BorrowedBooks = db.UserBook.Where(a => a.isBorrowed == true && a.UserID == UserID && a.Date.Month == DateTime.Now.Month && a.IsReturned == false).ToList();
            foreach (var item in BorrowedBooks)
            {

                BorBooks.Add(db.Book.Where(a => a.ISBN == item.ISBN).SingleOrDefault());
            }
            return View(BorBooks);
        }

        public ActionResult NewArrivedBooks()
        {
            return View(db.Book.Where(a => a.IsNewArrived == true).ToList());
        }


        public ActionResult BorrowBooks(string id)
        {
            UserBook userbook = new UserBook();
            int ISBN = int.Parse(id);
            string userID = Session["UserID"].ToString();
            Book book = db.Book.Where(a => a.ISBN == ISBN && a.NoOfCopy > 1 && a.Available == true).SingleOrDefault();
            bool checkBorrow = CheckBorrowedRead(userID, ISBN);
            if (checkBorrow)
            {
                book.NoOfCopy -= 1;
                userbook.UserID = userID;
                userbook.ISBN = book.ISBN;
                userbook.Date = DateTime.Now;
                userbook.isBorrowed = true;
                db.UserBook.Add(userbook);
                db.SaveChanges();
                return RedirectToAction("ShowBooks");
            }
            else
            {
                ViewBag.message = "You can't borrow this book until you return the Borrowed Books";
                return View("Message");
            }


        }
        public ActionResult ReadBooks(string id)
        {
            UserBook userbook = new UserBook();
            int ISBN = int.Parse(id);
            string userID = Session["UserID"].ToString();
            Book book = db.Book.Where(a => a.ISBN == ISBN && a.NoOfCopy > 1 && a.Available == true).SingleOrDefault();
            bool checkRead = CheckBorrowedRead(userID, ISBN);

            if (checkRead)
            {
                book.NoOfCopy -= 1;
                userbook.UserID = userID;
                userbook.ISBN = book.ISBN;
                userbook.Date = DateTime.Now;
                userbook.isRead = true;
                db.UserBook.Add(userbook);
                db.SaveChanges();
                return RedirectToAction("ShowBooks");
            }
            else
            {
                ViewBag.message = "You can't read this book until you return the Borrowed Books";
                return View("Message");
            }


        }

        public ActionResult ReturnBorrowedBook(string id) //ReturnReadBook
        {
            int ISBN = int.Parse(id);
            string userID = Session["UserID"].ToString();
            UserBook userbook = db.UserBook.Where(a => a.ISBN == ISBN && a.UserID == userID).SingleOrDefault();
            ArchiveBook archiveBook = new ArchiveBook();
            Book book = db.Book.Where(a => a.ISBN == ISBN).SingleOrDefault();
            if (!userbook.IsReturned)
            {
                if (userbook.isBorrowed)
                {
                    archiveBook.UserID = userID;
                    archiveBook.ISBN = ISBN;
                    archiveBook.isBorrowed = true;
                }
                else if (userbook.isRead)
                {
                    archiveBook.UserID = userID;
                    archiveBook.ISBN = ISBN;
                    archiveBook.isRead = true;
                }

                book.NoOfCopy += 1;
                userbook.IsReturned = true;
                db.ArchiveBook.Add(archiveBook);
                db.UserBook.Remove(userbook);
                db.SaveChanges();
            }

            return View("Index");
        }
        public ActionResult HistoryBorrowed()
        {
            string userID = Session["UserID"].ToString();
            var archives = db.ArchiveBook.Where(a => a.UserID == userID && a.isBorrowed == true).ToList();
            List<Book> Books = new List<Book>();
            foreach (var item in archives)
            {

                Books.Add(db.Book.Where(a => a.ISBN == item.ISBN).SingleOrDefault());
            }
            return View(Books);
        }

        public ActionResult HistoryRead()
        {
            string userID = Session["UserID"].ToString();
            var archives = db.ArchiveBook.Where(a => a.UserID == userID && a.isRead == true).ToList();
            List<Book> Books = new List<Book>();
            foreach (var item in archives)
            {

                Books.Add(db.Book.Where(a => a.ISBN == item.ISBN).SingleOrDefault());
            }
            return View(Books);
        }

        public bool CheckBorrowedRead(string userID, int ISBN)
        {
            List<Boolean> check = new List<bool>();
            bool ReturnCheck = true;
            var userbooks = db.UserBook.Where(a => a.ISBN == ISBN && a.UserID == userID).ToList();
            foreach (var item in userbooks)
            {
                if (item.Duration <= 30)
                {
                    check.Add(true);
                }
                else
                {
                    check.Add(false);
                }
            }
            foreach (var item in check)
            {
                if (item == true)
                {
                    ReturnCheck = true;

                }
                else
                {
                    ReturnCheck = false;
                }
            }
            return ReturnCheck;
        }

        public ActionResult WaitValidation()
        {
            return View();
        }

    }
}