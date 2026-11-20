using AutoMapper;
using ShipBuilding.Services.Shipyards;

namespace ShipBuilding.Web.ViewModels.Shipyards
{
    /// <summary>Перетворення між даними сервісу верфей і view-моделями (поля зіставляються за назвами).</summary>
    public class ShipyardProfile : Profile
    {
        public ShipyardProfile()
        {
            CreateMap<ShipyardDto, ShipyardBrowsingViewModel>();
            CreateMap<ShipyardDto, ShipyardDeletingViewModel>();
            CreateMap<ShipyardDto, ShipyardEditingViewModel>().ReverseMap();

            // Критерії пошуку; сортування контролер задає окремо
            CreateMap<ShipyardsFilter, ShipyardsQuery>();
        }
    }
}
