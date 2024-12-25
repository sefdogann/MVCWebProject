using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                // E-posta adresinin zaten var olup olmadığını kontrol et
                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "This email address is already registered.");
                    return View(user);
                }

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login","Home");
            }
            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var loggedInUser = _context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            if (loggedInUser != null)
            {
                // Başarılı giriş işlemi
                return RedirectToAction("Index", "Home");
            }
            // Hatalı giriş işlemi
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(user);
        }
    }
}
