using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DoAn07_11.Startup))]
namespace DoAn07_11
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
