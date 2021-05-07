using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChazuraProgram.Models;
using Microsoft.AspNetCore.Identity;
using ChazuraProgram.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace ChazuraProgram.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        public ManagerController(IChazuraUnitOfWork unitOfWork,
            UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            Data = unitOfWork;
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public IChazuraUnitOfWork Data { get; private set; }
        public UserManager<User> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; }

        [HttpGet]
        public IActionResult SponsorDefaultSett()
        {
            DefaultSponsor defaultSponsor = Data.DefaultSponsor.Get(new QueryOptions<DefaultSponsor>())
                ?? new DefaultSponsor();
            return View(defaultSponsor);
        }
        [HttpPost]
        public IActionResult SponsorDefaultSett(DefaultSponsor defaultSponsor)
        {
            if (ModelState.IsValid)
            {
                DefaultSponsor defaults = Data.DefaultSponsor.Get(new QueryOptions<DefaultSponsor>());
                if (defaults == null)
                {
                    Data.DefaultSponsor.Insert(defaultSponsor);
                }
                else
                {
                    defaults.DescriptionElse = defaultSponsor.DescriptionElse;
                    defaults.DescriptionName = defaultSponsor.DescriptionName;
                    defaults.GetSponserType = defaultSponsor.GetSponserType;
                    defaults.IsActive = defaultSponsor.IsActive;
                    Data.DefaultSponsor.Update(defaults);
                }
                Data.Save();
            }
                
            return View(defaultSponsor);
        }
        [HttpGet]
        public IActionResult EditAdminRoles()
        {
            Dictionary<string, string> users = Data.Users.List(new QueryOptions<User>
            {
                OrderBy = u => u.LastName
            }).ToDictionary(u=>u.Id,u=>u.FirstName +" " +u.LastName) ?? new Dictionary<string, string>();
            return View(users);
        }
        [HttpPost]
        public async Task<IActionResult> EditAdminRolesAsync(AdminRoleSettVM model)
        {
            User user =await UserManager.FindByIdAsync(model.UserId);
            if (model.IsAdmin)
            {
                if (!await UserManager.IsInRoleAsync(user,RoleNames.Admin))
                    await UserManager.AddToRoleAsync(user, RoleNames.Admin);
            }
            else
            {
                if (await UserManager.IsInRoleAsync(user, RoleNames.Admin))
                await UserManager.RemoveFromRoleAsync(user, RoleNames.Admin);
            }

            if (model.IsManager)
            {
                if (!await UserManager.IsInRoleAsync(user, RoleNames.Manager))

                    await UserManager.AddToRoleAsync(user, RoleNames.Manager);
            }
            else
            {
                if (await UserManager.IsInRoleAsync(user, RoleNames.Manager))
                    await UserManager.RemoveFromRoleAsync(user, RoleNames.Manager);
            }
            return RedirectToAction("EditAdminRoles");
        }
        public async Task<PartialViewResult> GetEditFormAsync(string id)
        {
            User user =await UserManager.FindByIdAsync(id);
            AdminRoleSettVM model = new AdminRoleSettVM
            {
                UserId = id,
                IsAdmin = await UserManager.IsInRoleAsync(user, RoleNames.Admin),
                IsManager = await UserManager.IsInRoleAsync(user, RoleNames.Manager)
            };
            return PartialView(model);
        }
        [AllowAnonymous]
        public JsonResult SearchUsers(string namePart)
        {
            IEnumerable<User> users = Data.Users.List(new QueryOptions<User>
            {
                Where = u => u.FirstName.Contains(namePart) || u.LastName.Contains(namePart),
                OrderBy = u => u.LastName
            });
            string json = JsonConvert.SerializeObject(users);
            if (json == null)
            {
                return Json(false);
            }
            return Json(json);
        }
    }
}
