using OnlyFarms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Data
{
    public static class DbInitializer
    {
        public static void Initialize(FarmContext context)
        {
            context.Database.EnsureCreated();

            if (context.Crops.Any())
            {
                return;
            }

            //var contracts = new CropSale[]
            //{
            //    new CropSale{ Crop=new Crop{ CropName = "Beetroot", ExpectedYield = 50, Price = 60}, SaleDate=DateTime.Parse("2021-09-01"), Quantity=20}
            //};

            //foreach(CropSale c in contracts)
            //{
            //    context.CropSales.Add(c);
            //}

            //context.SaveChanges();

            var crops = new Crop[]
            {
                new Crop{ CropName = "Beetroot", ExpectedYield = 50, Price = 60},
                new Crop{ CropName = "Colza", ExpectedYield = 5, Price = 3000},
            };
            foreach (Crop c in crops)
            {
                context.Crops.Add(c);
            }

            context.SaveChanges();
        }
    }
}
