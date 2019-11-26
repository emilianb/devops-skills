using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Benday.Presidents.WebUI.Startup))]
namespace Benday.Presidents.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
