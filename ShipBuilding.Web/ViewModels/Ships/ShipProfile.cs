using AutoMapper;
using ShipBuilding.Services.Ships;

namespace ShipBuilding.Web.ViewModels.Ships
{
    public class ShipProfile : Profile
    {
        public ShipProfile()
        {
            CreateMap<ShipDto, ShipBrowsingViewModel>();
            CreateMap<ShipDto, ShipDeletingViewModel>();
            CreateMap<ShipDto, ShipEditingViewModel>().ReverseMap();

            // Критерії пошуку; сортування контролер задає окремо
            CreateMap<ShipsFilter, ShipsQuery>();
        }
    }
}
