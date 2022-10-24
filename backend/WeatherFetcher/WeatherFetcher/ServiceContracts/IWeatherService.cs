using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherFetcher.Models.Response;

namespace WeatherFetcher.ServiceContracts
{
    public interface IWeatherService
    {
        public Task<WeatherResponse> GetCityAndCurrentTemperatureAsync(string zip);
        public Task<IEnumerable<WeatherResponse>> GetQueryHistoryAsync();
    }
}
