using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PokemonMArketWeb.Models;
using PokemonsMarketWeb.Models;
using System.Diagnostics;
using System.Globalization;

namespace PokemonMArketWeb.Controllers
{

    [Authorize(Roles ="admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Context c = new Context();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Market()
        {
            if (Request.Cookies["id"]==null) { 
            var userId = Request.Cookies["id"];
            User user = c.Users.FirstOrDefault(a => a.id == Int32.Parse(userId));
            }

            var query = (from k in c.Pokemons
                         where  -1 == k.UserId
                         select new Pokemon() { id=k.id, age=k.age,name=k.name,power=k.power,UserId=k.UserId, price = k.price }).ToList();

         
            return View(query);
        }

        public IActionResult Profil()
        {
            var userId = Request.Cookies["id"];
            User user = c.Users.FirstOrDefault(a => a.id == Int32.Parse(userId));


            var query = (from k in c.Pokemons
                         where user.id == k.UserId
                         select new Pokemon() { id = k.id, age = k.age, name = k.name, power = k.power, UserId = k.UserId,price=k.price }).ToList();

            ViewBag.user = user;
            return View(query);
        }


        [HttpGet]
        public IActionResult Buy(string filename)
        {
            string asdasd = filename;
            Pokemon updatePokemon = c.Pokemons.FirstOrDefault(I => I.id == Int32.Parse(filename));

            var userId = Request.Cookies["id"];
            User upDateUser = c.Users.FirstOrDefault(a => a.id == Int32.Parse(userId));
            if (upDateUser.wallet-updatePokemon.price>=0) { 
            updatePokemon.UserId = upDateUser.id;
            
            upDateUser.wallet = upDateUser.wallet - updatePokemon.price;

                if(updatePokemon.price>100)
            updatePokemon.price = updatePokemon.price - 50;

            c.SaveChanges();
            return RedirectToAction("Profil", "Home");
            }

            return RedirectToAction("Market");
        }

        [HttpGet]
        public IActionResult Sell(string filename)
        {
            string asdasd = filename;
            Pokemon updatePokemon = c.Pokemons.FirstOrDefault(I => I.id == Int32.Parse(filename));

            var userId = Request.Cookies["id"];
            User upDateUser = c.Users.FirstOrDefault(a => a.id == Int32.Parse(userId));

            updatePokemon.UserId = -1;

            upDateUser.wallet = upDateUser.wallet + updatePokemon.price;
            updatePokemon.price = updatePokemon.price +35;
            c.SaveChanges();
            return RedirectToAction("Profil", "Home");
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}