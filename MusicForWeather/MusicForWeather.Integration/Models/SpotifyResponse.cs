using MusicForWeather.Domain.Models;
using System.Collections.Generic;

namespace MusicForWeather.Integration.Models
{
    public class SpotifyResponse
    {
        public IList<Song> tracks { get; set; }
    }
}