using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(kali.Startup))]
namespace kali
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
