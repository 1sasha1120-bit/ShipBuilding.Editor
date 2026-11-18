using Common.Processing.Editing;
using Common.Processing.Interfaces;
using ShipBuilding.Entities;
using ShipBuilding.Processing.Interfaces;
using ShipBuilding.Processing.Models;
using ShipBuilding.Repositories.Interfaces;
using System;
using System.Linq;

namespace ShipBuilding.Processing.Editing
{
    public class ShipsEditingController : BaseEditingController<ShipModel>
    {
        readonly IUnitOfWork unitOfWork;
        readonly IShipEditingView editingView;

        public ShipsEditingController(IUnitOfWork unitOfWork, ISelectionView selectionView,
            IShipEditingView editingView) : 
            base(new ShipsEditor(unitOfWork), selectionView, editingView)
        {
            this.unitOfWork = unitOfWork 
                ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.editingView = editingView;
        }

        public override void UpdateViewsData()
        {
            //throw new NotImplementedException();
            string[] keys = unitOfWork.ShipsRepository.GetAll()
                .Select(e => e.Key).OrderBy(e => e).ToArray();
            SelectionView.SetKeys(keys);

            string[] typesKeys = unitOfWork.TypeOfShipsRepository.GetAll()
                .Select(e => e.Key).OrderBy(e => e).ToArray();
            editingView.UpdateTypeOfShipSelectionList(typesKeys);

            string[] shipyardsKeys = unitOfWork.ShipyardsRepository.GetAll()
                .Select(e => e.Key).OrderBy(e => e).ToArray();
            editingView.UpdateShipyardSelectionList(shipyardsKeys);
        }

        protected override void KeyToModel(string? key, out ShipModel model)
        {
            //throw new NotImplementedException();
            Ship? obj = unitOfWork.ShipsRepository.GetAll()
                .FirstOrDefault(el => el.Key == key);
            model = (ShipModel)obj!;
        }
    }
}
