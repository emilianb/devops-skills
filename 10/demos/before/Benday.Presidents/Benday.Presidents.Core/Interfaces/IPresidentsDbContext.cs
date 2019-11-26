using Benday.Presidents.Core.DataAccess;
using Benday.Presidents.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Benday.Presidents.Common
{
    public interface IPresidentsDbContext
    {
        IDbSet<Person> Persons { get; set; }
        IDbSet<PersonFact> PersonFacts { get; set; }
        IDbSet<Relationship> Relationships { get; set; }
        DbEntityEntry Entry(object entity);
        int SaveChanges();
    }
}
