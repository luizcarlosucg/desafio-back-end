namespace MusicForWeather.Integration.Models
{
    public class WeatherResponse
    {
        public Main main { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
    }
}