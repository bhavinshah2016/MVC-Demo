using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(For_MSBC.Startup))]
namespace For_MSBC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
