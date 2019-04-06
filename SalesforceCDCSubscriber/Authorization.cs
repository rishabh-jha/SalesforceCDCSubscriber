using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SalesforceCDCSubscriber
{
    class Authorization
    {
        public async static Task<AuthResponse> AsyncAuthRequest()
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("client_id", GlobalConstants.CAPP_CONSUMER_KEY),
                new KeyValuePair<string, string>("client_secret", GlobalConstants.CAPP_CONSUMER_SECRET),
                new KeyValuePair<string, string>("username", GlobalConstants.SFDC_USERNAME),
                new KeyValuePair<string, string>("password", GlobalConstants.SFDC_PASSWORD + GlobalConstants.SFDC_SEC_TOKEN)
            });
            HttpClient _httpClient = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(GlobalConstants.SFDC_TOKEN_ENDPOINTURL),
                Content = content
            };
            var responseMessage = await _httpClient.SendAsync(request);
            var response = await responseMessage.Content.ReadAsStringAsync();
            AuthResponse responseDyn = JsonConvert.DeserializeObject<AuthResponse>(response);
            return responseDyn;
        }
    }
}
