using Job_offers.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;

namespace Job_offers.Controllers
{
    public class HomeController : Controller
    {
        private readonly JobContext context;

        public HomeController(JobContext _context)
        {
            context = _context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactModel contact)
        {
            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("engrahma1422003@gmail.com", "etrkprfhvrhixlxz");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("engrahma1422003@gmail.com"));
            mail.Subject = contact.Subject;
            mail.IsBodyHtml = true;
            string body = "اسم المرسل: " + contact.Name + "<br>" +
                          "بريد المرسل: " + contact.Email + "<br>" +
                          "عنوان الرسالة: " + contact.Subject + "<br>" +
                          "نص الرسالة: <b>" + contact.Message + "</b>" ;
            mail.Body = body;
            var smtp = new SmtpClient("smtp.gmail.com" , 587);
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = loginInfo;
            smtp.Send(mail);

            return RedirectToAction("Index" , "Account");
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(string searchName)
        {
            var res = context.Jobs.Where(a => a.JobTitle.Contains(searchName)
            || a.JobContent.Contains(searchName)
            || a.Category.CategoryName.Contains(searchName)
            || a.Category.CategoryDescription.Contains(searchName)  
            );

            return View(res);
        }
    }
}
