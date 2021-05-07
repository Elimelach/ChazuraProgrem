using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChazuraProgram.Models
{
    public class CustomVM 
    {
        public int CustomId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Now.Date;
        [Range(1, 16)]
        public int YearsContinue { get; set; } = 7;
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
