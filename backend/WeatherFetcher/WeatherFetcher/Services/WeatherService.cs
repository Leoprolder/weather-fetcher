using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherFetcher.Constants;
using WeatherFetcher.Models.Response;
using WeatherFetcher.ServiceContracts;

namespace WeatherFetcher.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<WeatherResponse> GetCityAndCurrentTemperatureAsync(string zip)
        {
            var coordinatesResponse = await GetCoorditanes(zip);

            using (var client = _httpClientFactory.CreateClient(NamedHttpClients.WeatherApi))
            {
                client.BaseAddress = new Uri(client.BaseAddress.AbsoluteUri
                    + $"&lat={coordinatesResponse.Latitude}&lon={coordinatesResponse.Latitude}");
                var response = await client.GetAsync("");
                var result = (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                var currentTemperature = (double)result["main"]["temp"];

                return new WeatherResponse()
                {
                    City = coordinatesResponse.City,
                    CurrentTemperature = currentTemperature
                };
            }
        }

        private async Task<CoordinatesResponse> GetCoorditanes(string zip)
        {
            using (var client = _httpClientFactory.CreateClient(NamedHttpClients.GeocodingApi))
            {
                client.BaseAddress = new Uri(client.BaseAddress.AbsoluteUri + $"&zip={zip},US");
                var response = await client.GetAsync("");
                var result = JsonConvert.DeserializeObject<GeocodingResponse>(await response.Content.ReadAsStringAsync());

                return new CoordinatesResponse()
                {
                    Latitude = result.Lat,
                    Longitude = result.Lon,
                    City = result.Name
                };
            }
        }
    }
}
