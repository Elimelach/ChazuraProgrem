using System;
using System.Collections.Generic;
using System.Data;

namespace ChazuraProgram.Models
{
    public class Requests : IRequests
    {
        private IChazuraUnitOfWork Data { get; set; }
        public Requests(IChazuraUnitOfWork unitOfWork, IDateConverter Converter)
        {
            Data = unitOfWork;
            dateConverter = Converter;
        }
        private readonly IDateConverter dateConverter;
        private LimudimDayList limudimDayList;
        private DateTime QueryDate { get; set; }
        private List<LimudChart> LimudChart = new List<LimudChart>();
        private readonly List<LimudimDayList> ListOfDayLists = new List<LimudimDayList>();
        public LimudimDayList GetLimudimOf1Day(User user, DateTime queryDate)
        {
            QueryDate = queryDate;
            ExecuteQueryMethods(user);
            return limudimDayList;
        }

        private void ExecuteQueryMethods(User user)
        {
            limudimDayList = new LimudimDayList
            {
                DateOfList = QueryDate
            };
            GetUsersLimudim(user.Id);
            GetListsOfLimudByType();
            GetCustomLimudim(user);
        }

        public List<LimudimDayList> GetLimudim(User user, DateTime queryDateStart, int totolDays)
        {
            QueryDate = queryDateStart;
            for (int i = 0; i < totolDays; i++)
            {
                ExecuteQueryMethods(user);
                ListOfDayLists.Add(limudimDayList);
                QueryDate = QueryDate.AddDays(1);
            }
            return ListOfDayLists;
        }

        private void GetUsersLimudim(string id)
        {
            LimudChart = (List<LimudChart>)Data.Limud.List(
                new QueryOptions<LimudChart>
                {
                    Where = l => l.UserId == id,
                    Includes = "CompletedList"
                });
        }
       
        private void GetListsOfLimudByType()
        {
            foreach (var LC in LimudChart)
            {
                if (LC.ChazurahType == ChazurahType.ShasAllDaf)
                {
                    AddShasDafAll(LC);
                }
                else if (LC.ChazurahType == ChazurahType.ShasMeshchtaDaf)
                {
                    AddShasDafMashcta(LC);
                }
                else if (LC.ChazurahType == ChazurahType.ShasAllAumid)
                {
                    AddShasAumidAll(LC);
                }
                else if (LC.ChazurahType == ChazurahType.ShasMeschteAumid)
                {
                    AddMeschteAumid(LC);
                }

            }
        }
        private void AddShasAumidAll(LimudChart LC)
        {
            DateTime fixedDate = dateConverter.GetCorrectDateToQuery(LC.DateStarted, QueryDate
            , LC.ChazurahType, LC.MeshctaCode);
            var options = new QueryOptions<ShasChazuraAumidData>
            {
                Includes = "Aumid.Meshacta",
                Where = s => s.Date.Date == fixedDate.Date,
                OrderBy = s => s.ChazuraTimes
            };
            LoadsListOfShasAumidDTO_IntoListOfDayLists(LC, options);
        }
        private void AddMeschteAumid(LimudChart LC)
        {
            DateTime fixedDate = dateConverter.GetCorrectDateToQuery(LC.DateStarted, QueryDate
           , LC.ChazurahType, LC.MeshctaCode);
            var options = new QueryOptions<ShasChazuraAumidData>
            {
                Includes = "Aumid.Meshacta",
                Where = s => s.Date.Date == fixedDate.Date && s.MeshactaID == LC.MeshctaCode,
                OrderBy = s => s.ChazuraTimes
            };
            LoadsListOfShasAumidDTO_IntoListOfDayLists(LC, options);
        }

        private void AddShasDafMashcta(LimudChart LC)
        {
            DateTime fixedDate = dateConverter.GetCorrectDateToQuery(LC.DateStarted, QueryDate
        , LC.ChazurahType, LC.MeshctaCode);
            var options = new QueryOptions<ShasChazuraData>
            {
                Includes = "Daf.Meshacta",
                Where = s => s.Date.Date == fixedDate.Date && s.MeshactaID == LC.MeshctaCode,
                OrderBy = s => s.ChazuraTimes
            };
            LoadsListOfShasDafDTO_IntoListOfDayLists(LC, options);
        }

        private void AddShasDafAll(LimudChart LC)
        {
            DateTime fixedDate = dateConverter.GetCorrectDateToQuery(LC.DateStarted, QueryDate
                , LC.ChazurahType);
            var options = new QueryOptions<ShasChazuraData>
            {
                Includes = "Daf.Meshacta",
                Where = s => s.Date.Date == fixedDate.Date,
                OrderBy = s => s.ChazuraTimes
            };
            LoadsListOfShasDafDTO_IntoListOfDayLists(LC, options);
        }

