using MultipleApiRequest.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace MultipleApiRequest.Core
{
    /// <summary>
    /// Class for the third company API configuration and execution (XML)
    /// </summary>
    class Company3XmlApi : ICompanyApi
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
        /// Constructs a Third company API with its configurations
        /// </summary>
        public Company3XmlApi()
        {
            _name = "Company3";
            _apiType = ApiTypeEnum.XmlApi.ToString();
            _url = "https://thirdcompanyxmlapisdb.azurewebsites.net/"; // Util.ReadSetting("Company3URL");
            _requestUri = "api/Quote"; // Util.ReadSetting("Company3RequestURI");
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

            //LXHXW  format
            foreach (CartonDimension carton in cartonDimensions)
            {
                string formatted = $"{carton.Length}X{carton.Height}X{carton.Width}";
                var ob = new { package = formatted };
                cartonList.Add(ob);
            }

            /* XML Sctructure for API 3
            <QuoteParam>
	            <source>
		            <fullAddress>string</fullAddress>
		            <zipCode>string</zipCode>
	            </source>
	            <destination>
		            <fullAddress>string</fullAddress>
		            <zipCode>string</zipCode>
	            </destination>
	            <packages>
		            <package>string</package>
	            </packages>
            </QuoteParam>
             */
            //Define anonymous type for the input parameters
            var newObject = new
            {
                source = new {
                    fullAddress =$"{sourceAddress.StreetNumber}, {sourceAddress.FullAddress}",
                    zipCode = sourceAddress.Identifier
                },
                destination = new
                {
                    fullAddress = $"{destinationAddress.StreetNumber}, {destinationAddress.FullAddress}",
                    zipCode = destinationAddress.Identifier
                },
                packages = cartonList
            };
            //Converts to JSON (intemediate step)
            var result = JsonConvert.SerializeObject(newObject, Newtonsoft.Json.Formatting.Indented);
            //Converts to XMLNode
            System.Xml.Linq.XNode node = JsonConvert.DeserializeXNode(result, "QuoteParam");
            
            return node;
        }

        /// <summary>
        /// Reads the async request result and parses the price from the current API result format
        /// </summary>
        /// <param name="responseResult">async request result string in XML format</param>
        /// <returns>Shipment price</returns>
        public override double ReadResult(string responseResult)
        {
            //Parses the XML result
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(responseResult);
            XmlNode newNode = doc.DocumentElement;

            double result = double.Parse(newNode.FirstChild.InnerText, CultureInfo.InvariantCulture);
            
            return result;
        }
        
        #endregion
    }
}
