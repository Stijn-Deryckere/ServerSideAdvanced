using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Language.Web.Startup))]
namespace Language.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
