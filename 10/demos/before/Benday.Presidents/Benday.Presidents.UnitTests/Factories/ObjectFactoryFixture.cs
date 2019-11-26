using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Benday.Presidents.Core.Factories;
using Benday.Presidents.Common;
using Benday.Presidents.Core.Models;
using System.Collections.Generic;
using Benday.Presidents.Core.DataAccess;

namespace Benday.Presidents.UnitTests.Factories
{
    [TestClass]
    public class ObjectFactoryFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _SystemUnderTest = null;
        }
        

        private ObjectFactory _SystemUnderTest;
        public ObjectFactory SystemUnderTest
        {
            get
            {
                if (_SystemUnderTest == null)
                {
                    _SystemUnderTest = new ObjectFactory();
                }

                return _SystemUnderTest;
            }
        }

        [TestMethod]
        public void WhenCreateIsCalledForIObjectFactoryThenObjectFactoryReturnsItself()
        {
            var actual = SystemUnderTest.Create<IObjectFactory>();

            Assert.IsNotNull(actual);
            Assert.AreSame(SystemUnderTest, actual);
        }

        [TestMethod]
        public void ResolveIRepositoryOfPersonToSqlEntityFrameworkPersonRepository()
        {
            var actual = SystemUnderTest.Create<IRepository<Person>>();

            Assert.IsInstanceOfType(actual, typeof(SqlEntityFrameworkPersonRepository));
        }

        [TestMethod]
        public void RegisterInstanceReplacesDefaultRegisteredTypesForCallsToCreateObject()
        {
            var original = SystemUnderTest.Create<IRepository<Person>>();

            var expected = new SomeOtherImplementationOfRepository();

            SystemUnderTest.RegisterInstance<IRepository<Person>>(expected);

            var actual = SystemUnderTest.Create<IRepository<Person>>();
            
            Assert.AreSame(expected, actual, "RegisterInstance didn't replace default type.");
        }

        public class SomeOtherImplementationOfRepository : IRepository<Person>
        {
            public void Delete(Person deleteThis)
            {
                throw new NotImplementedException();
            }

            public IList<Person> GetAll()
            {
                throw new NotImplementedException();
            }

            public Person GetById(int id)
            {
                throw new NotImplementedException();
            }

            public void Save(Person saveThis)
            {
                throw new NotImplementedException();
            }
        }
    }
}
