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
    [Authorize(Roles = "client")]
    public class ClientResumeController : Controller
    {
        private ResumeContext db = new ResumeContext();

        // GET: ClientResume
        public ActionResult Index()
        {

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

                if(FileName !=null && FileName.ContentLength > 0)
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

                    Client client = db.Clients
                    .Include(r => r.Resumes)
                    .Where(i => i.Email == currentClient.Email).Single();

                    resume.CreatedOn = DateTime.Now;
                    resume.ClientID = client.ID;
                    resume.ExpertID = null;
                    db.Resumes.Add(resume);
                    db.SaveChanges();
                    string resumeName = resume.ResumeID + resume.ResumeName + DateTime.Now.ToString("MMddyyyy");

                    //Rename, upload file
                    FileUploads fileUpload = new FileUploads();
                    FileResults fileResult = fileUpload.RenameUploadFile(FileName, resumeName);


                   
                    



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
            Resume resume = db.Resumes.Find(ID);
            return new FilePathResult("~\\ResumeFile\\" + resume.ResumeID + resume.ResumeName + ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
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
