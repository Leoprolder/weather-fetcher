namespace WeatherFetcher.Models.Response
{
    public class GeocodingResponse
    {
        public string Zip { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Country { get; set; }
    }
}
