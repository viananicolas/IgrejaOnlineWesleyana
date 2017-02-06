using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IgrejaOnlineWesleyana.Startup))]
namespace IgrejaOnlineWesleyana
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
