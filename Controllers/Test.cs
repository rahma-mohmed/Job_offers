using Microsoft.AspNetCore.Mvc;

namespace Job_offers.Controllers
{
    public class Test : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
