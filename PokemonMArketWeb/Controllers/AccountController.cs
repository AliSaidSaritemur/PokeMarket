using Microsoft.AspNetCore.Mvc;
using PokemonsMarketWeb.Models;
using System.Collections.Specialized;

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
        public IActionResult Login(LoginUser u)
        {
            Console.WriteLine(u.mail);
            var user = c.Users.FirstOrDefault(x => x.mail == u.mail && x.password == u.password);

            if (user!=null)
            {

                return RedirectToAction("Index", "Home");

            }
            return View();
        }
    }
}
