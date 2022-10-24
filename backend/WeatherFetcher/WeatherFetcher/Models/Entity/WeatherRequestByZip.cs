namespace WeatherFetcher.Models.Entity
{
    public class WeatherRequestByZip
    {
        public int Id { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public double Temperature { get; set; }
    }
}
