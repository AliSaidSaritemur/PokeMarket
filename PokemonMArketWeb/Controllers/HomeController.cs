using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PokemonMArketWeb.Models;
using PokemonsMarketWeb.Controllers;
using PokemonsMarketWeb.Models;
using System.Diagnostics;
using System.Globalization;

namespace PokemonMArketWeb.Controllers
{

    [Authorize(Roles = "user")]
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
            var userId = Request.Cookies["id"];
            User user = c.Users.FirstOrDefault(a => a.id == Int32.Parse(userId));                 
            

            var query = (from k in c.Pokemons
                         where  "selling" == k.sellStatue && k.UserId != Int32.Parse(userId)
                         select new Pokemon() { id=k.id, age=k.age,name=k.name,power=k.power,UserId=k.UserId, price = k.price, species=k.species, sellStatue = k.sellStatue }).ToList();

            ViewBag.user = user;
            return View(query);
        }




        public IActionResult Profil()
        {
            
            var userId = Request.Cookies["id"];

            User user = c.Users.FirstOrDefault(a => a.id == Int32.Parse(userId));


            var query = (from k in c.Pokemons
                         where user.id == k.UserId
                         select new Pokemon() { id = k.id, age = k.age, name = k.name, power = k.power, UserId = k.UserId,price=k.price,species=k.species,sellStatue=k.sellStatue}).ToList();

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



            if (updatePokemon.UserId != -1)
            {
                User owner = c.Users.FirstOrDefault(a => a.id == updatePokemon.UserId);
                owner.wallet = owner.wallet + updatePokemon.price;
            }


                if (upDateUser.wallet-updatePokemon.price>=0) { 
            updatePokemon.UserId = upDateUser.id;
            
            upDateUser.wallet = upDateUser.wallet - updatePokemon.price;

                if(updatePokemon.price>100)
            updatePokemon.price = updatePokemon.price - 50;



                updatePokemon.sellStatue = "not selling";
                c.SaveChanges();
            return RedirectToAction("Market");
            }

            return RedirectToAction("Market");
        }

        [HttpGet]
        public IActionResult Sell(string filename)
        {
            string asdasd = filename;
            Pokemon updatePokemon = c.Pokemons.FirstOrDefault(I => I.id == Int32.Parse(filename));


            updatePokemon.sellStatue = "selling"; 
            c.SaveChanges();
            return RedirectToAction("Profil", "Home");
        }

        [HttpGet]
        public IActionResult NotSell(string filename)
        {
            string asdasd = filename;
            Pokemon updatePokemon = c.Pokemons.FirstOrDefault(I => I.id == Int32.Parse(filename));

            updatePokemon.sellStatue = "not selling";
            c.SaveChanges();
            return RedirectToAction("Profil", "Home");
        }

        [HttpGet]
        public IActionResult Detail(string filename)
        {
            string asdasd = filename;
            Pokemon detailPokemon = c.Pokemons.FirstOrDefault(I => I.id == Int32.Parse(filename));

            var query = (from k in c.Comments
                         where detailPokemon.id == k.PokeId
                         select new Comment() { id = k.id, text = k.text, PokeId = k.PokeId, UserId = k.UserId,UserName =k.UserName}).ToList();

            var userId = Request.Cookies["id"];
            User user = c.Users.FirstOrDefault(a => a.id == Int32.Parse(userId));
            
            User salesuser = c.Users.FirstOrDefault(a => a.id == detailPokemon.UserId);
            string salesUserName;
            if (detailPokemon.UserId != -1)
             salesUserName= salesuser.userName;
            else
            {
                salesUserName = "Market";
            }

            ViewBag.comments=query;
            ViewBag.pokemon = detailPokemon;
            ViewBag.user = user;
            ViewBag.salesUserName = salesUserName;


            return View(new Comment());
        }

        [HttpPost]
        public IActionResult AddCommet(Comment comment)
        {        
            c.Comments.Add(comment);
                c.SaveChanges();
                return RedirectToAction("Detail", new { filename = comment.PokeId });

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


        public IActionResult UpdatePokemonfromUser(string filename)
        {
            string asdasd = filename;
            Pokemon updatePokemon = c.Pokemons.FirstOrDefault(I => I.id == Int32.Parse(filename));
            return View(updatePokemon);
        }

        [HttpPost]
        public IActionResult UpdatePokemonfromUser(Pokemon pokemon)
        {
            ModelState.Remove("id");
            ModelState.Remove("UserId");
            ModelState.Remove("species");
            ModelState.Remove("sellStatue");
            ModelState.Remove("age");
            ModelState.Remove("power");

            string pokeId = (pokemon.id).ToString();
            if (ModelState.IsValid)
            {
                PokemonAPIResponseController PAR = new();
                PAR.Update(pokemon);
                Thread.Sleep(250);
                return RedirectToAction("Profil");
            }
            return View(pokemon);
        }

        public IActionResult balanceLoading()
        {
     
            return View(new User());
        }

        [HttpPost]
        public IActionResult balanceLoading(User balanceUser)
        {
            var userId = Request.Cookies["id"];
            User user = c.Users.FirstOrDefault(a => a.id == Int32.Parse(userId));

            user.wallet = user.wallet + balanceUser.wallet;
            c.SaveChanges();
            return RedirectToAction("Profil");
        }

    }
}