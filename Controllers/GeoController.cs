using GeoDetectorMvcApp.Context;
using GeoDetectorMvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeoDetectorWebApi.Model;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace GeoDetectorMvcApp.Controllers;

public class GeoController : Controller
{
    private MainContext _mainContext;

    public GeoController(MainContext mainContext)
    {
        _mainContext = mainContext;
    }
    
    [HttpGet]
    public async Task<IActionResult> GeoIndex()
    {
        return View(await _mainContext.Geo.ToListAsync());
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
            _mainContext.Geo.Add(model);
            await _mainContext.SaveChangesAsync();
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
            GeoModel geo = new GeoModel { Id = id };
            _mainContext.Entry(geo).State = EntityState.Deleted;
            await _mainContext.SaveChangesAsync();
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
        GeoModel? geo = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(_mainContext.Geo, p => p.Id == id);
        return View(geo);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(GeoModel geo)
    {
        try
        {
            _mainContext.Geo.Update(geo);
            await _mainContext.SaveChangesAsync();
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
        Guid id = new Guid();
        
        var result = await (from g in _mainContext.Geo
            join w in _mainContext.Weather on g.Id equals w.Id
            select new CombinedModel
            {
                Id = id.ToString(),
                Longitude = g.Longitude,
                Latitude = g.Latitude,
                Name = g.Name,
                Temperature = w.Temperature,
                GeoLocation = w.GeoLocation
            }).ToListAsync();

        return View(result);
    }
}