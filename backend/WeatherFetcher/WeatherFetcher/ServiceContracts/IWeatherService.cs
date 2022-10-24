using System.Threading.Tasks;
using WeatherFetcher.Models.Response;

namespace WeatherFetcher.ServiceContracts
{
    public interface IWeatherService
    {
        public Task<WeatherResponse> GetCityAndCurrentTemperatureAsync(string zip);
    }
}
