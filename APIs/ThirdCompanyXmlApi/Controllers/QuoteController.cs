using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThirdCompanyXmlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        [Produces("application/xml")]
        [Consumes("application/xml")]
        [HttpPost]
        public QuoteResult Post([FromBody] QuoteParam value)
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
            double[] prices = { 19.10, 7.05, 18.26, 39.89, 10.70 };

            //Random tests
            //return new QuoteResult { Quote = 6 + Math.Round(20 *new Random().NextDouble(), 2) };

            if (value.Packages.Count <= 0)
                return new QuoteResult { Quote = 0 };


            //First Case
            if (CompareAddress(value.Source, streetNumbers[0], streetNames[0], zipCodes[0]) && CompareAddress(value.Destination, streetNumbers[5], streetNames[5], zipCodes[5]))
            {
                //Length x width x height(mm)
                //152 102 102 - 432 318 267
                if (value.Packages.Count == 2 && ComparePackage(value.Packages[0], 152, 102, 102) && ComparePackage(value.Packages[1], 432, 318, 267))
                {
                    return new QuoteResult { Quote = prices[0] };
                }
            }

            //Second Case
            if (CompareAddress(value.Source, streetNumbers[1], streetNames[1], zipCodes[1]) && CompareAddress(value.Destination, streetNumbers[6], streetNames[6], zipCodes[6]))
            {
                //Length x width x height(mm)
                //198	198	68
                if (value.Packages.Count == 1 && ComparePackage(value.Packages[0], 198, 198, 68))
                {
                    return new QuoteResult { Quote = prices[1] };
                }

            }

            //Third Case
            if (CompareAddress(value.Source, streetNumbers[2], streetNames[2], zipCodes[2]) && CompareAddress(value.Destination, streetNumbers[7], streetNames[7], zipCodes[7]))
            {
                //Length x width x height(mm)
                //432,318,267 - 610,457,305     610,457,305
                if (value.Packages.Count == 3 && ComparePackage(value.Packages[0], 432, 318, 267) && ComparePackage(value.Packages[1], 610, 457, 305) && ComparePackage(value.Packages[2], 610, 457, 305))
                {
                    return new QuoteResult { Quote = prices[2] };
                }
            }

            //Fourth Case
            if (CompareAddress(value.Source, streetNumbers[3], streetNames[3], zipCodes[3]) && CompareAddress(value.Destination, streetNumbers[8], streetNames[8], zipCodes[8]))
            {
                //Length x width x height(mm)
                //762,457,457     -   762	457	457     762	457	457     762	457	457
                if (value.Packages.Count == 4 && ComparePackage(value.Packages[0], 762, 457, 457) && ComparePackage(value.Packages[1], 762, 457, 457) && ComparePackage(value.Packages[2], 762, 457, 457) && ComparePackage(value.Packages[3], 762, 457, 457))
                {
                    return new QuoteResult { Quote = prices[3] };
                }
            }


            //Fifth Case
            if (CompareAddress(value.Source, streetNumbers[4], streetNames[4], zipCodes[4]) && CompareAddress(value.Destination, streetNumbers[9], streetNames[9], zipCodes[9]))
            {
                //Length x width x height(mm)
                //1070,870,900
                if (value.Packages.Count == 1 && ComparePackage(value.Packages[0], 1070, 870, 900))
                {
                    return new QuoteResult { Quote = prices[4] };
                }

            }

            return new QuoteResult { Quote = 0 };
        }

        private bool ComparePackage(PackageDetail value, int length, int width, int height)
        {
            // in mm  LXHXW
            string formatted = $"{length}X{height}X{width}";

            bool result = value.Package  == formatted;

            return result;
        }

        private bool CompareAddress(Address value, int streetNumber, string streetName, string zipCode)
        {
            //number, street

            bool result = value.FullAddress == $"{streetNumber}, {streetName}" && value.ZipCode == zipCode;

            return result;
        }
    }
}
