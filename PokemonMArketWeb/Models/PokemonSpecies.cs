using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PokemonsMarketWeb.Models
{
    public class PokemonSpecies
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]  
        public string name { get; set; }

        public string detailColor { get; set; }

        public string rarity { get; set; }

    }
}
