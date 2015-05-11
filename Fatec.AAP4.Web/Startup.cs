using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fatec.AAP4.Web.Startup))]
namespace Fatec.AAP4.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
