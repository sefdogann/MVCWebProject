using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MVCWebProject.Data;
using MVCWebProject.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    return Json(new { success = false, message = "This email address is already registered." });
                }

                _context.Add(user);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Sign up successful! Redirecting to login page..." });
            }
            return Json(new { success = false, message = "Invalid data." });
        }

        [HttpPost]
        public JsonResult CheckEmail(string email)
        {
            bool emailExists = _context.Users.Any(u => u.Email == email);
            return Json(new { exists = emailExists });
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(User user)
        {
            var loggedInUser = _context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            if (loggedInUser != null)
            {
                HttpContext.Session.SetString("Username", loggedInUser.FullName);
                return Json(new { success = true, message = "Login successful! Redirecting to home page..." });
            }
            return Json(new { success = false, message = "Invalid login attempt." });
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
