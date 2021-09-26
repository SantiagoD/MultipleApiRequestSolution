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
    /// Support service for making the async Xml calls to the apis 
    /// </summary>
    public static class XmlRequestService
    {
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


                    //Forms the content as string
                    var contentString = new StringContent(param.ToString(), Encoding.UTF8, "application/xml");
                    //Async call passing XML as string
                    HttpResponseMessage response = await client.PostAsync(requestUri, contentString);

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
                return $"<error>{ex.Message}<error/>";
                //throw;
            }
        }
    }
}
