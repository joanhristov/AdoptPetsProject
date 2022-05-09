namespace AdoptPetsProject.Infrastructure
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AdoptPetsProject.Data;
    using AdoptPetsProject.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using static WebConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDataBase (
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedKinds(services);
            SeedAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<AdoptPetsDbContext>();

            data.Database.Migrate();
        }

        private static void SeedKinds(IServiceProvider services)
        {
            var data = services.GetRequiredService<AdoptPetsDbContext>();

            if (data.Kinds.Any())
            {
                return;
            }

            data.Kinds.AddRange(new[]
            {
                new Kind { Name = "Dog" },
                new Kind { Name = "Cat" },
                new Kind { Name = "Pig" },
                new Kind { Name = "Mouse" },
                new Kind { Name = "Reptile" },
                new Kind { Name = "Arthropods" },
            });

            data.SaveChanges();
        }

        private static void SeedAdministrator (IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@app.com";
                    const string adminPassword = "admin12";

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
