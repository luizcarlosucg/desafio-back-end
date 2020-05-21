using MusicForWeather.Domain.Contracts.Integration;
using MusicForWeather.Domain.Contracts.Services;
using MusicForWeather.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicForWeather.Domain.Services
{
    public class MusicService : BaseService<Song>, IMusicService
    {
        private readonly ISpotifyIntegration _spotifyIntegration;
        private readonly IOpenWeatherMapsIntegration _weatherIntegration;

        public MusicService(ISpotifyIntegration spotifyIntegration, IOpenWeatherMapsIntegration weatherIntegration)
        {
            _spotifyIntegration = spotifyIntegration;
            _weatherIntegration = weatherIntegration;
        }

        /// <summary>
        /// Identifica o gênero ideal de música de acordo com a temperatura informada
        /// </summary>
        /// <param name="temperature">Temperatura</param>
        /// <returns>Gênero de música ideal para se apreciar</returns>
        public EnumMusicGender GetMusicGenderByTemperature(double temperature)
        {
            if (temperature > 30)
            {
                return EnumMusicGender.party;
            }
            else if (temperature >= 15 && temperature <= 30)
            {
                return EnumMusicGender.pop;
            }
            else if (temperature >= 10 && temperature <= 14)
            {
                return EnumMusicGender.rock;
            }
            else
            {
                return EnumMusicGender.classical;
            }
        }

        /// <summary>
        /// Obtem a playlist ideal para a temperatura climática atual da cidade informada.
        /// </summary>
        /// <param name="cityname">Nome da cidade</param>
        /// <returns>Playlist de músicas com o gênero adequado para a temperatura climática atual da cidade</returns>
        public async Task<IList<Song>> GetPlayListAccordingTemperature(string cityname)
        {
            var temperature = await _weatherIntegration.GetTemperature(cityname);
            return await GetPlayListAccordingTemperature(temperature);
        }

        /// <summary>
        /// Obtem a playlist ideal para a temperatura climática atual da localização informada
        /// </summary>
        /// <param name="latitude">Informação de latitude da coordenada</param>
        /// <param name="longitude">Informação de longitude da coordenada</param>
        /// <returns>Playlist de músicas com o gênero adequado para a temperatura climática atual da localização</returns>
        public async Task<IList<Song>> GetPlayListAccordingTemperature(double latitude, double longitude)
        {
            var temperature = await _weatherIntegration.GetTemperature(latitude, longitude);
            return await GetPlayListAccordingTemperature(temperature);
        }

        /// <summary>
        /// Obtem a playlist ideal para a temperatura climática informada.
        /// </summary>
        /// <param name="temperature">Temperatura</param>
        /// <returns>Playlist de mísucas com o gênero adequado para a temperatura climática informada</returns>
        public async Task<IList<Song>> GetPlayListAccordingTemperature(double temperature)
        {
            var gender = GetMusicGenderByTemperature(temperature);
            return await GetPlayListAccordingGender(gender);
        }

        /// <summary>
        /// Obtem uma playlist de acordo com o gênero informado
        /// </summary>
        /// <param name="gender">Gênero musical</param>
        /// <returns></returns>
        public async Task<IList<Song>> GetPlayListAccordingGender(EnumMusicGender gender)
        {
            return await _spotifyIntegration.GetRecomendationByGender(gender);
        }
    }
}