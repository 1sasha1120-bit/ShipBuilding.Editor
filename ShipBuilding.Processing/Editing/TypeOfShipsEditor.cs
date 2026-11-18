using Common.Processing.Editing;
using ShipBuilding.Entities;
using ShipBuilding.Processing.Models;
using ShipBuilding.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipBuilding.Processing.Analysis;

namespace ShipBuilding.Processing.Editing
{
    public class TypeOfShipsEditor : BaseEditor<TypeOfShipModel>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITypeOfShipsRepository repository;

        public TypeOfShipsEditor(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));
            repository = unitOfWork.TypeOfShipsRepository;
        }

        protected override bool CheckCreation(TypeOfShipModel model, out string? message)
        {
            return model.CheckCreation(unitOfWork, out message);
        }

        protected override bool CheckUpdating(TypeOfShipModel model, out string? message)
        {
            return model.CheckUpdating(unitOfWork, out message);
        }

        protected override bool CheckDeleting(TypeOfShipModel model, out string? message)
        {
            return model.CheckDeleting(unitOfWork, out message);
        }

        

        protected override void DoCreate(TypeOfShipModel model)
        {
            repository.Add(model.ToTypeOfShip(repository.GetAll()));
        }

        protected override void DoDelete(TypeOfShipModel model)
        {
            repository.Delete(model.Id);
        }

        protected override void DoUpdate(TypeOfShipModel model)
        {
            repository.Add(model.ToTypeOfShip(repository.GetAll()));
        }
    }
}
