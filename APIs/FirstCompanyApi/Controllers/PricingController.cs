using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FirstCompanyApi.Controllers
{
    /// <summary>
    /// Controller for requesting shipping prices
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PricingController : ControllerBase
    {

        /// <summary>
        /// Returns the total price of a shippment considering its origin, destination and packages dimensions.
        /// </summary>
        /// <param name="value">Input parameters including Addresses and packages dimensions</param>
        /// <returns>Price result</returns>
        // POST api/<PricingController>
        [HttpPost]
        public TotalResult Post([FromBody] PriceRequest value)
        {
            /*
            Street	1343 Montreal Road	833 Stum Lake Road	4627 MacLaren Street	559 Summerfield Blvd	4804 Merivale Road	1500 Brew Creek Rd	1648 Brand Road	1878 Blanshard	1855 MacLaren Street	2586 No. 3 Road
            City	Ottawa	            Horsefly	        Ottawa	                Airdrie	                Ottawa	            Squamish	        Humboldt	    Victoria	    Ottawa	                Richmond
            State	ON	                BC	                ON	                    AB	                    ON	                BC	                SK	            BC	            ON	                    BC
            ZipCode K1L 6C7	            V0L 1L0	            K1P 5M7	                T4B 2C2	                K2P 0K1	            V0N 3G0	            S7K 1W8	        V8W 2H9	        K1P 5M7	                V6X 2B8

            
            Length x width x height (mm)
            152	102	102     -   432	318	267
            198	198	68       
            432	318	267     -   610	457	305     610	457	305
            762	457	457     -   762	457	457     762	457	457     762	457	457
            1070 870 900     

             */
            string[] streetNames = { "Montreal Road", "Stum Lake Road", "MacLaren Street", "Summerfield Blvd", "Merivale Road", "Brew Creek Rd", "Brand Road", "Blanshard", "MacLaren Street", "No. 3 Road" };
            int[] streetNumbers = { 1343, 833, 4627, 559, 4804, 1500, 1648, 1878, 1855, 2586 };
            string[] zipCodes = { "K1L 6C7", "V0L 1L0", "K1P 5M7", "T4B 2C2", "K2P 0K1", "V0N 3G0", "S7K 1W8", "V8W 2H9", "K1P 5M7", "V6X 2B8" };
            double[] prices = { 15.60, 6.12, 21.56, 36.14, 10.90 };

            if (value.packageDimensions.Count <= 0)
                return new TotalResult { Total = 0 };


            //First Case
            if (CompareAddress(value.contactAddress, streetNumbers[0], streetNames[0], zipCodes[0]) && CompareAddress(value.warehouseAddress, streetNumbers[5], streetNames[5], zipCodes[5]))
            {
                //Length x width x height(mm)
                //152 102 102 - 432 318 267
                if (value.packageDimensions.Count == 2 && ComparePackage(value.packageDimensions[0], 152, 102, 102) && ComparePackage(value.packageDimensions[1], 432, 318, 267))
                {
                    return new TotalResult { Total = prices[0] };
                }
            }

            //Second Case
            if (CompareAddress(value.contactAddress, streetNumbers[1], streetNames[1], zipCodes[1]) && CompareAddress(value.warehouseAddress, streetNumbers[6], streetNames[6], zipCodes[6]))
            {
                //Length x width x height(mm)
                //198	198	68
                if (value.packageDimensions.Count == 1 && ComparePackage(value.packageDimensions[0], 198, 198, 68))
                {
                    return new TotalResult { Total = prices[1] };
                }
                
            }

            //Third Case
            if (CompareAddress(value.contactAddress, streetNumbers[2], streetNames[2], zipCodes[2]) && CompareAddress(value.warehouseAddress, streetNumbers[7], streetNames[7], zipCodes[7]))
            {
                //Length x width x height(mm)
                //432,318,267 - 610,457,305     610,457,305
                if (value.packageDimensions.Count == 3 && ComparePackage(value.packageDimensions[0], 432, 318, 267) && ComparePackage(value.packageDimensions[1], 610, 457, 305) && ComparePackage(value.packageDimensions[2], 610, 457, 305))
                {
                    return new TotalResult { Total = prices[2] };
                }
            }

            //Fourth Case
            if (CompareAddress(value.contactAddress, streetNumbers[3], streetNames[3], zipCodes[3]) && CompareAddress(value.warehouseAddress, streetNumbers[8], streetNames[8], zipCodes[8]))
            {
                //Length x width x height(mm)
                //762,457,457     -   762	457	457     762	457	457     762	457	457
                if (value.packageDimensions.Count == 4 && ComparePackage(value.packageDimensions[0], 762, 457, 457) && ComparePackage(value.packageDimensions[1], 762, 457, 457) && ComparePackage(value.packageDimensions[2], 762, 457, 457) && ComparePackage(value.packageDimensions[3], 762, 457, 457))
                {
                    return new TotalResult { Total = prices[3] };
                }
            }


            //Fifth Case
            if (CompareAddress(value.contactAddress, streetNumbers[4], streetNames[4], zipCodes[4]) && CompareAddress(value.warehouseAddress, streetNumbers[9], streetNames[9], zipCodes[9]))
            {
                //Length x width x height(mm)
                //1070,870,900
                if (value.packageDimensions.Count == 1 && ComparePackage(value.packageDimensions[0], 1070, 870, 900))
                {
                    return new TotalResult { Total = prices[4] };
                }

            }

            return new TotalResult { Total = 0 };
        }

        private bool ComparePackage(PackageDimensionSet value, int length, int width, int height)
        {
            bool result = value.Length == length && value.Width == width && value.Height == height;

            return result;
        }

        private bool CompareAddress(PricingAddress value, int streetNumber, string streetName, string ZipCode)
        {
            bool result = value.StreetName == streetName && value.StreetNumber == streetNumber && value.ZipCode == ZipCode;

            return result;
        }
    }
}
