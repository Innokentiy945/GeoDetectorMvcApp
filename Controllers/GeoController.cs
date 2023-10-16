using GeoDetectorMvcApp.DbContext;
using GeoDetectorMvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeoDetectorMvcApp.Controllers
{
    public class GeoController : Controller
    {
        GeoContext db;
        public GeoController(GeoContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IActionResult> GeoIndex(string searchString)
        {
            var geoObj = from x in db.Geo select x;

            if (db.Geo == null)
            {
                return Problem("Entity set Geo is null.");
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                geoObj = geoObj.Where(s => s.Name.Contains(searchString));
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
            db.Geo.Add(model);
            await db.SaveChangesAsync();
            return RedirectToAction("GeoIndex");
        }

        public async Task<IActionResult> Delete(string id)
        {
            GeoModel geo = new GeoModel { ItemId = id };
            db.Entry(geo).State = EntityState.Deleted;
            await db.SaveChangesAsync();
            return RedirectToAction("GeoIndex");
        }

        public async Task<IActionResult> Edit(string id)
        {
            GeoModel geo = await db.Geo.FirstOrDefaultAsync(p => p.ItemId == id);
            return View(geo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GeoModel geo)
        {
            db.Geo.Update(geo);
            await db.SaveChangesAsync();
            return RedirectToAction("GeoIndex");
        }
    }
}
