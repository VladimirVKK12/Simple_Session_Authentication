using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SessionTRaining.DbConnection;
using SessionTRaining.Models;

namespace SessionTRaining.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UsersToDoController : Controller
    {
        private readonly AppDbContext db;
        public UsersToDoController(AppDbContext _db)
        {
            db = _db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("LogIn", "User"); 
            }

            var list = db.UsersToDoList.Where(x=>x.UserID == userId);

            return View(list);
        }
        [HttpPost]
        public IActionResult CreateList(UsersToDoList usersToDoList)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            usersToDoList.UserID = userId.Value;

            db.UsersToDoList.Add(usersToDoList);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
