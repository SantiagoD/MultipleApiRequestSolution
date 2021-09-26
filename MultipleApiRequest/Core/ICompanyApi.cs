using MultipleApiRequest.Model;
using MultipleApiRequest.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultipleApiRequest.Core
{
    public abstract class ICompanyApi
    {
        /// <summary>
        /// Type of format accepted by the API 
        /// </summary>
        public abstract string ApiType { get; }
        /// <summary>
        /// API Name or Company Name
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// Credentials for the API
        /// </summary>
        public abstract ApiCredentials Credentials { get; set; }
        /// <summary>
        /// Base URL for the API
        /// </summary>
        public abstract string URL { get;}
        /// <summary>
        /// Request URI for the quote search action, part after the base URL
        /// </summary>
        public abstract string RequestUri { get; }


        /// <summary>
        /// Search for the shipping price on the current API
        /// </summary>
        /// <param name="sourceAddress">Source Address</param>
        /// <param name="destinationAddress">Destination Address</param>
        /// <param name="cartonDimensions">Packages dimensions list</param>
        /// <returns>Price, status and message if necessary</returns>
        public Task<SearchResult> SearchPrice(Address sourceAddress, Address destinationAddress, List<CartonDimension> cartonDimensions)
        {

            Task<string> responseTask;

            //create header OAuth value
            string headerCredentialsValue = $"{Credentials.Type} {Credentials.Token}";

            if (ApiType==ApiTypeEnum.JsonApi.ToString())
            {
                //Realizes the async request to the API
                responseTask = JsonRequestService.PerformRequestAsync(URL, RequestUri, BuildInput(sourceAddress, destinationAddress, cartonDimensions), Credentials);
            }
            else
            {
                responseTask = XmlRequestService.PerformRequestAsync(URL, RequestUri, BuildInput(sourceAddress, destinationAddress, cartonDimensions), Credentials);
            }

            Task<SearchResult> continuation = responseTask.ContinueWith(t =>
            {
                
                SearchResult result = new SearchResult();
                result.CompanyName = Name;
                //Checks if there´s a json string result
                if (t.Result != null && !t.Result.Contains("error"))
                {
                    result.Status = true;
                    result.Price = ReadResult(t.Result);
                }
                else
                {
                    result.Status = false;
                    result.Message = t.Result;
                }
                    

                return result;
            });

            return continuation;

        }

        public abstract object BuildInput(Address sourceAddress, Address destinationAddress, List<CartonDimension> cartonDimensions);

        public abstract double ReadResult(string responseResult);

    }
}
