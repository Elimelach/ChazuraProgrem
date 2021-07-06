using ChazuraProgram.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using static ChazuraProgram.Models.ControllerHelper;

namespace ChazuraProgram.Controllers
{
    [Authorize(Roles = "Sponsor,Admin,Manager")]

    public class SponsershipController : Controller
    {
        private new User User { get; set; }
        private ISessCook SessCook { get; set; }
        private IChazuraUnitOfWork Data { get; set; }
        private ISponserRequests SponserRequests { get; set; }
        //private UserManager<User> UserManager { get; set; }
        public SponsershipController(ISessCook sess, IChazuraUnitOfWork unitOfWork , IHttpContextAccessor httpContext
            , ISponserRequests requests, UserManager<User> userManager, IHttpContextAccessor http)
        {
            
            SessCook = sess;
            Data = unitOfWork;
            //UserManager = userManager;
            try
            {
                User = LoadUserIntoSession(SessCook, userManager,http.HttpContext.User);
            }
            catch (NullReferenceException)
            {
                string action = httpContext.HttpContext.Request.RouteValues["Action"].ToString();
                if(action!="Index")
                    throw new NullReferenceException("Cookie is Expierd");
            }
            SponserRequests = requests;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User == null)
            {
                string returnURL = HttpContext.Request.Path.Value;
                return RedirectToAction("LogIn", "Account",new { returnURL, fromSponser=true});
            }
            bool notadmin = !base.User.IsInRole(RoleNames.Admin);
            bool notsponsr = !base.User.IsInRole(RoleNames.Sponsor);
            bool notMngr = !base.User.IsInRole(RoleNames.Manager);
            if (notadmin && notsponsr && notMngr)
            {
                return RedirectToAction("GetAddress", "Account",new { id=User.Id});
            }
           return RedirectToAction("DatePicker");
        }
        [Route("[controller]/[action]/date/{date?}")]
        public IActionResult DatePicker(DateTime? date)
        {
            DateTime date2 = date == null ? DateTime.Now : (DateTime)date;
            List<SponserDTO> sponserDTOs = SponserRequests.GetListOfDays(date2);
            return View(sponserDTOs);
        }
        [HttpPost]
        [Route("[controller]/[action]/date/{date?}")]
        public IActionResult DatePicker(DateTime date,string _2)
        {
            SponsorInfo info = new SponsorInfo
            {
                SponsorData = new SponsorData
                {
                    Date = date,
                    UserId = User.Id
                },
                Payment = new Payment
                {
                    UserId = User.Id,
                   
                },
                VistedDate = true
            };
            SessCook.SetSponsor(info);
            return RedirectToAction("Payment");
        }
        [HttpGet]
        public IActionResult Payment()
        {
            SponsorInfo info = SessCook.GetSponsor();
            if (info == null)
            {
                return SessionEndedReturn(this);
            }
            if (!info.VistedDate)
            {
                return RedirectToAction("DatePicker");
            }
            return View(info.Payment);
        }
        [HttpPost]
        public IActionResult Payment(Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return View(payment);
            }
            SponsorInfo info = SessCook.GetSponsor();
            if (info == null)
            {
                return SessionEndedReturn(this);
            }
            info.Payment = payment;
            info.Payment.DateEnterd = DateTime.Now;
            info.Payment.Amount = 36.00m;
            info.VistedPayment = true;
            SessCook.SetSponsor(info);
            return RedirectToAction("NameDetails");

        }
        [HttpGet]
        public IActionResult NameDetails()
        {
            SponsorInfo info = SessCook.GetSponsor();
            if (info == null)
            {
                return SessionEndedReturn(this);
            }
            if (!info.VistedDate)
            {
                return RedirectToAction("DatePicker");
            }
            if (!info.VistedPayment)
            {
                return RedirectToAction("Payment");
            }
            return View(info.SponsorData);
        }
        [HttpPost]
        public IActionResult NameDetails(SponsorData sponsorData)
        {
            SponsorInfo info = SessCook.GetSponsor();
            if (info == null)
            {
                return SessionEndedReturn(this);
            }
            if (!ModelState.IsValid)
            {
                return View(sponsorData);
            }
            info.SponsorData = sponsorData;
            info.VistedSponser = true;
            SessCook.SetSponsor(info);
            return RedirectToAction("Confirm");
        }
        [HttpGet]
        public IActionResult Confirm()
        {
            SponsorInfo info = SessCook.GetSponsor();
            if (info == null)
            {
                return SessionEndedReturn(this);
            }
            if (!info.VistedDate)
            {
                return RedirectToAction("DatePicker");
            }
            if (!info.VistedPayment)
            {
                return RedirectToAction("Payment");
            }
            if (!info.VistedSponser)
            {
                return RedirectToAction("NameDetails");
            }
            return View(info);
        }
        [HttpPost]
        public IActionResult Confirm(string _1)
        {
            SponsorInfo info = SessCook.GetSponsor();
            if (info == null)
            {
                return SessionEndedReturn(this);
            }
            Data.Payment.Insert(info.Payment);
            Data.Save();
            var sponser = info.SponsorData;
            sponser.PaymentId = info.Payment.PaymentId;
            Data.Sponsor.Insert(sponser);
            Data.Save();
            SessCook.ClearSponsor();
            return RedirectToAction("HomePage", "Home");
        }
    }
}
