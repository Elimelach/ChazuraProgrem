using ChazuraProgram.Areas.Admin.Models;
using ChazuraProgram.Models;
using ChazuraProgram.MyEmail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace ChazuraProgram.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Manager")]
    public class SponsorController : Controller
    {
        public IRepository<SponsorData> SponData { get; set; }
        public IChazuraUnitOfWork Data { get; }
        public ISessCook SessCook { get; private set; }
        private IMailer Mailer { get; }
        private LinkGenerator Linker { get; }
        private IWebHostEnvironment Envi { get; }
        private UserManager<User> UserManager { get; set; }

        public SponsorController(ISessCook sessCook, IRepository<SponsorData> repository,
            IChazuraUnitOfWork unitOfWork, UserManager<User> userManager, IMailer mailer, IWebHostEnvironment webHost,LinkGenerator linkGenerator)
        {
            SponData = repository;
            Data = unitOfWork;
            UserManager = userManager;
            SessCook = sessCook;
            Mailer = mailer;
            Linker = linkGenerator;
            Envi = webHost;
        }
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }
        public IActionResult List(AdminSponsorGridDTO gridDTO)
        { 
           
            var requsts = new AdminSponsorRequests(SponData, SessCook);
            var model = requsts.GetListOfSponserdDates(gridDTO);
           
            return View(model);
        }
        [HttpPost]
        public RedirectToActionResult FilterDate(DateTime filterDate,bool clear)
        {
            var builder =new SponserGridBuilder(SessCook);
            var routes = builder.CurrentRoute;
            if (!clear)
            {
                routes.StartDate = filterDate.GetDashDate();
                routes.FilterTime = "all"; 
            }
            else
            {
                routes.StartDate = null;
            }
            return RedirectToAction("List",routes); 
        }
        [HttpPost]
        public async Task<RedirectToActionResult> SetStatusAsync(int SponsId,Status status) 
        {
            var sponsor = SponData.Get(SponsId);
            if (sponsor!=null)
            {
                if (status != Status.accepted || CheckIfDayIsStillAvailable(sponsor.Date,SponsId))
                {
                    sponsor.Status = status;
                    SponData.Update(sponsor);
                    SponData.Save();
                    if (status== Status.accepted)
                    {
                        sponsor.User = await UserManager.FindByIdAsync(sponsor.UserId);
                        sponsor.Payment = Data.Payment.Get(sponsor.PaymentId ?? 0);
                        if (sponsor.Payment != null)
                        {
                            sponsor.Payment.DateCharged = DateTime.Now;
                            SendEmailConfirming(sponsor);
                        }
                    }
                }
                else
                {
                    TempData["sessMsg"] = "!Can't update, there is already an accepted sponsor for "
                        + sponsor.Date.ToShortDateString();
                }
            }
            var builder = new SponserGridBuilder(SessCook);
            var routes = builder.CurrentRoute;
            return RedirectToAction("List", routes);
        }
        public async Task<IActionResult> DetailAsync(string id)
        {
            User user =await UserManager.FindByIdAsync(id);
            user.RoleNames = await UserManager.GetRolesAsync(user);
            return View(user);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            SponsorData sponsor;
            if (id > 0)
            {
                sponsor = SponData.Get(id); 
            }
            else
            {
                sponsor = new SponsorData
                {
                    UserId =  UserManager.FindByNameAsync(User.Identity.Name).Result.Id
                };
            }
            var builder = new SponserGridBuilder(SessCook);
            ViewBag.routes = builder.CurrentRoute;
            return View(sponsor);
        }
        [HttpPost]
        public IActionResult Edit(SponsorData sponsor)
        {
            if (ModelState.IsValid)
            {
                if (sponsor.Status != Status.accepted || CheckIfDayIsStillAvailable(sponsor.Date,sponsor.SponsId))
                {
                    if (sponsor.SponsId > 0)
                    {
                        SponData.Update(sponsor);
                    }
                    else
                    {
                        SponData.Insert(sponsor);
                    }
                    SponData.Save(); 
                }
                else
                {
                    ModelState.AddModelError("Status", "another sponsor is already accepted for this day");
                    return View(sponsor);
                }
                
                var builder = new SponserGridBuilder(SessCook);
                var routes = builder.CurrentRoute;
                return RedirectToAction("List",routes);
            }
            if (sponsor.Status == Status.accepted && !CheckIfDayIsStillAvailable(sponsor.Date,sponsor.SponsId))
            {
                ModelState.AddModelError("Status", "another sponsor is already accepted for this day");
            }
            return View(sponsor);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            SponsorData sponsor = SponData.Get(id);
            if (sponsor  != null)
            {
                SponData.Delete(sponsor);
                SponData.Save(); 
            }
            else
            {
                TempData["sessMsg"] = "! error  unable to delete.";
            }
            var builder = new SponserGridBuilder(SessCook);
            var routes = builder.CurrentRoute;
            return RedirectToAction("List", routes);
        }
        public IActionResult Payment(int id)
        {
            Payment payments = SponData.GetWithLinq(new QueryOptions<SponsorData>
            {
                Where = s => s.SponsId == id,
                Includes = "Payment"
            }).Select(s => s.Payment).FirstOrDefault();
            var builder = new SponserGridBuilder(SessCook);
            ViewBag.routes = builder.CurrentRoute;
            return View(payments);
        }
        private bool CheckIfDayIsStillAvailable(DateTime currentdate,int sponsorId)
        {
            var sponsor = SponData.Get(new QueryOptions<SponsorData>
            {
                Where = s => s.Date.Date == currentdate.Date && s.Status == Status.accepted && sponsorId !=s.SponsId
            });
            return sponsor == null;
        }
        private void SendEmailConfirming(SponsorData sponsor)
        {
            SponsorEmail sponsorEmail = new SponsorEmail(sponsor,Linker,HttpContext.Request.Host);
            EmailInfo emailInfo = sponsorEmail.GetEmailConfirming();
            if (emailInfo.GotIt)
            {
                Mailer.SendEmailAsync(emailInfo.Address, emailInfo.Subject, emailInfo.EmailBody);
            }
            else
            {
                TempData["msgToAdmin"] = emailInfo.ErrorMsg;
            }
        }
    }
}
