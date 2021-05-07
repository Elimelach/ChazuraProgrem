using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ChazuraProgram.Models
{
    public class GetScheduleVM
    {
        [HebrewDateLimit]
        public DateTime Date { get; set; } = DateTime.Now.Date;
        [Range(3,30,ErrorMessage = "Number of days must be between 3 and 30.")]
        public int Days { get; set; } = 7;

    }
}
