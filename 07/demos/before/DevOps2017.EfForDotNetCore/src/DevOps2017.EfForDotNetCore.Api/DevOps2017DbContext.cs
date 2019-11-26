using Microsoft.EntityFrameworkCore;
using System;

namespace DevOps2017.EfForDotNetCore.Api
{
    public class DevOps2017DbContext : DbContext
    {
        public DevOps2017DbContext(DbContextOptions options) :
            base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }

        //public DbSet<Phone> Phones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Person");

            //modelBuilder.Entity<Phone>()
            //    .ToTable("Phone")
            //    .HasOne(x => x.Person)
            //    .WithMany(y => y.PhoneNumbers);
        }
    }
}
