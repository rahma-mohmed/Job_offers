using Job_offers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace Job_offers.Controllers
{
    public class AccountsController : Controller
    {
        JobContext context = new JobContext();
        Account account = new Account();

        public IActionResult Index()
        {
            return View();
        }
        private bool UserExistsInDatabase(string userName, string email)
        {
            return context.Accounts.Any(u => u.Name == userName && u.Email == email);
        }

        public IActionResult Add()
        {
            List<string> Accounts_Type = new List<string>() { "ناشر", "باحث" };
            ViewBag.Types = Accounts_Type;
            if (!UserExistsInDatabase(account.Name, account.Email))
            {
                ViewBag.Authorized = true;
            }
            else
            {
                ViewBag.Authorized = false;
            }
            return View(account);
        }

        public IActionResult SaveAdd(Account newac)
        {
            List<string> Accounts_Type = new List<string>() { "ناشر", "باحث" };
            ViewBag.Types = Accounts_Type;

            if (!UserExistsInDatabase(account.Name, account.Email))
            {
                ViewBag.Authorized = true;
            }
            else
            {
                ViewBag.Authorized = false;
            }

            if (newac.Name != null && newac.Email != null)
            {
                ViewBag.Name = newac.Name;
                context.Accounts.Add(newac);
                context.SaveChanges();
                //return RedirectToAction("Add");
            }
            
            return View("Add", newac);
        }


        public IActionResult logoff(int id)
        {
            var ac = context.Accounts.Find(id);
            if (ac != null)
            {
                context.Accounts.Remove(ac);
                context.SaveChanges();
                return RedirectToAction("Add");
            }
            return NotFound();
        }
    }
}
