using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Service.Models;
using Project.Service.Service;
using Project.Service.ViewModels;
using System.Linq;

namespace Project.MVC.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;
        private readonly ILogger<VehicleMakeController> _logger;


        public VehicleMakeController(IVehicleService vehicleService, IMapper mapper, ILogger<VehicleMakeController> logger)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int? makeId, CancellationToken cancellationToken)
        {
            var vehicleMakesList = await _vehicleService.GetAllMakesAsync(cancellationToken);
            var VmList = _mapper.Map<List<VehicleMakeVm>>(vehicleMakesList);
            return View("VehicleMake", VmList);
        }
        [HttpGet]
        public async Task<IActionResult> GetVehicleMakesQuery(string? searchFilter, string? sortFilter, int? page, CancellationToken cancellationToken)
        {
            var vehicleMakesList = await _vehicleService.GetSortedAndFilteredMakesAsync(sortFilter, searchFilter, page, cancellationToken);
            var VmList = _mapper.Map<List<VehicleMakeVm>>(vehicleMakesList.Items);
            var paginatedList = new PaginatedList<VehicleMakeVm>
            {
                Items = VmList,
                PageIndex = vehicleMakesList.PageIndex,
                TotalPages = vehicleMakesList.TotalPages,
                TotalCount = vehicleMakesList.TotalCount
            };
            return View("VehicleMake", paginatedList);
        }
        public ActionResult Create()
        {
            return View("CreateVehicleMake", new VehicleMakeVm());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VehicleMakeVm vm, CancellationToken cancellationToken)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newEntity = new VehicleMake
                    {
                        Name = vm.Name,
                        Abrv = vm.Abrv,
                    };
                    await _vehicleService.AddMakeAsync(newEntity, cancellationToken);
                    return RedirectToAction("GetVehicleMakesQuery");
                }
                else
                {
                    return View("CreateVehicleMake", vm);
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
            var vehicleMake = await _vehicleService.GetMakeByIdAsync(id, cancellationToken);
            var vehicleMakeVm = _mapper.Map<VehicleMakeVm>(vehicleMake);
            return View("EditVehicleMake", vehicleMakeVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(VehicleMakeVm vm, CancellationToken cancellationToken)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = await _vehicleService.GetMakeByIdAsync(vm.Id, cancellationToken);
                    if (entity == null)
                    {
                        return BadRequest();
                    }
                    entity.Name = vm.Name;
                    entity.Abrv = vm.Abrv;
                    await _vehicleService.UpdateMakeAsync(entity, cancellationToken);
                    return RedirectToAction("GetVehicleMakesQuery");
                }
                else
                {
                    return View("EditVehicleMake", vm);
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
                var entity = await _vehicleService.GetMakeByIdAsync(id, cancellationToken);
                if (entity == null)
                {
                    return BadRequest();
                }
                await _vehicleService.DeleteMakeAsync(id, cancellationToken);
                return RedirectToAction("GetVehicleMakesQuery");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return View("Error");
            }
        }
    }
}
