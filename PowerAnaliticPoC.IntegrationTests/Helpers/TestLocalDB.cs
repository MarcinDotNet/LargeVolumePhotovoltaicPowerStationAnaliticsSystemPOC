using PowerAnaliticPoC.Infrastructure.Persistance.EFRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace PowerAnaliticPoC.IntegrationTests.Helpers
{
    public class TestLocalDB:IDisposable
    {
        protected PowerAnaliticsDBContext DbContext;
        private const string ConnectionString = @"Server=SERVERNEW;Database=PowerAnaliticPoCTestDB;Trusted_Connection=True";



        /// <summary>
        /// this can be used for manuall testing 
        /// you can run each of the test and check results on DB
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void ConfigureDbContextToLocalServer()
        {
            if (!System.Environment.MachineName.Contains("SERVERNEW")) throw new Exception("It is only for local machine");
            DbContext = new PowerAnaliticsDBContext(ConnectionString);
        }
        public void Dispose() => DbContext?.Dispose();
    }
}
