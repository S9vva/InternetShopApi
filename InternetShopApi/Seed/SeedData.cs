using InternetShopApi.Data;
using Microsoft.AspNetCore.Identity;

namespace InternetShopApi.Seed
{
    public static class SeedData
    {
        public static async Task SeedRolesAsync(IServiceProvider servicesProvider)
        {
            var roleManager = servicesProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "User" };

            foreach (var roleName in roleNames)
            {
                if(!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        public static async Task SeedAdminToUserAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByNameAsync("Alpaka"); 

            if (user != null && !(await userManager.IsInRoleAsync(user, "Admin")))
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
