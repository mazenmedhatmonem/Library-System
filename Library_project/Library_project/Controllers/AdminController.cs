using Library_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Library_project.Controllers
{
   [Authorize(Roles ="Admin,BasicAdmin")]
   

    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // show report 

        public ActionResult ShowProfile()
        {
            var UserID = Session["UserID"].ToString();
            ViewBag.UserID = UserID;
            var worker = db.Worker.Where(a => a.WorkerID == UserID).FirstOrDefault();
            return View(worker);
        }

        public ActionResult UpdateProfile(string id)
        {
            var worker = db.Worker.Where(a => a.WorkerID == id).FirstOrDefault();
            return View(worker);

        }

        [HttpPost]

        public ActionResult UpdateProfile(Worker worker, string id, HttpPostedFileBase file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var guid = Guid.NewGuid().ToString();
            var path = Path.Combine(Server.MapPath("~/images"), guid + fileName);
            file.SaveAs(path);
            string fl = path.Substring(path.LastIndexOf("\\"));
            string[] split = fl.Split('\\');
            string newpath = split[1];
            string imagepath = "images/" + newpath;
            TempData["Success"] = "Upload successful";
            Worker OLdWorker = db.Worker.Where(a => a.WorkerID == id).Single();
            OLdWorker.FirstName = worker.FirstName;
            OLdWorker.LastName = worker.LastName;
            OLdWorker.Birthdate = worker.Birthdate;
            OLdWorker.HireDate = worker.HireDate;
            OLdWorker.Phone = worker.Phone;
            OLdWorker.Photo = imagepath;
            OLdWorker.Salary = worker.Salary;
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult ShowEmployee()
        {
            List<Worker> Workers = db.Worker.Where(a => a.UserType == ("Employee")).ToList();
            return View(Workers);
        }

        public ActionResult CreateEmployee(string id)
        {
            ViewBag.Insertedrole = id;
            return View("~/Views/Account/Register.cshtml");
        }

        public ActionResult EditEmployee(string id)
        {
            return View("~/Views/Admin/UpdateProfile.cshtml", db.Worker.Where(a => a.WorkerID == id).FirstOrDefault());
        }


        public ActionResult SelectNodification()
        {
            var Nodifications = db.Nodification.Where(a => a.Role == "Admin");
            return View(Nodifications);
        }
        public ActionResult ShowMember()
        {
            return RedirectToAction("", "Employee");
        }

        public ActionResult DeleteMember()
        {
            return RedirectToAction("Delete", "Account");
        }
    }
}