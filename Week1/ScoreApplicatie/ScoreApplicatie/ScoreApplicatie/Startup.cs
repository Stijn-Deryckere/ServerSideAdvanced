using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ScoreApplicatie.Startup))]
namespace ScoreApplicatie
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
