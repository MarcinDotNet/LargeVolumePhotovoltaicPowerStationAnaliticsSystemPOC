

using System.ComponentModel.DataAnnotations;

namespace PowerAnaliticPoC.Domain.PowerGenerator
{
    /// <summary>
    /// Base data for power generator 
    /// </summary>
    public class PowerGenerator
    {
       [Key]
       public int GeneratorId { get; set; }
       public string Name { get; set; }
        /// <summary>
        /// Location of the generator. Simply string for now, but it should be separate table /or json
        /// </summary>
       public string Location { get; set; }
        /// <summary>
        /// This is expected current production for this generator.
        /// This should be in separate table for easy growth of similar properties
        /// </summary>
      public double ExpectedCurrent { get; set; }
    }
}
