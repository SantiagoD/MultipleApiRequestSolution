using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstCompanyApi
{
    /// <summary>
    /// Business class with the inputs necessary to request shipping quote
    /// </summary>
    public class PriceRequest
    {
        /// <summary>
        /// Origin Contact Address for the shipping
        /// </summary>
        public PricingAddress contactAddress { get; set; }
        /// <summary>
        /// Destination warehouse Address for the shipping
        /// </summary>
        public PricingAddress warehouseAddress { get; set; }
        /// <summary>
        /// List of packages dimensions for the shipping
        /// </summary>
        public List<PackageDimensionSet> packageDimensions { get; set; }
    }
}
