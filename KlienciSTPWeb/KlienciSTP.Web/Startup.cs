using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KlienciSTP.Web.Startup))]
namespace KlienciSTP.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
