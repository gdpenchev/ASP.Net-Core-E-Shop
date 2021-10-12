namespace E_Shop.Infrastructure
{
    using E_Shop.Data;
    using E_Shop.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using static WebConstants;
    public static class ApplicationBuilderExtensions
    {   // extension method for the DB - it has to be static class and static method and we need this
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);
            SeedCategories(services);
            SeedAdministrator(services);

            return app;
        }
        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<EShopDbContext>();
            data.Database.Migrate();
        }

        private static void SeedCategories(IServiceProvider services)
        {
            var data = services.GetRequiredService<EShopDbContext>();
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category{Name = "T-Shirt"},
                new Category{Name = "Hoodie"},
                new Category{Name = "Sweatshirt"}
            });
            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            //since the method is void and the creation of a role is async, we do it with a Task.Run...
            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                {
                    return;
                }
                //creating the role of administrator
                var role = new IdentityRole { Name = AdministratorRoleName };
                await roleManager.CreateAsync(role);
                //creating the user for that role

                const string adminEmail = "admin@crs.com";
                const string adminPassword = "admin123";

                var user = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FullName = "Admin"
                };

                await userManager.CreateAsync(user, adminPassword);
                await userManager.AddToRoleAsync(user, role.Name);
            })
                .GetAwaiter()
                .GetResult();
        }
    }
}
