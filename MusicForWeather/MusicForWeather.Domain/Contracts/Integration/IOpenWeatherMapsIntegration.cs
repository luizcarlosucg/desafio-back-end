using System.Threading.Tasks;

namespace MusicForWeather.Domain.Contracts.Integration
{
    public interface IOpenWeatherMapsIntegration
    {
        Task<double> GetTemperature(string cityname);
        Task<double> GetTemperature(double latitude, double longitude);
    }
}