using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ChazuraProgram.Models
{
    public class SeedShasChazuraData
    {
        private ChazuraContext Context { get; set; }
        private Repository<DafimShas> DafimData { get; set; }
        private Repository<ShasChazuraData> ChartData { get; set; }
        public SeedShasChazuraData(ChazuraContext ctx)
        {
            DafimData = new Repository<DafimShas>(ctx);
            ChartData = new Repository<ShasChazuraData>(ctx);
            Context = ctx;
        }
        public void InsertChart()
        {
            List<ShasChazuraData> charts;
            Calculation<DafimShas, ShasChazuraData> calculation = new Calculation<DafimShas, ShasChazuraData>
                (DafimData.List(new QueryOptions<DafimShas>()), DateTime.Now);
            charts = calculation.GetChartList();
            foreach (var item in charts)
            {
                ChartData.Insert(item);
            }
            ChartData.Save();
        }
        public void DeleteData()
        {
            Context.Database.ExecuteSqlRaw("delete ShasChazuraData");
        }
    }
}
