using ChazuraProgram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChazuraProgram.Areas.Admin.Models
{
    public class AdminSponsorRequests
    {
        public IRepository<SponsorData> SponData { get; set; }
        ISessCook SessCook { get; set; }
        public AdminSponsorRequests(IRepository<SponsorData> repository,ISessCook sessCook)
        {
            SponData = repository;
            SessCook = sessCook;
        }

        public GridVM<SponsorData> GetListOfSponserdDates(AdminSponsorGridDTO gridDTO)
        {
            SponserGridBuilder gridBuilder = new SponserGridBuilder(SessCook, gridDTO, nameof(SponsorData.Date));
            var options = new SponsorQueryOptions
            {
                Includes = "User",
                PageNumber = gridDTO.PageNumber,
                PageSize = gridDTO.PageSize,
                OrderByDirection = gridDTO.SortDirection
            };
            options.SortFilter(gridBuilder);
            GridVM<SponsorData> gridVM = new GridVM<SponsorData>
            {
                Items = (List<SponsorData>)(SponData.List(options) ?? new List<SponsorData>()),
                CurrentRoute = gridBuilder.CurrentRoute,
                TotalPages = gridBuilder.GetTotalPages(SponData.Count)
            };
            return gridVM;
        }
    }
}
