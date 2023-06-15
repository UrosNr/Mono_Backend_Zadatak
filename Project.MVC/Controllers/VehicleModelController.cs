using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Service.Models;
using Project.Service.Service;
using Project.Service.ViewModels;

namespace Project.MVC.Controllers
{
    public class VehicleModelController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;
        private readonly ILogger<VehicleModelController> _logger;

        public VehicleModelController(IVehicleService vehicleService, IMapper mapper, ILogger<VehicleModelController> logger)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int? page, CancellationToken cancellationToken)
        {
            var vehicleModelList = await _vehicleService.GetAllModelsAsync(cancellationToken);
            var VmList = _mapper.Map<List<VehicleModelVm>>(vehicleModelList);
            return View("VehicleModel", VmList);
        }
        [HttpGet]
        public async Task<IActionResult> GetVehicleModelsQuery(string? searchFilter, string? sortFilter, int? page, CancellationToken cancellationToken)
        {
            var vehicleModelList = await _vehicleService.GetSortedAndFilteredModelsAsync(sortFilter, searchFilter, page, cancellationToken);
            var VmList = _mapper.Map<List<VehicleModelVm>>(vehicleModelList.Items);
            var paginatedList = new PaginatedList<VehicleModelVm>
            {
                Items = VmList,
                PageIndex = vehicleModelList.PageIndex,
                TotalPages = vehicleModelList.TotalPages,
                TotalCount = vehicleModelList.TotalCount
            };
            return View("VehicleModel", paginatedList);
        }

        public async Task<ActionResult> Create(CancellationToken cancellationToken)
        {
            ViewData["VehicleMakeList"] = _mapper.Map<List<VehicleMakeVm>>(await _vehicleService.GetAllMakesAsync(cancellationToken));
            return View("CreateVehicleModel", new VehicleModelVm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VehicleModelVm vm, CancellationToken cancellationToken)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var newEntity = new VehicleModel
                    {
                        Name = vm.Name,
                        Abrv = vm.Abrv,
                        MakeId = vm.MakeId,
                    };
                    await _vehicleService.AddModelAsync(newEntity, cancellationToken);
                    return RedirectToAction("GetVehicleModelsQuery");
                }
                else
                {
                    ViewData["VehicleMakeList"] = _mapper.Map<List<VehicleMakeVm>>(await _vehicleService.GetAllMakesAsync(cancellationToken));
                    return View("CreateVehicleModel", vm);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return View("Error");
            }
        }
        public async Task<ActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var vehicleModel = await _vehicleService.GetModelByIdAsync(id, cancellationToken);
            var vehicleModelVm = _mapper.Map<VehicleModelVm>(vehicleModel);
            ViewData["VehicleMakeList"] = _mapper.Map<List<VehicleMakeVm>>(await _vehicleService.GetAllMakesAsync(cancellationToken));
            return View("EditVehicleModel", vehicleModelVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(VehicleModelVm vm, CancellationToken cancellationToken)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = await _vehicleService.GetModelByIdAsync(vm.Id, cancellationToken);
                    if (entity == null)
                    {
                        return BadRequest();
                    }
                    entity.Name = vm.Name;
                    entity.Abrv = vm.Abrv;
                    entity.MakeId = vm.MakeId;
                    await _vehicleService.UpdateModelAsync(entity, cancellationToken);
                    return RedirectToAction("GetVehicleModelsQuery");
                }
                else
                {
                    ViewData["VehicleMakeList"] = _mapper.Map<List<VehicleMakeVm>>(await _vehicleService.GetAllMakesAsync(cancellationToken));
                    return View("EditVehicleModel", vm);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return View("Error");
            }
        }

        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _vehicleService.GetModelByIdAsync(id, cancellationToken);
                if (entity == null)
                {
                    return BadRequest();
                }
                await _vehicleService.DeleteModelAsync(id, cancellationToken);
                return RedirectToAction("GetVehicleModelsQuery");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return View("Error");
            }
        }
    }
}
