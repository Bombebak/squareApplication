using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SquareApplication.Startup))]
namespace SquareApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
