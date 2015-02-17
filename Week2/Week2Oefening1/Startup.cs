using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Week2Oefening1.Startup))]
namespace Week2Oefening1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
