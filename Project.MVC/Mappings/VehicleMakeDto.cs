using Project.Service.Models;
using Project.Service.ViewModels;

namespace Project.MVC.Mappings
{
    public class VehicleMakeDto : VehicleMakeVm, IMapFrom<VehicleMake>
    {
        public static void Mapping(MappingProfile profile)
        {
            profile.CreateMap<VehicleMake, VehicleMakeVm>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Abrv, opt => opt.MapFrom(s => s.Abrv));
        }
    }
}
