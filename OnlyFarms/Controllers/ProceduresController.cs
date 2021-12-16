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
        private List<Supply> supplies;

        public ProceduresController(FarmContext context)
        {
            _context = context;
            supplies = _context.Supplies.ToList();
            ViewBag.supplies = supplies;
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
                .Include(p => p.Supplies)
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
            ViewData["EquipmentID"] = new SelectList(_context.Equipments.Where(s => s.Status != "For repair"), "ID", "Name");
            ViewData["FieldID"] = new SelectList(_context.Fields, "ID", "Tag");
            ViewData["MachineID"] = new SelectList(_context.Machines.Where(s => s.Status != "For repair"), "ID", "Name");
            ViewData["WorkerID"] = new SelectList(_context.Workers, "ID", "FirstName");
            ViewBag.supplies = supplies;
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
                procedure.Supplies = new List<Supply>();
                foreach (Supply item in supplies)
                {
                    string isChecked = Request.Form["cx+" + item.ID].ToString();
                    if (isChecked == "on")
                    {
                        procedure.Supplies.Add(item);
                    }
                }
                _context.Add(procedure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipmentID"] = new SelectList(_context.Equipments, "ID", "Name", procedure.EquipmentID);
            ViewData["FieldID"] = new SelectList(_context.Fields, "ID", "Tag", procedure.FieldID);
            ViewData["MachineID"] = new SelectList(_context.Machines, "ID", "Name", procedure.MachineID);
            ViewData["WorkerID"] = new SelectList(_context.Workers, "ID", "FirstName", procedure.WorkerID);
            ViewBag.supplies = supplies;
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

            List<Supply> suppliesInProcedure;

            suppliesInProcedure = await _context.Supplies
                .Include(s => s.Procedures)
                .Where(s => s.Procedures.Contains(procedure))
                .ToListAsync();

            ViewData["EquipmentID"] = new SelectList(_context.Equipments, "ID", "Name", procedure.EquipmentID);
            ViewData["FieldID"] = new SelectList(_context.Fields, "ID", "Tag", procedure.FieldID);
            ViewData["MachineID"] = new SelectList(_context.Machines, "ID", "Name", procedure.MachineID);
            ViewData["WorkerID"] = new SelectList(_context.Workers, "ID", "FirstName", procedure.WorkerID);
            ViewBag.suppliesInProcedure = suppliesInProcedure;
            ViewBag.supplies = supplies;
            return View(procedure);
        }

        // POST: Procedures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Label,StartDate,DurationInHours,Status,FieldID,EquipmentID,MachineID,WorkerID")] Procedure procedure)
        {

            var procedureSupply = await _context.Procedures
                .Include(p => p.Equipment)
                .Include(p => p.Field)
                .Include(p => p.Machine)
                .Include(p => p.Worker)
                .Include(p => p.Supplies)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (id != procedure.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(procedureSupply);
                    _context.SaveChanges();

                    procedureSupply.Supplies = new List<Supply>();
                    foreach (Supply item in supplies)
                    {
                        string isChecked = Request.Form["cx+" + item.ID].ToString();
                        if (isChecked == "on")
                        {
                            procedureSupply.Supplies.Add(item);
                        }
                    }

                    procedureSupply.DurationInHours = procedure.DurationInHours;
                    procedureSupply.EquipmentID = procedure.EquipmentID;
                    procedureSupply.FieldID = procedure.FieldID;
                    procedureSupply.Label = procedure.Label;
                    procedureSupply.MachineID = procedure.MachineID;
                    procedureSupply.WorkerID = procedure.WorkerID;
                    procedureSupply.StartDate = procedure.StartDate;
                    procedureSupply.Status = procedure.Status;

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
            ViewData["FieldID"] = new SelectList(_context.Fields, "ID", "Tag", procedure.FieldID);
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
                .Include(p => p.Supplies)
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
