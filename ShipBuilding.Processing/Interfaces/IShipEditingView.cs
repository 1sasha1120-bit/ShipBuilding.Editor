using Common.Processing.Interfaces;
using ShipBuilding.Processing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBuilding.Processing.Interfaces
{
    public interface IShipEditingView : IEditingView<ShipModel>
    {
        void UpdateShipyardSelectionList(string[] items);
        void UpdateTypeOfShipSelectionList(string[] items);
    }
}
