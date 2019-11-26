using Benday.Presidents.Common;
using Benday.Presidents.Core.DataAccess;
using Benday.Presidents.Core.Models;
using Benday.Presidents.Core.Services;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benday.Presidents.Core.Factories
{
    public class ObjectFactory : IObjectFactory
    {
        public T Create<T>()
        {
            try
            {
                return Container.Resolve<T>();
            }
            catch (ResolutionFailedException ex)
            {
                throw new ObjectFactoryException(
                    String.Format("Problem creating instance of type '{0}'.", typeof(T).FullName),
                    ex);
            }
        }

        private UnityContainer _Container;
        public UnityContainer Container
        {
            get
            {
                VerifyContainerIsInitialized();

                return _Container;
            }
        }

        private void VerifyContainerIsInitialized()
        {
            if (_Container == null)
            {
                _Container = new UnityContainer();

                RegisterTypes();
            }
        }

        public void RegisterInstance<T>(T instance)
        {
            Container.RegisterInstance<T>(instance);
        }

        public void RegisterType<TFrom,  TTo>() where TTo : TFrom
        {
            Container.RegisterType<TFrom, TTo>();            
        }

        private void RegisterTypes()
        {
            RegisterInstance<IObjectFactory>(this);

            RegisterType<IPresidentsDbContext, PresidentsDbContext>();
            RegisterType<IRepository<Person>, SqlEntityFrameworkPersonRepository>();
            RegisterType<IPresidentValidatorStrategy, PresidentValidatorStrategy>();
            RegisterType<IPresidentService, PresidentService>();
        }
    }
}
