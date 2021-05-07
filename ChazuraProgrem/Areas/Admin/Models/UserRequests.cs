using ChazuraProgram.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChazuraProgram.Models
{
    public class UserRequests
    {
        public UserManager<User> UserManager { get; set; }
        IEnumerable<User> UserList { get; set; } = new List<User>();
        IQueryable<User> Users { get; set; }
        private int Count => count ?? UserList.Count();
        private int? count;
        public ISessCook SessCook { get; set; }
        public UserRequests(UserManager<User> userManager,ISessCook sessCook)
        {
            UserManager = userManager;
            SessCook = sessCook;
        }
       
        public GridVM<User> GetUsersWithRoles(GridDTO grid)
        {
             UserList = UserManager.Users;
            
            foreach (var user in UserList)
            {
                user.RoleNames = UserManager.GetRolesAsync(user).Result ?? new List<string>();
            }
           
            UsersGridBuilder gridBuilder = new UsersGridBuilder(SessCook, grid, nameof(User.LastName));
            var options = new UserQueryOption
            {
                OrderByDirection = gridBuilder.CurrentRoute.SortDirection,
                PageNumber = gridBuilder.CurrentRoute.PageNumber,
                PageSize = gridBuilder.CurrentRoute.PageSize
            };
            if (gridBuilder.IsFilterByRoleAdmin)
            {
                UserList = UserList.Where(p => p.RoleNames.Contains(RoleNames.Admin));
                Users = UserList.AsQueryable();
                count = Users.Count();

            }
            else  if (gridBuilder.IsFilterByRoleSponser)
            { 
                UserList = UserList.Where(p => p.RoleNames.Contains(RoleNames.Sponsor));
                Users = UserList.AsQueryable();
                count = Users.Count();

            }
            else
            {
                Users = UserList.AsQueryable();

            }
            options.SortFilter(gridBuilder);
               
            //if (options.HasWhere)
            //{
            //    foreach (var clause in options.WhereClauses)
            //    {
            //        Users = Users.Where(clause);
            //    }
            //    count = Users.Count();
            //}
            if (options.HasOrderBy)
            {
                if (options.OrderByDirection == "asc")
                    Users = Users.OrderBy(options.OrderBy);
                else
                    Users = Users.OrderByDescending(options.OrderBy);
            }
            if (options.HasPaging)
                Users = Users.PageBy(options.PageNumber, options.PageSize);

            return new GridVM<User>
            {
                Items = Users.ToList(),
                TotalPages = gridBuilder.GetTotalPages(Count),
                CurrentRoute = gridBuilder.CurrentRoute
            };
        }
    }
}
