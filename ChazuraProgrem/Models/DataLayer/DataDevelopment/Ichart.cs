using System;

namespace ChazuraProgram.Models
{
    public interface IChart
    {
        public DateTime Date { get; set; }
        public ChazuraTimes ChazuraTimes { get; set; }

    }
}
