using Microsoft.AspNetCore.Mvc;
using MusicForWeather.Domain.Contracts.Services;
using System.Threading.Tasks;

namespace MusicForWeather.WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusicsController : ControllerBase
    {
        private readonly IMusicService _service;
        public MusicsController(IMusicService service)
        {
            _service = service;
        }

        [HttpGet("GetByCity/{cityname}")]
        public async Task<IActionResult> GetByCity(string cityname)
        {
            return Ok(await _service.GetPlayListAccordingTemperature(cityname));
        }

        [HttpGet("GetByLocation/{latitude}/{longitude}")]
        public async Task<IActionResult> GetByLocation(double latitude, double longitude)
        {
            return Ok(await _service.GetPlayListAccordingTemperature(latitude, longitude));
        }
    }
}