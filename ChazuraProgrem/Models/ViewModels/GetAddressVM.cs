using System.ComponentModel.DataAnnotations;

namespace ChazuraProgram.Models
{
    public class GetAddressVM:IRegisterLogVM
    {
        public string Name { get; set; }
        public string  UserId { get; set; }
        [Required(ErrorMessage = "required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "required")]
        public string State { get; set; }
        [Required(ErrorMessage = "required")]
        public string City { get; set; }
        [Required(ErrorMessage = "required")]
        [RegularExpression(@"(^\d{5}$)|(^\d{5}-\d{4}$)", ErrorMessage = "please enter a valid 5-9 digit zip code.")]
        public string ZipCode { get; set; }
        public bool Rem { get; set; }
        public string Username { get ; set ; }
        public bool RememberMe { get ; set ; }
    }
}