        private void LoadsListOfShasDafDTO_IntoListOfDayLists(LimudChart LC, QueryOptions<ShasChazuraData> options)
        {
            options.Where = c => (int)c.ChazuraTimes + 4 <= LC.YearsChazurah;
            IEnumerable<ShasChazuraData> shasChazuraData = Data.ShasChazuraData.List(options);

            foreach (var d in shasChazuraData)
            {
                LimudDTO limudDTO = new LimudDTO
                {
                    LimudDate = QueryDate,
                    ChazuraTimes = d.ChazuraTimes,
                    LimudChartId = LC.ChartId,
                    LimudId = d.DafID,
                    LimudStringHebraw = d.Daf.ToString(),
                    LimudStringEng = d.Daf.ToStringEng(),
                    ChazurahType=LC.ChazurahType
                };
                limudDTO.Completed = IsCompleted(LC, limudDTO);
                limudimDayList.Add(limudDTO);
            }
        }
        private void LoadsListOfShasAumidDTO_IntoListOfDayLists(LimudChart LC, QueryOptions<ShasChazuraAumidData> options)
        {
            options.Where = c => (int)c.ChazuraTimes + 4 <= LC.YearsChazurah;
            IEnumerable<ShasChazuraAumidData> shasChazuraData = Data.ShasChazuraAumidData.List(options);

            foreach (var d in shasChazuraData)
            {
                LimudDTO limudDTO = new LimudDTO
                {
                    LimudDate = QueryDate,
                    ChazuraTimes = d.ChazuraTimes,
                    LimudChartId = LC.ChartId,
                    LimudId = d.AumidID,
                    LimudStringHebraw = d.Aumid.ToString(),
                    LimudStringEng = d.Aumid.ToStringEng(),
                    ChazurahType=LC.ChazurahType
                };
                limudDTO.Completed = IsCompleted(LC, limudDTO);
                limudimDayList.Add(limudDTO);
            }
        }
        public DetailVM GetDetailVM_Loaded(User user,DetailPram detailPram)
        {
            DetailVM detailVM = new DetailVM();
            GetAllDatesForThisPage(user, detailPram, detailVM);
            GetDetailStartingEndingPoints(detailPram, detailVM);
            return detailVM;
        }
        private void GetAllDatesForThisPage(User user,DetailPram detail,DetailVM detailVM)
        {
            GetUsersLimudim(user.Id);
            List<LimudDTO> limuds = new List<LimudDTO>(); 
           
            if(detail.ChazurahType == ChazurahType.Custom)
            {
                List<CustomLimud> customs = (List<CustomLimud>)Data.Custom.List(new QueryOptions<CustomLimud>
                {
                    Where = c => c.LimudString == detail.Description.DashToSpace()
                    && c.Type ==detail.PageId.DashToSpace() && c.UserId == user.Id,
                    OrderBy = c => c.Date
                });
                foreach (var c in customs)
                {
                    LimudDTO limud = new LimudDTO
                    {
                        ChazuraTimes = c.ChazuraTimes,
                        LimudDate = c.Date,
                        LimudStringHebraw = c.LimudString,
                        LimudStringEng = c.LimudString,
                        Completed = c.Completed,
                        LimudId = c.Type,///have to take this out /?
                        ChazurahType=ChazurahType.Custom,
                        LimudChartId=c.CustomID,
                        MeschTitle=c.LimudString
                    };
                    if (limud.LimudDate.Date == detail.Date.ToDate().Date)
                        detailVM.TargtedLimudDTO = limud;
                    else
                    limuds.Add(limud);
                }
                detailVM.LimudDTOs = limuds;
                return;
            }
            LimudChart limudCharts = Data.Limud.Get(detail.Id);
            if (limudCharts.ChazurahType == ChazurahType.ShasAllDaf ||limudCharts.ChazurahType == ChazurahType.ShasMeshchtaDaf) 
            {
                List<ShasChazuraData> shasChazuras = (List<ShasChazuraData>)Data.ShasChazuraData.List(new QueryOptions<ShasChazuraData>
                {
                    Where=s=>s.DafID==detail.PageId,
                    Includes= "Daf.Meshacta",
                    OrderBy=s=>s.Date
                });
                
                foreach (var item in shasChazuras)
                {
                    LimudDTO limud = new LimudDTO
                    {
                        ChazuraTimes = item.ChazuraTimes,
                        LimudChartId = detail.Id,
                        LimudStringHebraw = item.Daf.ToString(),
                        LimudStringEng = item.Daf.ToStringEng(),
                        LimudId = item.DafID,
                        ChazurahType=limudCharts.ChazurahType,
                       

                    };
                    limud.Completed = IsCompleted(limudCharts, limud);
                    if (limudCharts.ChazurahType==ChazurahType.ShasAllDaf)
                    {
                        limud.LimudDate = dateConverter.ConvertChartsDate(limudCharts.DateStarted, item.Date
                            , ChazurahType.ShasAllDaf);
                        limud.MeschTitle = "כל הש\"ס";
                    }
                    else 
                    {
                        limud.LimudDate = dateConverter.ConvertChartsDate(limudCharts.DateStarted, item.Date
                           , ChazurahType.ShasMeshchtaDaf,item.MeshactaID);
                        limud.MeschTitle = item.Daf.Meshacta.MeshactaHebrawName;
                    }
                    if (limud.LimudDate.Date == detail.Date.ToDate().Date)
                        detailVM.TargtedLimudDTO = limud;
                    else
                        limuds.Add(limud);
                }
            }
            else if (limudCharts.ChazurahType == ChazurahType.ShasAllAumid || limudCharts.ChazurahType == ChazurahType.ShasMeschteAumid)
            {
                List<ShasChazuraAumidData> shasChazuras = (List<ShasChazuraAumidData>)Data.ShasChazuraAumidData.List(new QueryOptions<ShasChazuraAumidData>
                {
                    Where = s => s.AumidID == detail.PageId,
                    Includes = "Aumid.Meshacta",
                    OrderBy = s => s.Date
                });

                foreach (var item in shasChazuras)
                {
                    LimudDTO limud = new LimudDTO
                    {
                        ChazuraTimes = item.ChazuraTimes,
                        LimudChartId = detail.Id,
                        LimudStringHebraw = item.Aumid.ToString(),
                        LimudStringEng = item.Aumid.ToStringEng(),
                        LimudId = item.AumidID,
                        ChazurahType=limudCharts.ChazurahType
                    };
                    limud.Completed = IsCompleted(limudCharts, limud);
                    if (limudCharts.ChazurahType == ChazurahType.ShasAllAumid)
                    {
                        limud.LimudDate = dateConverter.ConvertChartsDate(limudCharts.DateStarted, item.Date
                            , ChazurahType.ShasAllAumid);
                        limud.MeschTitle = "כל הש\"ס";
                    }
                    else
                    {
                        limud.LimudDate = dateConverter.ConvertChartsDate(limudCharts.DateStarted, item.Date
                           , ChazurahType.ShasMeschteAumid, item.MeshactaID);
                        limud.MeschTitle = item.Aumid.Meshacta.MeshactaHebrawName;
                    }
                    if (limud.LimudDate.Date == detail.Date.ToDate().Date)
                        detailVM.TargtedLimudDTO = limud;
                    else
                        limuds.Add(limud);
                }
            }
            detailVM.LimudDTOs = limuds;
            return;

        }

