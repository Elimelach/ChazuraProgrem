using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChazuraProgram.Models;

namespace ChazuraProgram.Areas.Admin.Models
{
    public static class StatusCon
    {
        public static string GetStatusString(Status status)
        {
            return status switch
            {
                Status.accepted => "Charged",
                Status.pending => "Not Charged",
                Status.rejected => "Declined",
                _ => throw new NotImplementedException(),
            };
        }
    }
}
