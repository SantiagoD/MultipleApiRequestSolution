using MultipleApiRequest.Core;
using MultipleApiRequest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleApiRequest.Creators
{
    /// <summary>
    /// API class Creator base
    /// </summary>
    public abstract class CompanyApiCreator
    {
        public abstract ICompanyApi CreateCompanyApi();

        public static SearchResult QuoteMultipleApi(Address sourceAddress, Address destinationAddress, List<CartonDimension> cartonDimensions)
        {
            //Factories
            CompanyApiCreator factory1 = new CompanyJsonApiCreator();
            CompanyApiCreator factory2 = new CompanyTwoJsonApiCreator();
            CompanyApiCreator factory3 = new CompanyThreeXmlApiCreator();

            //APIs
            var api1 = factory1.CreateCompanyApi();
            var api2 = factory2.CreateCompanyApi();
            var api3 = factory3.CreateCompanyApi();

            //Starts all tasks
            Task<SearchResult> t1 = api1.SearchPrice(sourceAddress, destinationAddress, cartonDimensions);
            Task<SearchResult> t2 = api2.SearchPrice(sourceAddress, destinationAddress, cartonDimensions);
            Task<SearchResult> t3 = api3.SearchPrice(sourceAddress, destinationAddress, cartonDimensions);
            var tasks = new List<Task<SearchResult>> { t1, t2, t3 };

            SearchResult winner = null;
            double price = 0;
            //Process the results
            var finalTask = Task.Factory.ContinueWhenAll(tasks.ToArray(), searchResults =>
            {
                foreach (var result in searchResults)
                {
                    if (result.Result.Status && (price == 0 || result.Result.Price < price))
                    {
                        winner = result.Result;
                        price = result.Result.Price;
                    }
                }
                
            });
            finalTask.Wait();
            return winner;
        }
    }
}
