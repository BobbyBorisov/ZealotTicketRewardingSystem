namespace TicketRewardSystem.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TicketRewardSystem.Models;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //password of each user is "oassword"
            var adminUser = CreateOrLoadUser(context, "admin", "admin");
            var supportUser = CreateOrLoadUser(context, "support", "support");
            //var regularUser = CreateOrLoadUser(context, "regular", "regular");

            context.Tickets.AddOrUpdate(i => i.Title,
            new Ticket
            {
                Title = "Not working TFS with VS 2013",
                Description = "Team Foundation Server doest work with Visual Studio 2013 RC. ",
                PostedBy = adminUser,
                PostedOn = DateTime.Now,
                Priority = PriorityEnum.High,
                Status = StatusEnum.Open
            },
            new Ticket
            {
                Title = "Bug found in VS 2013 Preview using Code First",
                Description = "Code first does not generate the database correct.",
                PostedBy = adminUser,
                PostedOn = DateTime.Now.AddHours(2),
                Priority = PriorityEnum.Medium,
                Status = StatusEnum.Open
            },
            new Ticket
            {
                Title = "Localization issue when updating to Q1 2013 (2013.1 319)",
                Description = @"If you have just updated your project to version 2013.1.319 which is the official Q1 of 2013 and the widgets are no longer localized then you need to replace the satellite assemblies with new ones.
                The actual reason is the satellite assemblies inside the folder which contains the main Kendo DLL that you refer :",
                PostedBy = adminUser,
                PostedOn = DateTime.Now.AddHours(2),
                Priority = PriorityEnum.Medium,
                Status = StatusEnum.Open
            },
            new Ticket
            {
                Title = "Getting wrong path for texture file - 500 error",
                Description = @"This is a weird problem.  The issue occurs only occasionally.  There is an image file in the  (a Kendo UI file).  Occasionally when I try to create a new in the Scheduler I will get a 500 error.  Here is the message:
                NetworkError: 500 Internal Server Error - http://mywebsite.azurewebsites.net/error/Error?aspxerrorpath=/Sessions/textures/highlight.png",
                PostedBy = adminUser,
                PostedOn = DateTime.Now.AddHours(2),
                Priority = PriorityEnum.Medium,
                Status = StatusEnum.Open
            }
                );
        }

        private ApplicationUser CreateOrLoadUser(ApplicationDbContext context,string username, string rolename)
        {
            var existingUser = context.Users.FirstOrDefault(x => x.UserName == username);
            if (existingUser == null)
            {
                existingUser = CreateUser(context, username, rolename);
            }

            return existingUser;
        }

        private Role CreateRole(string roleName)
        {
            var role = new Role() { Name = roleName };

            return role;
        }

        private ApplicationUser CreateUser(ApplicationDbContext context, string userName, string roleName)
        {
            var role = CreateRole(roleName);

            context.Roles.AddOrUpdate(role);

            var adminUser = new ApplicationUser()
            {
                UserName = userName,
                Roles = new List<UserRole>()
            };

            context.Users.AddOrUpdate(adminUser);

            adminUser.Roles.Add(new UserRole()
            {

                Role = role,
                UserId = adminUser.Id
            });



            var logins = new UserLogin()
            {
                LoginProvider = "Local",
                ProviderKey = adminUser.UserName,
                UserId = adminUser.Id
            };

            var list = new List<UserLogin>() { logins };
            adminUser.Logins = list;


            context.UserSecrets.AddOrUpdate(new UserSecret()
            {
                Secret = "AI+y+rXkPiP9UEW7kYgYnvnVut4gBexTIQiouk880cIlMqmxaw71HH6m5DPHr1Gz4A==",
                UserName = adminUser.UserName
                
            });

            return adminUser;
        }
    }
}
