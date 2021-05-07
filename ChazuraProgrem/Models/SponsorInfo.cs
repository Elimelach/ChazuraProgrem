using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChazuraProgram.Models
{
    public class SponsorInfo
    {
        public Payment Payment { get; set; }
        public SponsorData SponsorData { get; set; }
        public bool VistedDate { get; set; }
        public bool VistedPayment { get; set; }
        public bool VistedSponser{ get; set; }
    }
}
