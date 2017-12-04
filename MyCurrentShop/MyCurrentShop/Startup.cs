using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyCurrentShop.Startup))]
namespace MyCurrentShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
