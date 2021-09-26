using MultipleApiRequest.Model;
using MultipleApiRequest.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleApiRequest.Core
{
    /// <summary>
    /// Class for the first company API configuration and execution (JSON)
    /// </summary>
    class Company1JsonApi : ICompanyApi
    {
        #region Properties

        private readonly string _apiType;
        private string _name;
        private string _url;
        private string _requestUri;
        private ApiCredentials _apiCredentials;

        public override string ApiType => _apiType;

        public override string Name { get => _name; }
        public override ApiCredentials Credentials { get => _apiCredentials; set => _apiCredentials = value; }
        public override string URL { get => _url; }

        public override string RequestUri { get => _requestUri; }

        #endregion

        /// <summary>
        /// Constructs a First company API with its configurations
        /// </summary>
        public Company1JsonApi()
        {
            _name = "Company1";
            _apiType = ApiTypeEnum.JsonApi.ToString();
            _url = "https://firstcompanyapisdb.azurewebsites.net/"; //Util.ReadSetting("Company1URL");
            _requestUri = "api/Pricing"; // Util.ReadSetting("Company1RequestURI");
            _apiCredentials = new ApiCredentials { Type = "Basic", Token = "17x752xx74004x307xx508xx63805xx92x58349x5x78f5xx34xxxxx51" };

        }

        //Previous version before moving to abstract class
        //public Task<SearchResult> SearchPriceFormer(Address sourceAddress, Address destinationAddress, List<CartonDimension> cartonDimensions)
        //{
            
        //    //Realizes the async request to the API
        //    Task<string> responseTask = JsonRequestService.PerformRequestAsync(URL, RequestUri, BuildInput(sourceAddress, destinationAddress, cartonDimensions), Credentials);

        //    Task<SearchResult> continuation = responseTask.ContinueWith(t =>
        //    {
        //        SearchResult result = new SearchResult();
        //        result.CompanyName = Name;
        //        //Checks if there´s a json string result
        //        if (t.Result != null)
        //        {
        //            result.Status = true;
        //            result.Price = ReadResult(t.Result);
        //        }
        //        else
        //            result.Status = false;

        //        return result;
        //    });

        //    return continuation;

        //}

        #region Parsing methods
        /// <summary>
        /// Build the correct input format for this API from the common parameters
        /// </summary>
        /// <param name="sourceAddress">Source Address</param>
        /// <param name="destinationAddress">Destination Address</param>
        /// <param name="cartonDimensions">Packages dimensions list</param>
        /// <param name="credentialsValue">Value for the authorization heder</param>
        /// <returns>Dynamic object for JSON deserialization</returns>
        public override object BuildInput(Address sourceAddress, Address destinationAddress, List<CartonDimension> cartonDimensions)
        {
            //Defines a dimensions list with the current format
            List<dynamic> cartonList = new List<dynamic>();
            foreach (CartonDimension carton in cartonDimensions)
            {
                var ob = new { length = carton.Length, width = carton.Width, height = carton.Height };
                cartonList.Add(ob);
            }

            var newObject = new
            {
                contactAddress = new
                {
                    streetName = sourceAddress.FullAddress,
                    streetNumber = sourceAddress.StreetNumber,
                    zipCode = sourceAddress.Identifier
                },
                warehouseAddress = new
                {
                    streetName = destinationAddress.FullAddress,
                    streetNumber = destinationAddress.StreetNumber,
                    zipCode = destinationAddress.Identifier
                },
                packageDimensions = cartonList
            };

            return newObject;
        }

        /// <summary>
        /// Reads the async request result and parses the price from the current API result format
        /// </summary>
        /// <param name="responseResult">async request result string</param>
        /// <returns>Shipment price</returns>
        public override double ReadResult(string responseResult)
        {
            //Object representing the API 1 result structure
            var obj = new { total = 0.0 };
            var result = JsonConvert.DeserializeAnonymousType(responseResult, obj);

            return result.total;

        }

        #endregion
    }
}
