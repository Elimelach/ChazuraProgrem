using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChazuraProgram.Models
{
    public class CustomTypeVM
    {
        [Required]
        public string Title { get; set; }
        public string OldTitle { get; set; }
        public  DateTime Date { get; set; }
        public DateTime OldDate { get; set; }
        //[Range(1, 16)]
        //public  int YearsContinue { get; set; } 
        public bool EmailNotify { get; set; }

        public RouteDictionary Route { get; set; }

        public Dictionary<string, string> DatesList => GetDateList();

        private Dictionary<string, string> GetDateList()
        {
            DateTime dateTime = Date.Date;
            Dictionary<string, string> dates = new Dictionary<string, string>
            {
                { $"{dateTime:yyyy-MM-dd}", CalendarHebrew.GetHebrewDateString(dateTime) }
            };
            for (int i = 0; i < 50; i++)
            {
                dateTime = dateTime.AddDays(1);
                dates.Add($"{dateTime:yyyy-MM-dd}", CalendarHebrew.GetHebrewDateString(dateTime));
            }
            return dates;
        }
    }
}
