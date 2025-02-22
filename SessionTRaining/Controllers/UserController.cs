using Microsoft.AspNetCore.Mvc;
using SessionTRaining.DbConnection;
using SessionTRaining.Models;

namespace SessionTRaining.Controllers
{
    public class UserController : Controller
    {
        public readonly AppDbContext db;
        public UserController(AppDbContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (db.Users.Any(x => x.Name == user.Name))
            {
                ModelState.AddModelError("", "Username already exists.");
                return View();
            }
            if (!db.Users.Any())
            {
                user.Role = "Admin";
            }
            else
            {
                user.Role = "User";
            }
            db.Add(user);
            db.SaveChanges();
            return RedirectToAction("LogIn");
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(User users)
        {
            var user = db.Users.FirstOrDefault(x=>x.Name == users.Name && x.Password == users.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View();
            }

            HttpContext.Session.Clear();

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserName",user.Name);
            HttpContext.Session.SetString("UserPassword",user.Password);
            HttpContext.Session.SetString("UserIP",HttpContext.Connection.RemoteIpAddress?.ToString());

            return RedirectToAction("Index","Home");
        }

        [HttpPatch]
        public IActionResult Logout() 
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }
    }
}
