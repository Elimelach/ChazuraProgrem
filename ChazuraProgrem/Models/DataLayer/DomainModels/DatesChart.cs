using System;
using System.ComponentModel.DataAnnotations;

namespace ChazuraProgram.Models
{
    public class DatesChart
    {
        [Key]
        public int ID { get; set; }
        public ChazurahType ChazurahType { get; set; }
        [MaxLength(10)]
        public string MeshactaID { get; set; }
        public DateTime DateStarted { get; set; }
    }
}
