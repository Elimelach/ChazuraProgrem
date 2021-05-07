using System;

namespace ChazuraProgram.Models
{
    public class LimudDTO
    {
        public int LimudChartId { get; set; }
        public ChazurahType ChazurahType { get; set; }
        public String LimudId { get; set; }
        public string MeschTitle { get; set; }
        public DateTime LimudDate { get; set; }
        public ChazuraTimes ChazuraTimes { get; set; }
        public bool? Completed { get; set; } 
        public string LimudStringHebraw { get; set; }
        public string LimudStringEng { get; set; }
        public int LimudTimes
        {
            get
            {
                return (int)ChazuraTimes + 1;
            }
        }
        public string HebrawData
        {
            get
            {
                return CalendarHebrew.GetHebrewDateString(LimudDate);
            }
        }

        public int CompletedId { get;  set; }
    }
}
