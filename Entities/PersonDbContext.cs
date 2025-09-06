using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PersonDbContext:DbContext
    {
        public DbSet<Person> Persons  { get; set; }
        public DbSet<Country> Countries { get; set; }

        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options) { }
  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
