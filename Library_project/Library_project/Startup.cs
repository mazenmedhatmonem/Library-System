using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Library_project.Startup))]
namespace Library_project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
