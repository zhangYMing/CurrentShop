using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CurrentShop.Startup))]
namespace CurrentShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
