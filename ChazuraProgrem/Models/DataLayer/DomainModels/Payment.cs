using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChazuraProgram.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public DateTime DateEnterd { get; set; }
        public DateTime? DateCharged  { get; set; }
        [Required(ErrorMessage = "required")]
        [CreditCard(ErrorMessage = "please enter a valid card number.")]
        public string CC_Number { get; set; }
        public CreditCard CardType { get; set; }
        [Required(ErrorMessage = "required")]
        [RegularExpression(@"^\d{5}$",ErrorMessage ="please enter a valid 5 digit zip code.")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "required")]
        [RegularExpression(@"^((0[1-9])|(1[0-2]))[\/\.\-]*(2[1-9])$", ErrorMessage = "please enter a valid month/year format.")]
        public string ExpDate { get; set; }
        [Required(ErrorMessage = "required")]
        [RegularExpression(@"^[0-9]{3,4}$", ErrorMessage = "please enter a valid security number.")]
        public string BCN { get; set; }//number in back of card
        public decimal Amount { get; set; }
        public Status GetStatus { get; set; } = Status.pending;
        [Required]

        public string UserId { get; set; }
        public User User { get; set; }
        public IList<SponsorData> Sponsors { get; set; }

    }
}
