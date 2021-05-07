using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace ChazuraProgram.Models
{
    public class User :IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Active { get; set; } = true;
        public string SavedPassword { get; set; }
        public string Address  { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public IList<SponsorData> Sponsors { get; set; }
        public IList<Payment> Payments { get; set; }
        public IList<LimudChart> LimudCharts { get; set; }
        public IList<CustomLimud> CustomLimudim { get; set; }
        [NotMapped]
        public IList<string> RoleNames { get; set; }

       
    }
}
