using Microsoft.EntityFrameworkCore;
using PowerAnaliticPoCCore.Infrastructure.Persistance.EFRepository;

namespace PowerAnaliticPoC.IntegrationTests.Helpers;

public class TestLocalDB : IDisposable
{
    protected PowerAnaliticsDBContext? DbContext;
    private const string ConnectionString = @"Server=SERVERNEW;Database=PowerAnaliticPoCTestDB;Trusted_Connection=True";


    /// <summary>
    /// this can be used for manuall testing 
    /// you can run each of the test and check results on DB
    /// </summary>
    /// <exception cref="Exception"></exception>
    private void ConfigureDbContextToLocalServer()
    {
        if (!Environment.MachineName.Contains("SERVERNEW")) throw new Exception("It is only for local machine");
        var options = new DbContextOptionsBuilder();
        options.UseSqlServer(ConnectionString);
        DbContext = new PowerAnaliticsDBContext(options.Options);
    }

    public void Dispose()
    {
        DbContext?.Dispose();
    }
}