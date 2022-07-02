using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MusicProWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicProWeb.Models
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context, IServiceProvider serviceProvider,
            UserManager<IdentityUser> userManager)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] RoleNames = { "Admin", "User" };
            IdentityResult roleResult;
            foreach (var roleName in RoleNames)
            {
                var roleExists = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            string Email = "admin@musicproweb.com";
            string password = "Admin,./123";
            if (userManager.FindByEmailAsync(Email).Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = Email;
                user.Email = Email;
                IdentityResult result = userManager.CreateAsync(user, password).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
