using ChazuraProgram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChazuraProgram
{
    public class SponserDTO
    {
        public DateTime Date { get; set; }
        public Status Status { get; set; }
        public int SponserId { get; set; }
    }
}
