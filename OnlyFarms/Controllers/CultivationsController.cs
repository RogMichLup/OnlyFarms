﻿using System;
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
    public class CultivationsController : Controller
    {
        private readonly FarmContext _context;

        public CultivationsController(FarmContext context)
        {
            _context = context;
        }

        // GET: Cultivations
        public async Task<IActionResult> Index()
        {
            var farmContext = _context.Cultivations.Include(c => c.Crop).Include(c => c.Field);
            return View(await farmContext.ToListAsync());
        }

        // GET: Cultivations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cultivation = await _context.Cultivations
                .Include(c => c.Crop)
                .Include(c => c.Field)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cultivation == null)
            {
                return NotFound();
            }

            return View(cultivation);
        }

        // GET: Cultivations/Create
        public IActionResult Create()
        {
            ViewData["CropID"] = new SelectList(_context.Crops, "ID", "CropName");
            ViewData["FieldID"] = new SelectList(_context.Fields, "ID", "City");
            return View();
        }

        // POST: Cultivations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AreaInHectar,CropStatus,CropID,FieldID")] Cultivation cultivation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cultivation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CropID"] = new SelectList(_context.Crops, "ID", "CropName", cultivation.CropID);
            ViewData["FieldID"] = new SelectList(_context.Fields, "ID", "City", cultivation.FieldID);
            return View(cultivation);
        }

        // GET: Cultivations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cultivation = await _context.Cultivations.FindAsync(id);
            if (cultivation == null)
            {
                return NotFound();
            }
            ViewData["CropID"] = new SelectList(_context.Crops, "ID", "CropName", cultivation.CropID);
            ViewData["FieldID"] = new SelectList(_context.Fields, "ID", "City", cultivation.FieldID);
            return View(cultivation);
        }

        // POST: Cultivations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AreaInHectar,CropStatus,CropID,FieldID")] Cultivation cultivation)
        {
            if (id != cultivation.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cultivation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CultivationExists(cultivation.ID))
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
            ViewData["CropID"] = new SelectList(_context.Crops, "ID", "CropName", cultivation.CropID);
            ViewData["FieldID"] = new SelectList(_context.Fields, "ID", "City", cultivation.FieldID);
            return View(cultivation);
        }

        // GET: Cultivations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cultivation = await _context.Cultivations
                .Include(c => c.Crop)
                .Include(c => c.Field)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cultivation == null)
            {
                return NotFound();
            }

            return View(cultivation);
        }

        // POST: Cultivations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cultivation = await _context.Cultivations.FindAsync(id);
            _context.Cultivations.Remove(cultivation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CultivationExists(int id)
        {
            return _context.Cultivations.Any(e => e.ID == id);
        }
    }
}