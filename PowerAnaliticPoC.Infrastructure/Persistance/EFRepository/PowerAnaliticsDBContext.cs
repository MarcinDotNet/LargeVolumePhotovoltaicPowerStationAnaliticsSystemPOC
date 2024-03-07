using PowerAnaliticPoC.Domain.PowerGenerator;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerAnaliticPoC.Infrastructure.Persistance.EFRepository
{
    public class PowerAnaliticsDBContext: DbContext
    {
        public DbSet<PowerGenerator> PowerGenerators { get; set; }
        public DbSet<PowerGeneratorDetailData> PowerGeneratorDetailData { get; set; }   
        public DbSet<PowerGeneratorTimeRangeData> PowerGeneratorTimeRangeData { get; set; }
    }

}
