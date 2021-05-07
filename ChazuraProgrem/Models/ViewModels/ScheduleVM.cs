using System;
using System.Collections.Generic;

namespace ChazuraProgram.Models
{
    public class ScheduleVM
    {
        private readonly DateTime date;
        public ScheduleVM()
        {
            date = DateTime.Now.Date;
        }
        public ScheduleVM(DateTime dateTime)
        {
            date = dateTime;
        }
        public LimudChart LimudChart { get; set; } = new LimudChart();
        public CustomVM CustomVM { get; set; }
        public string LimudTypeString { get; set; } = "כל הש\"ס דף חדש כל יום";
        public string Name { get; set; }
        public IEnumerable<IMeshcta> MeshctasList { get; set; }
        public Dictionary<string, string> DatesList => GetDateList();
        public RouteDictionary Route { get; set; }
        private Dictionary<string, string> GetDateList()
        {
            DateTime dateTime = date;
            Dictionary<string, string> dates = new Dictionary<string, string>
            {
                { $"{dateTime:yyyy-MM-dd}", CalendarHebrew.GetHebrewDateString(dateTime) }
            };
            for (int i = 0; i < 50; i++)
            {
                dateTime= dateTime.AddDays(1);
                dates.Add($"{dateTime:yyyy-MM-dd}", CalendarHebrew.GetHebrewDateString(dateTime));
            }
            return dates;
        }
        public Dictionary<string, string> GetMeschtaDic()
        {
            Dictionary<string, string> meshctes = new Dictionary<string, string>();
            foreach (var m in MeshctasList)
            {
                meshctes.Add(m.MeshactaID, m.MeshactaHebrawName);
            }
            return meshctes;
        }
        public string GetLimudString(ChazurahType type)
        {
            string limud = type switch
            {
                ChazurahType.ShasAllDaf => "כל הש\"ס דף חדש כל יום",
                ChazurahType.ShasMeshchtaDaf => "מסכת אחת דף חדש כל יום",
                ChazurahType.ShasAllAumid => "כל הש\"ס עמוד חדש כל יום",
                ChazurahType.ShasMeschteAumid => "מסכת אחת עמוד חדש כל יום",
                _ => ""
            };
            return limud;
        }
    }
}
