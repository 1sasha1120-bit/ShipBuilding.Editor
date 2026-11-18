using Common.Processing.Editing;
using ShipBuilding.Processing.Analysis;
using ShipBuilding.Processing.Models;
using ShipBuilding.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBuilding.Processing.Editing
{
    public class ShipyardsEditor : BaseEditor<ShipyardModel>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IShipyardsRepository repository;

        public ShipyardsEditor(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));
            repository = unitOfWork.ShipyardsRepository;
        }

        protected override bool CheckCreation(ShipyardModel model, out string? message)
        {
            return model.CheckCreation(unitOfWork, out message);
        }

        protected override bool CheckDeleting(ShipyardModel model, out string? message)
        {
            return model.CheckDeleting(unitOfWork, out message);
        }

        protected override bool CheckUpdating(ShipyardModel model, out string? message)
        {
            return model.CheckUpdating(unitOfWork, out message);
        }

        protected override void DoCreate(ShipyardModel model)
        {
            repository.Add(model.ToShipyard());
        }

        protected override void DoDelete(ShipyardModel model)
        {
            repository.Delete(model.Id);
        }

        protected override void DoUpdate(ShipyardModel model)
        {
            repository.Add(model.ToShipyard());
        }
    }
}