        private bool IsCompleted(LimudChart l, LimudDTO dTO)
        {
            l.CompletedList ??= new List<Completed>();
            foreach (var c in l.CompletedList)
            {
                Completed completed = Data.Completed.Get(new QueryOptions<Completed>
                {
                    Where = f => f.ChazuraTimes == dTO.ChazuraTimes && f.LimudFinishedCode == dTO.LimudId
                });
                dTO.CompletedId = (completed != null) ? completed.CompId : 0;
                if (completed != null) return true;
            }
            return false;
        }
        private void GetCustomLimudim(User user)
        {
            List<CustomLimud> customs = (List<CustomLimud>)(Data.Custom.List(new QueryOptions<CustomLimud>
            {
                Where = c => c.UserId == user.Id && c.Date == QueryDate.Date
            }) ?? new List<CustomLimud>());
            foreach (var c in customs)
            {
                LimudDTO limudDTO = new LimudDTO
                {
                    LimudDate = c.Date,
                    ChazuraTimes = c.ChazuraTimes,
                    Completed = c.Completed,
                    LimudChartId = c.CustomID,
                    LimudId = c.Type,
                    LimudStringHebraw = c.LimudString,
                    LimudStringEng=c.LimudString,
                    ChazurahType=ChazurahType.Custom
                };
                limudimDayList.Add(limudDTO);
            }
            
        }
        public CustomVM GetCustomInfoForEdit(int id)
        {
            CustomLimud customLimud = Data.Custom.Get(new QueryOptions<CustomLimud>
            {
                Where = l => l.LimudString == Data.Custom.Get(id).LimudString && l.Type == Data.Custom.Get(id).Type,
                OrderBy = l => l.Date
            });
            if (customLimud==null)
            {
                throw new DataException("Can't find this id");
            }
            CustomVM custom = new CustomVM
            {
                CustomId = customLimud.CustomID,
                Date=customLimud.Date,
                Description=customLimud.LimudString,
                Title=customLimud.Type,
                EmailNotify=customLimud.EmailNotify
                
            };
            return custom;
        }
        private string GetMeschteCodePageId(string dafId, ChazurahType type)
        {
            return type switch
            {
                ChazurahType.ShasAllDaf => dafId[0..^3],
                ChazurahType.ShasMeshchtaDaf => dafId[0..^3],
                ChazurahType.ShasAllAumid => dafId[0..^5],
                ChazurahType.ShasMeschteAumid => dafId[0..^5],
                ChazurahType.Custom => dafId.DashToSpace(),
                _ => throw new NotImplementedException()
            };
        }
        private void GetDetailStartingEndingPoints(DetailPram detail,DetailVM detailVM)
        {
            
            string mescCode = GetMeschteCodePageId(detail.PageId, detail.ChazurahType);
            string mescCodeToEnterInConverter = (detail.ChazurahType == ChazurahType.ShasAllDaf
                || detail.ChazurahType == ChazurahType.ShasAllAumid) ? "all" : mescCode;
           
            if (detail.ChazurahType == ChazurahType.Custom) 
            {
                string userId = Data.Custom.Get(detail.Id).UserId;
                var optins = new QueryOptions<CustomLimud>
                {
                    Where = c => c.Type == mescCode && c.ChazuraTimes == detail.ChazuraTimes && c.UserId == userId,
                    OrderBy = c => c.Date
                };
                detailVM.DateStarted = Data.Custom.Get(optins).Date;
                optins.OrderByDirection = "desc";
                detailVM.DateEnd = Data.Custom.Get(optins).Date;
                return;
            }
          
            LimudChart limudCharts = Data.Limud.Get(detail.Id);

           
            if (detail.ChazurahType == ChazurahType.ShasMeshchtaDaf || detail.ChazurahType == ChazurahType.ShasAllDaf)
            {
                var options = new QueryOptions<ShasChazuraData>
                {
                    Where = s => s.ChazuraTimes == detail.ChazuraTimes,
                    OrderBy = s => s.Date
                };
                options.Where = s => s.MeshactaID == mescCode;
                detailVM.DateStarted = Data.ShasChazuraData.Get(options).Date
                    .ConvertChartDateToRealDate(limudCharts.DateStarted, detail.ChazurahType, dateConverter,mescCodeToEnterInConverter);
                options.OrderByDirection = "desc";
                detailVM.DateEnd = Data.ShasChazuraData.Get(options).Date
                    .ConvertChartDateToRealDate(limudCharts.DateStarted, detail.ChazurahType, dateConverter,mescCodeToEnterInConverter);

                if (detail.ChazurahType==ChazurahType.ShasAllDaf)
                {
                    options= new QueryOptions<ShasChazuraData>
                    {
                        Where = s => s.ChazuraTimes == detail.ChazuraTimes,
                        OrderBy = s => s.Date
                    };
                    detailVM.DateStartedAll = Data.ShasChazuraData.Get(options).Date
                    .ConvertChartDateToRealDate(limudCharts.DateStarted, detail.ChazurahType, dateConverter);
                    options.OrderByDirection = "desc";
                    detailVM.DateEndAll= Data.ShasChazuraData.Get(options).Date
                    .ConvertChartDateToRealDate(limudCharts.DateStarted, detail.ChazurahType, dateConverter);
                }
                return;
            }
            if (detail.ChazurahType == ChazurahType.ShasMeschteAumid || detail.ChazurahType == ChazurahType.ShasAllAumid)
            {
                var options = new QueryOptions<ShasChazuraAumidData>
                {
                    Where = s => s.ChazuraTimes == detail.ChazuraTimes,
                    OrderBy = s => s.Date
                };
                options.Where = s => s.MeshactaID == mescCode;
                detailVM.DateStarted = Data.ShasChazuraAumidData.Get(options).Date
                    .ConvertChartDateToRealDate(limudCharts.DateStarted, detail.ChazurahType, dateConverter, mescCodeToEnterInConverter);
                options.OrderByDirection = "desc";
                detailVM.DateEnd = Data.ShasChazuraAumidData.Get(options).Date
                    .ConvertChartDateToRealDate(limudCharts.DateStarted, detail.ChazurahType, dateConverter, mescCodeToEnterInConverter);

                if (detail.ChazurahType == ChazurahType.ShasAllAumid)
                {
                    options = new QueryOptions<ShasChazuraAumidData>
                    {
                        Where = s => s.ChazuraTimes == detail.ChazuraTimes,
                        OrderBy = s => s.Date
                    };
                    detailVM.DateStartedAll = Data.ShasChazuraAumidData.Get(options).Date
                    .ConvertChartDateToRealDate(limudCharts.DateStarted, detail.ChazurahType, dateConverter);
                    options.OrderByDirection = "desc";
                    detailVM.DateEndAll = Data.ShasChazuraAumidData.Get(options).Date
                    .ConvertChartDateToRealDate(limudCharts.DateStarted, detail.ChazurahType, dateConverter);
                }
                return;
            }


        }

    }
}
