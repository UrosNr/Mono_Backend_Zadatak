using Microsoft.EntityFrameworkCore;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Database
{
    public class VehicleDbContext : DbContext, IVehicleDbContext
    {
        public VehicleDbContext(DbContextOptions<VehicleDbContext> options) : base(options) { }

        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
    }
}
