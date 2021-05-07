using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChazuraProgram.Models
{
    public class Completed
    {
        [Key]
        public int CompId { get; set; }
        /// <summary>
        /// Foreign key to limud chart
        /// </summary>
        public int LimudChartId { get; set; }
        [ForeignKey("LimudChartId")]
        [InverseProperty("CompletedList")]
        public LimudChart LimudChart { get; set; }
        public string LimudFinishedCode { get; set; }
        public ChazuraTimes ChazuraTimes { get; set; }
    }
}
