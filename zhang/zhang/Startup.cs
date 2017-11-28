using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(zhang.Startup))]
namespace zhang
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
