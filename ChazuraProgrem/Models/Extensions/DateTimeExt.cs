using System;

namespace ChazuraProgram.Models
{
    public static class DateTimeExt
    {
        public static string GetDashDate(this DateTime date)
        {
            return date.ToShortDateString().Replace('/', '-');
        }
       
        public static DateTime ToDate(this string date)
        {
            DateTime.TryParse(date, out DateTime dateTime);
            return dateTime;
        }
        public static string ToHebrewDate(this DateTime date)
        {
            return CalendarHebrew.GetHebrewDateString(date);
        }
        public static DateTime ConvertChartDateToRealDate(this DateTime chartDate,DateTime limudChart_startDate
            ,ChazurahType chazurahType,IDateConverter dateConverter,string mescCode="all")
        {
            return dateConverter.ConvertChartsDate(limudChart_startDate, chartDate, chazurahType,mescCode);
        }
    }
}
