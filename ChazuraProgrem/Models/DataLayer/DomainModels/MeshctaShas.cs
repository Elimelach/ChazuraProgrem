using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChazuraProgram.Models
{
    public class MeshctaShas : IMeshcta
    {
        [Key]
        [Required]
        [MaxLength(10)]
        public string MeshactaID { get; set; }
        [Required]
        [MaxLength(15)]
        public string MeshactaHebrawName { get; set; }
        [Required]
        [MaxLength(15)]

        public string MeshachtaEngName { get; set; }
        [Range(2, 180)]
        public int TotolDafim { get; set; }
        [Range(2, 40)]
        public int StartsAtDaf { get; set; } = 2;

        public ICollection<DafimShas> ShasDafim { get; set; }
        public ICollection<ShasChazuraData> ChazuraCharts { get; set; }
        public ICollection<Shas1Sided> Shas1Sided { get; set; }

    }
}
