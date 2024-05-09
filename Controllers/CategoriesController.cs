using Job_offers.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;


namespace Job_offers.Controllers
{
    public class CategoriesController : Controller
    {
        JobContext context = new JobContext();

        public IActionResult IndexCategory()
        {
            List<Category> categories = context.Categories.ToList();
            return View("IndexCategory", categories);
        }

        public IActionResult AddCategory()
        {
            Category categories = new Category();
            return View("AddCategory", categories);
        }

        public IActionResult SaveAdd(Category category)
        {

            if (category.CategoryName != null && category.CategoryDescription != null)
            {
                context.Categories.Add(category);
                context.SaveChanges();
                return RedirectToAction("IndexCategory");
            }
            return View("AddCategory", category);
        }

        public IActionResult Edit(int id)
        {
            Category categories = context.Categories.FirstOrDefault(x => x.Id == id);
            return View(categories);
        }

        public IActionResult SaveEdit([FromRoute] int id, Category categor)
        {
            Category categories = context.Categories.FirstOrDefault(x => x.Id == id);
            categories.Id = categor.Id;
            categories.CategoryName = categor.CategoryName;
            categories.CategoryDescription = categor.CategoryDescription;
            context.SaveChanges();
            return RedirectToAction("IndexCategory");
        }

        public IActionResult Confirm_delete(int id)
        {
            Category category = context.Categories.FirstOrDefault(c => c.Id == id);
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            Category category = context.Categories.FirstOrDefault(c => c.Id == id);
            context.Categories.Remove(category);
            context.SaveChanges();
            return RedirectToAction("IndexCategory");
        }

        public IActionResult Details(int id)
        {
            Category category = context.Categories.FirstOrDefault(x => x.Id == id);
            return View(category);

        }
        
        public IActionResult Back()
        {
            return RedirectToAction("IndexCategory");
        }
    }
}
