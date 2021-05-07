using System;
using System.Threading.Tasks;
using ChazuraProgram.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace ChazuraProgram.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private SessCook SessCook { get; set; }
        private IChazuraUnitOfWork Data { get; set; }
        public AccountController(UserManager<User> userMngr, SignInManager<User> signInMngr, RoleManager<IdentityRole> role
            , IHttpContextAccessor ctx,IChazuraUnitOfWork data)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            roleManager = role;
            SessCook = new SessCook(ctx);
            Data = data;
        }
        public IActionResult Index()
        {
            return RedirectToAction("LogIn");
        }
        [HttpGet]
        public ViewResult Register(string returnURL = "")
        {
            RegisterViewModel model = new RegisterViewModel
            {
                ReturnUrl = returnURL
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult RegisterSponsor(string returnURL = "")
        {
            SponsorRegisterVM model = new SponsorRegisterVM
            {
                ReturnUrl = returnURL
            };
            return View(model);
        }
        [HttpPost]
        public Task<IActionResult> RegisterSponsor(SponsorRegisterVM model)
        {
            return Register(model);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            bool isSponsor = model.GetType() == typeof(SponsorRegisterVM);
            SponsorRegisterVM model2;
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    SavedPassword=model.Password
                };
                if (isSponsor)
                {
                     model2 = (SponsorRegisterVM)model;
                    user.Address = model2.Address;
                    user.City = model2.City;
                    user.State = model2.State;
                    user.ZipCode = model2.ZipCode;
                }
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, RoleNames.PlainUser);
                   if (isSponsor)
                        await AddUserToSponsorRole(model.Username);
                    await signInManager.SignInAsync(user, new AuthenticationProperties { IsPersistent=model.RememberMe,ExpiresUtc=DateTime.UtcNow.AddDays(30)});
                    

                    SignInSession(model);

                    if (!String.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        return Redirect(model.ReturnUrl);
                    else  return RedirectToAction("HomePage", "Home");
                }
                else
                {
                    foreach (var e in result.Errors)
                    {
                        ModelState.AddModelError("", e.Description);
                    }
                }
            }
            if (isSponsor)
            {
                SponsorRegisterVM model3 = (SponsorRegisterVM)model;
                return View("RegisterSponsor", model3);
            }
            return View(model);
        }
       


        [HttpGet]
        public IActionResult LogIn(string returnURL="",bool fromSponser=false)
        {
            var model = new LogViewModel
            { 
                ReturnUrl = returnURL,
                FromSponser=fromSponser
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Username, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    SignInSession(model);
                    if (!String.IsNullOrEmpty(model.ReturnUrl)&& Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("HomePage", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid username/password.");
            return View(model);
        }
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            SessCook.ClearUsersCookSess();
            return RedirectToAction("HomePage", "Home");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAddressAsync(string id)
        {
            User user =await userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["msg"] = "Can't find user.";///need additional implementing 
                return RedirectToAction("HomePage", "Home");
            }
            var model = new GetAddressVM
            {
                Username = user.UserName,
                Name = $"{user.FirstName} {user.LastName}"
            };

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetAddress(GetAddressVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user= await AddUserToSponsorRole(model.Username);
            user.Address = model.Address;
            user.City = model.City;
            user.State = model.State;
            user.ZipCode = model.ZipCode;
            Data.Users.Update(user);
            Data.Save();
            await signInManager.SignInAsync(user, new AuthenticationProperties { IsPersistent = model.RememberMe, ExpiresUtc = DateTime.UtcNow.AddDays(30) });
            SignInSession(model);
            return RedirectToAction("DatePicker", "Sponsership");
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            
            if (User.Identity.Name == null)
            {
                TempData["sessMsg"] = "can't change password before signing in!";
                return RedirectToAction("HomePage", "Home");
            }
            ChangePasswordVM model = new ChangePasswordVM
            {
                Username = User.Identity.Name
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordVM model)
        {
            User user = await userManager.FindByNameAsync(model.Username);
            var result =await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            
            if (result.Succeeded)
            {
                if (user.SavedPassword != null)
                {
                    try
                    {
                        User user2 = await userManager.FindByNameAsync(model.Username);
                        user2.SavedPassword = model.NewPassword;
                        Data.Users.Update(user2);
                        Data.Save();
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e.Message);
                        return View(model);
                    }
                }
                return RedirectToAction("HomePage", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditInfoAsync()
        {
            if (User.Identity.Name==null)
            {
                TempData["sessMsg"] = "can't change password before signing in!";
                return RedirectToAction("HomePage", "Home");
            }
            User user;
            if (SessCook.GetAdminLoIntoUserAcc() == null)
            {
                user = await userManager.FindByNameAsync(User.Identity.Name);
            }
            else
            {
                user = SessCook.GetAdminLoIntoUserAcc();
            }
            var model = new EditPersonalAllVM
            {
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserId = user.Id
            };
            if (User.IsInRole(RoleNames.Sponsor))
            {
                model.Address = user.Address;
                model.City = user.City;
                model.State = user.State;
                model.ZipCode = user.ZipCode;
                return View("EditInfoAll",model);
            }
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> EditInfoAsync(EditPersonalInfoVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            User user = await userManager.FindByIdAsync(model.UserId);
            user.UserName = model.Username;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("HomePage", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditInfoAllAsync(EditPersonalAllVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            User user = await userManager.FindByIdAsync(model.UserId);
            user.UserName = model.Username;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Address = model.Address;
            user.City = model.City;
            user.State = model.State;
            user.ZipCode = model.ZipCode;
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("HomePage", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        [NonAction]
        private void SignInSession(IRegisterLogVM model)
        {
            User user1 = userManager.FindByNameAsync(model.Username).Result;
            SessCook.SetUserObgInSession(user1);
            
        }
        [NonAction]
        private async Task<User> AddUserToSponsorRole( string userName)
        {
            User user1 = await userManager.FindByNameAsync(userName);
            IdentityRole sponserRoleObgect = await roleManager.FindByNameAsync(RoleNames.Sponsor);
            if (sponserRoleObgect == null)
                await roleManager.CreateAsync(new IdentityRole(RoleNames.Sponsor));
            await userManager.AddToRoleAsync(user1, RoleNames.Sponsor);
            return user1;
        }
    }
}
