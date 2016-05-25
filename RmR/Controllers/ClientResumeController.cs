using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RmR.DAL;
using RmR.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RmR.ViewModels;

namespace RmR.Controllers
{
    public class ClientResumeController : Controller
    {
        private ResumeContext db = new ResumeContext();

        // GET: ClientResume
        public ActionResult Index()
        {
            //return View();
            string userId = User.Identity.GetUserId();
            if (!string.IsNullOrEmpty(userId))
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));
                var currentClient = manager.FindByEmail(User.Identity.GetUserName());



                Client client = db.Clients                    
                    .Where(i => i.Email == currentClient.Email).Single();

                var resumes = db.Resumes.Where(i => i.ClientID == client.ID).ToList();



                return View(resumes);
            }
            else
            {
                return HttpNotFound();
            }


        }

        // GET: ClientResume/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resume resume = db.Resumes.Find(id);
            if (resume == null)
            {
                return HttpNotFound();
            }
            return View(resume);
        }

        // GET: ClientResume/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientResume/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResumeName,Description")] Resume resume, HttpPostedFileBase FileName)
        {
            if (ModelState.IsValid)
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));
                var currentClient = manager.FindByEmail(User.Identity.GetUserName());

                Client client = db.Clients
                    .Include(r=>r.Resumes)
                    .Where(i => i.Email == currentClient.Email).Single();


                resume.ClientID = client.ID;
                resume.CreatedOn = DateTime.Now;
                resume.ExpertID = null;
                db.Resumes.Add(resume);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resume);
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
