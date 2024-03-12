using System.ComponentModel.DataAnnotations;

namespace PowerAnaliticPoCCore.Domain.PowerGenerator;

/// <summary>
/// Base data for power generator.
/// This is base model, so information about who/when created/modified is not included.
/// connection to power data was not included, because we do not want DB to check Foreign key on every insert.
/// And it could be pleaced in different storage.
/// </summary>
public class PowerGenerator
{
    [Key] public int GeneratorId { get; set; }
    [MaxLength(40)] public required string Name { get; set; }

    /// <summary>
    /// Location of the generator. Simply string for now, but it should be separate table /or json
    /// </summary>
    [MaxLength(40)]
    public required string Location { get; set; }

    /// <summary>
    /// This is expected current production for this generator.
    /// This should be in separate table for easy growth of similar properties
    /// </summary>
    public double ExpectedCurrent { get; set; }
}