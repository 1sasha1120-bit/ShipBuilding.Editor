using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipBuilding.Data.Interfaces;
using ShipBuilding.Entities;

namespace ShipBuilding.Data
{
    [Serializable]
    public class ReadOnlyDataSet : IReadOnlyDataSet
    {
        public required IEnumerable<Ship> Ships { get; set; }

        public required IEnumerable<Shipyard> Shipyards { get; set; }

        public required IEnumerable<TypeOfShip> Types { get; set; }

        public required IEnumerable<ShipProject> ShipProjects { get; set; }
    }
}
