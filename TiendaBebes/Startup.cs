using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TiendaBebes.Startup))]
namespace TiendaBebes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
