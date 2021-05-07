using System;
using System.Collections.Generic;

namespace ChazuraProgram.Models
{
    public class Calculation<TGet, Tset> where TGet : IChartBuilder<Tset> where Tset : class,  IChart,new()
    {
        private DateTime StartDate { get; set; }
        private DateTime DateMoving { get; set; }
        private readonly IEnumerable<TGet> GetList;
        private readonly List<Tset> SetList = new List<Tset>();
        public Calculation(IEnumerable<TGet> gets,DateTime date)
        {
            StartDate = date;
            GetList = gets;
            DateMoving = date;
        }
        private DateTime GetDate(ChazuraTimes times)
        {
            int days = times switch
            {
                ChazuraTimes.FirstTime => 0,
                ChazuraTimes.TomorrowsChazurah => 1,
                ChazuraTimes.WeeksChazurah => 8,
                ChazuraTimes.Day30Chazurah => 38,
                ChazuraTimes.Day90Chazurah => 128,
                ChazuraTimes.Year1Chazurah => 365,
                ChazuraTimes.Year2Chazurah => 730,
                ChazuraTimes.Year3Chazurah => 1095,
                ChazuraTimes.Year4Chazurah => 1460,
                ChazuraTimes.Year5Chazurah => 1825,
                ChazuraTimes.Year6Chazurah => 2190,
                ChazuraTimes.Year7Chazurah => 2555,
                ChazuraTimes.Year8Chazurah => 2920,
                ChazuraTimes.Year9Chazurah => 3285,
                ChazuraTimes.Year10Chazurah =>3650,
                ChazuraTimes.Year11Chazurah =>4015,
                ChazuraTimes.Year12Chazurah =>4380,
                ChazuraTimes.Year13Chazurah =>4745,
                ChazuraTimes.Year14Chazurah =>5110,
                ChazuraTimes.Year15Chazurah =>5475,
                ChazuraTimes.Year16Chazurah =>5840,
                ChazuraTimes.Year17Chazurah =>6205,
                _ =>0
            };
            return DateMoving.AddDays(days);
        }
        private void First6TimesCalc()
        {
            foreach (var item in GetList)
            {
                for (int i = 0; i < 6; i++)
                {
                    ChazuraTimes times = (ChazuraTimes)i;
                    Tset obg = item.BuildChart();
                    obg.ChazuraTimes = times;
                    obg.Date = GetDate(times);
                    SetList.Add(obg);
                   
                }
                DateMoving = DateMoving.AddDays(1);
            }
        }
        private DateTime GetLatestDateFromSetList()
        {
            return SetList[^1].Date;
        }
        private void AdditionalCalculation(int years)
        {
            DateTime lastDate = GetLatestDateFromSetList();
            DateMoving = StartDate;
            foreach (var item in GetList)
            {
                bool check = true;
                int i = 6;
                while (true)
                {
                    ChazuraTimes times = (ChazuraTimes)i;
                    Tset obg = item.BuildChart();
                    obg.ChazuraTimes = times;
                    obg.Date = GetDate(times);
                   
                    if (obg.Date > lastDate && obg.Date > StartDate.AddYears(years).AddDays(10)) break;
                    SetList.Add(obg);
                    check = false;
                    i++;
                }
                DateMoving = DateMoving.AddDays(1);
                if (check) break;
            }
        }
        public List<Tset> GetChartList(int years=7)
        {
            if (years > 16 || years < 0) 
            {
                throw new ArgumentOutOfRangeException("years", "Must be an integer between 0-16");
            }
            First6TimesCalc();
            AdditionalCalculation(years);
            return SetList;
        }
    }
}
