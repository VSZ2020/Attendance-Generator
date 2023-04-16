using Core.Database.Entities;
using SQLiteRepository;
using SQLiteRepository.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.Tests
{
    public class ProviderManagerTest
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]

        public void TestRepositoryGet()
        {
            ProvidersManager pm = new ProvidersManager();
            var provider = pm.Get<EstablishmentEntity>();
            Assert.IsTrue(provider is EstablishmentProvider);

            var provider2 = pm.Get<EmployeeEntity>();
            Assert.IsTrue(provider2 is EmployeeProvider);
        }
    }
}
