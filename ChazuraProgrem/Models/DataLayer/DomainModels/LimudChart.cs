using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChazuraProgram.Models
{
    public class LimudChart
    {
        [Key]
        public int ChartId { get; set; }
        /// <summary>
        /// Foreign key for User
        /// </summary>
        [MaxLength(450)]
        public string UserId { get; set; }
       [JsonIgnore]
        public User User { get; set; }
        public ChazurahType ChazurahType { get; set; } = ChazurahType.ShasAllDaf;
        [MaxLength(15)]
        public string MeshctaCode { get; set; }
        public DateTime DateStarted { get; set; } = DateTime.Now.Date;
        public bool EmailNotify { get; set; } = false;
        [Range(1, 16)]
        public int YearsChazurah { get; set; } = 7;
        [InverseProperty("LimudChart")]
        public ICollection<Completed> CompletedList { get; set; }
        [NotMapped]
        public string MeschtaName { get; set; }
    }
}
