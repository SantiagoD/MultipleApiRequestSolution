using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondCompanyJsonApi
{
    public class RequestParam
    {
        /// <summary>
        /// Address for the consignee with the format: street number, street or location, zipcode
        /// </summary>
        public string Consignee { get; set; }

        /// <summary>
        /// Address for the consignee with the format: street number, street or location, zipcode
        /// </summary>
        public string Consignor { get; set; }

        /// <summary>
        /// Cartons dimensions list 
        /// </summary>
        public List<Carton> Cartons { get; set; }
    }
}
