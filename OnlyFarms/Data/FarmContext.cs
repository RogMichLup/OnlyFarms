using Microsoft.EntityFrameworkCore;
using OnlyFarms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Data
{
    public class FarmContext : DbContext
    {
        public FarmContext(DbContextOptions<FarmContext> options) : base(options)
        {
        }
        //public DbSet<Contract> Contracts { get; set; }
        public DbSet<Crop> Crops { get; set; }
        //public DbSet<CropSale> CropSales { get; set; }
        //public DbSet<Cultivation> Cultivations { get; set; }
        //public DbSet<Equipment> Equipments { get; set; }
        //public DbSet<Field> Fields { get; set; }
        //public DbSet<Machine> Machines { get; set; }
        //public DbSet<Procedure> Procedures { get; set; }
        //public DbSet<Supply> Supplies { get; set; }
        //public DbSet<Weather> Weathers { get; set; }
        //public DbSet<Worker> Workers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBulider)
        {
            //modelBulider.Entity<Contract>().ToTable("Contract");
            modelBulider.Entity<Crop>().ToTable("Crop");
            //modelBulider.Entity<CropSale>().ToTable("CropSale");
            //modelBulider.Entity<Cultivation>().ToTable("Cultivation");
            //modelBulider.Entity<Equipment>().ToTable("Equipment");
            //modelBulider.Entity<Field>().ToTable("Field");
            //modelBulider.Entity<Machine>().ToTable("Machine");
            //modelBulider.Entity<Procedure>().ToTable("Procedure");
            //modelBulider.Entity<Supply>().ToTable("Supply");
            //modelBulider.Entity<Weather>().ToTable("Weather");
            //modelBulider.Entity<Worker>().ToTable("Worker");
        }
    }
}
