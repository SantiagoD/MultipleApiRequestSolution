using MultipleApiRequest.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MultipleApiRequest.Core
{
    /// <summary>
    /// Class for the second company API configuration and execution (JSON)
    /// </summary>
    class Company2JsonApi : ICompanyApi
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
        /// Constructs a Second company API with its configurations
        /// </summary>
        public Company2JsonApi()
        {
            _name = "Company2";
            _apiType = ApiTypeEnum.JsonApi.ToString();
            _url = "https://secondcompanyjsonapisdb.azurewebsites.net/"; // Util.ReadSetting("Company2URL");
            _requestUri = "api/Consignment"; // Util.ReadSetting("Company2RequestURI");
            _apiCredentials = new ApiCredentials { Type = "Basic", Token = "05xx92x58349x5x78f5xx34xxxxx51xx508xx63817x752xx74004x307" };

        }

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
                int newLength = Convert.ToInt32(Math.Ceiling((decimal)carton.Length / 10));
                int newWidth = Convert.ToInt32(Math.Ceiling((decimal)carton.Width / 10));
                int newHeight = Convert.ToInt32(Math.Ceiling((decimal)carton.Height / 10));

                var ob = new { length = newLength, width = newWidth, height = newHeight };
                cartonList.Add(ob);
            }

            var newObject = new
            {
                consignee = $"{sourceAddress.StreetNumber}, {sourceAddress.FullAddress}, {sourceAddress.Identifier}",
                consignor = $"{destinationAddress.StreetNumber}, {destinationAddress.FullAddress}, {destinationAddress.Identifier}",
                cartons = cartonList
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
            var obj = new { amount = 0.0 };
            var result = JsonConvert.DeserializeAnonymousType(responseResult, obj);

            return result.amount;

        }

        #endregion
    }
}
