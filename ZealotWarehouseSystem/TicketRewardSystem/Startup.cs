using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TicketRewardSystem.Startup))]
namespace TicketRewardSystem
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder app) 
        {
            ConfigureAuth(app);
        }
    }
}
