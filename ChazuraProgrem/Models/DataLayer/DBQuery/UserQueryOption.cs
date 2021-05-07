using ChazuraProgram.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChazuraProgram.Models
{
    public class UserQueryOption:QueryOptions<User>
    {
        public void SortFilter(UsersGridBuilder builder)
        {
            
            if (builder.IsSortByFirstName)
            {
                    OrderBy = u => u.FirstName;
            }
            else if (builder.IsSortUserName)
            {
                OrderBy = u => u.UserName;
            }
            else
            {
                OrderBy = u => u.LastName;
            }
        }
    }
}
