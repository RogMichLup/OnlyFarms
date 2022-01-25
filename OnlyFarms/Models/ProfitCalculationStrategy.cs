using Microsoft.EntityFrameworkCore;
using OnlyFarms.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models {
    interface ProfitCalculationStrategy {
        public double CalculateProfit(List<Procedure> allProceduresDone, Cultivation cultivation, Double fuelPricePerUnit);
    }
}
