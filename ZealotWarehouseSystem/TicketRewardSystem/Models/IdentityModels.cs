using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;

namespace TicketRewardSystem.Models
{
    // You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : User
    {
        public virtual ICollection<Achievement> Achievements { get; set; }
        public string AvatarUrl { get; set; }

        public ApplicationUser() : base()
        {
            this.Achievements = new HashSet<Achievement>();
            this.Roles = new HashSet<UserRole>();
        }
    }

    public class ApplicationDbContext : IdentityDbContextWithCustomUser<ApplicationUser>
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<AchievementRule> Rules { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
    }
}