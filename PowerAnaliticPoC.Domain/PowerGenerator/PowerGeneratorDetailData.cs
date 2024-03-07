using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerAnaliticPoC.Domain.PowerGenerator
{

    /// <summary>
    /// All incomming data for power generator will be stored here
    /// </summary>
    public class PowerGeneratorDetailData
    {
        [Key, Column(Order = 0)]
        public int GeneratorId { get; set; }
        [Key, Column(Order = 1)]
        public DateTime TimeStamp { get; set; }

            /// <summary>
            /// Double is faster then decimal, and we don't need that much precision
            /// </summary>
          public double CurrentProduction { get; set; }
    }
}
