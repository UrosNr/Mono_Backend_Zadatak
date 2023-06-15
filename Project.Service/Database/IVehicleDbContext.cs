using Microsoft.EntityFrameworkCore;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Database
{
    public interface IVehicleDbContext
    {
        DbSet<VehicleMake> VehicleMakes { get; set; }
        DbSet<VehicleModel> VehicleModels { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
