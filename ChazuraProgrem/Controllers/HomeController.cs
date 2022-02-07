using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ChazuraProgram.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

namespace ChazuraProgram.Controllers
{
    public class HomeController : Controller
    {
        public UserManager<User> UserManager { get; private set; }

        private IHttpContextAccessor gj;

        public HomeController(UserManager<User> userManager)
        {
            UserManager = userManager;
            
        }

        //private ChazuraContext Context { get; set; }
        //public HomeController(ChazuraContext ctx,IChazuraUnitOfWork unitOfWork)
        //{
        //    Context = ctx;
        //    //DateConverter converter = new DateConverter(Context);
        //    //DateTime dateStarted = new DateTime(2019, 7, 1);
        //    //DateTime chartDate = new DateTime(2022, 4, 5);
        //    //var result = converter.ConvertChartsDate(dateStarted, chartDate, ChazurahType.ShasAllDaf);
        //    //var y = 7;
        //    //SeedShasAumidData seedShasAumidData = new SeedShasAumidData(unitOfWork, ctx);
        //    //seedShasAumidData.InsertChart();


        //}

        public async Task<IActionResult> Index()
        {
            User user = await UserManager.FindByNameAsync(User.Identity.Name ?? "");
            if (user != null)
            {
                if (await UserManager.IsInRoleAsync(user,RoleNames.Admin))
                {
                    return RedirectToAction("Index", "Users", new { area = "Admin" });
                }
            }
            return HomePage();
        }
        //[Authorize]
        public IActionResult HomePage()
        { 
            GetScheduleVM VM = new GetScheduleVM();
            return View("HomePage", VM);
        }
        [HttpPost]
        public IActionResult CreateGetScheduleUrl1(GetScheduleVM model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("GetSchedule1", "Schedule", new { date = model.Date.GetDashDate() }); 
            }
            return View("HomePage");
        }
        [HttpPost]
        public IActionResult CreateGetScheduleUrlMulti(GetScheduleVM model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("GetScheduleMultiple", "Schedule",
                        new { date = model.Date.GetDashDate(), days = model.Days }); 
            }
            return View("HomePage");
        }

        public IActionResult Instructions()
        {
            return View();
        }

       




    }
}
