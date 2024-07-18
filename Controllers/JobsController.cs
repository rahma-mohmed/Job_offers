using Job_offers.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System.Threading.Tasks;


namespace Job_offers.Controllers
{
    public class JobsController : Controller
    {
        private readonly JobContext context;
        UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManger;
        private readonly IWebHostEnvironment webHostEnvironment;

        public JobsController(JobContext _context , UserManager<IdentityUser> _usermanager , IWebHostEnvironment _webHostEnvironment)
        {
            context = _context;
            userManager = _usermanager;
            webHostEnvironment = _webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Job> categories = context.Jobs.ToList();
            List<Category> ctg = context.Categories.ToList();
            ViewData["Categories"] = ctg;
            return View(categories);
        }

        public IActionResult Add()
        {
            Job jb = new Job();
            List<Category> ctg = context.Categories.ToList();
            ViewData["Categories"] = ctg;
            return View("Add", jb);
        }

        [HttpPost]
        public IActionResult Add(Job Jb , IFormFile JobImage)
        {
            //if (ModelState.IsValid) {
                string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");

                if (JobImage != null)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(JobImage.FileName);

                    using (var filestream = new FileStream(Path.Combine(uploadFolder, uniqueFileName), FileMode.Create))
                    {
                        JobImage.CopyTo(filestream);
                    }

                    Jb.JobImage = Path.Combine("Images", uniqueFileName);
                }
                    Jb.UserId = userManager.GetUserId(User);
                    context.Jobs.Add(Jb);
                    context.SaveChanges();
                    return RedirectToAction("Index");
    
            //}
            List<Category> ctg = context.Categories.ToList();
            ViewData["Categories"] = ctg;
            return View("Add", Jb);
        }

        public IActionResult Edit(int id)
        {
            Job Jb = context.Jobs.FirstOrDefault(x => x.Id == id);
            List<Category> ctg = context.Categories.ToList();
            ViewData["Categories"] = ctg;
            return View(Jb);
        }

        [HttpPost]
        public IActionResult SaveEdit([FromRoute] int id, Job Jb , IFormFile JobImage)
        {
            Job Job = context.Jobs.FirstOrDefault(x => x.Id == id);

            List<Category> ctg = context.Categories.ToList();
            ViewData["Categories"] = ctg;

            Job.Id = Jb.Id;
            Job.JobTitle = Jb.JobTitle;
            Job.JobContent = Jb.JobContent;

            string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");

            if (JobImage != null)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(JobImage.FileName);

                using (var filestream = new FileStream(Path.Combine(uploadFolder, uniqueFileName), FileMode.Create))
                {
                    JobImage.CopyTo(filestream);
                }

                Job.JobImage = Path.Combine("Images", uniqueFileName);
            }
            Job.CategoryId = Jb.CategoryId;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Confirm_delete(int id)
        {
            Job Jb = context.Jobs.FirstOrDefault(c => c.Id == id);
            return View(Jb);
        }

        public IActionResult Delete(int id)
        {
            Job Jb = context.Jobs.FirstOrDefault(c => c.Id == id);
            context.Jobs.Remove(Jb);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            Job Jb = context.Jobs.FirstOrDefault(c => c.Id == id);
            if(Jb == null)
            {
                return NotFound();
            }
            List<Category> ctg = context.Categories.ToList();
            ViewData["Categories"] = ctg;
            HttpContext.Session.SetInt32("JobId", id);
            return View(Jb);
        }

        [Authorize]
        public IActionResult Apply()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Apply(ApplyForJob model)
        {
            var userId = userManager.GetUserId(User);
            var jobId = HttpContext.Session.GetInt32("JobId");

            var check = context.ApplyForJobs.Where(a => a.JobId == jobId && a.UserId == userId).ToList();

            if (check.Count < 1)
            {
                var job = new ApplyForJob();
                job.JobId = jobId.Value;
                job.Message = model.Message;
                job.UserId = userId;
                job.ApplyDate = DateTime.Now;

                if (model.CVFile != null && model.CVFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.CVFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.CVFile.CopyToAsync(fileStream);
                    }
                    job.CVFileName = uniqueFileName; 
                }

                context.ApplyForJobs.Add(job);
                context.SaveChanges();

                ViewBag.Result = "تمت الاضافه بنجاح";
            }
            else
            {
                ViewBag.Result = "المعذرة، لقد سبق و تقدمت الى هذة الوظيفة";
            }

            return View(model);
        
        }

        [Authorize]
        public IActionResult GetJobsByUser()
        {
            var userId = userManager.GetUserId(User);
            var jobs = context.ApplyForJobs.Include(a => a.job).Where(a => a.UserId == userId); 
            return View(jobs.ToList());
        }

        public IActionResult Back()
        {
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult DetailsForJob(int id)
        {
            var Jb = context.ApplyForJobs.Include(a => a.job).FirstOrDefault(a => a.Id == id);
            if (Jb == null)
            {
                return NotFound();
            }
            return View(Jb);
        }

        public IActionResult EditForJob(int id)
        {
            var Jb = context.ApplyForJobs.FirstOrDefault(a => a.Id == id);
            return View(Jb);
        }

        [HttpPost]
        public IActionResult EditForJob(ApplyForJob Jb)
        {
            var job = context.ApplyForJobs.FirstOrDefault(a => a.Id == Jb.Id);
            if (job != null)
            {
                if (ModelState.IsValid) {
                    job.ApplyDate = Jb.ApplyDate;
                    job.Message = Jb.Message;
                    context.SaveChanges();
                    return RedirectToAction("GetJobsByUser");
                }
            }
            return View(job);
        }

        public IActionResult ConfirmDelete(int id)
        {
            var job = context.ApplyForJobs
                     .Include(a => a.job)
                     .FirstOrDefault(a => a.Id == id);
            return View(job);
        }

       
        public IActionResult DeleteForJob(int id) {
            var job = context.ApplyForJobs.FirstOrDefault(a => a.Id == id);
            if (job != null) { 
                context.ApplyForJobs.Remove(job);
                context.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            return NotFound();
        }

        [Authorize]
        public IActionResult GetJobsByPublisher()
        {
            var UserId = userManager.GetUserId(User);

            var jobs = context.ApplyForJobs
                              .Where(app => app.job.UserId == UserId)
                              .Include(app => app.job)
                              .Include(app => app.User)
                              .Select(app => new ApplyForJob
                              {
                                  Id = app.Id,
                                  Message = app.Message,
                                  ApplyDate = app.ApplyDate,
                                  CVFileName = app.CVFileName, 
                                  job = app.job, 
                                  User = app.User 
                              })
                              .ToList();

            var grouped = from j in jobs
                          group j by j.job.JobTitle
                          into g
                          select new JobsViewModel
                          {
                              JobTitle = g.Key,
                              Items = g
                          };

            return View(grouped.ToList());
        }
    }
}
