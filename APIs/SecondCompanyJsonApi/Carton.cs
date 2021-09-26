using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondCompanyJsonApi
{
    public class Carton
    {
        //Length x width x height in inches

        /// <summary>
        /// Carton length in cm
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// Carton Width in cm
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Carton Height in cm
        /// </summary>
        public int Height { get; set; }
    }
}
