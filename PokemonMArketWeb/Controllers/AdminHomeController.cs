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
                             select new Pokemon() { id = k.id, age = k.age, name = k.name, power = k.power, UserId = k.UserId, price = k.price, species = k.species,sellStatue=k.sellStatue}).ToList();

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
                query = (from k in c.Pokemons
                         where searchUser.id == k.UserId
                         select new Pokemon() { id = k.id, age = k.age, name = k.name, power = k.power, UserId = k.UserId, price = k.price, species = k.species, sellStatue = k.sellStatue }).ToList();
                ViewBag.pokemons = query;
                ViewBag.selectUser = searchUser;
            }
            }

            return View();
        }



        public IActionResult Market()
        {
            var userId = Request.Cookies["id"];
            User user = c.Users.FirstOrDefault(a => a.id == Int32.Parse(userId));


            var query = (from k in c.Pokemons
                         where "selling" == k.sellStatue && k.UserId != Int32.Parse(userId)
                         select new Pokemon() { id = k.id, age = k.age, name = k.name, power = k.power, UserId = k.UserId, price = k.price, species = k.species, sellStatue = k.sellStatue }).ToList();


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
            ModelState.Remove("sellStatue");
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

            User salesuser = c.Users.FirstOrDefault(a => a.id == detailPokemon.UserId);
            string salesUserName;
            if (detailPokemon.UserId != -1)
                salesUserName = salesuser.userName;
            else
            {
                salesUserName = "Market";
            }

            var userId = Request.Cookies["id"];
            User user = c.Users.FirstOrDefault(a => a.id == Int32.Parse(userId));
            ViewBag.comments = query;
            ViewBag.pokemon = detailPokemon;
            ViewBag.user = user;
            ViewBag.salesUserName = salesUserName;


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
        [HttpGet]
        public IActionResult UpdatePokemon(string filename)
        {
            string asdasd = filename;
            Pokemon updatePokemon = c.Pokemons.FirstOrDefault(I => I.id == Int32.Parse(filename));
            return View(updatePokemon);
        }
        [HttpPost]
        public IActionResult UpdatePokemon(Pokemon pokemon)
        {
            ModelState.Remove("id");
            ModelState.Remove("UserId");
            ModelState.Remove("species");
            ModelState.Remove("sellStatue");
            string pokeId = (pokemon.id).ToString();
            if (ModelState.IsValid)
            {
                var updatePokemon = c.Pokemons.FirstOrDefault(I => I.id == pokemon.id);
                updatePokemon.price=pokemon.price;
                updatePokemon.power=pokemon.power;
                updatePokemon.age=pokemon.age;
                updatePokemon.species=pokemon.species;
                updatePokemon.name = pokemon.name;
                c.SaveChanges();
                return RedirectToAction("Market");
            }
            return View(pokemon);
        }

        [HttpGet]
        public IActionResult DeleteUser(string username)
        {

            var userId = Request.Cookies["id"];
            User user = c.Users.FirstOrDefault(a => a.id == Int32.Parse(userId));
            List<Pokemon> query;
            ViewBag.user = user;
            if (username != null)
            {
                string asdasd = username;
                User deleteUser = c.Users.FirstOrDefault(a => a.userName == username);

                if (deleteUser != null)
                {
                    List<Pokemon> Pokelist = c.Pokemons.Where(I => I.UserId == deleteUser.id).ToList();

                    foreach (var item in Pokelist)
                    {
                        item.UserId = -1;
                    }


                    c.Users.Remove(deleteUser);
                    c.SaveChanges();
                    return RedirectToAction("Profil");
                }
            }

            return RedirectToAction("Profil");
        }


        [HttpGet]
        public IActionResult UpdateUser(string username)
        {
            string asdasd = username;
            var userId = Request.Cookies["id"];
            User user = c.Users.FirstOrDefault(a => a.id == Int32.Parse(userId));
            List<Pokemon> query;
            ViewBag.user = user;
            if (username != null)
            {

                User deleteUser = c.Users.FirstOrDefault(a => a.userName == username);

                if (deleteUser != null)
                {                    
                    return View(deleteUser);
                }
            }

            return RedirectToAction("Profil");
        }
        [HttpPost]
        public IActionResult UpdateUser(User user)
        {
            ModelState.Remove("id");
            if (ModelState.IsValid)
            {
                var updateUser = c.Users.FirstOrDefault(I => I.id == user.id);

                updateUser.name = user.name;
                updateUser.surname = user.surname;
                updateUser.wallet = user.wallet;
                updateUser.phone = user.phone;
                updateUser.userName = user.userName;
                updateUser.role = user.role;
                updateUser.password = user.password;
                c.SaveChanges();
                return RedirectToAction("Market");
            }
            return View();
        }

    
        public IActionResult GetAllPokemons()
        {
            PokemonAPIResponseController PAR =new();

            List < Pokemon > pokelist= PAR.GetAllPokemons().Result;
            return View(pokelist);
        }


        public IActionResult DeletePokemonWithAPI(string filename)
        {
            string asdasd = filename;
            PokemonAPIResponseController PAR = new();
          PAR.Delete(Int32.Parse(filename));
            return RedirectToAction("GetAllPokemons");
        }

        public IActionResult ApiUpdatePokemon(string filename)
        {
            string asdasd = filename;
            Pokemon updatePokemon = c.Pokemons.FirstOrDefault(I => I.id == Int32.Parse(filename));
            return View(updatePokemon);
        }

        [HttpPost]
        public IActionResult ApiUpdatePokemon(Pokemon pokemon)
        {
            ModelState.Remove("id");
            ModelState.Remove("UserId");
            ModelState.Remove("species");
            ModelState.Remove("sellStatue");
            string pokeId = (pokemon.id).ToString();
            if (ModelState.IsValid)
            {
                PokemonAPIResponseController PAR = new();
                PAR.Update(pokemon);
                Thread.Sleep(300);
                return RedirectToAction("GetAllPokemons");
            }
            return View(pokemon);
        }
    }
}
