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
        public IEnumerable<Pokemon> Get(int id)
        {
            return c.Pokemons.ToList();
        }
    }
}
