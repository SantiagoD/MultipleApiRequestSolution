using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleApiRequest.Model
{
    public class Address
    {
        
        /// <summary>
        /// Street or location number
        /// </summary>
        public int StreetNumber { get; set; }

        /// <summary>
        /// Street or location name
        /// </summary>
        public string FullAddress { get; set; }

        /// <summary>
        /// Zipcode
        /// </summary>
        public string Identifier { get; set; }
    }
}
