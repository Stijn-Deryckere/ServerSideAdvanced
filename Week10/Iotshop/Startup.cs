using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Iotshop.Startup))]
namespace Iotshop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
