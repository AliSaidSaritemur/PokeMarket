using System.ComponentModel.DataAnnotations;

namespace PokemonsMarketWeb.Models
{
    public class LoginUser
    {
        public string mail { get; set; }
        public string password { get; set; }
    }
}
