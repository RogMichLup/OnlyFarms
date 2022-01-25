using Microsoft.EntityFrameworkCore;
using OnlyFarms.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlyFarms.Models;

namespace OnlyFarms.Models.Strategies {
    public class WorkerlessProfitCalculation : ProfitCalculationStrategy {
        public double CalculateProfit(List<Procedure> allProceduresDone, Cultivation cultivation, double fuelPricePerUnit) {
            double profitBillanse = 0;
            for (int i = 0; i < allProceduresDone.Count; i++) {
                profitBillanse -= (double)allProceduresDone[i].DurationInHours * allProceduresDone[i].Machine.FuelUsageRate * fuelPricePerUnit * (cultivation.AreaInHectar / allProceduresDone[i].Field.FieldSurface);
                foreach (Supply s in allProceduresDone[i].Supplies) {
                    profitBillanse -= s.PricePerKilo * s.SupplyAmountPerHectare * cultivation.AreaInHectar;
                }
            }
            profitBillanse += (cultivation.Crop.ExpectedYield * cultivation.AreaInHectar) * cultivation.Crop.SellPricePerTonne;
            return profitBillanse;
        }
    }
}
