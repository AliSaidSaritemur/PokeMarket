using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonsMarketWeb.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [MaxLength(35, ErrorMessage = "Username cant be so long")]
        [MinLength(3, ErrorMessage = "Username cant be so short")]
        [Required(ErrorMessage = "Should be username")]
        public string userName { get; set; }


        [Required(ErrorMessage = "Should be mail")]
        [EmailAddress]
        public string  mail { get; set; }

        [MinLength(3, ErrorMessage = "Name cant be so short")]
        [MaxLength(35, ErrorMessage = "Name cant be so long")]
        [Required(ErrorMessage = "Should be name")]
        public string  name { get; set; }

        [MinLength(3, ErrorMessage = "Surname cant be so short")]
        [MaxLength(35, ErrorMessage = "Surname cant be so long")]
        [Required(ErrorMessage = "Should be username")]
        public string  surname { get; set; }

        [RegularExpression(@"^0?[0-9]{10}$", ErrorMessage = "is not valid phone number")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Should be phone")]
        public string phone { get; set; }

        public int  wallet{ get; set; }

        [Required(ErrorMessage = "Should be password")]
        public string password { get; set; }


        public string role { get; set; }

    }
}
