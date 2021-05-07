using System;
using System.ComponentModel.DataAnnotations;

namespace ChazuraProgram.Models
{
    public class CustomLimud:IChart,IChartBuilder<CustomLimud>
    {
        [Key]
        public int CustomID { get; set; }
        public string Type { get; set; }
        public ChazuraTimes ChazuraTimes { get; set; }
        [Required]
        public string LimudString { get; set; }
        public DateTime Date { get; set; } 
        public bool Completed { get; set; } = false;
        public bool EmailNotify { get; set; } = false;
        [MaxLength(450)]
        public string UserId { get; set; }
        public User User { get; set; }

        public CustomLimud BuildChart()
        {
            return new CustomLimud();
        }
    }
}
