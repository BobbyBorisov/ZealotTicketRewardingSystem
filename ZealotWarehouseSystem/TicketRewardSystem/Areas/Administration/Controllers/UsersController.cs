using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketRewardSystem.Repository;
using TicketRewardSystem.Areas.Administration.ViewModels;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TicketRewardSystem.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace TicketRewardSystem.Areas.Administration.Controllers
{
    public class UsersController : Controller
    {

        private UowData db;
        public UsersController()
        {
            this.db = new UowData();
        }
        //
        // GET: /Administration/Users/
        public ActionResult Index()
        {
            var allRoles = this.db.Roles.All().ToList();

            var allRolesItems = allRoles.Select(r => new SelectListItem
            {
                Value = r.Id,
                Text = r.Name
            });

            ViewBag.AllRoles = new SelectList(allRoles, "Id", "Name", null);

            return View();
        }

        public ActionResult ReadAllUsers([DataSourceRequest]DataSourceRequest request)
        {
            var users = this.db.Users.All().Select(UsersAdminViewModel.FromUser);
            
            DataSourceResult result = users.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateUser([DataSourceRequest]DataSourceRequest request, UsersAdminViewModel userVM)
        {
            if (userVM != null && ModelState.IsValid)
            {
                var user = this.db.Users.All().FirstOrDefault(u => u.Id == userVM.UserId);
                
                Role selectedRole = db.Roles.All().FirstOrDefault(r => r.Id == userVM.RoleId);

                if (user.Roles != null)
                {
                    user.Roles.Clear();
                }

                var newRole = new UserRole { Role = selectedRole, RoleId = selectedRole.Id, User = user, UserId = user.Id };
                user.Roles.Add(newRole);
                
                this.db.SaveChanges();
            }

            var users = this.db.Users.All().Select(UsersAdminViewModel.FromUser);

            DataSourceResult result = users.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
            //return View("Index");
        }
	}
}