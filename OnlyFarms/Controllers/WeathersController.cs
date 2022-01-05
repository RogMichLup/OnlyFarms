using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlyFarms.Data;
using OnlyFarms.Models;

namespace OnlyFarms.Controllers
{
    public class WeathersController : Controller
    {
        private readonly FarmContext _context;

        public WeathersController(FarmContext context)
        {
            _context = context;
        }

        // GET: Weathers
        public async Task<IActionResult> Index()
        {
            var farmContext = _context.Weathers.Include(w => w.Field);
            return View(await farmContext.ToListAsync());
        }

        // GET: Weathers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weather = await _context.Weathers
                .Include(w => w.Field)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (weather == null)
            {
                return NotFound();
            }

            return View(weather);
        }

        // GET: Weathers/Create
        public IActionResult Create()
        {
            ViewData["FieldID"] = new SelectList(_context.Fields, "ID", "City");
            return View();
        }

        // POST: Weathers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Temperature,Moisture,AirPressure,RainfallAmount,WindDirection,WindSpeed,Date,FieldID")] Weather weather)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weather);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FieldID"] = new SelectList(_context.Fields, "ID", "City", weather.FieldID);
            return View(weather);
        }

        // GET: Weathers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weather = await _context.Weathers.FindAsync(id);
            if (weather == null)
            {
                return NotFound();
            }
            ViewData["FieldID"] = new SelectList(_context.Fields, "ID", "City", weather.FieldID);
            return View(weather);
        }

        // POST: Weathers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Temperature,Moisture,AirPressure,RainfallAmount,WindDirection,WindSpeed,Date,FieldID")] Weather weather)
        {
            if (id != weather.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weather);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeatherExists(weather.ID))
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
            ViewData["FieldID"] = new SelectList(_context.Fields, "ID", "City", weather.FieldID);
            return View(weather);
        }

        // GET: Weathers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weather = await _context.Weathers
                .Include(w => w.Field)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (weather == null)
            {
                return NotFound();
            }

            return View(weather);
        }

        // POST: Weathers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weather = await _context.Weathers.FindAsync(id);
            _context.Weathers.Remove(weather);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeatherExists(int id)
        {
            return _context.Weathers.Any(e => e.ID == id);
        }
    }
}
