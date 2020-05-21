using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicForWeather.Domain.Contracts.Integration;
using MusicForWeather.Domain.Contracts.Services;
using MusicForWeather.Domain.Models;
using MusicForWeather.Domain.Services;
using MusicForWeather.Integration.OpenWeatherMaps;
using MusicForWeather.Integration.Spotify;
using Xunit;

namespace MusicForWeather.Domain.Test
{
    public class MusicServiceTest
    {
        private readonly IMusicService _musicService;
        private ServiceCollection services;
        public MusicServiceTest()
        {
            services = new ServiceCollection();
            ConfigureDependencyInjection();

            var serviceProvider = services.BuildServiceProvider();

            _musicService = serviceProvider.GetService<IMusicService>();
        }

        [Theory]
        [InlineData(30.1, EnumMusicGender.party)]
        [InlineData(9.9, EnumMusicGender.classical)]
        [InlineData(-0.1, EnumMusicGender.classical)]
        [InlineData(10, EnumMusicGender.rock)]
        [InlineData(15, EnumMusicGender.pop)]
        public void GetMusicGenderByTemperatureTest(double temperature, EnumMusicGender expected)
        {
            var result = _musicService.GetMusicGenderByTemperature(temperature);
            Assert.Equal(expected, result);
        }

        private void ConfigureDependencyInjection()
        {
            services.AddTransient<IMusicService, MusicService>();
            services.AddHttpClient<ISpotifyIntegration, SpotifyIntegration>();
            services.AddHttpClient<IOpenWeatherMapsIntegration, OpenWeatherMapsIntegration>();

            services.AddTransient<IConfiguration>(serviceProvider =>
            {
                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.AddJsonFile("appsettings.json");
                return configurationBuilder.Build();
            });
        }
    }
}