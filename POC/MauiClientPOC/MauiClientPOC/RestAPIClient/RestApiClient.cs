using Newtonsoft.Json;
using RestAPIClient.Exceptions;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RestAPIClient
{
    public class RestApiClient
    {
        private Func<string> getBaseUrl;
        private readonly HttpClient _client;


        public RestApiClient(Func<string> getBaseUrl)
        {
            this.getBaseUrl = getBaseUrl;
            HttpClientHandler insecureHandler = GetInsecureHandler();
            _client = new HttpClient(insecureHandler);
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }

        private string GetApiBaseUrl()
        {
            return Path.Combine(getBaseUrl(), "api");
        }

        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                    return true;
            };
            return handler;
        }

        public async Task<returnType> Get<returnType>(string restPath)
        {
            try
            {
                var msg = new HttpRequestMessage(HttpMethod.Get, Path.Combine(GetApiBaseUrl(), restPath));
                var response = await _client.SendAsync(msg);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<returnType>(content);
                }
                else
                {
                    throw new RestApiRequestException(response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {

                throw new RestApiRequestException(System.Net.HttpStatusCode.ServiceUnavailable, ex.Message);
            }

        }
        public async Task<returnType> Post<returnType>(string restPath, object data)
        {
            try { 
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                string requestPath = Path.Combine(GetApiBaseUrl(), restPath);
                var response = await _client.PostAsync(requestPath, content);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<returnType>(result);
                }
                else
                {
                    throw new RestApiRequestException(response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {

                throw new RestApiRequestException(System.Net.HttpStatusCode.ServiceUnavailable, ex.Message);
            }

        }
    }
}
