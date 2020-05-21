using MusicForWeather.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicForWeather.Domain.Contracts.Integration
{
    public interface ISpotifyIntegration
    {
        Task<IList<Song>> GetRecomendationByGender(EnumMusicGender gender);
    }
}