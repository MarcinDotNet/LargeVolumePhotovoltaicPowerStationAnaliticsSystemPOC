using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace PowerAnaliticPoCCore.Infrastructure.Persistance.EFRepository
{
    public class DBContextFactory
    {
        public static PowerAnaliticsDBContext GetDBContext(string connectionString)
        {

            return new PowerAnaliticsDBContext(new DbContextOptionsBuilder()
                .UseSqlServer(connectionString).Options);
        }
    }
}
