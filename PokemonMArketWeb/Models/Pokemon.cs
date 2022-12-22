using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonsMarketWeb.Models
{
    public class Pokemon 
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [MinLength(3, ErrorMessage = "Name cant be so short")]
        [MaxLength(35, ErrorMessage = "Name cant be so long")]
        [Required(ErrorMessage = "Should be name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Should be power")]
        public int power { get; set; }

        [Required(ErrorMessage = "Should be age")]
        public int age { get; set; }

    
        public int price { get; set; }

        public int UserId { get; set; }


        [MinLength(3, ErrorMessage = "Species cant be so short")]
        [MaxLength(35, ErrorMessage = "Species cant be so long")]
        [Required(ErrorMessage = "Should be species")]
        public string species { get; set; }


        public string sellStatue { get; set; }
    }
}
