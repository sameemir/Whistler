using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Whistler.Model;

namespace Whistler.Helpers
{
    public class WebServiceHandler:IWebServiceHandler
    {
        public WebServiceHandler()
        {

        }

        public async Task<string> GetResponseThroughGet(string url, bool requiresAuthorization)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoStore = true, NoCache = true };
            
            if(requiresAuthorization)
                client.DefaultRequestHeaders.Add("Authorization", AppData.AuthorizationKey);
            string result;
            try
            {
                result = await client.GetStringAsync(url);
            }
            catch(Exception exp)
            {
                result = "";
            }
            return result;
        }

        public async Task<string> GetResponseThroughPost(string url, bool requiresAuthorization)
        {
           
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoStore = true, NoCache = true };
            HttpResponseMessage responseMessage;

            if (requiresAuthorization)
            {
                var values = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("Authorization", AppData.AuthorizationKey)
                    };
                responseMessage = await client.PostAsync(url, new FormUrlEncodedContent(values));
                responseMessage.EnsureSuccessStatusCode();
            }
            else
            {
               responseMessage = await client.PostAsync(url, null);
            }

            var responseString = await responseMessage.Content.ReadAsStringAsync();
            return responseString;
        }
    }
}
