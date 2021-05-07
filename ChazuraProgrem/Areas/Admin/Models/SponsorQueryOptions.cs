using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChazuraProgram.Models;

namespace ChazuraProgram.Areas.Admin.Models
{
    public class SponsorQueryOptions:QueryOptions<SponsorData>
    {
        public void SortFilter(SponserGridBuilder builder)
        {
            if (builder.IsFilterByAproved)
            {
                Where = b => b.Status == Status.accepted;
            }
            else if (builder.IsFilterByPending)
            {
                Where = b => b.Status == Status.pending;
            }
            else if (builder.IsFilterByRejected)
            {
                Where = b => b.Status == Status.rejected;
            }
            if (!builder.IsFilterTimeByAll)
            {
                DateTime today = DateTime.Now.Date;
                if (builder.IsFilterTimeByToday)
                {
                    Where = b => b.Date.Date >= today;
                }
                else
                {
                    Where = b => b.Date.Date >= today.AddMonths(-1);
                }
            }
            if (builder.IsStartDate)
            {
                DateTime.TryParse(builder.CurrentRoute.StartDate, out DateTime dateTime);
                Where = b => b.Date.Date >= dateTime.Date;
            }

            if (builder.IsSortByDate)
            {
                OrderBy = b => b.Date;
            }
            else if (builder.IsSortByUser)
            {
                OrderBy = b => b.User.UserName;
            }
            else
            {
                OrderBy = b => b.Date;
            }
        }
    }
}
