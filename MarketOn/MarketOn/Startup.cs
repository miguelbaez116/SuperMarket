using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MarketOn.Startup))]
namespace MarketOn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
