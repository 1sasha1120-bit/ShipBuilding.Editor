using AutoMapper;
using ShipBuilding.Services.TypeOfShips;

namespace ShipBuilding.Web.ViewModels.TypeOfShips
{
    public class TypeOfShipProfile : Profile
    {
        public TypeOfShipProfile()
        {
            // Вузол дерева або рядок плаского списку; підтипи (Children) мапляться рекурсивно
            CreateMap<TypeOfShipDto, TypeOfShipBrowsingViewModel>();
            CreateMap<TypeOfShipDto, TypeOfShipDeletingViewModel>();
            CreateMap<TypeOfShipDto, TypeOfShipEditingViewModel>().ReverseMap();

            CreateMap<TypeOfShipsFilter, TypeOfShipsQuery>();
        }
    }
}
