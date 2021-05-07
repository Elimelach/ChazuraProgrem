using System;
using System.Collections.Generic;

namespace ChazuraProgram.Models
{
    public class DetailVM
    {
        public List<LimudDTO> LimudDTOs { get; set; }
        public LimudDTO TargtedLimudDTO { get; set; }
        public int GoBackInfo { get; set; }
        public RouteDictionary Route { get; set; }
        public bool IsCustom { get; set; } = false;
        public DateTime DateStarted { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime DateStartedAll { get; set; }
        public DateTime DateEndAll { get; set; }
    }
}
