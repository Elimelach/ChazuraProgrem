using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChazuraProgram.Models
{
    public class SponsorRegisterVM:RegisterViewModel
    {
        [Required(ErrorMessage = "required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "required")]
        public string State { get; set; }
        [Required(ErrorMessage = "required")]
        public string City { get; set; }
        [Required(ErrorMessage = "required")]
        [RegularExpression(@"(^\d{5}$)|(^\d{5}-\d{4}$)",ErrorMessage ="please enter a valid 5-9 digit zip code.")]
        public string ZipCode { get; set; }
    }
}
