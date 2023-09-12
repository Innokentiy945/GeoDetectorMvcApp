using System.Text;
using GeoDetectorMvcApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeoDetectorMvcApp.Controllers;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(User user)
    {
        if (!string.IsNullOrEmpty(user.UserName) && string.IsNullOrEmpty(user.Password))
        {
            return RedirectToAction("Login");
        }

        if(user.UserName=="Admin@gmail.com" && user.Password == "Password")
        {
            bool flag = true;
            var byteArr = Encoding.UTF8.GetBytes(flag.ToString());

            HttpContext.Session.Set("IsLoggedIn", byteArr);
            return RedirectToAction("Index", "Home");
        }

        return View("Login");
    }
    
    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("IsLoggedIn");
        return RedirectToAction("Login");
    }
}

