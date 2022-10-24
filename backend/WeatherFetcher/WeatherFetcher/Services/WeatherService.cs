using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherFetcher.Constants;
using WeatherFetcher.DbContexts;
using WeatherFetcher.Models.Entity;
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

                using (var context = new SqlContext())
                {
                    context.Add(new WeatherRequestByZip()
                    {
                        City = coordinatesResponse.City,
                        Temperature = currentTemperature,
                        Time = DateTime.UtcNow,
                        Zip = zip
                    });

                    await context.SaveChangesAsync();
                }

                return new WeatherResponse()
                {
                    City = coordinatesResponse.City,
                    Temperature = currentTemperature,
                    Time = DateTime.UtcNow,
                    Zip = zip
                };
            }
        }

        public async Task<IEnumerable<WeatherResponse>> GetQueryHistoryAsync()
        {
            using (var context = new SqlContext())
            {
                return await context.WeatherRequestByZip.Select(r => new WeatherResponse()
                {
                    City = r.City,
                    Temperature = r.Temperature,
                    Time = r.Time,
                    Zip = r.Zip
                }).ToListAsync();
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
