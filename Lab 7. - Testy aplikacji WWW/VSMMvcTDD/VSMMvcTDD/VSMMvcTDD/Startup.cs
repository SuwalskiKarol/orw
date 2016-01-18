using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VSMMvcTDD.Startup))]
namespace VSMMvcTDD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
