using ChazuraProgram.MyEmail;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChazuraProgram.Controllers
{
    public class AboutController : Controller
    {
        public AboutController(IMailer mailer)
        {
            Mailer = mailer;
        }

        private IMailer Mailer { get; set; }
        public IActionResult Index()
        {
            //Mailer.SendEmailAsync("elimelacho@Gmail.com", "test", "hello").Wait();
            return View();
        }
    }
}
