namespace TicketRewardSystem.Repository
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using TicketRewardSystem.Models;

    public interface IUowData : IDisposable
    {
        IRepository<Ticket> Tickets { get; }
        IRepository<AchievementRule> Rules { get; }
        IRepository<Achievement> Achievements { get; }
        IRepository<ApplicationUser> Users { get; }
        IRepository<Role> Roles { get; }

        int SaveChanges();
    }
}
