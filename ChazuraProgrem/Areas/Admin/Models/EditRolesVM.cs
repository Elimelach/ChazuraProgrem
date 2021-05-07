using ChazuraProgram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChazuraProgram.Areas.Admin.Models
{
    public class EditRolesVM
    {
        public User User { get; set; }
        public bool IsSponsor { get; set; }
        public bool IsPlainUser { get; set; }
    }
}
