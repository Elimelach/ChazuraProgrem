using System;
using System.Collections.Generic;

namespace ChazuraProgram.Models
{
    public interface ISponserRequests
    {
        List<SponserDTO> GetListOfDays(DateTime date);
    }
}