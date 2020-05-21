using Microsoft.Extensions.Configuration;
using MusicForWeather.Domain.Contracts.Integration;
using MusicForWeather.Domain.Models;
using MusicForWeather.Integration.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MusicForWeather.Integration.Spotify
{
    public class SpotifyIntegration : ISpotifyIntegration
    {
        private readonly HttpClient _client;
        public IConfiguration _configuration { get; }
        private string _uri;
        private HttpRequestMessage _request;

        public SpotifyIntegration(IConfiguration configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;
            _uri = $"{_configuration.GetSection("Integration:Spotify:ApiUri").Value}{_configuration.GetSection("Integration:Spotify:RecomendationsResource").Value}" +
                   $"?limit={_configuration.GetSection("Integration:Spotify:Limit").Value}" +
                   $"&market={_configuration.GetSection("Integration:Spotify:Market").Value}";

            _request = new HttpRequestMessage();
            _request.Method = HttpMethod.Get;
        }

        public async Task<IList<Song>> GetRecomendationByGender(EnumMusicGender gender)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration.GetSection("Integration:Spotify:Token").Value);
            _uri += $"&seed_genres={Enum.GetName(typeof(EnumMusicGender), gender)}";

            _request = new HttpRequestMessage(HttpMethod.Get, _uri);
            var result = await _client.SendAsync(_request);

            var data = JsonConvert.DeserializeObject<SpotifyResponse>(await result.Content.ReadAsStringAsync());

            return data.tracks;
        }
    }
}