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
                new Crop{ CropName = "Beetroot", ExpectedYield = 50, SellPricePerTonne = 60},
                new Crop{ CropName = "Colza", ExpectedYield = 5, SellPricePerTonne = 3000},
            };
            foreach (Crop c in crops)
            {
                context.Crops.Add(c);
            }

            var contracts = new Contract[]
            {
                new Contract{ DeliveryDate = DateTime.Parse("2021-11-01"), ClientName ="Mlekovita"}
            };
            foreach (Contract c in contracts)
            {
                context.Contracts.Add(c);
            }

            context.SaveChanges();

            List<Crop> cropsList = new();
            cropsList.Add(crops[0]);
            cropsList.Add(crops[1]);

            var contractcrop = new ContractCrop[]
            {
                new ContractCrop{ Quantity = 10, Crop = crops[0], Contract = contracts[0]},
                new ContractCrop{ Quantity = 20, Crop = crops[1], Contract = contracts[0]},

            };
            foreach (ContractCrop c in contractcrop)
            {
                context.ContractCrops.Add(c);
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
                new Field{ FieldSurface = 15, City = "ExampleVillage", Street = "Nowy Świat", Tag = "Behind the barn."}
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
                new Equipment{ UtilizationCost = 100, Name="Amazone", Status="Fine"},
                new Equipment{ UtilizationCost = 0, Name="Brak", Status="Fine"}

            };

            foreach (Equipment e in equipments)
            {
                context.Equipments.Add(e);
            }

            context.SaveChanges();
            
            var machines = new Machine[]
            {
                new Machine{ UtilizationCost = 500, Name="Case", Status="Fine", FuelUsageRate = 23.5},
                new Machine{ UtilizationCost = 0, Name="Brak", Status="Fine", FuelUsageRate = 0}

            };

            foreach (Machine m in machines)
            {
                context.Machines.Add(m);
            }

            context.SaveChanges();

            var suppies = new Supply[]
           {
                new Supply{ Name = "Phosphorus", PricePerKilo = 4, SupplyAmountPerHectare = 10},
                new Supply{ Name = "Nitrogen", PricePerKilo = 6, SupplyAmountPerHectare = 20},
           };

            List<Supply> suppliesList = new();
            suppliesList.Add(suppies[0]);
            suppliesList.Add(suppies[1]);
            
            foreach (Supply s in suppies)
            {
                context.Supplies.Add(s);
            }

            context.SaveChanges(); 
            
            var workers = new Worker[]
           {
                new Worker{ FirstName ="Michał", Surname="Rogiński", HourlyPay=27.5, HiringDate = DateTime.Parse("2021-10-01")},
                new Worker{ FirstName = "Piotr", Surname="Łupiński", HourlyPay=420, HiringDate = DateTime.Parse("2021-10-01")},
                new Worker{ FirstName = "Karol", Surname="Michalski", HourlyPay=32.5, HiringDate = DateTime.Parse("2021-10-01")},
           };

            foreach (Worker w in workers)
            {
                context.Workers.Add(w);
            }

            context.SaveChanges();


            var procedures = new Procedure[]
            {
                new Procedure{ Label="Fertilization", StartDate = DateTime.Parse("2021-03-01"), 
                Equipment = equipments[0], Machine = machines[0], Field = fields[0], Status="Done", 
                DurationInHours = 5, Worker = workers[0], Supplies = suppliesList},
                
                new Procedure{ Label="Fertilization", StartDate = DateTime.Parse("2021-03-15"),
                Equipment = equipments[0], Machine = machines[0], Field = fields[0], Status="Done",
                DurationInHours = 4, Worker = workers[1], Supplies = suppliesList}
            };

            foreach (Procedure p in procedures)
            {
                context.Procedures.Add(p);
            }

            context.SaveChanges();
            
            var weathers = new Weather[]
            {
                new Weather{ Temperature = 19.5, Moisture = 50, AirPressure = 1005, 
                RainfallAmount = 2, Field = fields[0], WindDirection = "N", WindSpeed = 40, 
                Date =  DateTime.Parse("2021-10-02")}
            };

            foreach (Weather w in weathers)
            {
                context.Weathers.Add(w);
            }

            context.SaveChanges();

        }
    }
}
