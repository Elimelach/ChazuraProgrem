using System;
using System.Collections.Generic;
using System.Linq;
using ChazuraProgram.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using static ChazuraProgram.Models.ControllerHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace ChazuraProgram.Controllers
{
    [Authorize(Roles = "PlainUser,Sponsor,Admin,Manager")]

    public class ScheduleController : Controller
    {
        protected IChazuraUnitOfWork Data { get; set; }
        protected IRequests Requests { get; set; }
        protected ISessCook SessCook { get; set; }
        private readonly User user = new User();
        public ScheduleController(IChazuraUnitOfWork unitOfWork, IRequests requests,
            UserManager<User> userManager,ISessCook sessCook, IHttpContextAccessor http)
        {
            Data = unitOfWork;
            Requests = requests;
            SessCook = sessCook;
            user = LoadUserIntoSession(sessCook, userManager,http.HttpContext.User);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Index(ScheduleVM model)
        {
            model.Name = user.FirstName;
            
            return View(model);
        }
        [HttpGet]
        [Route("[controller]/[action]/{LimudChart.ChazurahType}")]
        public IActionResult GetLimudForms(ScheduleVM model)
        {
            model.Name = user.FirstName;
            LoadMesechteList(model);
            if (model.LimudChart.ChazurahType==ChazurahType.Custom)
            {
                return PartialView("_PartialCustom", new CustomVM());

            }
            return PartialView("_PartLimudFormShas", model);
        }
       

        [HttpPost]
        public IActionResult SetSchedule(ScheduleVM model)
        { 
            model.LimudChart.UserId = user.Id;
            if (ModelState.IsValid)
            {
                Data.Limud.Insert(model.LimudChart);
                Data.Save();
                return RedirectToAction("HomePage", "Home");
            }
            LoadMesechteList(model);
            return View("Index", model);
        }
        public IActionResult Custom(CustomVM model)
        {
            if (ModelState.IsValid)
            {
                List<CustomLimud> customLimuds = new List<CustomLimud>
                {
                    new CustomLimud()
                };
                var cal = new Calculation<CustomLimud, CustomLimud>(customLimuds, model.Date);
                customLimuds = cal.GetChartList(model.YearsContinue);
                foreach (var c in customLimuds)
                {
                    c.EmailNotify = model.EmailNotify;
                    c.LimudString = model.Description;
                    c.Type = model.Title;
                    c.UserId = user.Id;
                    Data.Custom.Insert(c);
                }
                Data.Save();
                return RedirectToAction("HomePage", "Home");
            }
            var schModel = new ScheduleVM();
            schModel.LimudChart.ChazurahType = ChazurahType.Custom;
            schModel.CustomVM = model;
            return View("Index", schModel);
        }
        [HttpGet]
        [Route("[controller]/[action]/{Id}/{Date}/{PageId}/{Description}/{ChazurahType}/{ChazuraTimes}")]
        public IActionResult Detail(DetailPram pram)
        {
            DetailVM model;
            try
            {
                _ = new RouteBuilder(SessCook, pram);
                 model = Requests.GetDetailVM_Loaded(user, pram);
            }
            catch (Exception)
            {

                return NotFound();
            }

            model.GoBackInfo = SessCook.GetRedirectFromDetails();
            if (model.TargtedLimudDTO.ChazurahType == ChazurahType.Custom)
                model.IsCustom = true;
            if (model.GoBackInfo== 0)
            {
                return SessionEndedReturn(this);
            }
            return View(model);
        }

       

        [Route("[controller]/[action]")]
        [Route("[controller]/[action]/date/{date}")]
        [HttpGet]
        public IActionResult GetSchedule1(GetScheduleVM model)
        {
            DateTime startDate = model.Date;
            LimudimDayList limudDTO = Requests.GetLimudimOf1Day(user, startDate);
            SessCook.SetRedirectFromDetails(1);
            return View(limudDTO);
        }
        [Route("[controller]/[action]")]
        [Route("[controller]/[action]/date/{date}")]
        [Route("[controller]/[action]/date/{date}/days-{days}")]
        [HttpGet]
        public IActionResult GetScheduleMultiple(GetScheduleVM model)
        {
            DateTime startDate = model.Date;
            List<LimudimDayList> limudim = Requests.GetLimudim(user, startDate,model.Days);
            SessCook.SetRedirectFromDetails(model.Days);
            return View(limudim);
        }
        
        [HttpPost]
        public JsonResult Completed(LimudDTO limudDTO)
        {
            CompDTO compDTO = new CompDTO();
            if (limudDTO.Completed == true)
            {
                Completed completed = Data.Completed.Get(limudDTO.CompletedId);
                Data.Completed.Delete(completed); 
                Data.Save();
                compDTO.BtnText = "Mark as completed";
                compDTO.CompVal = false;
                compDTO.CompId = 0;
                compDTO.CompText = "Incomplete";
            }
            else
            {
                Completed completed = new Completed
                {
                    LimudFinishedCode = limudDTO.LimudId,
                    LimudChartId = limudDTO.LimudChartId,
                    ChazuraTimes = limudDTO.ChazuraTimes
                };
                Data.Completed.Insert(completed);
                Data.Save();
                //Completed completed1 = Data.Completed.Get(new QueryOptions<Completed>
                //{
                //    Where = c => c.LimudChartId == limudDTO.LimudChartId &&
                //      c.LimudFinishedCode == limudDTO.LimudId && c.ChazuraTimes == limudDTO.ChazuraTimes
                //});
                compDTO.BtnText = "Unmark completed";
                compDTO.CompVal = true;
                compDTO.CompId = completed.CompId;
                compDTO.CompText = "Completed";
            }
           
            return Json(JsonConvert.SerializeObject(compDTO));
        }
        
        [HttpPost]
        public JsonResult CompletedCustem(LimudDTO limudDTO)
        {
            CompDTO compDTO = new CompDTO();

            CustomLimud custom = Data.Custom.Get(Convert.ToInt32(limudDTO.LimudChartId));
            if (limudDTO.Completed == true)
            {
                custom.Completed = false;
                compDTO.BtnText = "Mark as completed";
                compDTO.CompVal = false;
                compDTO.CompId = limudDTO.LimudChartId;
                compDTO.CompText = "Incomplete";
            }
            else
            {
                custom.Completed = true;
                compDTO.BtnText = "Unmark completed";
                compDTO.CompId = limudDTO.LimudChartId;
                compDTO.CompVal = true;
                compDTO.CompText="Completed";
            }
            Data.Custom.Update(custom);
            Data.Save();
            return Json(JsonConvert.SerializeObject(compDTO));

        }
        
        [Route("[controller]/[action]/{Id}/{Type}")]
        [HttpGet]
        public IActionResult Edit(int id,ChazurahType type)
        {
            RouteBuilder Routes = new RouteBuilder(SessCook);
            if (Routes.CurrentRoute.Count == 0 || SessCook.GetRedirectFromDetails() == 0)
            {
                return SessionEndedReturn(this);
            }
            if (type!= ChazurahType.Custom)
            {
                LimudChart limudChart = Data.Limud.Get(id);
                ScheduleVM model = new ScheduleVM(limudChart.DateStarted)
                {
                    Route = Routes.CurrentRoute,
                    LimudChart = limudChart,
                };
                LoadMesechteList(model);

                return View(model);
            }
            CustomVM customVM = Requests.GetCustomInfoForEdit(id);
            customVM.Route = Routes.CurrentRoute;
            return View("EditCustom",customVM);

        }
        [HttpPost]
        public IActionResult EditP(ScheduleVM model)
        {
            RouteBuilder routes = new RouteBuilder(SessCook);

            if (routes.CurrentRoute.Count == 0 || SessCook.GetRedirectFromDetails() == 0)
            {
                return SessionEndedReturn(this);
            }
            if (!ModelState.IsValid)
            {
                model.Route = routes.CurrentRoute;
                LoadMesechteList(model);
                return View("Edit", model);
            }
            model.LimudChart.UserId = user.Id;
            Data.Limud.Update(model.LimudChart);
            Data.Save();
            return RedirectToDaysView(routes);

        }
        [HttpGet]
        [Route("[controller]/[action]/{title}")]
        public IActionResult EditTitle(string title)///have to do the numbers of yers
        {
            RouteBuilder Routes = new RouteBuilder(SessCook);
            if (Routes.CurrentRoute.Count == 0 || SessCook.GetRedirectFromDetails() == 0)
            {
                return SessionEndedReturn(this);
            }
            CustomLimud custom = Data.Custom.Get(new QueryOptions<CustomLimud>
            {
                Where = c => c.Type == title,
                OrderBy = c => c.Date
            });
            CustomTypeVM model = new CustomTypeVM
            {
                Title = custom.Type,
                OldTitle=custom.Type,
                Date = custom.Date,
                OldDate=custom.Date,
                Route=Routes.CurrentRoute
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult EditTitle(CustomTypeVM model)
        {
            RouteBuilder routes = new RouteBuilder(SessCook);
            if (routes.CurrentRoute.Count == 0 || SessCook.GetRedirectFromDetails() == 0)
            {
                return SessionEndedReturn(this);
            }
            if (!ModelState.IsValid)
            {
                model.Route = routes.CurrentRoute;
                return View(model);
            }
            List<CustomLimud> customLimuds = Data.Custom.List(new QueryOptions<CustomLimud>
            {
                Where = c => c.Type == model.OldTitle,
                OrderBy = c => c.Date
            }).ToList();
            customLimuds = Editer.UpdateTypeCustom(customLimuds, model);
            foreach (var limud in customLimuds)
                Data.Custom.Update(limud);
            Data.Save();
            return RedirectToDaysView(routes);
        }



        [HttpPost]
        public IActionResult EditCustom(CustomVM model)
        {
            RouteBuilder routes = new RouteBuilder(SessCook);
            if (routes.CurrentRoute.Count == 0 || SessCook.GetRedirectFromDetails() == 0)
            {
                return SessionEndedReturn(this);
            }
            if (!ModelState.IsValid)
            {
                model.Route = routes.CurrentRoute;
                return View( model);
            }
            List<CustomLimud> customs = Data.Custom.List(new QueryOptions<CustomLimud>
            {
                Where = c => c.Type == model.Title && c.LimudString == model.Description,
                OrderBy = c => c.Date
            }).ToList();
            CustomLimud customToChange = Data.Custom.Get(model.CustomId);
            customs = Editer.UpdateCustom(customs, customToChange, model);

            foreach (var limud in customs)
                Data.Custom.Update(limud);

            Data.Save();
            return RedirectToDaysView(routes);
        }
        [HttpPost]
        public IActionResult Delete(LimudChart limud)
        {
            RouteBuilder Routes = new RouteBuilder(SessCook);
            if (Routes.CurrentRoute.Count == 0 || SessCook.GetRedirectFromDetails() == 0)
            {
                return SessionEndedReturn(this);
            }
            Data.Limud.Delete(limud);
            Data.Save();
            return RedirectToDaysView(Routes);
                
        }
        [HttpPost]
        public IActionResult DeleteCustom(int ChartId)
        {
            RouteBuilder Routes = new RouteBuilder(SessCook);
            if (Routes.CurrentRoute.Count == 0 || SessCook.GetRedirectFromDetails() == 0)
            {
                return SessionEndedReturn(this);
            }
            CustomLimud limud = Data.Custom.Get(ChartId) ?? new CustomLimud();
            List<CustomLimud> customs = Data.Custom.List(new QueryOptions<CustomLimud>
            {
                Where = c => c.LimudString == limud.LimudString && c.Type == limud.Type
            }).ToList() ?? new List<CustomLimud>();
            foreach (var c in customs)
            {
                Data.Custom.Delete(c);
            }
            Data.Save();
            return RedirectToDaysView(Routes);
        }

        [NonAction]
        private void LoadMesechteList(ScheduleVM model)
        {
            if (model.LimudChart.ChazurahType == ChazurahType.ShasMeshchtaDaf
                ||model.LimudChart.ChazurahType==ChazurahType.ShasMeschteAumid)
            {
                model.MeshctasList = Data.Meshcta.List(
                     new QueryOptions<MeshctaShas> { OrderBy = m => m.MeshactaID });

            }
        } 
      
        [NonAction]
        private IActionResult RedirectToDaysView(RouteBuilder routes)
        {
            int days = SessCook.GetRedirectFromDetails();

            string date = routes.CurrentRoute.Date;
            if (days == 1)
            {

                return RedirectToAction("GetSchedule1", new { date });
            }
            return RedirectToAction("GetScheduleMultiple", new { date, days });
        }
    }
}
