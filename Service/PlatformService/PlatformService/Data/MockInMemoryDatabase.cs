using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.PlatformDomain;
using System;
using System.Linq;

namespace PlatformService.Data
{
    public class MockInMemoryDatabase
    {
        public static void MockPopulation(IApplicationBuilder app, bool isProduction)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<ApplicationDbContext>(), isProduction);
            }
        }

        private static void SeedData(ApplicationDbContext context, bool isProduction)
        {
            if (isProduction)
            {
                Console.WriteLine(">>>Attempting to apply migration...");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($">>>Cannot run migration: {ex.Message}");
                }
            }
            if (!context.Platforms.Any())
            {
                Console.WriteLine(">>>Seeding data...");

                context.Platforms.AddRange(
                    new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine(">>>Seed data exist...");
            }
        }
    }
}
