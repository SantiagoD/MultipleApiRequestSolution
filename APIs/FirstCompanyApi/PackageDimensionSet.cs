using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstCompanyApi
{
    /// <summary>
    /// Package dimensions in millimeters, detailed in L X W X H
    /// </summary>
    public class PackageDimensionSet
    {
        //Length x width x height in mm

        /// <summary>
        /// Length of the package in mm
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// Width of the package in mm
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Height of the package in mm
        /// </summary>
        public int Height { get; set; }
    }
}
