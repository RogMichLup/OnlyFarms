using Microsoft.EntityFrameworkCore;
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

            var cropsales = new CropSale[]
            {
                new CropSale{ Crop = crops[0], Quantity = 200, SaleDate = DateTime.Parse("2021-09-01")},
                new CropSale{ Crop = crops[1], Quantity = 20, SaleDate = DateTime.Parse("2021-07-01")},
            };
            foreach (CropSale c in cropsales)
            {
                context.CropSales.Add(c);
            }

            context.SaveChanges();

            var fields = new Field[]
            {
                new Field{ FieldSurface = 15, Location = "ExampleVillage", Street = "Nowy Świat", Tag = "Behind the barn."}
            };
            foreach (Field c in fields)
            {
                context.Fields.Add(c);
            }

            context.SaveChanges();

            var cultivations = new Cultivation[]
            {
                new Cultivation{ AreaInHectar = 15, Crop = crops[1], CropStatus="Planted", Field = fields[0] }
            };
            foreach (Cultivation c in cultivations)
            {
                context.Cultivations.Add(c);
            }

            context.SaveChanges();

            var equipments = new Equipment[]
            {
                new Equipment{ AmortizationCost = 500, Name="Case", Status="Fine"}
            };

            foreach (Equipment e in equipments)
            {
                context.Equipments.Add(e);
            }

            context.SaveChanges();


            //var machines = new Machine[]
            //{
            //    new Equipment{ AmortizationCost = 500, Name="Case", Status="Fine"}
            //};

            //foreach (Equipment e in equipments)
            //{
            //    context.Equipments.Add(e);
            //}

            //context.SaveChanges();
        }
    }
}
