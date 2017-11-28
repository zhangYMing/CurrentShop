using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyBallShop.Startup))]
namespace MyBallShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
