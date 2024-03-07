using PowerAnaliticPoC.Domain.PowerGenerator;
using System;
using System.Collections.Generic;

namespace PowerAnaliticPoC.Infrastructure.Persistance
{
    /// <summary>
    /// We will hide data access from the rest of the application, to change storage type we will need to change only implementation of this interface.
    /// In this interfave we should provide all analitics methods that we need for our application.
    /// To consider to split it to more interfaces, to separate addding with the selecting and analitics.
    /// </summary>
    public interface IPowerDataRepository
    {
        /// <summary>
        /// Get all power generators
        /// </summary>
        /// <returns></returns>
        IEnumerable<PowerGenerator> GetPowerGenerators();

        /// <summary>
        /// Save power generator
        /// </summary>
        /// <returns></returns>
        void SavePowerGenerator(PowerGenerator powerGenerator);


        /// <summary>
        /// Get all power generator data for given time range
        /// </summary>
        /// <param name="generatorId"></param>
        /// <param name="timeRange"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        IEnumerable<PowerGeneratorTimeRangeData> GetPowerGeneratorTimeRangeData(int generatorId, TimeRange timeRange, DateTime from, DateTime to);

        // <summary>
        /// Get all power generator data for given time range
        /// </summary>
        /// <param name="generatorId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        IEnumerable<PowerGeneratorDetailData> GetPowerGeneratorData(int generatorId, DateTime from, DateTime to);

        ///<summary>
        /// Save power generator data
        /// </summary>
        /// <param name="data"></param>
        void SavePowerGeneratorData(PowerGeneratorDetailData data);

        ///<summary>
        /// Save power generator time range data
        /// </summary>
        /// <param name="data"></param>
        void SavePowerGeneratorData(PowerGeneratorTimeRangeData data);


        #region processing jobs

        /// <summary>
        /// Get all power generator data for given time range for all generators
        /// </summary>
        /// <param name="generatorId"></param>
        /// <param name="timeRange"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        IEnumerable<PowerGeneratorTimeRangeData> GetPowerGeneratorTimeRangeData(TimeRange timeRange, DateTime from, DateTime to);


        // <summary>
        /// Get all power generator data for given time range
        /// </summary>
        /// <param name="generatorId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        IEnumerable<PowerGeneratorDetailData> GetPowerGeneratorData(DateTime from, DateTime to);

        ///<summary>
        /// Save power generator time range data
        /// </summary>
        /// <param name="data"></param>
        void SavePowerGeneratorData(PowerGeneratorTimeRangeData[] data);

        #endregion


        #region analitics

        ///<summary>
        /// Gets all power generators that do not produce expected current
        /// </summary>
        /// <param name="data"></param>
        IEnumerable<PowerGeneratorDetailData> GetPowerGeneratorDataBelowExpectedCurrent(DateTime from, DateTime to);


        #endregion


    }
}
