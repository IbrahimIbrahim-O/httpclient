using httpclient.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace httpclient.Service
{
    public class FinHubService : IFinnhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public FinHubService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<Dictionary<string, object>> GetStockPriceQuote(string company)
        {
            using var client = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                //RequestUri = new Uri(_config["sites:finhub"])

                RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={company}" +
                $"&token={_config["FinnhubToken"]}")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            var streamReader = new StreamReader(httpResponseMessage.Content.ReadAsStream());

            var response = streamReader.ReadToEnd();

            var dictionaryObject = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

            if(dictionaryObject == null)
            {
                throw new InvalidOperationException("No response from finnhub server");
            }

            if (dictionaryObject.ContainsKey("Error"))
            {
                throw new InvalidOperationException("Error when retrieving data");
            }

            return dictionaryObject;
        }

      
    }
}