using Project.Service.Models;
using Project.Service.ViewModels;

namespace Project.MVC.Mappings
{
    public class VehicleModelDto : VehicleModelVm, IMapFrom<VehicleModel>
    {
        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<VehicleModel, VehicleModelVm>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.MakeId, opt => opt.MapFrom(s => s.MakeId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Abrv, opt => opt.MapFrom(s => s.Abrv));
        }
    }
}
