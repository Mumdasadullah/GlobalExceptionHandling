using InMemoryDBSpecificationRepositoryUOWProject.Context;
using InMemoryDBSpecificationRepositoryUOWProject.Models;

namespace InMemoryDBSpecificationRepositoryUOWProject.Extensions
{
    public static class ApplicationCollectionExtension
    {
        public static IApplicationBuilder AddSeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<InMemoryDBContext>();
                //SeedData(dbContext);
            }

            return app;
        }

        //public static void SeedData(InMemoryDBContext context)
        //{
        //    // Seed initial data into the in-memory database if necessary
        //    if (!context.Employees.Any())
        //    {
        //        // Status Seed Data
        //        var active = new Status { Id = 1, Name = "Active" };
        //        var inActive = new Status { Id = 2, Name = "InActive" };

        //        // Country Seed Data
        //        var pakistan = new Country { Id = 1, Name = "Pakistan" };
        //        var india = new Country { Id = 2, Name = "India" };

        //        // Province Seed Data
        //        var sindh = new Province { Id = 1, Name = "Sindh", Country = pakistan };
        //        var gujarat = new Province { Id = 2, Name = "Gujarat", Country = india };

        //        // City Seed Data
        //        var karachi = new City { Id = 1, Name = "Karachi", Country = pakistan, Province = sindh };
        //        var ahmedabad = new City { Id = 2, Name = "Ahmedabad", Country = india, Province = gujarat };

        //        // Employee Seed Data
        //        var asad = new Employee { Id = 1, FirstName = "Muhammad", LastName = "Asad", UserName = "Asad22115", Email = "asadullah@test.com", Country = pakistan, Province = sindh, City = karachi, Status = active };
        //        var saif = new Employee { Id = 2, FirstName = "Muhammad", LastName = "Saif", UserName = "Saif22115", Email = "saifullah@test.com", Country = pakistan, Province = sindh, City = karachi, Status = inActive };
        //        var ramesh = new Employee { Id = 3, FirstName = "Ramesh", LastName = "Rao", UserName = "RameshRao22115", Email = "rameshrao@test.com", Country = india, Province = gujarat, City = ahmedabad, Status = active };
        //        var vidyut = new Employee { Id = 4, FirstName = "Vidyut", LastName = "Kapoor", UserName = "VidyutKapoor22115", Email = "vidyutkapoor@test.com", Country = india, Province = gujarat, City = ahmedabad, Status = inActive };

        //        context.Statuses.AddRange(active, inActive);
        //        context.Countries.AddRange(pakistan, india);
        //        context.Provinces.AddRange(sindh, gujarat);
        //        context.Cities.AddRange(karachi, ahmedabad);
        //        context.Employees.AddRange(asad, saif, ramesh, vidyut);
        //        context.SaveChanges();
        //    }
        //}
    }
}
