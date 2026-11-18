using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipBuilding.Entities;

namespace ShipBuilding.Data.Interfaces
{
    public interface IDataSet : IReadOnlyDataSet
    {
        new ICollection<Ship> Ships { get; }
        new ICollection<Shipyard> Shipyards { get; }
        new ICollection<TypeOfShip> Types { get; }
        new ICollection<ShipProject> ShipProjects { get; }

        bool IsEmpty();

        void CopyFrom(IReadOnlyDataSet other);
    }
}
