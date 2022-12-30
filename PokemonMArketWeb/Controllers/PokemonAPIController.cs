using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonsMarketWeb.Models;
using System.Reflection.Metadata;

namespace PokemonsMarketWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonAPIController : ControllerBase
    {
        Context c = new Context();
        [HttpGet]
        public IEnumerable<Pokemon> Get()
        {
            return c.Pokemons.ToList();
        }



        [HttpPut]
        public Pokemon Put([FromBody]Pokemon pokemon)
        {

            var poke = c.Pokemons.FirstOrDefault(a => a.id == pokemon.id);

            poke.sellStatue = pokemon.sellStatue;
            poke.price = pokemon.price;
            poke.power=pokemon.power;
            poke.id = pokemon.id;
            poke.age = pokemon.age;
            poke.species = pokemon.species;
            poke.UserId = pokemon.UserId;
            poke.name = pokemon.name;

            c.SaveChanges();


            return poke;
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id )
        {
            var pokemon = c.Pokemons.FirstOrDefault(a=> a.id ==id);
            if (pokemon != null)
            {
                c.Pokemons.Remove(pokemon);
                c.SaveChanges();
     
                return Ok(pokemon);

            }
            return BadRequest();

        }
    }
}
