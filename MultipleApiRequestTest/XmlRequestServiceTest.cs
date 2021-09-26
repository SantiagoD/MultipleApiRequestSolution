using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultipleApiRequest.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleApiRequestTest
{
    [TestClass]
    public class XmlRequestServiceTest
    {
        string[] _urls = { "https://thirdcompanyxmlapisdb.azurewebsites.net/", "https://run.mocky.io/v3/738dc262-078b-43df-839b-c38e17b5904e", "https://thirdcompanyxmlapisdb_invalid.azurewebsites.net/" };
        string[] _uris = { "api/Quote", "", "api/Quote" };

        [TestMethod]
        public void PerformRequestAsync_Valid_Input_API3_noCredentials_ReturnOK()
        {
            List<dynamic> cartonList = new List<dynamic>();
            string formatted = $"152X102X102";
            var ob = new { package = formatted };
            cartonList.Add(ob);
            formatted = $"432X267X318";
            ob = new { package = formatted };
            cartonList.Add(ob);

            var objectApi3 = new
            {
                source = new
                {
                    fullAddress = "1343, Montreal Road",
                    zipCode = "K1L 6C7"
                },
                destination = new
                {
                    fullAddress = "1500, Brew Creek Rd",
                    zipCode = "V0N 3G0"
                },
                packages = cartonList
            };
            //Converts to JSON (intemediate step)
            var result = JsonConvert.SerializeObject(objectApi3, Newtonsoft.Json.Formatting.Indented);
            //Converts to XMLNode
            System.Xml.Linq.XNode node = JsonConvert.DeserializeXNode(result, "QuoteParam");

            Task<string> task = XmlRequestService.PerformRequestAsync(_urls[0], _uris[0], node, null);
            task.Wait();
            string response = task.Result;

            Assert.IsNotNull(response, "Null response from PerformRequestAsync");
            Assert.IsTrue(response.Contains("<Quote>"), "Wrong or inexistent tag");

        }

        [TestMethod]
        public void PerformRequestAsync_Valid_Input_MockAPI_noCredentials_ReturnOK()
        {
            List<dynamic> cartonList = new List<dynamic>();
            string formatted = $"152X102X102";
            var ob = new { package = formatted };
            cartonList.Add(ob);
            formatted = $"432X267X318";
            ob = new { package = formatted };
            cartonList.Add(ob);

            var objectApi3 = new
            {
                source = new
                {
                    fullAddress = "1343, Montreal Road",
                    zipCode = "K1L 6C7"
                },
                destination = new
                {
                    fullAddress = "1500, Brew Creek Rd",
                    zipCode = "V0N 3G0"
                },
                packages = cartonList
            };
            //Converts to JSON (intemediate step)
            var result = JsonConvert.SerializeObject(objectApi3, Newtonsoft.Json.Formatting.Indented);
            //Converts to XMLNode
            System.Xml.Linq.XNode node = JsonConvert.DeserializeXNode(result, "QuoteParam");

            Task<string> task = XmlRequestService.PerformRequestAsync(_urls[1], _uris[1], node, null);
            task.Wait();
            string response = task.Result;

            Assert.IsNotNull(response, "Null response from PerformRequestAsync");
            Assert.IsTrue(response.Contains("<Quote>"), "Wrong or inexistent tag");

        }

        [TestMethod]
        public void PerformRequestAsync_Invalid_MockAPI_noCredentials_NotOK()
        {
            List<dynamic> cartonList = new List<dynamic>();
            string formatted = $"152X102X102";
            var ob = new { package = formatted };
            cartonList.Add(ob);
            formatted = $"432X267X318";
            ob = new { package = formatted };
            cartonList.Add(ob);

            var objectApi3 = new
            {
                source = new
                {
                    fullAddress = "1343, Montreal Road",
                    zipCode = "K1L 6C7"
                },
                destination = new
                {
                    fullAddress = "1500, Brew Creek Rd",
                    zipCode = "V0N 3G0"
                },
                packages = cartonList
            };
            //Converts to JSON (intemediate step)
            var result = JsonConvert.SerializeObject(objectApi3, Newtonsoft.Json.Formatting.Indented);
            //Converts to XMLNode
            System.Xml.Linq.XNode node = JsonConvert.DeserializeXNode(result, "QuoteParam");

            Task<string> task = XmlRequestService.PerformRequestAsync(_urls[2], _uris[2], node, null);
            task.Wait();
            string response = task.Result;

            Assert.IsNotNull(response, "Null response from PerformRequestAsync");
            Assert.IsTrue(response.Contains("<error>"), "Wrong or inexistent tag");

        }

    }
}
