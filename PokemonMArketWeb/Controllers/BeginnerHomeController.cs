using Microsoft.AspNetCore.Mvc;
using PokemonsMarketWeb.Models;

namespace PokemonsMarketWeb.Controllers
{
    public class BeginnerHomeController : Controller
    {
        Context c = new Context();
        public IActionResult Market()
        {
            PokemonAPIResponseController PAR = new();

            List<Pokemon> pokelist = PAR.GetAllPokemons().Result;
            return View(pokelist);
        }
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
             salesUserName= salesuser.userName;
            else
            {
                salesUserName = "Market";
            }

    ViewBag.comments=query;
            ViewBag.pokemon = detailPokemon;
            ViewBag.salesUserName = salesUserName;


            return View(new Comment());
        }
    }
}
