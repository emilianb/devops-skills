using Benday.Presidents.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace Benday.Presidents.Core.DataAccess
{
    public abstract class SqlEntityFrameworkRepositoryBase<T> : 
        IDisposable where T : class, IInt32Identity
    {
        public SqlEntityFrameworkRepositoryBase(
            IPresidentsDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context", "context is null.");

            _Context = context;
        }

        public void Dispose()
        {
            ((IDisposable)_Context).Dispose();
        }

        private IPresidentsDbContext _Context;

        protected IPresidentsDbContext Context
        {
            get
            {
                return _Context;
            }
        }

        protected void VerifyItemIsAddedOrAttachedToDbSet(IDbSet<T> dbset, T item)
        {
            if (item == null)
            {
                return;
            }
            else
            {
                if (item.Id == 0)
                {
                    dbset.Add(item);
                }
                else
                {
                    var entry = _Context.Entry(item);

                    if (entry.State == EntityState.Detached)
                    {
                        dbset.Attach(item);
                    }

                    entry.State = EntityState.Modified;
                }
            }
        }
    }
}
