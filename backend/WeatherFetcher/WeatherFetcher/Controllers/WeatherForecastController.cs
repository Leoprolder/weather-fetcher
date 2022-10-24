using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherFetcher.ServiceContracts;

namespace WeatherFetcher.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet(nameof(GetCityAndCurrentTemperature))]
        public async Task<JsonResult> GetCityAndCurrentTemperature(string zip)
        {
            return new JsonResult(await _weatherService.GetCityAndCurrentTemperatureAsync(zip));
        }

        [HttpGet(nameof(GetQueryHistory))]
        public async Task<JsonResult> GetQueryHistory()
        {
            return new JsonResult(await _weatherService.GetQueryHistoryAsync());
        }
    }
}
