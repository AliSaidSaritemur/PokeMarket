using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace PokemonsMarketWeb.Models
{
    public class Comment
    {
        [Key]
        public int id { get; set; }

        public string text { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public int PokeId { get; set; }
    }
}
