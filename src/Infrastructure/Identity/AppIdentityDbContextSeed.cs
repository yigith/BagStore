using ApplicationCore.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, AppIdentityDbContext db)
        {
            await db.Database.MigrateAsync();

            if (!await roleManager.RoleExistsAsync(Authorization.Roles.ADMINISTRATOR))
            {
                await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.ADMINISTRATOR));
            }

            if (!await userManager.Users.AnyAsync(x => x.UserName == Authorization.DEFAULT_ADMIN_EMAIL))
            {
                var adminUser = new ApplicationUser()
                {
                    UserName = Authorization.DEFAULT_ADMIN_EMAIL,
                    Email = Authorization.DEFAULT_ADMIN_EMAIL,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(adminUser, Authorization.DEFAULT_PASSWORD);
                await userManager.AddToRoleAsync(adminUser, Authorization.Roles.ADMINISTRATOR);
            }

            if (!await userManager.Users.AnyAsync(x => x.UserName == Authorization.DEFAULT_USER_EMAIL))
            {
                var demoUser = new ApplicationUser()
                {
                    UserName = Authorization.DEFAULT_USER_EMAIL,
                    Email = Authorization.DEFAULT_USER_EMAIL,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(demoUser, Authorization.DEFAULT_PASSWORD);
            }
        }
    }
}
