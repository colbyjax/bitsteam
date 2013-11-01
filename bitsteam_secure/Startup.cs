using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(bitsteam_secure.Startup))]
namespace bitsteam_secure
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
