using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ZTW.Startup))]
namespace ZTW
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
