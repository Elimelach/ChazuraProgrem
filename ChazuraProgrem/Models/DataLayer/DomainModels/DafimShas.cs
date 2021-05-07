using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChazuraProgram.Models
{
    public class DafimShas:IChartBuilder<ShasChazuraData>
    {
        [Key]
        [Required]
        [MaxLength(14)]
        public string DafID { get; set; }
        public bool TwoSided { get; set; } = true;
       
        [Required]
        [MaxLength(3)]
        public string DafHebraw { get; set; }
       
        [Range(2, 180)]
        public int DafNumber { get; set; }
        [Required]
        [MaxLength(10)]
        public string MeshactaID { get; set; }
        public MeshctaShas Meshacta { get; set; }
        public ICollection<ShasChazuraData> ChazuraCharts { get; set; }

        public ShasChazuraData BuildChart()
        {
            return new ShasChazuraData
            {
                DafID = this.DafID,
                MeshactaID = this.MeshactaID
            };
        }

        public override string ToString()
        {
            return (Meshacta.MeshactaHebrawName ?? "") + " " + "דף" + " " + DafHebraw;
        }
        public string ToStringEng() => $"{Meshacta.MeshachtaEngName ?? ""} daf {DafNumber}";
    }
}

