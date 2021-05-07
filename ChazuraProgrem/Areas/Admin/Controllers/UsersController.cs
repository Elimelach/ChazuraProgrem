using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChazuraProgram.Models;
using Microsoft.AspNetCore.Http;
using static ChazuraProgram.Models.ControllerHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ChazuraProgram.Areas.Admin.Models;

namespace ChazuraProgram.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Manager")]

    public class UsersController : Controller
    {
        private IChazuraUnitOfWork Data { get; set; }
        private ISessCook SessCook { get; set; }
        private UserManager<User> UserManager { get; set; }
        public UsersController(IChazuraUnitOfWork data, ISessCook sessCook,UserManager<User> userManager )
        {
            Data = data;
            SessCook = sessCook;
            UserManager = userManager;
        }
        public IActionResult Index(GridDTO gridDTO)
        {
            UserRequests userRequests = new UserRequests(UserManager, SessCook);
            GridVM<User> gridVM = userRequests.GetUsersWithRoles(gridDTO);
            return View(gridVM);
        }
        [HttpPost]
        public RedirectToActionResult SetUser(LogViewModel model)
        {
            User user = Data.Users.Get(new QueryOptions<User>
            {
                Where = u => u.UserName == model.Username
            });
            SessCook.SetUserObgInSession(user);
            SessCook.SetAdminLoIntoUserAcct(user);
            return RedirectToAction(model.ReturnUrl);
        }
        public IActionResult RemoveUserOfSess()
        {
            SessCook.ClearAdminLoIntoUserAcc();
            SessCook.ClearUsersCookSess();
            UsersGridBuilder gridBuilder = new UsersGridBuilder(SessCook);

            return RedirectToAction("Index", gridBuilder.CurrentRoute);
        }
        public async Task<IActionResult> DetailsAsync(string id)
        {
            User user = Data.Users.Get(new QueryOptions<User>
            {
                Where = u => u.Id == id,
                Includes = "LimudCharts,CustomLimudim"
            });
            if (user.LimudCharts != null)
            {
                foreach (var limudChart in user.LimudCharts)
                {
                    if (limudChart.MeshctaCode != null)
                    {
                        limudChart.MeschtaName = Data.Meshcta.Get(limudChart.MeshctaCode).MeshactaHebrawName; 
                    }
                } 
            }
            user.RoleNames = await UserManager.GetRolesAsync(user) ?? new List<string>();
            return View(user);
        }
        public async Task<IActionResult> GetRolesFormAsync(string id)
        {
            EditRolesVM model = new EditRolesVM
            {
                User = Data.Users.Get(id),
            };
            
            model.User.RoleNames = await UserManager.GetRolesAsync(model.User) ;
            model.IsPlainUser = model.User.RoleNames.Contains(RoleNames.PlainUser);
            model.IsSponsor = model.User.RoleNames.Contains(RoleNames.Sponsor);
            return PartialView("GetRolesFormAsync", model);
        }
        public async Task<IActionResult> EditRolesAsync(EditRolesVM model)
        {
            User user = await UserManager.FindByIdAsync(model.User.Id);
            if (model.IsSponsor)
            {
                if (!await UserManager.IsInRoleAsync(user, RoleNames.Sponsor))
                    await UserManager.AddToRoleAsync(user, RoleNames.Sponsor);
            }
            else
            {
                if (await UserManager.IsInRoleAsync(user, RoleNames.Sponsor))
                    await UserManager.RemoveFromRoleAsync(user, RoleNames.Sponsor);
            }

            if (model.IsPlainUser)
            {
                if (!await UserManager.IsInRoleAsync(user, RoleNames.PlainUser))

                    await UserManager.AddToRoleAsync(user, RoleNames.PlainUser);
            }
            else
            {
                if (await UserManager.IsInRoleAsync(user, RoleNames.PlainUser))
                    await UserManager.RemoveFromRoleAsync(user, RoleNames.PlainUser);
            }
            UsersGridBuilder gridBuilder = new UsersGridBuilder(SessCook);

            return RedirectToAction("Index",gridBuilder.CurrentRoute);
        }


    }
}
