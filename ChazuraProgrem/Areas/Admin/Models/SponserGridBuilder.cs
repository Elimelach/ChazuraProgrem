using ChazuraProgram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChazuraProgram.Areas.Admin.Models
{
    public class SponserGridBuilder:GridBuilder
    {
        public SponserGridBuilder(ISessCook sess) : base(sess) { }
        public SponserGridBuilder(ISessCook sess, AdminSponsorGridDTO values, string defaultSortField)
            : base(sess, values, defaultSortField) {
            Routes.FilterTime = values.FilterTime;
            Routes.StartDate = values.StartDate;
            SaveRouteSegments();
        }
        public bool IsFilterByAproved => Routes.Filter == "aproved";
        public bool IsFilterByPending => Routes.Filter == "pending";
        public bool IsFilterByRejected  => Routes.Filter == "rejected";

        public bool IsFilterTimeByToday => Routes.FilterTime == "today";
        public bool IsFilterTimeByAll => Routes.FilterTime == "all";
        public bool IsStartDate => DateTime.TryParse(Routes.StartDate, out _);


        public bool IsSortByDate =>
            Routes.SortField.EqualsNoCase(nameof(SponsorData.Date));
        public bool IsSortByUser =>
            Routes.SortField.EqualsNoCase(nameof(User.UserName));
        //public bool IsSortUserName =>
        //   Routes.SortField.EqualsNoCase(nameof(User.UserName));
    }
}
