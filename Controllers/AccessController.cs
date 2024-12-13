using Microsoft.AspNetCore.Mvc;
using ShoeStore.Models;

namespace ShoeStore.Controllers
{
    public class AccessController : Controller
    {
        ShoeStoreContext db = new ShoeStoreContext();
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.Users
                          .FirstOrDefault(x => x.Username.Equals(user.Username) && x.PasswordHash.Equals(user.PasswordHash));
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.Username);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Invalid username or password.";
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Index", "Home");
        }
    }
}
