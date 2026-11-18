using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipBuilding.Entities;

namespace ShipBuilding.Data.Interfaces
{
    public interface IReadOnlyDataSet
    {
        IEnumerable<Ship> Ships { get; }
        IEnumerable<Shipyard> Shipyards { get; }
        IEnumerable<TypeOfShip> Types { get; }
        IEnumerable<ShipProject> ShipProjects { get; }
    }
}
