using System;

namespace WeatherFetcher.Models.Response
{
    public class WeatherResponse
    {
        public string City { get; set; }
        public double Temperature { get; set; }
        public DateTime Time { get; set; }
        public string Zip { get; set; }
    }
}
