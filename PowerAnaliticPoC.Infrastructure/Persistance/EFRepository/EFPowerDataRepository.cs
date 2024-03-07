using PowerAnaliticPoC.Domain.PowerGenerator;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerAnaliticPoC.Infrastructure.Persistance.EFRepository
{
    public class EFPowerDataRepository : IPowerDataRepository
    {
        private PowerAnaliticsDBContext _context;

        public EFPowerDataRepository(PowerAnaliticsDBContext context)
        {
            _context = context;
        }

        public IEnumerable<PowerGenerator> GetPowerGenerators()
        {
            return _context.PowerGenerators;
        }

        public void SavePowerGenerator(PowerGenerator powerGenerator)
        {
            _context.PowerGenerators.Add(powerGenerator);
            _context.SaveChanges();
        }

        public IEnumerable<PowerGeneratorTimeRangeData> GetPowerGeneratorTimeRangeData(int generatorId, TimeRange timeRange, DateTime from, DateTime to)
        {
            return _context.PowerGeneratorTimeRangeData.Where(x => x.GeneratorId == generatorId && x.TimeRange == timeRange && x.TimeStamp >= from && x.TimeStamp <= to).AsNoTracking();
        }

        public IEnumerable<PowerGeneratorDetailData> GetPowerGeneratorData(int generatorId, DateTime from, DateTime to)
        {
            return _context.PowerGeneratorDetailData.Where(x => x.GeneratorId == generatorId && x.TimeStamp >= from && x.TimeStamp <= to).AsNoTracking();
        }

        public void SavePowerGeneratorData(PowerGeneratorDetailData data)
        {
            _context.PowerGeneratorDetailData.Add(data);
            _context.SaveChanges();
        }

        public void SavePowerGeneratorData(PowerGeneratorTimeRangeData data)
        {
            _context.PowerGeneratorTimeRangeData.Add(data);
            _context.SaveChanges();
        }

        public IEnumerable<PowerGeneratorTimeRangeData> GetPowerGeneratorTimeRangeData(TimeRange timeRange, DateTime from, DateTime to)
        {
            return _context.PowerGeneratorTimeRangeData.Where(x=> x.TimeRange == timeRange && x.TimeStamp >= from && x.TimeStamp <= to).AsNoTracking();
        }

        public IEnumerable<PowerGeneratorDetailData> GetPowerGeneratorData(DateTime from, DateTime to)
        {
            return _context.PowerGeneratorDetailData.Where(x =>  x.TimeStamp >= from && x.TimeStamp <= to).AsNoTracking();
        }

        public void SavePowerGeneratorData(PowerGeneratorTimeRangeData[] data)
        {
            _context.PowerGeneratorTimeRangeData.AddRange(data);
            _context.SaveChanges();
        }


        /// <summary>
        /// this is heavy logic operation, to cosider to create separate service/ kubernates
        /// There is a lot of options to test:
        ///  - we can cache power generators and their expected current
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<PowerGeneratorDetailData> GetPowerGeneratorDataBelowExpectedCurrent(DateTime from, DateTime to)
        {
            var powerGenerators = _context.PowerGenerators.Select(x => new { x.GeneratorId, x.ExpectedCurrent }).ToList();

            return _context.PowerGeneratorDetailData.Where(x=>x.TimeStamp >= from && x.TimeStamp <= to).AsNoTracking().ToList().Where(x =>
            {
                var expectedCurrent = powerGenerators.FirstOrDefault(y => y.GeneratorId == x.GeneratorId).ExpectedCurrent;
                return x.CurrentProduction < expectedCurrent;
            });
        }
    }
}
