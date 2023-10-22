using GeoDetectorMvcApp.Context;
using GeoDetectorMvcApp.Models;
using GeoDetectorWebApi.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeoDetectorWebApi.Model;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using WeatherServiceWebApi.Context;
using WeatherServiceWebApi.Model;

namespace GeoDetectorMvcApp.Controllers;

public class GeoController : Controller
{
    private GeoContext _geoContext;
    private CombinedContext _combContext;
    public GeoController(GeoContext contextGeo, CombinedContext _combinedContext)
    {
        _geoContext = contextGeo;
        _combContext = _combinedContext;
    }

    [HttpGet]
    public async Task<IActionResult> GeoIndex(string searchString)
    {
        var geoObj = from x in _geoContext.Geo select x;

        try
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                geoObj = geoObj.Where(s => s.Name.Contains(searchString));
            }
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }

        return View(await geoObj.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(GeoModel model)
    {
        try
        {
            _geoContext.Geo.Add(model);
            await _geoContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        return RedirectToAction("GeoIndex");
    }

    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            GeoModel geo = new GeoModel { ItemId = id };
            _geoContext.Entry(geo).State = EntityState.Deleted;
            await _geoContext.SaveChangesAsync();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        return RedirectToAction("GeoIndex");
    }

    public async Task<IActionResult> Edit(string id)
    {
        GeoModel? geo = await _geoContext.Geo.FirstOrDefaultAsync(p => p.ItemId == id);
        return View(geo);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(GeoModel geo)
    {
        try
        {
            _geoContext.Geo.Update(geo);
            await _geoContext.SaveChangesAsync();
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return RedirectToAction("GeoIndex");
    }
        
    [HttpGet]
    public async Task<ActionResult> Combined()
    {
        var getAllCombined = _combContext.Combined.FromSqlRaw("SELECT \n    GeoTable.Longitude, \n    GeoTable.Latitude, \n    WeatherTable.CloudType, \n    WeatherTable.RainPersantage, \n    WeatherTable.SunPersantage, \n    WeatherTable.Temperature, \n    WeatherTable.GeoLocation \nFROM GeoTable\nCROSS JOIN WeatherTable").AsEnumerable();
        return View(getAllCombined);
    }
}