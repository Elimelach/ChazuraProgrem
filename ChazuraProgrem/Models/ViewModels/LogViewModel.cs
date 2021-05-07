using System.ComponentModel.DataAnnotations;

namespace ChazuraProgram.Models
{
    public class LogViewModel:IRegisterLogVM
    {
        [Required(ErrorMessage = "Please enter a username.")]
        [StringLength(255)]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Please enter a password.")]
        [StringLength(255)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; } = false;
        public bool FromSponser { get;  set; }
    }
}
