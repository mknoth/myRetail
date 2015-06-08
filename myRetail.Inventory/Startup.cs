using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(myRetail.Inventory.Startup))]
namespace myRetail.Inventory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
