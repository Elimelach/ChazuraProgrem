using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChazuraProgram.Areas.Admin.Models
{
    public class AdminRoleSettVM
    {
        public string UserId { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsManager { get; set; }
    }
}
