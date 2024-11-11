using Microsoft.EntityFrameworkCore;
using Web_API.Domain.Models;

namespace Web_API.Infrastructure.Data
{
    public class Context : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Phone> Phones { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
