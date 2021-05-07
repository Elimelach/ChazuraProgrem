using System;
using System.Collections.Generic;

namespace ChazuraProgram.Models
{
    public class LimudimDayList:List<LimudDTO>
    {
        private DateTime date;
        public DateTime DateOfList {
            get
            {
                return date.Date;
            }
            set
            {
                date = value;
            }
        }
            
        public string HebrawData
        {
            get
            {
                return CalendarHebrew.GetHebrewDateString(DateOfList);
            }
        }
    }

}
