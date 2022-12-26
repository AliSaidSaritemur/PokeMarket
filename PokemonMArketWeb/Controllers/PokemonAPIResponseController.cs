using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PokemonsMarketWeb.Models;
using System.Reflection.Metadata;
using static System.Reflection.Metadata.BlobBuilder;
using System.Text;

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


        [HttpPut]
        public async Task<Pokemon> Update(Pokemon upokemon)
        {
            pokemon = new Pokemon();
            using (var httpclient = new HttpClient(clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(upokemon), Encoding.UTF8, "application/json");

                using (var response = await httpclient.PutAsync("https://localhost:7191/api/BlogsApi", content))
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    pokemon = JsonConvert.DeserializeObject<Pokemon>(apiresponse);
                }
            }
            return pokemon;
        }




        [HttpDelete]
        public async Task<string> Delete(int pokeid)
        {
            string messages = "";
            using (var httpclient = new HttpClient(clientHandler))
            {
                using (var response = await httpclient.DeleteAsync("https://localhost:7297/api/PokemonAPI/" + pokeid))
                {
                    messages = await response.Content.ReadAsStringAsync();

                }
            }
            return messages;
        }
        public IActionResult Index()
        {
            return View();
        }

    }
}
