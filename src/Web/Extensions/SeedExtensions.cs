using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Web.Extensions
{
    public static class SeedExtensions
    {
        public static async Task SeedDataAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<BagStoreContext>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                await BagStoreContextSeed.SeedAsync(db);
                await AppIdentityDbContextSeed.SeedAsync(roleManager, userManager);
            }
        }
    }
}
