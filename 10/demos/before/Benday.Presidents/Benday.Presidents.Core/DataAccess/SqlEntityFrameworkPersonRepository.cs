using Benday.Presidents.Common;
using Benday.Presidents.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Benday.Presidents.Core.DataAccess
{
    public class SqlEntityFrameworkPersonRepository : 
        SqlEntityFrameworkRepositoryBase<Person>, IRepository<Person>
    {
        public SqlEntityFrameworkPersonRepository(
            IPresidentsDbContext context) : base(context)
        {

        }

        public void Delete(Person deleteThis)
        {
            if (deleteThis == null)
                throw new ArgumentNullException("deleteThis", "deleteThis is null.");

            var entry = Context.Entry(deleteThis);

            if (entry.State == EntityState.Detached)
            {
                Context.Persons.Attach(deleteThis);
            }

            Context.Persons.Remove(deleteThis);

            Context.SaveChanges();
        }

        public IList<Person> GetAll()
        {
            return (
                from temp in Context.Persons
                    .Include(x => x.Relationships.Select(y => y.ToPerson))
                    .Include(p => p.Facts)
                orderby temp.LastName, temp.FirstName
                select temp
                ).ToList();
        }

        public Person GetById(int id)
        {
            return (
                from temp in Context.Persons
                    .Include(x => x.Relationships.Select(y => y.ToPerson))
                    .Include(p => p.Facts)
                where temp.Id == id
                select temp
                ).FirstOrDefault();
        }

        public void Save(Person saveThis)
        {
            if (saveThis == null)
                throw new ArgumentNullException("saveThis", "saveThis is null.");

            VerifyItemIsAddedOrAttachedToDbSet(Context.Persons, saveThis);

            Context.SaveChanges();
        }
    }
}
