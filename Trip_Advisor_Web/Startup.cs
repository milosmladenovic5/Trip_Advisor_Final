using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Trip_Advisor_Web.Startup))]
namespace Trip_Advisor_Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
