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

        //public async Task<IActionResult> Index(int id)
        //{

        //    int percentageOfContractCompletion = id;
        //    ViewBag.percentageOfContractCompletion = percentageOfContractCompletion;
        //    return View();
        //}

        public async Task<IActionResult> Show(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContractCrop contract = await _context.ContractCrops
                                .Include(s => s.Contract)
                                .Include(s => s.Crop)
                                .Where(s => s.ContractID == id)
                                .FirstOrDefaultAsync(s => s.ID == id);

            if (contract == null)
            {
                return NotFound();
            }

            List<Cultivation> cultivations = await _context.Cultivations
                                 .Include(s => s.Crop)
                                 .Include(s => s.Field)
                                 .Where(s => s.CropID == contract.CropID)
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

                percentageOfContractCompletion = ((double)expectedYieldFromCultivations / (double)contract.Quantity) * 100;
            }

            ViewBag.percentageOfContractCompletion = percentageOfContractCompletion;
            return View();
        }
    }
}
