using System.Collections.Generic;

namespace ChazuraProgram.Models
{
    public class SeedDatesChart
    {
        private Repository<DatesChart> DaChart { get; set; }
        private Repository<MeshctaShas> Meshchtes { get; set; }
        private Repository<ShasChazuraData> ChazChart { get; set; }
        private IChazuraUnitOfWork Data { get; set; }
        public SeedDatesChart(ChazuraContext ctx ,IChazuraUnitOfWork data)
        {
            DaChart = new Repository<DatesChart>(ctx);
            Meshchtes = new Repository<MeshctaShas>(ctx);
            ChazChart = new Repository<ShasChazuraData>(ctx);
            Data = data;
        }
        private IEnumerable<MeshctaShas> GetMeshctaShas()
        {
            QueryOptions<MeshctaShas> options = new QueryOptions<MeshctaShas> { OrderBy = m => m.MeshactaID };
             IEnumerable<MeshctaShas> meshctas = Meshchtes.List(options);
            return meshctas;
        }
        private IEnumerable<DatesChart> GetDatesCharts()
        {
            List<DatesChart> datesCharts = new List<DatesChart>();
            IEnumerable<MeshctaShas> meshctas = GetMeshctaShas();
            foreach (var m in meshctas)
            {
                QueryOptions<ShasChazuraData> options = new QueryOptions<ShasChazuraData>
                {
                    OrderBy = s => s.Date,
                    Includes = "Meshacta",
                    Where = s => s.Meshacta.MeshactaID == m.MeshactaID && s.ChazuraTimes == ChazuraTimes.FirstTime

                };
                ShasChazuraData chazuraData = ChazChart.Get(options);
                DatesChart date = new DatesChart
                {
                    ChazurahType = ChazurahType.ShasMeshchtaDaf,
                    MeshactaID = chazuraData.MeshactaID,
                    DateStarted = chazuraData.Date
                };
                datesCharts.Add(date);
            }
            return datesCharts;
        }
        public void Seed()
        {
            IEnumerable<DatesChart> datesCharts = GetDatesCharts();
            foreach (var d in datesCharts)
            {
                DaChart.Insert(d);
            }
            DaChart.Save();
        }
        private IEnumerable<DatesChart> GetDatesForAumid()
        {
            List<DatesChart> datesCharts = new List<DatesChart>();
            IEnumerable<MeshctaShas> meshctas = GetMeshctaShas();
            foreach (var m in meshctas)
            {
                QueryOptions<ShasChazuraAumidData> options = new QueryOptions<ShasChazuraAumidData>
                {
                    OrderBy = s => s.Date,
                    Where = s => s.MeshactaID == m.MeshactaID && s.ChazuraTimes == ChazuraTimes.FirstTime

                };
                ShasChazuraAumidData chazuraData = Data.ShasChazuraAumidData.Get(options);
                DatesChart date = new DatesChart
                {
                    ChazurahType = ChazurahType.ShasMeschteAumid,
                    MeshactaID = chazuraData.MeshactaID,
                    DateStarted = chazuraData.Date
                };
                datesCharts.Add(date);
            }
            return datesCharts;
        }
        public void SeedDataForAumid()
        {
            IEnumerable<DatesChart> datesCharts = GetDatesForAumid();
            foreach (var d in datesCharts)
            {
                DaChart.Insert(d);
            }
            DaChart.Save();
        }
    }
}
