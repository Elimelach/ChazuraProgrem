using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChazuraProgram.Models
{
    public static class ControllerHelper
    {
        public static IActionResult SessionEndedReturn(Controller controller )
        {
            controller.TempData["sessMsg"] = "Your session time has run out, please try again.";
            return controller.RedirectToAction("HomePage", "Home");
        }
        public static User LoadUserIntoSession(ISessCook sessCook ,UserManager<User> userManager,ClaimsPrincipal userClaim)
        {
            User userObgect = sessCook.GetUserObgFromSession();
            if (userObgect == null)
            {
                string name =  userClaim.Identity.Name;
                if (String.IsNullOrEmpty(name) )
                {
                    throw new NullReferenceException("Cookie is Expierd");
                }
               Task<User> user =  userManager.FindByNameAsync(name);
                userObgect = user.Result;
                if (userObgect==null)
                {
                    throw new NullReferenceException("Unable to find user");
                }
            }
            
            sessCook.SetUserObgInSession(userObgect);
            return userObgect;
        }
    }
}
