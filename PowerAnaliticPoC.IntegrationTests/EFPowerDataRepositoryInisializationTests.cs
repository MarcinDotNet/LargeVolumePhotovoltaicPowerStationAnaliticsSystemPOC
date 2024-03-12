using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerAnaliticPoC.Domain.PowerGenerator;
using PowerAnaliticPoC.Infrastructure.Persistance.EFRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace PowerAnaliticPoC.IntegrationTests
{
    [TestClass]
    public class EFPowerDataRepositoryInisializationTests
    {
        private const int NumberOfPowerGenerators = 50000;
        private DateTime StartDate = new DateTime(2023, 1, 1);
        private int iterationCount = 40000;
        private const int numberofWorkers = 20;
        private const string ConnectionString = @"Server=SERVERNEW;Database=PowerAnaliticPoCTestDB2;Trusted_Connection=True";
        [Ignore]
        [TestMethod]
        public void CratateDBRepositoryCheck_Should_Be_Success()
        {
            var dbContext = new PowerAnaliticsDBContext(ConnectionString);
            dbContext.Database.CreateIfNotExists();
        }

        [Ignore]
        [TestMethod]
        public void AddPowerGeneratorData()
        {
            string fmt = "000000";
            var dbContext = new PowerAnaliticsDBContext(ConnectionString);
            var repository = new EFPowerDataRepository(dbContext);
            List<PowerGenerator> ls = new List<PowerGenerator>();
            var seed = new Random();
            for (int i = 1; i <= NumberOfPowerGenerators; i++)
            {
                ls.Add(new PowerGenerator
                {
                    Name = "Generator_" + i.ToString(fmt),
                    Location = "Location" + i.ToString(fmt),
                    ExpectedCurrent = seed.Next(500, 1000)
                });
            }
            dbContext.PowerGenerators.AddRange(ls);
            dbContext.SaveChanges();
        }
        [Ignore]
        [TestMethod]
        public void Add_Power_Generator_Data_ForIterations()
        {
            var dbContext = new PowerAnaliticsDBContext(ConnectionString);

            
            var lastTime = dbContext.PowerGeneratorDetailData.Any() ? dbContext.PowerGeneratorDetailData.Max(x => x.TimeStamp) : StartDate;
          // dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE [dbo].[PowerGeneratorDetailDatas]");
            dbContext.SaveChanges();
            dbContext.Dispose();
            Console.WriteLine("Start adding data ");
            var nextTime = lastTime.AddSeconds(10);
            for (int j = 0; j < iterationCount; j++)
            {
                Console.WriteLine($"Start adding data for iteration {j} {DateTime.Now}");
                Task[] taskArray = new Task[numberofWorkers];
                for (int i = 0; i < numberofWorkers; i++)
                {                    
                    int powerGeneratorsPerWorker = NumberOfPowerGenerators / numberofWorkers;
                    taskArray[i] = Task.Factory.StartNew(async (object stateObj) =>
                    {
                        var paramsArr = (object[])stateObj;
                        var seed = new Random();
                        int taskNumber = (int)paramsArr[0];
                        DateTime time = (DateTime)paramsArr[1];
                        using (var dbContextInt = new PowerAnaliticsDBContext(ConnectionString))
                        {
                            var repository = new EFPowerDataRepository(dbContextInt);
                            for (int k = taskNumber * powerGeneratorsPerWorker + 1; k <= (taskNumber + 1) * powerGeneratorsPerWorker; k++)
                            {
                                var data = new PowerGeneratorDetailData
                                {
                                    GeneratorId = k,
                                    TimeStamp = time,
                                    CurrentProduction =seed.Next(0, 1000)
                                };
                               
                                repository.SavePowerGeneratorDataAsync(data);
                            }
                        }
                    }, new object[] { i, nextTime });                   
                }
                Task.WaitAll(taskArray);
                 nextTime = nextTime.AddSeconds(10);
            }
        }

    }
}



