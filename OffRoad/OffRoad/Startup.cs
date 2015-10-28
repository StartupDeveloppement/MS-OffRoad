using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OffRoad.Startup))]
namespace OffRoad
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
