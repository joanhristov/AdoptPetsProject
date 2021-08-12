namespace AdoptPetsProject.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AdoptPetsProject.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using AdoptPetsProject.Data.Models;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDataBase (
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<AdoptPetsDbContext>();

            data.Database.Migrate();

            SeedKinds(data);

            return app;
        }

        private static void SeedKinds(AdoptPetsDbContext data)
        {
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
    }
}
