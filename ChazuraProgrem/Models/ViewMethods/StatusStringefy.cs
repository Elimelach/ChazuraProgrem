using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChazuraProgram.Models
{
    public static class StatusStringefy
    {
        public static string GetStatusStringForAvalDates(Status status)
        {
            return status switch
            {
                Status.accepted => "Reserved",
                Status.pending => "Pending",
                Status.rejected => "Available",
                _ => "Available"
            };
        }
    }
}
