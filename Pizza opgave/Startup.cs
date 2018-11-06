using Microsoft.Owin;
using Owin;
using Pizza_opgave.Models;

[assembly: OwinStartupAttribute(typeof(Pizza_opgave.Startup))]
namespace Pizza_opgave
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var context = new ApplicationDbContext();
            IdentityHelper.SeedIdentities(context);
        } 
    }
}
