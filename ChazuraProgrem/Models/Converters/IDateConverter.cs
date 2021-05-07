using System;

namespace ChazuraProgram.Models
{
    public interface IDateConverter
    {
        DateTime GetCorrectDateToQuery(DateTime dateStarted, DateTime dateQuery, ChazurahType chazurahType, string code = "all");
        DateTime ConvertChartsDate(DateTime dateStarted, DateTime chartDate,
           ChazurahType chazurahType, string code = "all");
    }
}