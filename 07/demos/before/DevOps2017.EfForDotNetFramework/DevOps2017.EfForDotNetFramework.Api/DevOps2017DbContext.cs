using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps2017.EfForDotNetFramework.Api
{
    public class DevOps2017DbContext : DbContext
    {

        public DevOps2017DbContext() : base("name=(default)")
        {

        }

        protected DevOps2017DbContext(string connectionString) :
            base(connectionString)
        {

        }

        public IDbSet<Person> Persons { get; set; }

        // public IDbSet<Phone> PhoneNumbers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<Phone>()
            //    .HasRequired(phone => phone.Person)
            //    .WithMany(person => person.PhoneNumbers);
        }
    }
}
