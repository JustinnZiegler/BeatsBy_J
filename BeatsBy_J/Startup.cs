using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BeatsBy_J.Startup))]
namespace BeatsBy_J
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
