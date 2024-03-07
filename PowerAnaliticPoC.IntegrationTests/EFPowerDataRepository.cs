using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerAnaliticPoC.Infrastructure.Persistance.EFRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PowerAnaliticPoC.IntegrationTests
{
    [TestClass]
    public class EFPowerDataRepositoryInisializationTests
    {
        private const int NumberOfPowerGenerators = 50000;

        private const string ConnectionString = @"Server=SERVERNEW;Database=PowerAnaliticPoCTestDB;Trusted_Connection=True";
        [TestMethod]
        public void CratateDBRepositoryCheck_Should_Be_Success()
        {
            var dbContext = new PowerAnaliticsDBContext(ConnectionString);
            dbContext.Database.CreateIfNotExists();
        }


        [TestMethod]
        public void AddPowerGeneratorData()
        {
            string fmt = "000000";
            var dbContext = new PowerAnaliticsDBContext(ConnectionString);
            var repository = new EFPowerDataRepository(dbContext);
            var tasks = new List<Task>();
            if (repository.GetPowerGenerators().Count() == 0)
            {
                var seed = new Random();
                for (int i = 0; i < NumberOfPowerGenerators; i++)
                {

                    repository.SavePowerGenerator(new Domain.PowerGenerator.PowerGenerator
                    {
                        Name = "Generator_" + i.ToString(fmt),
                        Location = "Location" + i.ToString(fmt),
                        ExpectedCurrent = seed.Next(500, 10000)
                    });
                    };
                }
            }
        }
    }



