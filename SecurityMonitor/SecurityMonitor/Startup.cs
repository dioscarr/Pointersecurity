using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SecurityMonitor.Startup))]
namespace SecurityMonitor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
