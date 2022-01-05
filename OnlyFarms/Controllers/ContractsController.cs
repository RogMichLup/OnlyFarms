using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlyFarms.Data;
using OnlyFarms.Models;

namespace OnlyFarms.Controllers {
    public class ContractsController : Controller {
        private readonly FarmContext _context;
        private List<Crop> crops;

        public ContractsController(FarmContext context) {
            _context = context;
            crops = _context.Crops.ToList();
            ViewBag.crops = crops;
        }

        // GET: Contracts
        public async Task<IActionResult> Index() {
            return View(await _context.Contracts.ToListAsync());
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var contract = await _context.Contracts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contract == null) {
                return NotFound();
            }

            crops = await _context.Crops.ToListAsync();
            ViewBag.crops = crops;
            List<ContractCrop> contractCrops = await _context.ContractCrops.Where(p => p.ContractID == contract.ID).ToListAsync();
            ViewBag.contractCrops = contractCrops;

            List<ContractCrop> contractCrop = await _context.ContractCrops
                               .Include(s => s.Contract)
                               .Include(s => s.Crop)
                               .Where(s => s.ContractID == id)
                               .ToListAsync();

            

            if (contractCrop == null)
            {
                return NotFound();
            }

            List<double> percentageOfContractCompletionList = new List<double>();
            List<string> names = new List<string>();
            List<int> maxx = new List<int>();

            foreach (ContractCrop item in contractCrop)
            {
                List<Cultivation> cultivations = await _context.Cultivations
                                 .Include(s => s.Crop)
                                 .Include(s => s.Field)
                                 .Where(s => s.CropID == item.CropID)
                                 .ToListAsync();

                List<ContractCrop> contractCropBefore = await _context.ContractCrops
                              .Include(s => s.Contract)
                              .Where(s => s.Contract.DeliveryDate < item.Contract.DeliveryDate)
                              .Include(s => s.Crop)
                              .Where(s => s.CropID == item.CropID)
                              .ToListAsync();

                double percentageOfContractCompletion = 0;

                if (cultivations.Count != 0)
                {
                    int expectedYieldFromCultivations = 0;

                    for (int i = 0; i < cultivations.Count; i++)
                    {
                        expectedYieldFromCultivations += cultivations[i].Crop.ExpectedYield * cultivations[i].AreaInHectar;
                    }
                    if(contractCropBefore.Count == 0)
                        percentageOfContractCompletion = ((double)expectedYieldFromCultivations / (double)item.Quantity) * 100;
                    else
                    {
                        foreach (ContractCrop before in contractCropBefore)
                        {
                            expectedYieldFromCultivations -= before.Quantity;
                        }
                        percentageOfContractCompletion = ((double)expectedYieldFromCultivations / (double)item.Quantity) * 100;

                    }
                }
                percentageOfContractCompletionList.Add(percentageOfContractCompletion);
                names.Add(item.Crop.CropName);
                maxx.Add(item.Quantity);
            }

            ViewBag.percentageOfContractCompletionList = percentageOfContractCompletionList;
            ViewBag.names = names;
            ViewBag.maxx = maxx;
            return View(contract);
        }

        // GET: Contracts/Create
        public async Task<IActionResult> Create() {
            crops = await _context.Crops.ToListAsync();
            ViewBag.crops = crops;
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ClientName,DeliveryDate")] Contract contract) {
            if (ModelState.IsValid) {
                _context.Add(contract);
                await _context.SaveChangesAsync();
                List<Contract> contracts = await _context.Contracts.ToListAsync();
                int lastID = contracts.Last().ID;
                foreach (Crop item in crops) {
                    string isChecked = Request.Form["cx+" + item.ID].ToString();
                    if (isChecked == "on") {
                        string requestString = Request.Form["in+" + item.ID].ToString();
                        int cropCount = new int();
                        if (Int32.TryParse(requestString, out cropCount)) {
                            ContractCrop ctcr = new ContractCrop();
                            ctcr.ContractID = lastID;
                            ctcr.Contract = contracts.Last();
                            ctcr.CropID = item.ID;
                            ctcr.Crop = crops.Find(p => p.ID == item.ID);
                            ctcr.Quantity = cropCount;

                            _context.Add(ctcr);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contract);
        }

        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null) {
                return NotFound();
            }

            crops = await _context.Crops.ToListAsync();
            ViewBag.crops = crops;
            List<ContractCrop> contractCrops = await _context.ContractCrops.Where(p => p.ContractID == contract.ID).ToListAsync();
            ViewBag.contractCrops = contractCrops;
            
            return View(contract);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ClientName,DeliveryDate")] Contract contract) {
            if (id != contract.ID) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(contract);
                    _context.SaveChanges();
                    Contract contracts = await _context.Contracts.Where(p => p.ID == contract.ID).FirstAsync();
                    foreach (Crop item in crops) {
                        string isChecked = Request.Form["cx+" + item.ID].ToString();
                        List<ContractCrop> contractCrops = await _context.ContractCrops.Where(p => p.ContractID == contract.ID && p.CropID == item.ID).ToListAsync();
                        if (contractCrops.Count == 1) {
                            if (isChecked == "on") {
                                string requestString = Request.Form["in+" + item.ID].ToString();
                                int cropCount = new int();
                                if (Int32.TryParse(requestString, out cropCount)) {
                                    ContractCrop ctcr = new ContractCrop();
                                    contractCrops.Last().Quantity = cropCount;

                                    await _context.SaveChangesAsync();
                                }
                            }
                            else {
                                string requestString = Request.Form["in+" + item.ID].ToString();

                                ContractCrop ctcr = contractCrops.Last();
                                _context.Entry(ctcr).State = EntityState.Deleted;

                                _context.SaveChanges();
                                await _context.SaveChangesAsync();

                            }
                        }
                        else {
                            if (isChecked == "on") {
                                if (isChecked == "on") {
                                    string requestString = Request.Form["in+" + item.ID].ToString();
                                    int cropCount = new int();
                                    if (Int32.TryParse(requestString, out cropCount)) {
                                        ContractCrop ctcr = new ContractCrop();
                                        ctcr.ContractID = contract.ID;
                                        ctcr.Contract = contracts;
                                        ctcr.CropID = item.ID;
                                        ctcr.Crop = crops.Find(p => p.ID == item.ID);
                                        ctcr.Quantity = cropCount;

                                        _context.Add(ctcr);
                                        await _context.SaveChangesAsync();
                                    }
                                }
                            }
                        }

                    }
                }
                catch (DbUpdateConcurrencyException) {
                    if (!ContractExists(contract.ID)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contract);
        }

        // GET: Contracts/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var contract = await _context.Contracts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contract == null) {
                return NotFound();
            }

            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var contract = await _context.Contracts.FindAsync(id);
            List<ContractCrop> contractCrops = await _context.ContractCrops.Where(p => p.ContractID == contract.ID).ToListAsync();
            foreach(var item in contractCrops) {
                _context.Entry(item).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractExists(int id) {
            return _context.Contracts.Any(e => e.ID == id);
        }

    }
}
