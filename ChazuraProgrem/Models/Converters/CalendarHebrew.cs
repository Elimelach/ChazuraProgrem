using System;
using System.Globalization;
using System.Threading;

namespace ChazuraProgram.Models
{
    public static class CalendarHebrew
    {
        public static string GetHebrewDateString(DateTime date)
        {
            HebrewCalendar hc = new HebrewCalendar();
            CultureInfo culture = CultureInfo.CreateSpecificCulture("he-IL");
            culture.DateTimeFormat.Calendar = hc;
            Thread.CurrentThread.CurrentCulture = culture;
            string hebrawDateString = date.ToShortDateString();
            culture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentCulture = culture;

            //culture.DateTimeFormat.Calendar = culture.CultureTypes
            //culture.DateTimeFormat.Calendar = culture.Calendar;
            return hebrawDateString;
        }
    }
}
