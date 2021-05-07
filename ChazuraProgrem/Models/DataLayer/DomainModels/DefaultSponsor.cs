using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChazuraProgram.Models
{
    public class DefaultSponsor : ISponsorPoints
    {
        public int DefaultId { get; set; }
        public SponserType GetSponserType { get; set; }
        public string DescriptionName { get; set; }
        public string DescriptionElse { get; set; }
        public bool IsActive { get; set; }
    }
}
