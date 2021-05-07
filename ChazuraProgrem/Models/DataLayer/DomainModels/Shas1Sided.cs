using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChazuraProgram.Models
{
    public class Shas1Sided:IChartBuilder<ShasChazuraAumidData>
    {
        [Key]
        [Required]
        [MaxLength(14)]
        public string AumidID { get; set; }

        [Required]
        [MaxLength(4)]
        public string AumidHebraw { get; set; }

        [Range(2, 180)]
        public int DafNumber { get; set; }
        [Range(1,2)]
        public int AumidNumber { get; set; }
        [Required]
        [MaxLength(10)]
        public string MeshactaID { get; set; }
        public MeshctaShas Meshacta { get; set; }
        public ICollection<ShasChazuraAumidData> ChazuraCharts { get; set; }

        public ShasChazuraAumidData BuildChart()
        {
            return new ShasChazuraAumidData
            {
                 AumidID= this.AumidID,
                MeshactaID = this.MeshactaID
            };
        }

        public override string ToString()
        {
            return AumidHebraw.Substring(0,1)+(Meshacta.MeshactaHebrawName ?? "") + " " + "דף" + " " + AumidHebraw[1..];
        }
        public string ToStringEng() => $"{Meshacta.MeshachtaEngName ?? ""} daf {DafNumber} side {AumidNumber}";
    }
}
