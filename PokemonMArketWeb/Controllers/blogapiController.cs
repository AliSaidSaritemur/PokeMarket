using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonsMarketWeb.Models;

namespace PokemonsMarketWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class blogapiController : ControllerBase
    {
        Context _context = new Context();
        [HttpGet]
        public IEnumerable<Pokemon> get()
        {
            var pokemon = _context.Pokemons.ToList();
            return pokemon;

        }
    }
}
