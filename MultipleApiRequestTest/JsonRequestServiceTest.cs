using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultipleApiRequest.Model;
using MultipleApiRequest.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleApiRequestTest
{
    [TestClass]
    public class JsonRequestServiceTest
    {
        string[] _urls = { "https://firstcompanyapisdb.azurewebsites.net/", "https://secondcompanyjsonapisdb.azurewebsites.net/" };
        string[] _uris = { "api/Pricing", "api/Consignment" };


        [TestMethod]
        public void PerformRequestAsync_Valid_Input_API1_noCredentials_ReturnOK()
        {
            List<dynamic> cartonList = new List<dynamic>();
            var ob = new { length = 152, width = 102, height = 102 };
            cartonList.Add(ob);
            ob = new { length = 432, width = 318, height = 267 };
            cartonList.Add(ob);
            

            var objectApi1 = new
            {
                contactAddress = new
                {
                    streetName = "Montreal Road",
                    streetNumber = 1343,
                    zipCode = "K1L 6C7"
                },
                warehouseAddress = new
                {
                    streetName = "Brew Creek Rd",
                    streetNumber = 1500,
                    zipCode = "V0N 3G0"
                },
                packageDimensions = cartonList
            };

            Task<string> task = JsonRequestService.PerformRequestAsync(_urls[0], _uris[0], objectApi1, null);
            task.Wait();
            string response = task.Result;

            Assert.IsNotNull(response, "Null response from PerformRequestAsync");
            Assert.IsTrue(response.Contains("15.6"), "Wrong or inexistent price");
            Assert.AreEqual(response, "{\"total\":15.6}", "Incomplete response");


        }

        [TestMethod]
        public void PerformRequestAsync_Valid_Input__API2_Credentials_ReturnOK()
        {
            List<dynamic> cartonList = new List<dynamic>();
            var ob = new { length = 16, width = 11, height = 11 };
            cartonList.Add(ob);
            ob = new { length = 44, width = 32, height = 27 };
            cartonList.Add(ob);
            var cred = new ApiCredentials() { Type = "Basic", Token = "17x752xx74004x307xx508xx63805xx92x58349x5x78f5xx34xxxxx51" };

            var objectApi2 = new
            {
                consignee = "1343, Montreal Road, K1L 6C7",
                consignor = "1500, Brew Creek Rd, V0N 3G0",
                cartons = cartonList
            };
           

            Task<string> task = JsonRequestService.PerformRequestAsync(_urls[1], _uris[1], objectApi2, cred);
            task.Wait();
            string response = task.Result;

            Assert.IsNotNull(response, "Null response from PerformRequestAsync");
            Assert.IsTrue(response.Contains("17.8"), "Wrong or inexistent price");
            Assert.AreEqual(response, "{\"amount\":17.8}", "Incomplete response");


        }

    }
}
