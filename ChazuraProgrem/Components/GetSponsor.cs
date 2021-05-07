using ChazuraProgram.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChazuraProgram.Components
{
    public class GetSponsor : ViewComponent
    {
        private IChazuraUnitOfWork Data { get; set; }
        public GetSponsor(IChazuraUnitOfWork unitOfWork)
        {
            Data = unitOfWork;
        }
        private ISponsorPoints GetSponsorDefault()
        {
            DefaultSponsor defaultSponsor = Data.DefaultSponsor.Get(new QueryOptions<DefaultSponsor>());
            if (defaultSponsor == null || defaultSponsor.IsActive == false)
            {
                return null;
            }
            else
                return defaultSponsor;
        }

        public IViewComponentResult Invoke()
        {
            ISponsorPoints sponsor = Data.Sponsor.Get(new QueryOptions<SponsorData>
            {
                Where = s => s.Date.Date == DateTime.Now.Date && s.Status == Status.accepted
            }) ?? GetSponsorDefault();
            return View(sponsor);
        }
    }
}
