using Job_offers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Runtime.Intrinsics.X86;

namespace Job_offers.Controllers
{
    public class HomeController : Controller
    {
        Account account = new Account();
        JobContext context = new JobContext();

        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            List<Category> categories = context.Categories.Include(c => c.Jobs).ToList();
            return View(categories);
        }

        public IActionResult Signup()
        {

            List<string> Accounts_Type = new List<string>() { "ناشر", "باحث" };
            ViewBag.Types = Accounts_Type;

            return View(account);
        }

        [HttpPost]
        public IActionResult Signup(Account account) {

            List<string> Accounts_Type = new List<string>() { "ناشر", "باحث" };
            ViewBag.Types = Accounts_Type;

            if (context.Accounts.Any(x => x.Name == account.Name))
            {
                ViewBag.Notification = "هذا الحساب موجود بالفعل";
                return View(account);
            }
            else
            {
                context.Accounts.Add(account);
                context.SaveChanges();

                HttpContext.Session.SetString("Id", account.Id.ToString());
                HttpContext.Session.SetString("Name",account.Name);

                return RedirectToAction("Index");
            }
        }

        public IActionResult Login()
        {
            return View(account);
        }

        [HttpPost]
        public IActionResult Login(Account account)
        {
            var check = context.Accounts.Where(x => x.Name.Equals(account.Name) && x.Password.Equals(account.Password)).FirstOrDefault();
            if (check != null)
            {
                HttpContext.Session.SetString("Id" , account.Id.ToString());
                HttpContext.Session.SetString("Name", account.Name);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Notification = "خطأ فى أسم المستخدم او كلمه المرور";
                return View(account);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
