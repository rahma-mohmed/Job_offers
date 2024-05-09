using Job_offers.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Job_offers.Controllers
{
    public class JobsController : Controller
    {
        JobContext context = new JobContext();

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

        public IActionResult SaveAdd(Job Jb)
        {
            if (Jb.JobTitle != null && Jb.JobContent != null)
            {
                context.Jobs.Add(Jb);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
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

        public IActionResult SaveEdit([FromRoute] int id, Job Jb)
        {
            Job Job = context.Jobs.FirstOrDefault(x => x.Id == id);
            List<Category> ctg = context.Categories.ToList();
            ViewData["Categories"] = ctg;
            Job.Id = Jb.Id;
            Job.JobTitle = Jb.JobTitle;
            Job.JobContent = Jb.JobContent;
            Job.JobImage = Jb.JobImage;
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
            List<Category> ctg = context.Categories.ToList();
            ViewData["Categories"] = ctg;
            return View(Jb);

        }

        public IActionResult Back()
        {
            return RedirectToAction("Index");
        }
    }
}
