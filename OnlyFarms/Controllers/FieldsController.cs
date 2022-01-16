using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlyFarms.Data;
using OnlyFarms.Models;
using OnlyFarms.Services;

namespace OnlyFarms.Controllers
{
    public class FieldsController : Controller
    {
        private readonly FarmContext _context;
        private readonly IWeatherService _weatherService;

        public FieldsController(FarmContext context, IWeatherService weatherService)
        {
            _context = context;
            _weatherService = weatherService;
        }

        // GET: Fields
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fields.ToListAsync());
        }

        // GET: Fields/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @field = await _context.Fields
                .FirstOrDefaultAsync(m => m.ID == id);

            List<Cultivation> cultivations = await _context.Cultivations
                                                    .Include(c => c.Crop)
                                                    .Include(c => c.Field)
                                                    .Where(c => c.FieldID == field.ID)
                                                    .ToListAsync();
            ViewBag.cultivations = cultivations;

            if (@field == null)
            {
                return NotFound();
            }

            Weather weather = await _context.Weathers
                                    .Include(s => s.Field)
                                    .Where(s => s.Field.ID == id)
                                    .OrderBy(s => s.Date)
                                    .LastOrDefaultAsync();
            
            ViewBag.weather = weather;

            return View(@field);
        }

        // GET: Fields/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fields/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("ID,Tag,City,Street,FieldSurface")] Field @field)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@field);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@field);
        }

        // GET: Fields/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @field = await _context.Fields.FindAsync(id);
            if (@field == null)
            {
                return NotFound();
            }
            return View(@field);
        }

        // POST: Fields/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Tag,City,Street,FieldSurface")] Field @field)
        {
            if (id != @field.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@field);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FieldExists(@field.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@field);
        }

        // GET: Fields/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @field = await _context.Fields
                .FirstOrDefaultAsync(m => m.ID == id);
            if (@field == null)
            {
                return NotFound();
            }

            return View(@field);
        }

        // POST: Fields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @field = await _context.Fields.FindAsync(id);
            _context.Fields.Remove(@field);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FieldExists(int id)
        {
            return _context.Fields.Any(e => e.ID == id);
        }

        public async Task<IActionResult> UpdateWeather(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @field = await _context.Fields
                .FirstOrDefaultAsync(m => m.ID == id);

            List<Cultivation> cultivations = await _context.Cultivations
                                                    .Include(c => c.Crop)
                                                    .Include(c => c.Field)
                                                    .Where(c => c.FieldID == field.ID)
                                                    .ToListAsync();
            ViewBag.cultivations = cultivations;

            if (@field == null)
            {
                return NotFound();
            }

            Weather weather = await _context.Weathers
                                    .Include(s => s.Field)
                                    .Where(s => s.Field.ID == id)
                                    .OrderBy(s => s.Date)
                                    .LastOrDefaultAsync();

            Random rand = new Random();

            Weather newWeather = new Weather();
            newWeather.FieldID = (int)id;
            newWeather.Date = DateTime.Now;

            if (weather == null)
            {
                newWeather.Temperature = rand.Next(-20, 50);
                newWeather.Moisture = rand.Next(0, 100);
                newWeather.AirPressure = rand.Next(900, 1100);
                newWeather.RainfallAmount = rand.Next(0, 1825);
                int i = rand.Next(0, 3);
                switch (i)
                {
                    case 0:
                        newWeather.WindDirection = "E";
                        break;

                    case 1:
                        newWeather.WindDirection = "W";
                        break;
                    case 2:
                        newWeather.WindDirection = "N";
                        break;
                    case 3:
                        newWeather.WindDirection = "S";
                        break;
                }
                newWeather.WindSpeed = rand.Next(0, 200);
            }
            else
            {
                newWeather.Temperature = weather.Temperature + rand.Next(-5, 5);
                if(weather.Moisture > 10)
                    newWeather.Moisture = weather.Moisture + rand.Next(-10, 10);
                else
                    newWeather.Moisture = weather.Moisture + rand.Next(0, 10);
                newWeather.AirPressure = weather.AirPressure + rand.Next(-10, 10);
                if(weather.RainfallAmount > 20)
                    newWeather.RainfallAmount = weather.RainfallAmount + rand.Next(-20, 20);
                else
                    newWeather.RainfallAmount = weather.RainfallAmount + rand.Next(0, 20);
                int i = rand.Next(0, 3);
                switch (i)
                {
                    case 0:
                        newWeather.WindDirection = "E";
                        break;

                    case 1:
                        newWeather.WindDirection = "W";
                        break;
                    case 2:
                        newWeather.WindDirection = "N";
                        break;
                    case 3:
                        newWeather.WindDirection = "S";
                        break;
                }
                if(weather.WindSpeed > 5)
                    newWeather.WindSpeed= weather.WindSpeed + rand.Next(-5, 5);
                else
                    newWeather.WindSpeed = weather.WindSpeed + rand.Next(0, 5);
            }

            var harvestObserver = new HarvestObserver();
            var fertilizationObserver = new FertilizationObserver();

            _weatherService.Attach(harvestObserver);
            _weatherService.Attach(fertilizationObserver);

            Console.WriteLine("Updating Weather Status...");

            _weatherService.UpdateWeather(newWeather);

            _context.Add(newWeather);
            await _context.SaveChangesAsync();

            ViewBag.weather = newWeather;

            return View("Details", @field);
        }

    }
}
