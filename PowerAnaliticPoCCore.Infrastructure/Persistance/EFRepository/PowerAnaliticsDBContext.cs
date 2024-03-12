using Microsoft.EntityFrameworkCore;
using PowerAnaliticPoCCore.Domain.PowerGenerator;


namespace PowerAnaliticPoCCore.Infrastructure.Persistance.EFRepository;

public class PowerAnaliticsDBContext : DbContext
{

    public PowerAnaliticsDBContext():base(new DbContextOptionsBuilder()
        .UseSqlServer(String.Empty).Options) 
    {
    }
    public PowerAnaliticsDBContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PowerGeneratorDetailData>()
            .HasKey(m => new { m.TimeStamp, m.GeneratorId });
        modelBuilder.Entity<PowerGeneratorTimeRangeData>()
            .HasKey(m => new { m.TimeStamp, m.TimeRange,m.GeneratorId });
        modelBuilder.Entity<PowerGenerator>()
            .HasKey(m => new { m.GeneratorId });
    }
    public DbSet<PowerGenerator> PowerGenerators { get; set; }
    public DbSet<PowerGeneratorDetailData> PowerGeneratorDetailData { get; set; }
    public DbSet<PowerGeneratorTimeRangeData> PowerGeneratorTimeRangeData { get; set; }
}