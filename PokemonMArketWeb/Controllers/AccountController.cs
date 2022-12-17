using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PokemonsMarketWeb.Models;
using System.Collections.Specialized;
using System.Security.Claims;

namespace PokemonsMarketWeb.Controllers
{
    public class AccountController : Controller
    {

        Context c = new Context();

        [HttpGet]
        public IActionResult Login()
        {
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
                Response.Cookies.Append("id",user.id.ToString());
                return RedirectToAction("Market", "Home");

            }
            return View();
        }
    }
}
