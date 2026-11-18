using Common.Processing.Editing;
using Common.Processing.Interfaces;
using ShipBuilding.Entities;
using ShipBuilding.Processing.Interfaces;
using ShipBuilding.Processing.Models;
using ShipBuilding.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShipBuilding.Processing.Editing
{
    public class ShipyardsEditingController : BaseEditingController<ShipyardModel>
    {
        readonly IUnitOfWork unitOfWork;
        readonly IShipyardEditingView editingView;

        public ShipyardsEditingController(IUnitOfWork unitOfWork, ISelectionView selectionView,
            IShipyardEditingView editingView) 
            : base(new ShipyardsEditor(unitOfWork), selectionView, editingView)
        {
            this.unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.editingView = editingView;
        }

        public IShipEditingView? ShipEditingView { get; set; }

        public override void UpdateViewsData()
        {
            string[] keys = unitOfWork.ShipyardsRepository.GetAll()
                .Select(e => e.Key).OrderBy(e => e).ToArray();
            SelectionView.SetKeys(keys);
            ShipEditingView?.UpdateShipyardSelectionList(keys);

        }

        protected override void KeyToModel(string? key, out ShipyardModel model)
        {
            Shipyard? obj = unitOfWork.ShipyardsRepository.GetAll()
                .FirstOrDefault(el  => el.Key == key);
            model = (ShipyardModel)obj!;
        }
    }
}
