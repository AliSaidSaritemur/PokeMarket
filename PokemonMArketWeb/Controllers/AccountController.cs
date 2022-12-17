using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PokemonsMarketWeb.Models;
using System.Collections.Specialized;
using System.Security.Claims;
using System.Net;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace PokemonsMarketWeb.Controllers
{
    public class AccountController : Controller
    {

        Context c = new Context();

        [HttpGet]
        public IActionResult Login()
        {
            if (Request.Cookies["id"] != null)
                return RedirectToAction("Market", "Home");

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
                return RedirectToAction("Market", "Home");
            }
            return View();
        }

        public IActionResult LogOut()
        {
            Response.Cookies.Delete("id");
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
            if (ModelState.IsValid) { 
                c.Users.Add(user);
            c.SaveChanges();
            return RedirectToAction("Login");
            }
            return View();
        }
    }
}
