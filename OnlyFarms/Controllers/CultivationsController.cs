using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlyFarms.Data;
using OnlyFarms.Models;
using OnlyFarms.Models.Strategies;

namespace OnlyFarms.Controllers {
    public class CultivationsController : Controller {
        private readonly FarmContext _context;
        private const double fuelPrice = 5.5;
        ProfitCalculationStrategy profitCalculationStrategy;

        public CultivationsController(FarmContext context) {
            _context = context;
        }

        // GET: Cultivations
        public async Task<IActionResult> Index() {
            var farmContext = _context.Cultivations.Include(c => c.Crop).Include(c => c.Field);
            return View(await farmContext.ToListAsync());
        }

        // GET: Cultivations/Details/5
        public async Task<IActionResult> Details(int? id) {
            HttpContext.Session.GetString("strategy" + id.ToString());
            profitCalculationStrategy = DecodeStrategyFromSession(id);

            if (id == null) {
                return NotFound();
            }

            Cultivation cultivation = GetCultivationFromDB(_context, id);

            if (cultivation == null) {
                return NotFound();
            }

            List<Procedure> relevantProcedures = GetRelevantProceduresFromDB(_context, id);

            if (profitCalculationStrategy.GetType() == typeof(NoProfitCalculation)) {
                ViewBag.costBillanse = "Not calculated, please select profit calculation method.";
                ViewBag.strategySelected = "false";
            }
            else {
                ViewBag.costBillanse = profitCalculationStrategy.CalculateProfit(relevantProcedures, cultivation, fuelPrice);
                ViewBag.strategySelected = "true";
            }
            return View(cultivation);
        }

        // GET: Cultivations/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create() {
            ViewData["CropID"] = new SelectList(_context.Crops, "ID", "CropName");
            ViewData["FieldID"] = new SelectList(_context.Fields, "ID", "Tag");
            return View();
        }

        // POST: Cultivations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("ID,AreaInHectar,CropStatus,CropID,FieldID")] Cultivation cultivation) {
            if (ModelState.IsValid) {
                _context.Add(cultivation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CropID"] = new SelectList(_context.Crops, "ID", "CropName", cultivation.CropID);
            ViewData["FieldID"] = new SelectList(_context.Fields, "ID", "Tag", cultivation.FieldID);
            return View(cultivation);
        }

        // GET: Cultivations/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var cultivation = await _context.Cultivations.FindAsync(id);
            if (cultivation == null) {
                return NotFound();
            }
            ViewData["CropID"] = new SelectList(_context.Crops, "ID", "CropName", cultivation.CropID);
            ViewData["FieldID"] = new SelectList(_context.Fields, "ID", "Tag", cultivation.FieldID);
            return View(cultivation);
        }

        // POST: Cultivations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AreaInHectar,CropStatus,CropID,FieldID")] Cultivation cultivation) {
            if (id != cultivation.ID) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(cultivation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!CultivationExists(cultivation.ID)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CropID"] = new SelectList(_context.Crops, "ID", "CropName", cultivation.CropID);
            ViewData["FieldID"] = new SelectList(_context.Fields, "ID", "Tag", cultivation.FieldID);
            return View(cultivation);
        }

        // GET: Cultivations/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var cultivation = await _context.Cultivations
                .Include(c => c.Crop)
                .Include(c => c.Field)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cultivation == null) {
                return NotFound();
            }

            return View(cultivation);
        }

        // POST: Cultivations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var cultivation = await _context.Cultivations.FindAsync(id);
            _context.Cultivations.Remove(cultivation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CultivationExists(int id) {
            return _context.Cultivations.Any(e => e.ID == id);
        }
        private Cultivation GetCultivationFromDB(FarmContext dbContext, int? cultivationID) {
            Cultivation cultivation = dbContext.Cultivations
                .Include(c => c.Crop)
                .Include(c => c.Field)
                .FirstOrDefault(m => m.ID == cultivationID);
            return cultivation;
        }
        private List<Procedure> GetRelevantProceduresFromDB(FarmContext dbContext, int? cultivationID) {
            Cultivation cultivation = GetCultivationFromDB(dbContext, cultivationID);
            List<Procedure> allProceduresDone = dbContext.Procedures
                .Include(s => s.Field)
                .Where(s => s.FieldID == cultivation.FieldID)
                .Include(s => s.Equipment)
                .Include(s => s.Machine)
                .Include(s => s.Worker)
                .Include(s => s.Supplies)
                .ToList();
            return allProceduresDone;
        }
        private List<CropSale> GetCropSalesFromDB(FarmContext dbContext, int? cultivationID) {
            Cultivation cultivation = GetCultivationFromDB(dbContext, cultivationID);
            List<CropSale> allCropSale = dbContext.CropSales
                .Include(s => s.Crop)
                .Where(s => s.CropID == cultivation.CropID)
                .ToList();
            return allCropSale;
        }
        private List<ContractCrop> GetContractCropsFromDB(FarmContext dbContext, int? cultivationID) {
            Cultivation cultivation = GetCultivationFromDB(dbContext, cultivationID);
            List<ContractCrop> allContractCrop = dbContext.ContractCrops
            .Include(s => s.Crop)
            .Where(s => s.CropID == cultivation.CropID)
            .Include(s => s.Contract)
            .ToList();
            return allContractCrop;
        }
        private ProfitCalculationStrategy DecodeStrategyFromSession(int? id) {
            ProfitCalculationStrategy strategy = null;
            string sessionStrategyInfo = HttpContext.Session.GetString("strategy" + id.ToString());
            switch (sessionStrategyInfo) {
                case "workerless":
                    strategy = new WorkerlessProfitCalculation();
                    break;
                case "supplyless":
                    strategy = new SupplylessProfitCalculation();
                    break;
                case "regular":
                    strategy = new RegularProfitCalculation();
                    break;
                default:
                    strategy = new NoProfitCalculation();
                    break;
            }
            return strategy;
        }
        public async Task<IActionResult> SelectRegularProfitCalculation(int id) {
            Cultivation cultivation = GetCultivationFromDB(_context, id);
            HttpContext.Session.SetString("strategy" + id.ToString(), "regular");
            Details(id);
            return View("Details", cultivation);
        }
        public async Task<IActionResult> SelectWorkerlessProfitCalculation(int id) {
            Cultivation cultivation = GetCultivationFromDB(_context, id);
            HttpContext.Session.SetString("strategy" + id.ToString(), "workerless");
            Details(id);
            return View("Details", cultivation);
        }
        public async Task<IActionResult> SelectSupplylessProfitCalculation(int id) {
            Cultivation cultivation = GetCultivationFromDB(_context, id);
            HttpContext.Session.SetString("strategy" + id.ToString(), "supplyless");
            Details(id);
            return View("Details", cultivation);
        }
    }
}
