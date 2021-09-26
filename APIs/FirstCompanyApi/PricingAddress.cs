using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstCompanyApi
{
    /// <summary>
    /// Address information for shippings
    /// </summary>
    public class PricingAddress
    {
        /// <summary>
        /// Street or Location description as in: ONTARIO ST
        /// </summary>
        public string StreetName { get; set; }
        /// <summary>
        /// Street or location number
        /// </summary>
        public int StreetNumber { get; set; }
        /// <summary>
        /// Alpha numerical ZIP Code
        /// </summary>
        public string ZipCode { get; set; }

    }
}
