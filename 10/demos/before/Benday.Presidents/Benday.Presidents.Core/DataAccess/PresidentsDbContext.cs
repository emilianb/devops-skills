using Benday.Presidents.Common;
using Benday.Presidents.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace Benday.Presidents.Core.DataAccess
{
    public class PresidentsDbContext : DbContext, IPresidentsDbContext
    {
        
        public PresidentsDbContext() : base("name=(default)")
        {
            
        }

        protected PresidentsDbContext(string connectionString) : 
            base(connectionString)
        {

        }

        public IDbSet<Person> Persons { get; set; }
        public IDbSet<PersonFact> PersonFacts { get; set; }
        public IDbSet<Relationship> Relationships { get; set; }

        public override int SaveChanges()
        {
            CleanupOrphanedPersonFacts();
            CleanupOrphanedRelationships();

            return base.SaveChanges();
        }

        private void CleanupOrphanedPersonFacts()
        {
            var deleteThese = new List<PersonFact>();

            foreach (var deleteThis in PersonFacts.Local.Where(pf => pf.Person == null))
            {
                deleteThese.Add(deleteThis);
            }

            foreach (var deleteThis in deleteThese)
            {
                PersonFacts.Remove(deleteThis);
            }
        }

        private void CleanupOrphanedRelationships()
        {
            var deleteThese = new List<Relationship>();

            foreach (var deleteThis in Relationships.Local
                .Where(r => r.FromPerson == null))
            {
                deleteThese.Add(deleteThis);
            }

            foreach (var deleteThis in deleteThese)
            {
                Relationships.Remove(deleteThis);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Relationship>()
                .HasRequired(rel => rel.FromPerson)
                .WithMany(p => p.Relationships)
                .Map(m => m.MapKey("FromPersonId"));            
        }
    }
}
