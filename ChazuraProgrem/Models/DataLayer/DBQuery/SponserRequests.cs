using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChazuraProgram.Models;

namespace ChazuraProgram.Models
{
    public class SponserRequests : ISponserRequests
    {
        private IChazuraUnitOfWork Data { get; set; }
        public SponserRequests(IChazuraUnitOfWork unitOfWork)
        {
            Data = unitOfWork;
        }
        public List<SponserDTO> GetListOfDays(DateTime date)
        {
            List<SponserDTO> sponserDTOs = new List<SponserDTO>();
            DateTime dateStart = date;
            DateTime dateEnd = date.AddDays(30);
            while (dateStart <= dateEnd)
            {
                var sponsorStatus = Data.Sponsor.Get(new QueryOptions<SponsorData>
                {
                    Where = s => s.Date.Date == dateStart.Date
                });
                SponserDTO sponser = new SponserDTO
                {
                    Date = dateStart,
                    Status = sponsorStatus == null ? Status.rejected : sponsorStatus.Status
                };
                sponserDTOs.Add(sponser);
                dateStart= dateStart.AddDays(1);
            }
            return sponserDTOs;
        }
    }
}
