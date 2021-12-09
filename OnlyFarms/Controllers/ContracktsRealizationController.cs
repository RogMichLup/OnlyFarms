using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlyFarms.Data;
using OnlyFarms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Controllers
{
    public class ContracktsRealizationController : Controller
    {
        private readonly FarmContext _context;

        public ContracktsRealizationController(FarmContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Show(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContractCrop contractCrop = await _context.ContractCrops
                                .Include(s => s.Contract)
                                .Include(s => s.Crop)
                                .Where(s => s.ContractID == id)
                                .FirstOrDefaultAsync(s => s.ID == id);

            if (contractCrop == null)
            {
                return NotFound();
            }

            List<Cultivation> cultivations = await _context.Cultivations
                                 .Include(s => s.Crop)
                                 .Include(s => s.Field)
                                 .Where(s => s.CropID == contractCrop.CropID)
                                 .ToListAsync();

            double percentageOfContractCompletion = 0;

            if (cultivations.Count == 0)
            {
                ViewBag.percentageOfContractCompletion = percentageOfContractCompletion;
                return View();
            }
            else
            {
                int expectedYieldFromCultivations = 0;

                for (int i = 0; i < cultivations.Count; i++)
                {
                    expectedYieldFromCultivations += cultivations[i].Crop.ExpectedYield * cultivations[i].AreaInHectar;
                }

                percentageOfContractCompletion = ((double)expectedYieldFromCultivations / (double)contractCrop.Quantity) * 100;
            }

            ViewBag.percentageOfContractCompletion = percentageOfContractCompletion;
            return View();
        }
    }
}
