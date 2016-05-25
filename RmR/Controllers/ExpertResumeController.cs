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
using RmR.Helpers;

namespace RmR.Controllers
{
    [Authorize(Roles = "expert")]
    public class ExpertResumeController : Controller
    {
        
        private ResumeContext db = new ResumeContext();

        // GET: ExpertResume
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            if (!string.IsNullOrEmpty(userId))
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));
                var currentExpert = manager.FindByEmail(User.Identity.GetUserName());

                Expert expert = db.Experts
                    .Where(i => i.Email == currentExpert.Email).Single();

                var resumes = db.Resumes.Where(i => i.ExpertID == expert.ID).ToList();
                
                return View(resumes);

            }
            else
            {
                return HttpNotFound();
            }
            
        }

        // get:ExpertResume/unassigned

        public ActionResult Work()
        {
            string userId = User.Identity.GetUserId();
            if (!string.IsNullOrEmpty(userId))
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));
                var currentExpert = manager.FindByEmail(User.Identity.GetUserName());

                Expert expert = db.Experts
                    .Where(i => i.Email == currentExpert.Email).Single();

                var resumes = db.Resumes.Where(i => i.Status == Status.Submit).ToList();

                return View(resumes);

            }
            else
            {
                return HttpNotFound();
            }
            
        }

        // GET: ExpertResume/Details/5
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

        // GET: ExpertResume/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: ExpertResume/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResumeID,Status,ResumeName,CreatedOn,ClientID,ExpertID")] Resume resume, HttpPostedFileBase FileName)
        {
            if (ModelState.IsValid)
            {
               

                if (FileName != null && FileName.ContentLength > 0)
                {
                    var validFileType = new string[]
                    {
                        "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                    };

                    if (!validFileType.Contains(FileName.ContentType))
                    {
                        //file being upload in not pgn - display error
                        ModelState.AddModelError("", "Please choose a DOCX file to upload.");
                        return View(resume);
                    }

                    

                    //Rename, upload file
                    FileUploads fileUpload = new FileUploads();
                    FileResults fileResult = fileUpload.RenameUploadFile(FileName, resume.ResumeID + resume.ResumeName);

                    resume.CompletedOn = DateTime.Now;
                    if (resume.Status == Status.Review)
                    {
                        resume.Status = Status.Complete;
                    }
                    
                    db.Entry(resume).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    //nothing to upload - display error
                    ModelState.AddModelError("", "You have not added a Resume file to upload");
                    return View(resume);

                }
               
              
            }
            return View(resume);
        }
        public FileResult downloadFile(int ID)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));
            var currentExpert = manager.FindByEmail(User.Identity.GetUserName());

            Expert expert = db.Experts
                .Where(i => i.Email == currentExpert.Email).Single();

            

            Resume resume = db.Resumes.Find(ID);

            resume.ExpertID = expert.ID;
            resume.Status = Status.Review;

            db.Entry(resume).State = EntityState.Modified;
            db.SaveChanges();

            return new FilePathResult("~\\ResumeFile\\" + resume.ResumeID + resume.ResumeName + resume.CreatedOn.ToString("MMddyyyy") + ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
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
