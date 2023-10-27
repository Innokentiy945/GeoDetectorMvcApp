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

    public IActionResult GeoIndex()
    {
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> GeoIndex(string searchString)
    {
        if (_mainContext.Geo == null)
        {
            return Problem("Geo data is null.");
        }

        var results = from m in _mainContext.Geo
            select m;

        if (!String.IsNullOrEmpty(searchString))
        {
            results = results.Where(s => s.Name!.Contains(searchString));
        }

        return View(await results.ToListAsync());
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