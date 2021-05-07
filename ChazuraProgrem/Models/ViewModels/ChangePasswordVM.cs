using System.ComponentModel.DataAnnotations;

namespace ChazuraProgram.Models
{
    public class ChangePasswordVM
    {
        public string Username { get; set; }
        [Required(ErrorMessage = "Required")]
        
        public string  OldPassword { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPW")]
       
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Please confirm")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPW { get; set; }
    }
}