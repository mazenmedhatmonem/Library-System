using Library_project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Library_project.Controllers
{
    [Authorize(Roles = "Employee,BasicAdmin,Admin")]
    public class EmployeeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        UserManager<ApplicationUser> UM = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowProfile()
        {
            var UserID = Session["UserID"].ToString();
            ViewBag.UserID = UserID;
            return View(db.Worker.Where(a => a.WorkerID == UserID).FirstOrDefault());
        }

        public ActionResult UpdateProfile(string id)
        {
            //var UserID = Session["UserID"].ToString();

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
        
        public ActionResult SelectNodification()
        {
            var Nodifications= db.Nodification.Where(a => a.Role == "Employee");
            return View(Nodifications);
        }

        public ActionResult ReturnMembers()
        {
            return View(db.Member.ToList());
        }

        public ActionResult RequestedMembers()
        {
            ViewBag.State = "validate";
            return View("ReturnMembers", db.Member.Include(a => a.User).Where(a => a.User.IsValid == false).ToList());
        }

        public ActionResult findMember(string value, int option)
        {
            List<Member> listToSend;

            if (option == 0)
                listToSend = db.Member.Where(a => (a.FirstName + " " + a.LastName).Contains(value)).ToList();
            else
                listToSend = db.Member.Include(a =>a.User).Where(a => a.User.Email.Contains(value)).ToList();

            return PartialView(listToSend);
        }

        public ActionResult Validate(string id)
        {
            var val = UM.FindById(id);
            val.IsValid = true;
            UM.Update(val);
            return RedirectToAction(nameof(RequestedMembers));
        }

    }
}