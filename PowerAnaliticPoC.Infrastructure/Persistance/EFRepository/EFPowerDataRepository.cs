using PowerAnaliticPoC.Domain.PowerGenerator;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

        public async Task<IEnumerable<PowerGenerator>> GetPowerGeneratorsAsync()
        {
            return await  _context.PowerGenerators.ToListAsync();
        }

        public async Task SavePowerGeneratorAsync(PowerGenerator powerGenerator)
        {
            _context.PowerGenerators.Add(powerGenerator);
           await  _context.SaveChangesAsync();
        }

        public async  Task<IEnumerable<PowerGeneratorTimeRangeData>> GetPowerGeneratorTimeRangeDataAsync(int generatorId, TimeRange timeRange, DateTime from, DateTime to)
        {
            return await _context.PowerGeneratorTimeRangeData.Where(x => x.GeneratorId == generatorId && x.TimeRange == timeRange && x.TimeStamp >= from && x.TimeStamp <= to).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PowerGeneratorDetailData>> GetPowerGeneratorDataAsync(int generatorId, DateTime from, DateTime to)
        {
            return await _context.PowerGeneratorDetailData.Where(x => x.GeneratorId == generatorId && x.TimeStamp >= from && x.TimeStamp <= to).AsNoTracking().ToListAsync();
        }

        public async Task SavePowerGeneratorDataAsync(PowerGeneratorDetailData data)
        {

            ///much faster then EF add
               _context.Database.ExecuteSqlCommand($"INSERT [dbo].[PowerGeneratorDetailDatas]([GeneratorId], [TimeStamp], [CurrentProduction]) values ({data.GeneratorId},'{data.TimeStamp}',{data.CurrentProduction})");           
        }

        public async Task SavePowerGeneratorDataAsync(PowerGeneratorTimeRangeData data)
        {
           _context.PowerGeneratorTimeRangeData.Add(data);
            await  _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PowerGeneratorTimeRangeData>> GetPowerGeneratorTimeRangeDataAsync(TimeRange timeRange, DateTime from, DateTime to)
        {
            return await _context.PowerGeneratorTimeRangeData.Where(x=> x.TimeRange == timeRange && x.TimeStamp >= from && x.TimeStamp <= to).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PowerGeneratorDetailData>> GetPowerGeneratorDataAsync(DateTime from, DateTime to)
        {
            return  await _context.PowerGeneratorDetailData.Where(x =>  x.TimeStamp >= from && x.TimeStamp <= to).AsNoTracking().ToListAsync();
        }

        public async  Task SavePowerGeneratorDataAsync(PowerGeneratorTimeRangeData[] data)
        {
            _context.PowerGeneratorTimeRangeData.AddRange(data);
           await  _context.SaveChangesAsync();
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
        public async Task<IEnumerable<PowerGeneratorDetailData>> GetPowerGeneratorDataBelowExpectedCurrentAsync(DateTime from, DateTime to)
        {
            var powerGenerators =  await _context.PowerGenerators.Select(x => new { x.GeneratorId, x.ExpectedCurrent }).ToListAsync();

            var result = await _context.PowerGeneratorDetailData.Where(x => x.TimeStamp >= from && x.TimeStamp <= to).AsNoTracking().ToListAsync();
             return  result.Where(x =>
            {
                var expectedCurrent = powerGenerators.FirstOrDefault(y => y.GeneratorId == x.GeneratorId).ExpectedCurrent;
                return x.CurrentProduction < expectedCurrent;
            });
        }
    
    }
}
