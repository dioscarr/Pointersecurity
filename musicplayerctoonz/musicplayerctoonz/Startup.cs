using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(musicplayerctoonz.Startup))]
namespace musicplayerctoonz
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
