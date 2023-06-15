using Project.Service.Models;
using Project.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Service
{
    public interface IVehicleService
    {
        Task<List<VehicleMake>> GetAllMakesAsync(CancellationToken cancellationToken);
        Task<List<VehicleModel>> GetAllModelsAsync(CancellationToken cancellationToken);
        Task<VehicleMake> GetMakeByIdAsync(int id, CancellationToken cancellationToken);
        Task<VehicleModel> GetModelByIdAsync(int id, CancellationToken cancellationToken);
        Task AddMakeAsync(VehicleMake make, CancellationToken cancellationToken);
        Task AddModelAsync(VehicleModel model, CancellationToken cancellationToken);
        Task UpdateMakeAsync(VehicleMake make, CancellationToken cancellationToken);
        Task UpdateModelAsync(VehicleModel model, CancellationToken cancellationToken);
        Task DeleteMakeAsync(int id, CancellationToken cancellationToken);
        Task DeleteModelAsync(int id, CancellationToken cancellationToken);
        Task<PaginatedList<VehicleMake>> GetSortedAndFilteredMakesAsync(string sortOrder, string searchString, int? page, CancellationToken cancellationToken);
        Task<PaginatedList<VehicleModel>> GetSortedAndFilteredModelsAsync(string sortOrder, string searchString, int? page, CancellationToken cancellationToken);
    }
}
