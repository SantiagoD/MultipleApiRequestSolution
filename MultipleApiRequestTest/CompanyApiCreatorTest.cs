using MultipleApiRequest.Creators;
using MultipleApiRequest.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultipleApiRequestTest
{
    [TestClass]
    public class CompanyApiCreatorTest
    {
        private readonly Address source1;
        private readonly Address destination1;
        private readonly Address source2;
        private readonly Address destination2;
        private readonly Address source3;
        private readonly Address destination3;
        private readonly Address source4;
        private readonly Address destination4;
        private readonly Address source5;
        private readonly Address destination5;

        private readonly List<CartonDimension> cartonDimensions1;
        private readonly List<CartonDimension> cartonDimensions2;
        private readonly List<CartonDimension> cartonDimensions3;
        private readonly List<CartonDimension> cartonDimensions4;
        private readonly List<CartonDimension> cartonDimensions5;

        string[] streetNames = { "Montreal Road", "Stum Lake Road", "MacLaren Street", "Summerfield Blvd", "Merivale Road", "Brew Creek Rd", "Brand Road", "Blanshard", "MacLaren Street", "No. 3 Road" };
        int[] streetNumbers = { 1343, 833, 4627, 559, 4804, 1500, 1648, 1878, 1855, 2586 };
        string[] zipCodes = { "K1L 6C7", "V0L 1L0", "K1P 5M7", "T4B 2C2", "K2P 0K1", "V0N 3G0", "S7K 1W8", "V8W 2H9", "K1P 5M7", "V6X 2B8" };
        double[] prices1 = { 15.60, 6.12, 21.56, 36.14, 10.90 };
        double[] prices2 = { 17.80, 5.71, 20.81, 42.50, 12.17 };
        double[] pricesMin = { 15.60, 5.71, 20.81, 36.14, 10.90 };

        CompanyApiCreator factory1 = new CompanyJsonApiCreator();
        CompanyApiCreator factory2 = new CompanyTwoJsonApiCreator();
        

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

        CartonDimension cartonDimension1_1 = new CartonDimension { Length = 152, Width = 102, Height = 102 };
        CartonDimension cartonDimension1_2 = new CartonDimension { Length = 432, Width = 318, Height = 267 };

        CartonDimension cartonDimension2_1 = new CartonDimension { Length = 198, Width = 198, Height = 68 };

        CartonDimension cartonDimension3_1 = new CartonDimension { Length = 432, Width = 318, Height = 267 };
        CartonDimension cartonDimension3_2 = new CartonDimension { Length = 610, Width = 457, Height = 305 };

        CartonDimension cartonDimension4_1 = new CartonDimension { Length = 762, Width = 457, Height = 457 };

        CartonDimension cartonDimension5_1 = new CartonDimension { Length = 1070, Width = 870, Height = 900 };

        public CompanyApiCreatorTest()
        {
            source1 = new Address { Identifier = zipCodes[0], FullAddress = streetNames[0], StreetNumber = streetNumbers[0] };
            destination1 = new Address { Identifier = zipCodes[5], FullAddress = streetNames[5], StreetNumber = streetNumbers[5] };
            source2 = new Address { Identifier = zipCodes[1], FullAddress = streetNames[1], StreetNumber = streetNumbers[1] };
            destination2 = new Address { Identifier = zipCodes[6], FullAddress = streetNames[6], StreetNumber = streetNumbers[6] };
            source3 = new Address { Identifier = zipCodes[2], FullAddress = streetNames[2], StreetNumber = streetNumbers[2] };
            destination3 = new Address { Identifier = zipCodes[7], FullAddress = streetNames[7], StreetNumber = streetNumbers[7] };
            source4 = new Address { Identifier = zipCodes[3], FullAddress = streetNames[3], StreetNumber = streetNumbers[3] };
            destination4 = new Address { Identifier = zipCodes[8], FullAddress = streetNames[8], StreetNumber = streetNumbers[8] };
            source5 = new Address { Identifier = zipCodes[4], FullAddress = streetNames[4], StreetNumber = streetNumbers[4] };
            destination5 = new Address { Identifier = zipCodes[9], FullAddress = streetNames[9], StreetNumber = streetNumbers[9] };

            cartonDimensions1 = new List<CartonDimension>() { cartonDimension1_1, cartonDimension1_2 };
            cartonDimensions2 = new List<CartonDimension>() { cartonDimension2_1 };
            cartonDimensions3 = new List<CartonDimension>() { cartonDimension3_1, cartonDimension3_2, cartonDimension3_2 };
            cartonDimensions4 = new List<CartonDimension>() { cartonDimension4_1, cartonDimension4_1, cartonDimension4_1, cartonDimension4_1 };
            cartonDimensions5 = new List<CartonDimension>() { cartonDimension5_1};
        }


        
        [TestMethod]
        public void SearchPrice_API1_Setup1_Test_OK()
        {
            var api1 = factory1.CreateCompanyApi();
            Task<SearchResult> testTask = api1.SearchPrice(source1, destination1, cartonDimensions1);
            testTask.Wait();
            SearchResult response = testTask.Result;

            Assert.IsNotNull(response, "Null response from SearchPrice");
            Assert.IsTrue(response.Status, "False result status");
            Assert.AreEqual(response.Price, prices1[0], "Wrong price");

        }

        [TestMethod]
        public void SearchPrice_API1_Setup2_Test_OK()
        {
            var api1 = factory1.CreateCompanyApi();
            Task<SearchResult> testTask = api1.SearchPrice(source2, destination2, cartonDimensions2);
            testTask.Wait();
            SearchResult response = testTask.Result;

            Assert.IsNotNull(response, "Null response from SearchPrice");
            Assert.IsTrue(response.Status, "False result status");
            Assert.AreEqual(response.Price, prices1[1], "Wrong price");

        }

        [TestMethod]
        public void SearchPrice_API1_Setup3_Test_OK()
        {
            var api1 = factory1.CreateCompanyApi();
            Task<SearchResult> testTask = api1.SearchPrice(source3, destination3, cartonDimensions3);
            testTask.Wait();
            SearchResult response = testTask.Result;

            Assert.IsNotNull(response, "Null response from SearchPrice");
            Assert.IsTrue(response.Status, "False result status");
            Assert.AreEqual(response.Price, prices1[2], "Wrong price");

        }

        [TestMethod]
        public void SearchPrice_API1_Setup4_Test_OK()
        {
            var api1 = factory1.CreateCompanyApi();
            Task<SearchResult> testTask = api1.SearchPrice(source4, destination4, cartonDimensions4);
            testTask.Wait();
            SearchResult response = testTask.Result;

            Assert.IsNotNull(response, "Null response from SearchPrice");
            Assert.IsTrue(response.Status, "False result status");
            Assert.AreEqual(response.Price, prices1[3], "Wrong price");

        }

        [TestMethod]
        public void SearchPrice_API1_Setup5_Test_OK()
        {
            var api1 = factory1.CreateCompanyApi();
            Task<SearchResult> testTask = api1.SearchPrice(source5, destination5, cartonDimensions5);
            testTask.Wait();
            SearchResult response = testTask.Result;

            Assert.IsNotNull(response, "Null response from SearchPrice");
            Assert.IsTrue(response.Status, "False result status");
            Assert.AreEqual(response.Price, prices1[4], "Wrong price");

        }


        [TestMethod]
        public void SearchPrice_API2_Setup1_Test_OK()
        {
            var api2 = factory2.CreateCompanyApi();
            Task<SearchResult> testTask = api2.SearchPrice(source1, destination1, cartonDimensions1);
            testTask.Wait();
            SearchResult response = testTask.Result;

            Assert.IsNotNull(response, "Null response from SearchPrice");
            Assert.IsTrue(response.Status, "False result status");
            Assert.AreEqual(response.Price, prices2[0], "Wrong price");

        }

        [TestMethod]
        public void SearchPrice_API2_Setup2_Test_OK()
        {
            var api2 = factory2.CreateCompanyApi();
            Task<SearchResult> testTask = api2.SearchPrice(source2, destination2, cartonDimensions2);
            testTask.Wait();
            SearchResult response = testTask.Result;

            Assert.IsNotNull(response, "Null response from SearchPrice");
            Assert.IsTrue(response.Status, "False result status");
            Assert.AreEqual(response.Price, prices2[1], "Wrong price");

        }

        [TestMethod]
        public void SearchPrice_API2_Setup3_Test_OK()
        {
            var api2 = factory2.CreateCompanyApi();
            Task<SearchResult> testTask = api2.SearchPrice(source3, destination3, cartonDimensions3);
            testTask.Wait();
            SearchResult response = testTask.Result;

            Assert.IsNotNull(response, "Null response from SearchPrice");
            Assert.IsTrue(response.Status, "False result status");
            Assert.AreEqual(response.Price, prices2[2], "Wrong price");

        }

        [TestMethod]
        public void SearchPrice_API2_Setup4_Test_OK()
        {
            var api2= factory2.CreateCompanyApi();
            Task<SearchResult> testTask = api2.SearchPrice(source4, destination4, cartonDimensions4);
            testTask.Wait();
            SearchResult response = testTask.Result;

            Assert.IsNotNull(response, "Null response from SearchPrice");
            Assert.IsTrue(response.Status, "False result status");
            Assert.AreEqual(response.Price, prices2[3], "Wrong price");

        }

        [TestMethod]
        public void SearchPrice_API2_Setup5_Test_OK()
        {
            var api2 = factory2.CreateCompanyApi();
            Task<SearchResult> testTask = api2.SearchPrice(source5, destination5, cartonDimensions5);
            testTask.Wait();
            SearchResult response = testTask.Result;

            Assert.IsNotNull(response, "Null response from SearchPrice");
            Assert.IsTrue(response.Status, "False result status");
            Assert.AreEqual(response.Price, prices2[4], "Wrong price");

        }


        [TestMethod]
        public void QuoteMultipleApi_Setup1_Test_OK()
        {
            SearchResult winner = null;
            winner = CompanyApiCreator.QuoteMultipleApi(source1, destination1, cartonDimensions1);
            Assert.IsNotNull(winner, "Null response from SearchPrice");
            Assert.IsTrue(winner.Status, "False result status");
            Assert.IsTrue(winner.Price<= pricesMin[0], "Price too high");
            
            if (winner.CompanyName == "Company1")
            {
                Assert.AreEqual(winner.Price, prices1[0], "Wrong price");
            }
            if (winner.CompanyName == "Company2")
            {
                Assert.AreEqual(winner.Price, prices2[0], "Wrong price");
            }
        }

        [TestMethod]
        public void QuoteMultipleApi_Setup2_Test_OK()
        {
            SearchResult winner = null;
            winner = CompanyApiCreator.QuoteMultipleApi(source2, destination2, cartonDimensions2);
            Assert.IsNotNull(winner, "Null response from SearchPrice");
            Assert.IsTrue(winner.Status, "False result status");
            Assert.IsTrue(winner.Price <= pricesMin[1], "Price too high");

            if (winner.CompanyName == "Company1")
            {
                Assert.AreEqual(winner.Price, prices1[1], "Wrong price");
            }
            if (winner.CompanyName == "Company2")
            {
                Assert.AreEqual(winner.Price, prices2[1], "Wrong price");
            }
        }

        [TestMethod]
        public void QuoteMultipleApi_Test_only3()
        {
            //Only company 3 sends a price
            SearchResult winner = null;
            winner = CompanyApiCreator.QuoteMultipleApi(source1, destination2, cartonDimensions3);
            Assert.IsNotNull(winner, "Null response from SearchPrice");
            Assert.IsTrue(winner.Status, "False result status");
            Assert.AreEqual(winner.CompanyName, "Company3", "Should have been other company as winner");

        }
    }
}
