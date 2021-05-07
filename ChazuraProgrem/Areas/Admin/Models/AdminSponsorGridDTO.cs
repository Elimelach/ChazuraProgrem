using ChazuraProgram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChazuraProgram.Areas.Admin.Models
{
    public class AdminSponsorGridDTO:GridDTO
    {
        private string startDate;

        public string StartDate
        {
            get { return startDate; }
            set { startDate = value.GetDashDateString(); }
        }
        public string FilterTime { get; set; } = "last-month";

    }
}
