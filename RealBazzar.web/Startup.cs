using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RealBazzar.web.Startup))]
namespace RealBazzar.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
