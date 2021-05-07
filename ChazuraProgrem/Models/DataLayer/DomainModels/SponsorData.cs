using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ChazuraProgram.Models
{
    public class SponsorData:ISponsorPoints
    {
        [Key]
        public int SponsId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public SponserType GetSponserType { get; set; }
        public string DescriptionName { get; set; }

        public string DescriptionElse { get; set; }
        public Status Status { get; set; } = Status.pending;
        private int? paymentId;

        public int? PaymentId
        {
            get { return paymentId; }
            set { paymentId = (int?)value; }
        }
        [JsonIgnore]
        public Payment Payment { get; set; }
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

    }
}
