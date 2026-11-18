using Common.Processing.Editing;
using Common.Repositories;
using Common.Repositories.Interfaces;
using ShipBuilding.Processing.Analysis;
using ShipBuilding.Processing.Models;
using ShipBuilding.Repositories.Interfaces;
using System;

namespace ShipBuilding.Processing.Editing
{
    public class ShipsEditor : BaseEditor<ShipModel>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IShipsRepository repository;

        public ShipsEditor(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork 
                ?? throw new ArgumentNullException(nameof(unitOfWork));
            repository = unitOfWork.ShipsRepository;
        }

        protected override bool CheckCreation(ShipModel model, out string message)
        {
            return model.CheckCreation(unitOfWork, out message!);
        }

        protected override bool CheckDeleting(ShipModel model, out string message)
        {
            message = string.Empty;
            return true;
            //throw new NotImplementedException();
        }

        protected override bool CheckUpdating(ShipModel model, out string message)
        {
            return model.CheckUpdating(unitOfWork, out message!);
        }

        protected override void DoCreate(ShipModel model)
        {
            //throw new NotImplementedException();
            repository.Add(model.ToShip(unitOfWork.ShipyardsRepository.GetAll(), unitOfWork.TypeOfShipsRepository.GetAll(),
                unitOfWork.ShipProjectsRepository.GetAll()));
        }

        protected override void DoDelete(ShipModel model)
        {
            //throw new NotImplementedException();
            repository.Delete(model.Id);
        }

        protected override void DoUpdate(ShipModel model)
        {
            //throw new NotImplementedException();
            repository.Update(model.ToShip(unitOfWork.ShipyardsRepository.GetAll(), unitOfWork.TypeOfShipsRepository.GetAll(),
                unitOfWork.ShipProjectsRepository.GetAll()));
        }
    }
}
