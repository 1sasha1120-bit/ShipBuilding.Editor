using ShipBuilding.Repositories.Interfaces;
using System.Linq;

namespace WorldDividing.Repositories.Extensions
{
    public static class UnitOfWorkMethods
    {
        public static bool IsEmpty(this IUnitOfWork unitOfWork)
        {
            return !unitOfWork.TypeOfShipsRepository.GetAll().Any()
                && !unitOfWork.ShipyardsRepository.GetAll().Any()
                && !unitOfWork.ShipsRepository.GetAll().Any()
                && !unitOfWork.ShipProjectsRepository.GetAll().Any();
        }
    }
}
