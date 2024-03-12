using PowerAnaliticPoCCore.Domain.PowerGenerator;
using PowerAnaliticPoCCore.Infrastructure.Persistance.EFRepository;

namespace PowerAnaliticPoC.IntegrationTests;

[TestClass]
public class EFPowerDataRepositoryInisializationTests
{
    private const int NumberOfPowerGenerators = 50000;
    private const int numberofWorkers = 20;

    private const string ConnectionString =
        @"Server=SERVERNEW;Database=PowerAnaliticPoCTestDB2;Trusted_Connection=True";

    private readonly int iterationCount = 40000;
    private readonly DateTime StartDate = new(2023, 1, 1);

    [Ignore]
    [TestMethod]
    public void CratateDBRepositoryCheck_Should_Be_Success()
    {
        var dbContext = new PowerAnaliticsDBContext(ConnectionString);
        dbContext.Database.EnsureCreated();
    }

    [Ignore]
    [TestMethod]
    public void AddPowerGeneratorData()
    {
        var fmt = "000000";
        var dbContext = new PowerAnaliticsDBContext(ConnectionString);
        var repository = new EFPowerDataRepository(dbContext);
        var ls = new List<PowerGenerator>();
        var seed = new Random();
        for (var i = 1; i <= NumberOfPowerGenerators; i++)
            ls.Add(new PowerGenerator
            {
                Name = "Generator_" + i.ToString(fmt),
                Location = "Location" + i.ToString(fmt),
                ExpectedCurrent = seed.Next(500, 1000)
            });
        dbContext.PowerGenerators.AddRange(ls);
        dbContext.SaveChanges();
    }

    [Ignore]
    [TestMethod]
    public void Add_Power_Generator_Data_ForIterations()
    {
        var dbContext = new PowerAnaliticsDBContext(ConnectionString);


        var lastTime = dbContext.PowerGeneratorDetailData.Any()
            ? dbContext.PowerGeneratorDetailData.Max(x => x.TimeStamp)
            : StartDate;
        // dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE [dbo].[PowerGeneratorDetailDatas]");
        dbContext.SaveChanges();
        dbContext.Dispose();
        Console.WriteLine("Start adding data ");
        var nextTime = lastTime.AddSeconds(10);
        for (var j = 0; j < iterationCount; j++)
        {
            Console.WriteLine($"Start adding data for iteration {j} {DateTime.Now}");
            var taskArray = new Task[numberofWorkers];
            for (var i = 0; i < numberofWorkers; i++)
            {
                var powerGeneratorsPerWorker = NumberOfPowerGenerators / numberofWorkers;
                taskArray[i] = Task.Factory.StartNew(async stateObj =>
                {
                    var paramsArr = stateObj as object[];
                    if (paramsArr == null) return;
                    var seed = new Random();
                    var taskNumber = (int)paramsArr[0];
                    var time = (DateTime)paramsArr[1];
                    using (var dbContextInt = new PowerAnaliticsDBContext(ConnectionString))
                    {
                        var repository = new EFPowerDataRepository(dbContextInt);
                        for (var k = taskNumber * powerGeneratorsPerWorker + 1;
                             k <= (taskNumber + 1) * powerGeneratorsPerWorker;
                             k++)
                        {
                            var data = new PowerGeneratorDetailData
                            {
                                GeneratorId = k,
                                TimeStamp = time,
                                CurrentProduction = seed.Next(0, 1000)
                            };

                            await repository.SavePowerGeneratorDataAsync(data);
                        }
                    }
                }, new object[] { i, nextTime });
            }

            Task.WaitAll(taskArray);
            nextTime = nextTime.AddSeconds(10);
        }
    }
}