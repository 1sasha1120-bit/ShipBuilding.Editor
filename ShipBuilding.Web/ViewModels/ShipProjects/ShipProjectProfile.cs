using AutoMapper;
using ShipBuilding.Services.ShipProjects;

namespace ShipBuilding.Web.ViewModels.ShipProjects
{
    public class ShipProjectProfile : Profile
    {
        public ShipProjectProfile()
        {
            // Browsing використовується і для списку, і для сторінки видалення
            CreateMap<ShipProjectDto, ShipProjectBrowsingViewModel>();
            CreateMap<ShipProjectDto, ShipProjectEditingViewModel>().ReverseMap();

            // Критерії пошуку; сортування контролер задає окремо
            CreateMap<ShipProjectsFilter, ShipProjectsQuery>();
        }
    }
}
