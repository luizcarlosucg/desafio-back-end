using MusicForWeather.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicForWeather.Domain.Contracts.Services
{
    public interface IMusicService : IBaseService<Song>
    {
        EnumMusicGender GetMusicGenderByTemperature(double temperature);
        Task<IList<Song>> GetPlayListAccordingTemperature(string cityname);
        Task<IList<Song>> GetPlayListAccordingTemperature(double latitude, double longitude);
        Task<IList<Song>> GetPlayListAccordingTemperature(double temperature);
        Task<IList<Song>> GetPlayListAccordingGender(EnumMusicGender gender);
    }
}