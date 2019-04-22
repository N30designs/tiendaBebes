using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(universidadContoso.Startup))]
namespace universidadContoso
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
