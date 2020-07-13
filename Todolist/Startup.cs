using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Todolist.Startup))]
namespace Todolist
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
