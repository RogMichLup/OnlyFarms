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
    public class CropSalesController : Controller
    {
        private readonly FarmContext _context;

        public CropSalesController(FarmContext context)
        {
            _context = context;
        }

        // GET: CropSales
        public async Task<IActionResult> Index()
        {
            var farmContext = _context.CropSales.Include(c => c.Crop);
            return View(await farmContext.ToListAsync());
        }

        // GET: CropSales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cropSale = await _context.CropSales
                .Include(c => c.Crop)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cropSale == null)
            {
                return NotFound();
            }

            return View(cropSale);
        }

        // GET: CropSales/Create
        public IActionResult Create()
        {
            ViewData["CropID"] = new SelectList(_context.Crops, "ID", "CropName");
            return View();
        }

        // POST: CropSales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Quantity,SaleDate,CropID")] CropSale cropSale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cropSale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CropID"] = new SelectList(_context.Crops, "ID", "CropName", cropSale.CropID);
            return View(cropSale);
        }

        // GET: CropSales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cropSale = await _context.CropSales.FindAsync(id);
            if (cropSale == null)
            {
                return NotFound();
            }
            ViewData["CropID"] = new SelectList(_context.Crops, "ID", "CropName", cropSale.CropID);
            return View(cropSale);
        }

        // POST: CropSales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Quantity,SaleDate,CropID")] CropSale cropSale)
        {
            if (id != cropSale.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cropSale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CropSaleExists(cropSale.ID))
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
            ViewData["CropID"] = new SelectList(_context.Crops, "ID", "CropName", cropSale.CropID);
            return View(cropSale);
        }

        // GET: CropSales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cropSale = await _context.CropSales
                .Include(c => c.Crop)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cropSale == null)
            {
                return NotFound();
            }

            return View(cropSale);
        }

        // POST: CropSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cropSale = await _context.CropSales.FindAsync(id);
            _context.CropSales.Remove(cropSale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CropSaleExists(int id)
        {
            return _context.CropSales.Any(e => e.ID == id);
        }
    }
}
