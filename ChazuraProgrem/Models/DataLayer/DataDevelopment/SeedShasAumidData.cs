using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ChazuraProgram.Models
{
    public class SeedShasAumidData
    {
        private IChazuraUnitOfWork Data { get; set; }
        private ChazuraContext Context { get; set; }
        public SeedShasAumidData(IChazuraUnitOfWork unitOfWork,ChazuraContext ctx)
        {
            Data = unitOfWork;
            Context = ctx;
        }
        public void InsertChart()
        {
            List<ShasChazuraAumidData> charts;
            Calculation<Shas1Sided, ShasChazuraAumidData> calculation = new Calculation<Shas1Sided, ShasChazuraAumidData>
                (Data.Shas1Sided.List(new QueryOptions<Shas1Sided>()), DateTime.Now);
            charts = calculation.GetChartList();
            foreach (var item in charts)
            {
                Data.ShasChazuraAumidData.Insert(item);
            }
            Data.Save();
        }
        public void DeleteData()
        {
            Context.Database.ExecuteSqlRaw("delete ShasChazuraAumidData");
        }
    }
}
