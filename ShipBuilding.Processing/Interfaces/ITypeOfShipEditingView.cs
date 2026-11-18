using Common.Processing.Interfaces;
using ShipBuilding.Entities;
using ShipBuilding.Processing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBuilding.Processing.Interfaces
{
    public interface ITypeOfShipEditingView : IEditingView<TypeOfShipModel>
    {
        void UpdateParentSelectionList(string[] items);

    }
}
