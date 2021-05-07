using System;
using System.ComponentModel.DataAnnotations;

namespace ChazuraProgram.Models
{
    public class ShasChazuraAumidData:IChart
    {
        [Key]
        public int ChartId { get; set; }
        [Required]
        [MaxLength(14)]
        public string AumidID { get; set; }
        public Shas1Sided Aumid { get; set; }
        public DateTime Date { get; set; }
        [Required]
        [MaxLength(10)]
        public string MeshactaID { get; set; }
        public MeshctaShas Meshacta { get; set; }

        [Required]
        public ChazuraTimes ChazuraTimes { get; set; }


    }
}
