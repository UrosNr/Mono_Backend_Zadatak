using Microsoft.EntityFrameworkCore;
using Project.Service.Database;
using Project.Service.Models;
using Project.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Service
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleDbContext _db;

        public VehicleService(IVehicleDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<List<VehicleMake>> GetAllMakesAsync(CancellationToken cancellationToken)
        {
            return await _db.VehicleMakes.IgnoreQueryFilters().ToListAsync(cancellationToken);
        }

        public async Task<List<VehicleModel>> GetAllModelsAsync(CancellationToken cancellationToken)
        {
            return await _db.VehicleModels.IgnoreQueryFilters().Include(m=>m.Make).ToListAsync(cancellationToken);
        }

        public async Task<VehicleMake> GetMakeByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _db.VehicleMakes.FindAsync(id);
        }

        public async Task<VehicleModel> GetModelByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _db.VehicleModels.FindAsync(id);
        }

        public async Task AddMakeAsync(VehicleMake make, CancellationToken cancellationToken)
        {
            _db.VehicleMakes.Add(make);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task AddModelAsync(VehicleModel model, CancellationToken cancellationToken)
        {
            _db.VehicleModels.Add(model);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateMakeAsync(VehicleMake make, CancellationToken cancellationToken)
        {
            _db.VehicleMakes.Update(make);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateModelAsync(VehicleModel model, CancellationToken cancellationToken)
        {
            _db.VehicleModels.Update(model);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteMakeAsync(int id, CancellationToken cancellationToken)
        {
            var make = await _db.VehicleMakes.FindAsync(id);
            if (make != null)
            {
                _db.VehicleMakes.Remove(make);
                await _db.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteModelAsync(int id, CancellationToken cancellationToken)
        {
            var model = await _db.VehicleModels.FindAsync(id);
            if (model != null)
            {
                _db.VehicleModels.Remove(model);
                await _db.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<PaginatedList<VehicleMake>> GetSortedAndFilteredMakesAsync(string sortOrder, string searchString, int? page, CancellationToken cancellationToken)
        {
            var makes = _db.VehicleMakes.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                makes = makes.Where(m => m.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "desc":
                    makes = makes.OrderByDescending(m => m.Name);
                    break;
                default:
                    makes = makes.OrderBy(m => m.Name);
                    break;
            }
            int pageSize = 10;
            if (!page.HasValue)
                page = 1;

            var count = await makes.CountAsync(cancellationToken);
         
            PaginatedList<VehicleMake> paginatedlist = new PaginatedList<VehicleMake>
            {
                Items = await makes.Skip(((int)page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken),
                PageIndex = (int)page,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                TotalCount = count
            };

            return paginatedlist;
        }

        public async Task<PaginatedList<VehicleModel>> GetSortedAndFilteredModelsAsync(string sortOrder, string searchString, int? page, CancellationToken cancellationToken)
        {
            var models = _db.VehicleModels.Include(m => m.Make).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                models = models.Where(m => m.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "desc":
                    models = models.OrderByDescending(m => m.Name);
                    break;
                default:
                    models = models.OrderBy(m => m.Name);
                    break;
            }
            int pageSize = 10;
            if(!page.HasValue)
                page = 1;

            var count = await models.CountAsync(cancellationToken);
         
            PaginatedList<VehicleModel> paginatedlist = new PaginatedList<VehicleModel>
            {
                Items = await models.Skip(((int)page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken),
                PageIndex = (int)page,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                TotalCount = count
            };

            return paginatedlist;
        }
    }
}


