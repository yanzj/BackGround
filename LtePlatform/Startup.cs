using Microsoft.Owin;
using Owin;
using System.Web.Cors;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(LtePlatform.Startup))]

namespace LtePlatform
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
