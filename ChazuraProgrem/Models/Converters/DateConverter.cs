using System;

namespace ChazuraProgram.Models
{
    public class DateConverter : IDateConverter
    {
        private Repository<DatesChart> Dates { get; set; }
        public DateConverter(ChazuraContext ctx)
        {
            Dates = new Repository<DatesChart>(ctx);
        }
        /// <summary>
        ///  Gets the date to query the database by converting your date to the charts starting date
        /// </summary>
        /// <param name="dateStarted">Date when you started the program</param>
        /// <param name="dateQuery">date you want to query</param>
        /// <param name="chazurahType"></param>
        /// <param name="code"></param>
        /// <returns>the date to query the database</returns>
        public DateTime GetCorrectDateToQuery(DateTime dateStarted, DateTime dateQuery,
            ChazurahType chazurahType, string code = "all")
        {
            DateTime chartStarted = GetDateStarted(chazurahType, code);
            TimeSpan dif = chartStarted.Date - dateStarted.Date;
            return dateQuery.Add(dif);
        }
        public DateTime ConvertChartsDate(DateTime dateStarted, DateTime chartDate,
            ChazurahType chazurahType, string code = "all")
        {
            DateTime chartStarted = GetDateStarted(chazurahType, code);
            TimeSpan dif = dateStarted.Date - chartStarted.Date;
            return chartDate.Add(dif);
        }

        private DateTime GetDateStarted(ChazurahType chazurahType, string code)
        {
            DateTime chartStarted;

            if (chazurahType == ChazurahType.ShasAllDaf)
            {
                var date = Dates.Get(new QueryOptions<DatesChart>
                {
                    Where = d => d.MeshactaID == "01brc"
                    && d.ChazurahType == ChazurahType.ShasMeshchtaDaf
                });
                if (date == null)
                {
                    throw new ArgumentException("can't get data from database check your argument for the parameter 'code'");
                }
                chartStarted = date.DateStarted;
            }
            else if (chazurahType == ChazurahType.ShasMeshchtaDaf)
            {
                var date = Dates.Get(new QueryOptions<DatesChart>
                {
                    Where = d => d.MeshactaID == code &&
                    d.ChazurahType == ChazurahType.ShasMeshchtaDaf
                });
                if (date == null)
                {
                    throw new ArgumentException("can't get data from database check your argument for the parameter 'code'");
                }
                chartStarted = date.DateStarted;

            }
            else if (chazurahType == ChazurahType.ShasAllAumid)
            {
                var date = Dates.Get(new QueryOptions<DatesChart>
                {
                    Where = d => d.MeshactaID == "01brc"
                 && d.ChazurahType == ChazurahType.ShasMeschteAumid
                });
                if (date == null)
                {
                    throw new ArgumentException("can't get data from database check your argument for the parameter 'code'");
                }
                chartStarted = date.DateStarted;

            }
            else
            {
                var date = Dates.Get(new QueryOptions<DatesChart>
                {
                    Where = d => d.MeshactaID == code &&
                    d.ChazurahType == ChazurahType.ShasMeschteAumid
                });
                if (date == null)
                {
                    throw new ArgumentException("can't get data from database check your argument for the parameter 'code'");
                }
                chartStarted = date.DateStarted;

            }

            return chartStarted;
        }
    }
}
