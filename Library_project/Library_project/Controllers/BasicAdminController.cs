using Library_project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_project.Controllers
{
    [Authorize(Roles = "BasicAdmin")]
    public class BasicAdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: BasicAdmin
        public ActionResult Index()
        {
            return View();
        }

        //show report
        //
        public ActionResult ShowProfile()
        {
            var UserID = Session["UserID"].ToString();
            ViewBag.UserID = UserID;
            return View(db.Worker.Where(a => a.WorkerID == UserID).FirstOrDefault());
        }

        public ActionResult UpdateProfile(string id)
        {
            

            return View(db.Worker.Where(a => a.WorkerID == id).FirstOrDefault());
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

        public ActionResult ShowAdmin()
        {
            return View(db.Worker.Where(a => a.UserType == "Admin").ToList());
        }

        public ActionResult CreateAdmin(string id)
        {
            ViewBag.InsertedRole = id;

            return View("~/Views/Account/Register.cshtml");

        }

        public ActionResult EditAdmin(string id)
        {
            return View("~/Views/BasicAdmin/UpdateProfile.cshtml", db.Worker.Where(a => a.WorkerID == id).FirstOrDefault());
        }

        public ActionResult ShowEmployee()
        {
            return RedirectToAction("ShowEmployee", "Admin");
        }

        public ActionResult AddEmployee()
        {
            return RedirectToAction("CreateEmployee", "Admin");
        }
        public ActionResult EditEmployee()
        {
            return RedirectToAction("EditEmployee", "Admin");
        }
        public ActionResult DeleteEmployee()
        {
            return RedirectToAction("Delete", "Account");
        }
        public ActionResult ShowMember()
        {
            return RedirectToAction("ReturnMembers", "Employee");
        }


    }
}