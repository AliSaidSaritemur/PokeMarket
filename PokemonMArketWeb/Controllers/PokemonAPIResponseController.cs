using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PokemonsMarketWeb.Models;
using System.Reflection.Metadata;

namespace PokemonsMarketWeb.Controllers
{
    public class PokemonAPIResponseController : Controller
    {

        HttpClientHandler clientHandler = new HttpClientHandler();
        Pokemon pokemon = new Pokemon();
        List<Pokemon> _pokeList = new List<Pokemon>();
        
        public PokemonAPIResponseController()
        {
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<List<Pokemon>> GetAllPokemons()
        {
            _pokeList = new List<Pokemon>();
            using (var httpclient = new HttpClient(clientHandler))
            {
                using (var response = await httpclient.GetAsync("https://localhost:7297/api/PokemonAPI"))
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    _pokeList = JsonConvert.DeserializeObject<List<Pokemon>>(apiresponse);
                }
            }
            return _pokeList;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
