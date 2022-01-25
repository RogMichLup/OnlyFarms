using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models.Strategies {
    public class NoProfitCalculation : ProfitCalculationStrategy {
        public double CalculateProfit(List<Procedure> allProceduresDone, Cultivation cultivation, double fuelPricePerUnit) {
            return -2000000; //guardian value that is never used
        }
    }
}
