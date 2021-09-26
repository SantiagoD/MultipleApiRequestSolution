using MultipleApiRequest.Creators;
using MultipleApiRequest.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultipleApiRequest
{
    class Program
    {
        static void Main(string[] args)
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
            

            //INPUTS - first test
            Address source = new Address { Identifier = zipCodes[0], FullAddress = streetNames[0], StreetNumber = streetNumbers[0] };
            Address destination = new Address { Identifier = zipCodes[5], FullAddress = streetNames[5], StreetNumber = streetNumbers[5] };
            var cartonDimension1 = new CartonDimension { Length = 152, Width = 102, Height = 102 };
            var cartonDimension2 = new CartonDimension { Length = 432, Width = 318, Height = 267 };
            List<CartonDimension> cartonDimensions = new List<CartonDimension>() { cartonDimension1, cartonDimension2 };

            //Factories
            CompanyApiCreator factory1 = new CompanyJsonApiCreator();
            CompanyApiCreator factory2 = new CompanyTwoJsonApiCreator();
            CompanyApiCreator factory3 = new CompanyThreeXmlApiCreator();


            //API 1 Standalone
            var api1 = factory1.CreateCompanyApi();
            Console.WriteLine($"API 1 - Standalone run - test 1");
            Task<SearchResult> result1 = api1.SearchPrice(source, destination, cartonDimensions);
            result1.Wait();
            Console.WriteLine($"Result: {result1.Result.Price} CAD for Company: {result1.Result.CompanyName} ");

            //API 2 Standalone
            Console.WriteLine($"API 2 - Standalone run - test 1");
            var api2 = factory2.CreateCompanyApi();
            Task<SearchResult> result2 = api2.SearchPrice(source, destination, cartonDimensions);
            result2.Wait();
            Console.WriteLine($"Result: {result2.Result.Price} CAD for Company: {result2.Result.CompanyName} ");
            
            //API 3 Standalone
            Console.WriteLine($"API 3 - Standalone run - test 1");
            var api3 = factory3.CreateCompanyApi();
            Task<SearchResult> result3 = api3.SearchPrice(source, destination, cartonDimensions);
            result3.Wait();
            Console.WriteLine($"Result: {result3.Result.Price} CAD for Company: {result3.Result.CompanyName} ");

            //Simultaneous Run
            //Console.WriteLine($"API 1,2 & 3 - Async run - test 1");
            //Task<SearchResult> t1 = api1.SearchPrice(source, destination, cartonDimensions);
            //Task<SearchResult> t2 = api2.SearchPrice(source, destination, cartonDimensions);
            //Task<SearchResult> t3 = api3.SearchPrice(source, destination, cartonDimensions);
            //var tasks = new List<Task<SearchResult>> { t1, t2, t3 };

            //SearchResult winner = null;
            //double price = 0;

            //var finalTask = Task.Factory.ContinueWhenAll(tasks.ToArray(), searchResults =>
            //{
            //   foreach (var result in searchResults)
            //    {
            //        if(price==0 || result.Result.Price < price)
            //        {
            //            winner = result.Result;
            //            price = result.Result.Price;
            //        }
            //    }
            //    Console.WriteLine($"Result: {winner.Price} CAD for Company: {winner.CompanyName} ");
            //});


            //Executing on CompanyApiCreator class
            SearchResult winner = null;
            Console.WriteLine($"CompanyApiCreator - Async run - test 1");
            winner = CompanyApiCreator.QuoteMultipleApi(source, destination, cartonDimensions);
            Console.WriteLine($"CompanyApiCreator Result: {winner.Price} CAD for Company: {winner.CompanyName} ");

            string car = Console.ReadLine();
        }
    }
}
