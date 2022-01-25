using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models.Strategies {
    public class RegularProfitCalculation : ProfitCalculationStrategy {
        public double CalculateProfit(List<Procedure> allProceduresDone, Cultivation cultivation, double fuelPricePerUnit) {
            double profitBillanse = 0;
            for (int i = 0; i < allProceduresDone.Count; i++) {
                profitBillanse -= allProceduresDone[i].Worker.HourlyPay * (double)allProceduresDone[i].DurationInHours * (cultivation.AreaInHectar / allProceduresDone[i].Field.FieldSurface);
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
