using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonsMarketWeb.Models;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PokemonsMarketWeb.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminHomeController : Controller
    {
        Context c = new Context();



       

        [HttpGet]
        public IActionResult Profil()
        {
          
            var userId = Request.Cookies["id"];
            User user = c.Users.FirstOrDefault(a => a.id == Int32.Parse(userId));
            List<Pokemon> query;
            ViewBag.user = user;

                 query = (from k in c.Pokemons
                             where user.id == k.UserId
                             select new Pokemon() { id = k.id, age = k.age, name = k.name, power = k.power, UserId = k.UserId, price = k.price, species = k.species }).ToList();

            ViewBag.pokemons = query;
            return View(new LoginUser());
        }

        [HttpPost]
        public IActionResult Profil(LoginUser loginUser)
        {

            var userId = Request.Cookies["id"];
            User user = c.Users.FirstOrDefault(a => a.id == Int32.Parse(userId));
            List<Pokemon> query;
            ViewBag.user = user;
            if (loginUser != null)
            {
                string asdasd = loginUser.password;
                User searchUser = c.Users.FirstOrDefault(a => a.userName == loginUser.password);
          
              
            if (searchUser != null)
            {
                Pokemon selectPokemon = c.Pokemons.FirstOrDefault(I => I.id == searchUser.id);
           
                query = (from k in c.Pokemons
                         where selectPokemon.id == k.UserId
                         select new Pokemon() { id = k.id, age = k.age, name = k.name, power = k.power, UserId = k.UserId, price = k.price, species = k.species }).ToList();
                ViewBag.pokemons = query;
                ViewBag.selectUser = searchUser;
            }
            }

            return View();
        }



        public IActionResult Market()
        {
            if (Request.Cookies["id"] == null)
            {
                var userId = Request.Cookies["id"];
                User user = c.Users.FirstOrDefault(a => a.id == Int32.Parse(userId));
            }

            var query = (from k in c.Pokemons
                         where -1 == k.UserId
                         select new Pokemon() { id = k.id, age = k.age, name = k.name, power = k.power, UserId = k.UserId, price = k.price,species=k.species }).ToList();


            return View(query);
        }


        [HttpGet]
        public IActionResult Remove(string filename)
        {
              string asdasd = filename;
            Pokemon deletePokemon = c.Pokemons.FirstOrDefault(I => I.id == Int32.Parse(filename));

            c.Pokemons.Remove(deletePokemon);
                c.SaveChanges();
            

            return RedirectToAction("Market");
        }


        [HttpGet]
        public IActionResult CreatePokemon()
        {
            return View(new Pokemon());
        }

        [HttpPost]
        public IActionResult CreatePokemon(Pokemon pokemon)
        {
            ModelState.Remove("id");
            ModelState.Remove("UserId");
            ModelState.Remove("species");
            if (ModelState.IsValid)
            {
                c.Pokemons.Add(pokemon);
                c.SaveChanges();
                return RedirectToAction("Market");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Detail(string filename)
        {
            string asdasd = filename;
            Pokemon detailPokemon = c.Pokemons.FirstOrDefault(I => I.id == Int32.Parse(filename));

            var query = (from k in c.Comments
                         where detailPokemon.id == k.PokeId
                         select new Comment() { id = k.id, text = k.text, PokeId = k.PokeId, UserId = k.UserId, UserName = k.UserName }).ToList();

            var userId = Request.Cookies["id"];
            User user = c.Users.FirstOrDefault(a => a.id == Int32.Parse(userId));
            ViewBag.comments = query;
            ViewBag.pokemon = detailPokemon;
            ViewBag.user = user;


            return View(new Comment());
        }
      

            [HttpGet]
        public IActionResult DeleteComment(string filename)
        {
            string asdasd = filename;
            Comment deletecooment = c.Comments.FirstOrDefault(I => I.id == Int32.Parse(filename));

            c.Comments.Remove(deletecooment);
            c.SaveChanges();


            return RedirectToAction("Detail", new { filename = deletecooment.PokeId });
        }

        [HttpPost]
        public IActionResult AddCommet(Comment comment)
        {
            c.Comments.Add(comment);
            c.SaveChanges();
            return RedirectToAction("Detail", new { filename = comment.PokeId });

        }

    }
}
