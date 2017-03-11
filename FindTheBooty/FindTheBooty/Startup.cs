using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FindTheBooty.Startup))]
namespace FindTheBooty
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
