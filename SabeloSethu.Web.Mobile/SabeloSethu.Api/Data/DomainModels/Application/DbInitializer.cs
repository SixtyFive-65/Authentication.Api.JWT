using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Polls.Api.Data;

namespace SabeloSethu.Api.Data.DomainModels.Application
{
    public class DbInitializer
    {
        public static async Task Initialize(SabeloSethuAuthDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            context.Database.EnsureCreated();

            if (!await context.Roles.AnyAsync())
            {
                var roleAdmin = new ApplicationRole { Name = "Admin", NormalizedName = "ADMIN" };
                var roleUser = new ApplicationRole { Name = "User", NormalizedName = "USER" };

                await roleManager.CreateAsync(roleAdmin);
                await roleManager.CreateAsync(roleUser);
            }

            if (await userManager.Users.AnyAsync())
            {
                return; 
            }

            var adminEmail = "admin@admin.com";
            var adminPassword = "P@ssword1";

            var adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                NormalizedUserName = adminEmail.ToUpper(),
                NormalizedEmail = adminEmail.ToUpper()
            };

            var adminResult = await userManager.CreateAsync(adminUser, adminPassword);

            if (adminResult.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            var userEmail = "user@user.com";
            var userPassword = "P@ssword1";

            var regularUser = new ApplicationUser
            {
                UserName = userEmail,
                Email = userEmail,
                NormalizedUserName = userEmail.ToUpper(),
                NormalizedEmail = userEmail.ToUpper()
            };

            var userResult = await userManager.CreateAsync(regularUser, userPassword);

            if (userResult.Succeeded)
            {
                await userManager.AddToRoleAsync(regularUser, "User");
            }
        }
    }
}
