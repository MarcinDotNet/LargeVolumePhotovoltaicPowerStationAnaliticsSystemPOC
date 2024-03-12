using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerAnaliticPoCCore.Domain.PowerGenerator;

/// <summary>
/// For now we will use only 5 time ranges, but we can add more if needed
/// </summary>
public enum TimeRange
{
    Minute,
    Hour,
    Day,
    Month,
    Year
}

/// <summary>
/// Here we store all data for power generator by per range of time: ie . per hour, per day, per month
/// DB index should be on GeneratorId, TimeStamp, TimeRange
/// Maybe it should be better to store this data in separate table for each TimeRange, but for now we will use this approach
/// </summary>
public class PowerGeneratorTimeRangeData
{
     public int GeneratorId { get; set; }
     public DateTime TimeStamp { get; set; }
     public TimeRange TimeRange { get; set; }

    /// <summary>
    /// Double is faster then decimal, and we don't need that much precision
    /// </summary>
    public double CurrentProduction { get; set; }
}