using Job_offers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Runtime.Intrinsics.X86;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using System.Security.Claims;

namespace Job_offers.Controllers
{
    public class AccountController : Controller
    {
        Account account = new Account();

        UserManager<IdentityUser> userManager;
        private readonly JobContext context;
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<IdentityUser> signInManger;

        public AccountController(ILogger<AccountController> logger , JobContext _context , UserManager<IdentityUser> _userManager , SignInManager<IdentityUser> _signInManger)
        {
            context = _context;
            _logger = logger;
            userManager = _userManager;
            signInManger = _signInManger;
        }
        
        public IActionResult Index()
        {
            List<Category> categories = context.Categories.Include(c => c.Jobs).ToList();
            return View(categories);
        }

        public IActionResult Signup()
        {

            ViewBag.Types = new SelectList(context.Roles.Where(x => !x.Name.Contains("Administrators")).ToList() , "Name" , "Name");

            return View(account);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Signup(Account account) {

            ViewBag.Types = new SelectList(context.Roles.Where(x => !x.Name.Contains("Administrators")).ToList(), "Name" , "Name");

            if (ModelState.IsValid)
            {
                if (context.Accounts.Any(x => x.Name == account.Name))
                {
                    ViewBag.Notification = "هذا الحساب موجود بالفعل";
                    return View(account);
                }
                else
                {

                    IdentityUser identityUser = new IdentityUser();
                    identityUser.UserName = account.Name;
                    identityUser.Email = account.Email;


                    IdentityResult res = await userManager.CreateAsync(identityUser, account.Password.ToString());

                    if (res.Succeeded)
                    {
                        account.Id = identityUser.Id;
                        context.Accounts.Add(account);
                        context.SaveChanges();

                        //create cookies -> signInManger
                        await signInManger.SignInAsync(identityUser, false);

                        return RedirectToAction("Index");
                    }
                }
            }
            ViewBag.Notification = "هذا الحساب موجود بالفعل";
            return View(account);
        }

        public IActionResult Login(string ReturnUrl = "~/Account/Index")
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return View(account);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(Account account , string ReturnUrl = "~/Account/Index")
        {
            
                /*HttpContext.Session.SetString("Id" , account.Id.ToString());
                HttpContext.Session.SetString("Name", account.Name);*/
                IdentityUser identityUser = await userManager.FindByNameAsync(account.Name);

            if (identityUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult res = await signInManger.PasswordSignInAsync(identityUser , account.Password , account.IsPresistent , false);

                    if (res.Succeeded) {
                        return LocalRedirect(ReturnUrl);
                    }
                    else
                    {
                        ViewBag.Notification = "خطأ فى أسم المستخدم او كلمه المرور";
                    }

                }
                    ViewBag.Notification = "خطأ فى أسم المستخدم او كلمه المرور";
                    return View(account);
                
        }

        public IActionResult EditProfile()
        {
            var UserId = userManager.GetUserId(User);
            var user = context.Accounts.Where(a=> a.Id == UserId).FirstOrDefault();
            EditAccountViewModel profile = new EditAccountViewModel();
            profile.Name = user.Name;
            profile.Email = user.Email;
            return View(profile);
        }

        [HttpPost]
        public IActionResult EditProfile(EditAccountViewModel profile)
        {
            var UserId = userManager.GetUserId(User);
            var user = context.Accounts.Where(a => a.Id == UserId).FirstOrDefault();
            if (user.Password == profile.currentPassword) {
                user.Name = profile.Name;
                user.Email = profile.Email;
                user.Password = profile.NewPassword;
                user.RePassword = profile.RePassword;
                context.SaveChanges();
                ViewBag.Message = "تمت عمليه التعديل بنجاح";
                return RedirectToAction("Index");
            }

            ViewBag.Message = "كلمه السر الحاليه غير صحيحه";
            return View(profile);
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

        public async Task<IActionResult> Logout()
        {
            //HttpContext.Session.Clear();
            await signInManger.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
