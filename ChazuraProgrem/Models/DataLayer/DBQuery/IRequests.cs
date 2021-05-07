using System;
using System.Collections.Generic;

namespace ChazuraProgram.Models
{
    public interface IRequests
    {
        List<LimudimDayList> GetLimudim(User user, DateTime queryDateStart, int totolDays);
        LimudimDayList GetLimudimOf1Day(User user, DateTime queryDate);
        CustomVM GetCustomInfoForEdit(int id);
        DetailVM GetDetailVM_Loaded(User user, DetailPram detailPram);
    }
}