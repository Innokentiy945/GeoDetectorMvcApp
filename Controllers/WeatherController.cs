using GeoDetectorMvcApp.DbContext;
using GeoDetectorMvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeoDetectorMvcApp.Controllers
{
    [Route("api/WeatherService")]
    public class WeatherController : Controller
    {
        WeatherContext _weatherContext;

        public WeatherController(WeatherContext context)
        {
            _weatherContext = context;
        }

        [HttpGet]
        [Route("GetWateherData")]
        public async Task<IActionResult> WeatherIndex()
        {
            return View(_weatherContext.Weather);
        }

        public IActionResult WeatherSet()
        {
            return View();
        }

        [HttpPost]
        [Route("SetWateherData")]
        public async Task<IActionResult> WeatherSet(WeatherModel model)
        {
            _weatherContext.Weather.Add(model);
            await _weatherContext.SaveChangesAsync();
            return RedirectToAction("WeatherIndex");
        }

    }
}
