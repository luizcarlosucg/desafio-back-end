using Microsoft.Extensions.Configuration;
using MusicForWeather.Domain.Contracts.Integration;
using MusicForWeather.Integration.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MusicForWeather.Integration.OpenWeatherMaps
{
    public class OpenWeatherMapsIntegration : IOpenWeatherMapsIntegration
    {
        private readonly HttpClient _client;
        public IConfiguration _configuration { get; }
        private HttpRequestMessage _request;
        private string _uri;

        public OpenWeatherMapsIntegration(IConfiguration configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
            _uri = $"{_configuration.GetSection("Integration:OpenWeatherMaps:ApiUri").Value}{_configuration.GetSection("Integration:OpenWeatherMaps:WeatherResource").Value}" +
                   $"?appid={_configuration.GetSection("Integration:OpenWeatherMaps:Key").Value}" +
                   $"&units={_configuration.GetSection("Integration:OpenWeatherMaps:Units").Value}";
        }

        public async Task<double> GetTemperature(string cityname)
        {
            _uri += $"&q={cityname}";
            return await GetTemperature();
        }

        public async Task<double> GetTemperature(double latitude, double longitude)
        {
            _uri += $"&lat={latitude}";
            _uri += $"&lon={longitude}";
            return await GetTemperature();
        }

        private async Task<double> GetTemperature()
        {
            _request = new HttpRequestMessage(HttpMethod.Get, _uri);
            var result = await _client.SendAsync(_request);

            var data = JsonConvert.DeserializeObject<WeatherResponse>(await result.Content.ReadAsStringAsync());

            return data.main.temp;
        }
    }
}