using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PokemonsMarketWeb.Models;
using System.Collections.Specialized;
using System.Security.Claims;
using System.Net;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Data;

namespace PokemonsMarketWeb.Controllers
{
    public class AccountController : Controller
    {

        Context c = new Context();

        [HttpGet]
        public IActionResult Login()
        {
            if (Request.Cookies["id"] != null)
            {
                User user = c.Users.FirstOrDefault(a => a.id == Int32.Parse(Request.Cookies["id"]));
                if (user.role=="admin")
                    return RedirectToAction("Market", "AdminHome");

                return RedirectToAction("Market", "Home");
            }
               

            LoginUser u = new LoginUser();    
            return View(u);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUser u)
        {
            Console.WriteLine(u.mail);
            var user = c.Users.FirstOrDefault(x => x.mail == u.mail && x.password == u.password);

     
   
            if (user!=null)
            {

                var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.mail),
            new Claim(ClaimTypes.Role, user.role)};

                var claimIdentty = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();
               await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentty), authProperties);
                Response.Cookies.Append("id",user.id.ToString());  
                if(user.role=="admin")
                return RedirectToAction("Market", "AdminHome");
                else
                {
                    return RedirectToAction("Market", "Home");
                }
            }
            return View();
        }

        public IActionResult LogOut()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            return  RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new User());
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            ModelState.Remove("id");
            ModelState.Remove("wallet");
            ModelState.Remove("role");
            if (ModelState.IsValid) { 
                c.Users.Add(user);
            c.SaveChanges();
            return RedirectToAction("Login");
            }
            return View();
        }
    }
}
