
using GeoDetectorMvcApp.Context;
using Microsoft.AspNetCore.Mvc;
using WeatherModel = WeatherServiceWebApi.Model.WeatherModel;

namespace GeoDetectorMvcApp.Controllers;

public class WeatherController : Controller
{
    private MainContext _mainContext;

    public WeatherController(MainContext mainContext)
    {
        _mainContext = mainContext;
    }

    [HttpGet]
    public async Task<IActionResult> WeatherIndex()
    {
        return View(_mainContext.Weather);
    }

    public IActionResult WeatherSet()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> WeatherSet(WeatherModel model)
    {
        _mainContext.Weather.Add(model);
        await _mainContext.SaveChangesAsync();
        return RedirectToAction("WeatherIndex");
    }

}