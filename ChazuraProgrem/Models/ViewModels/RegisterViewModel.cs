using System.ComponentModel.DataAnnotations;

namespace ChazuraProgram.Models
{
    public class RegisterViewModel:IRegisterLogVM
    {
        [Required(ErrorMessage ="Please enter a username.")]
        [StringLength(255)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter a first name.")]
        [StringLength(255)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter a last name.")]
        [StringLength(255)]
        public string LastName { get; set; }
        [Required(ErrorMessage ="Please enter a email address.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage ="Please enter a password.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Please confirm your password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password",ErrorMessage ="")]
        public string ConfirmPassword { get; set; }
        public bool RememberMe { get; set; } = false;
        public string ReturnUrl { get; set; }
    }
}
