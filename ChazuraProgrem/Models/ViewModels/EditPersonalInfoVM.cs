using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChazuraProgram.Models
{
    public class EditPersonalInfoVM
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(255)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(255)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(255)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
