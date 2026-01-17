using InMemoryDBSpecificationRepositoryUOWProject.Models;
using Microsoft.EntityFrameworkCore;

namespace InMemoryDBSpecificationRepositoryUOWProject.Context
{
    public class InMemoryDBContext : DbContext
    {
        public InMemoryDBContext(DbContextOptions<InMemoryDBContext> options) : base(options)
        {
        }
        
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Status> Statuses { get; set; }
    }
}
