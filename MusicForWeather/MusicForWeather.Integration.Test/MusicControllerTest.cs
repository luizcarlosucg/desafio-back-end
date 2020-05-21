using MusicForWeather.Integration.Test.Base;
using Xunit;

namespace MusicForWeather.Integration.Test
{
    public class MusicControllerTest : BaseIntegrationTest
    {
        [Fact]
        public async void GetByCity()
        {
            var response = await Client.GetAsync($"/api/Musics/GetByCity/goiania");
            response.EnsureSuccessStatusCode();
            Assert.NotNull(response.Content);
        }

        [Fact]
        public async void GetByLocation()
        {
            var response = await Client.GetAsync($"/api/Musics/GetByLocation/-34.66/-48.23");
            response.EnsureSuccessStatusCode();
            Assert.NotNull(response.Content);
        }
    }
}
