using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCWebProject.Models;

namespace MVCWebProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View(); 
        } 
        public IActionResult Signup() 
        {
            return View();
        }
        public IActionResult termsofservice()
        {
            return View();
        }
        public IActionResult moderntemplate()
        {
            return View();
        }
        public IActionResult getstarted()
        {
            return View();
        }
        public IActionResult forgot()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
