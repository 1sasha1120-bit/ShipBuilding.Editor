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
    public class TypeOfShipsEditingController : BaseEditingController<TypeOfShipModel>
    {
        readonly IUnitOfWork unitOfWork;
        readonly ITypeOfShipEditingView editingView;

        public TypeOfShipsEditingController(IUnitOfWork unitOfWork, ISelectionView selectionView,
            ITypeOfShipEditingView editingView)
            : base(new TypeOfShipsEditor(unitOfWork), selectionView, editingView)
        {
            this.unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.editingView = editingView;
        }

        public IShipEditingView? ShipEditingView { get; set; }

        public override void UpdateViewsData()
        {
            string[] keys = unitOfWork.TypeOfShipsRepository.GetAll()
                .Select(e => e.Key).OrderBy(e => e).ToArray();
            SelectionView.SetKeys(keys);
            editingView.UpdateParentSelectionList(keys);
            ShipEditingView?.UpdateTypeOfShipSelectionList(keys);
        }

        protected override void KeyToModel(string? key, out TypeOfShipModel model)
        {
            TypeOfShip? obj = unitOfWork.TypeOfShipsRepository.GetAll()
                .FirstOrDefault(el  => el.Key == key);
            model = (TypeOfShipModel)obj!;
        }
    }
}
