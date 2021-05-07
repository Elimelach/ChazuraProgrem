using ChazuraProgram.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChazuraProgram.Components
{
    public class AdminLoginUser:ViewComponent
    {
        private ISessCook SessCook { get; set; }
        public AdminLoginUser(ISessCook sessCook) => SessCook = sessCook;
        public IViewComponentResult Invoke()
        {
            User user = SessCook.GetAdminLoIntoUserAcc();
            
            return View(user);
        }
    }
}
