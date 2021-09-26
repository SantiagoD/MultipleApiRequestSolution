using MultipleApiRequest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MultipleApiRequest.Service
{
    /// <summary>
    /// Support service for making the async Json calls to the apis
    /// </summary>
    public static class JsonRequestService
    {
        /// <summary>
        /// Calls the API asynchronously
        /// </summary>
        /// <param name="url">Base API URL</param>
        /// <param name="requestUri">Action request URI</param>
        /// <param name="param">JSON input object to serialize</param>
        /// <returns>Task with the async string request response</returns>
        public static async Task<string> PerformRequestAsync(string url, string requestUri, object param, ApiCredentials credentials)
        {
            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();

                    if (credentials != null)
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(credentials.Type, credentials.Token);


                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PostAsJsonAsync(requestUri, param);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return $"{{error:{ex.Message}}}";
                //throw;
            }

        }
    }
}
