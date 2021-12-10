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

namespace OnlyFarms.Controllers
{
    public class ProceduresController : Controller
    {
        private readonly FarmContext _context;

        public ProceduresController(FarmContext context)
        {
            _context = context;
        }

        // GET: Procedures
        public async Task<IActionResult> Index()
        {
            var farmContext = _context.Procedures.Include(p => p.Equipment).Include(p => p.Field).Include(p => p.Machine).Include(p => p.Worker);
            return View(await farmContext.ToListAsync());
        }

        // GET: Procedures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedure = await _context.Procedures
                .Include(p => p.Equipment)
                .Include(p => p.Field)
                .Include(p => p.Machine)
                .Include(p => p.Worker)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (procedure == null)
            {
                return NotFound();
            }

            return View(procedure);
        }

        // GET: Procedures/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewData["EquipmentID"] = new SelectList(_context.Equipments, "ID", "Name");
            ViewData["FieldID"] = new SelectList(_context.Fields, "ID", "City");
            ViewData["MachineID"] = new SelectList(_context.Machines, "ID", "Name");
            ViewData["WorkerID"] = new SelectList(_context.Workers, "ID", "FirstName");
            return View();
        }

        // POST: Procedures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("ID,Label,StartDate,DurationInHours,Status,FieldID,EquipmentID,MachineID,WorkerID")] Procedure procedure)
        {
            if (ModelState.IsValid)
            {
                _context.Add(procedure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipmentID"] = new SelectList(_context.Equipments, "ID", "Name", procedure.EquipmentID);
            ViewData["FieldID"] = new SelectList(_context.Fields, "ID", "City", procedure.FieldID);
            ViewData["MachineID"] = new SelectList(_context.Machines, "ID", "Name", procedure.MachineID);
            ViewData["WorkerID"] = new SelectList(_context.Workers, "ID", "FirstName", procedure.WorkerID);
            return View(procedure);
        }

        // GET: Procedures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedure = await _context.Procedures.FindAsync(id);
            if (procedure == null)
            {
                return NotFound();
            }
            ViewData["EquipmentID"] = new SelectList(_context.Equipments, "ID", "Name", procedure.EquipmentID);
            ViewData["FieldID"] = new SelectList(_context.Fields, "ID", "City", procedure.FieldID);
            ViewData["MachineID"] = new SelectList(_context.Machines, "ID", "Name", procedure.MachineID);
            ViewData["WorkerID"] = new SelectList(_context.Workers, "ID", "FirstName", procedure.WorkerID);
            return View(procedure);
        }

        // POST: Procedures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Label,StartDate,DurationInHours,Status,FieldID,EquipmentID,MachineID,WorkerID")] Procedure procedure)
        {
            if (id != procedure.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(procedure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcedureExists(procedure.ID))
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
            ViewData["EquipmentID"] = new SelectList(_context.Equipments, "ID", "Name", procedure.EquipmentID);
            ViewData["FieldID"] = new SelectList(_context.Fields, "ID", "City", procedure.FieldID);
            ViewData["MachineID"] = new SelectList(_context.Machines, "ID", "Name", procedure.MachineID);
            ViewData["WorkerID"] = new SelectList(_context.Workers, "ID", "FirstName", procedure.WorkerID);
            return View(procedure);
        }

        // GET: Procedures/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedure = await _context.Procedures
                .Include(p => p.Equipment)
                .Include(p => p.Field)
                .Include(p => p.Machine)
                .Include(p => p.Worker)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (procedure == null)
            {
                return NotFound();
            }

            return View(procedure);
        }

        // POST: Procedures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var procedure = await _context.Procedures.FindAsync(id);
            _context.Procedures.Remove(procedure);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcedureExists(int id)
        {
            return _context.Procedures.Any(e => e.ID == id);
        }
    }
}
