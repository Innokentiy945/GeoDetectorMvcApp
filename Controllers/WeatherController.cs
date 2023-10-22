
using Microsoft.AspNetCore.Mvc;
using WeatherServiceWebApi.Context;
using WeatherModel = WeatherServiceWebApi.Model.WeatherModel;

namespace GeoDetectorMvcApp.Controllers;

public class WeatherController : Controller
{
    WeatherContext _weatherContext;

    public WeatherController(WeatherContext context)
    {
        _weatherContext = context;
    }

    [HttpGet]
    [Route("GetWeatherData")]
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