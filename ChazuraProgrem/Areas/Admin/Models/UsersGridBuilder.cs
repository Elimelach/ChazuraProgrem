using ChazuraProgram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChazuraProgram.Areas.Admin.Models
{
    public class UsersGridBuilder:GridBuilder
    {
        public UsersGridBuilder(ISessCook sessCook) : base(sessCook) { }
        public UsersGridBuilder(ISessCook sess, GridDTO values, string defaultSortField)
            : base(sess, values, defaultSortField) { }
        public bool IsFilterByRoleAdmin => Routes.Filter == "admin";
        public bool IsFilterByRoleSponser => Routes.Filter == "sponsor";


        public bool IsSortByFirstName =>
            Routes.SortField.EqualsNoCase(nameof(User.FirstName));
        public bool IsSortLastName =>
            Routes.SortField.EqualsNoCase(nameof(User.LastName));
        public bool IsSortUserName =>
           Routes.SortField.EqualsNoCase(nameof(User.UserName));
    }
}
