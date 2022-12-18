using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonsMarketWeb.Models;
using System.Data;

namespace PokemonsMarketWeb.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminHomeController : Controller
    {
        Context c = new Context();
        public IActionResult Market()
        {
            if (Request.Cookies["id"] == null)
            {
                var userId = Request.Cookies["id"];
                User user = c.Users.FirstOrDefault(a => a.id == Int32.Parse(userId));
            }

            var query = (from k in c.Pokemons
                         where -1 == k.UserId
                         select new Pokemon() { id = k.id, age = k.age, name = k.name, power = k.power, UserId = k.UserId, price = k.price }).ToList();


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
    }
}
