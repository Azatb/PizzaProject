using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Pizza_opgave.Models;

namespace Pizza_opgave
{
    public class IdentityHelper
    {
        internal static void SeedIdentities(DbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists(RoleNames.Administrator))
                CreateUserAndRole(roleManager, userManager, RoleNames.Administrator, "admin@gmail.com", "Admin123!");

            if (roleManager.RoleExists(RoleNames.Customer))
                CreateUserAndRole(roleManager, userManager, RoleNames.Customer, "customer@gmail.com", "Customer123!");
        }
         
        public static void CreateUserAndRole(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, string roleName, string email, string password)
        {

            // 1. We need to create the role.
            var identityRole = new IdentityRole
            {
                Name = roleName
            };
            roleManager.Create(identityRole);

            // 2. We need to create the user.
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                UserName = email,
                EmailConfirmed = true
            };
            var chkUser = userManager.Create(user, password);

            // 3. Check if the user has been created. If yes then add the role to the user.
            if (chkUser.Succeeded)
            {
                userManager.AddToRole(user.Id, roleName);
            }
        }

        public class RoleNames
        {
            public const string Administrator = "Admin";
            public const string Customer = "Customer";
        }
    }
}