using Microsoft.EntityFrameworkCore;
using PowerAnaliticPoCCore.Domain.PowerGenerator;


namespace PowerAnaliticPoCCore.Infrastructure.Persistance.EFRepository;

public class PowerAnaliticsDBContext : DbContext
{
    public PowerAnaliticsDBContext(DbContextOptions options) : base(options)
    {
    }

    public PowerAnaliticsDBContext(string connectionString) : base(new DbContextOptionsBuilder()
        .UseSqlServer(connectionString).Options)
    {
    }


    public DbSet<PowerGenerator> PowerGenerators { get; set; }
    public DbSet<PowerGeneratorDetailData> PowerGeneratorDetailData { get; set; }
    public DbSet<PowerGeneratorTimeRangeData> PowerGeneratorTimeRangeData { get; set; }
}