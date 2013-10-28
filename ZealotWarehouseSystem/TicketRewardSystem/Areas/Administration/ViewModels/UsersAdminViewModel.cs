using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace TicketRewardSystem.Areas.Administration.ViewModels
{
    public class UsersAdminViewModel
    {
        public static Expression<Func<User, UsersAdminViewModel>> FromUser
        {
            get
            {
                return u => new UsersAdminViewModel
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    RoleId = u.Roles.Select(r => new UsersAdminRoleViewModel { RoleName = r.Role.Name, RoleId = r.RoleId }).FirstOrDefault().RoleId,
                    UserRole = u.Roles.Select(r => new UsersAdminRoleViewModel { RoleName = r.Role.Name, RoleId = r.RoleId }).FirstOrDefault()
                };
            }
        }

        [ScaffoldColumn(false)]
        public string UserId { get; set; }
        
        public string UserName { get; set; }

        public UsersAdminRoleViewModel UserRole { get; set; }

        public string RoleId { get; set; }
    }
}