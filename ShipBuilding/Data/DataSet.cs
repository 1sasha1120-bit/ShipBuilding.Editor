using ShipBuilding.Data.Interfaces;
using ShipBuilding.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBuilding.Data
{
    [Serializable]
    public class DataSet : IDataSet
    {
        readonly List<Ship> _ships = new();
        readonly List<Shipyard> _shipyards = new();
        readonly List<TypeOfShip> _types = new();
        readonly List<ShipProject> _shipProjects = new();

        public ICollection<Ship> Ships => _ships;
        public ICollection<Shipyard> Shipyards => _shipyards;
        public ICollection<TypeOfShip> Types => _types;
        public ICollection<ShipProject> ShipProjects => _shipProjects;

        IEnumerable<Ship> IReadOnlyDataSet.Ships => Ships;
        IEnumerable<Shipyard> IReadOnlyDataSet.Shipyards => Shipyards;
        IEnumerable<TypeOfShip> IReadOnlyDataSet.Types => Types;
        IEnumerable<ShipProject> IReadOnlyDataSet.ShipProjects => ShipProjects;

        public DataSet(IEnumerable<Ship> ships, IEnumerable<Shipyard> shipyards,
            IEnumerable<TypeOfShip> types, IEnumerable<ShipProject> shipProjects)
        {
            _ships.AddRange(ships);
            _shipyards.AddRange(shipyards);
            _types.AddRange(types);
            _shipProjects.AddRange(shipProjects);
        }

        public DataSet(IDataSet dataSet)
            : this(dataSet.Ships, dataSet.Shipyards, dataSet.Types, dataSet.ShipProjects) { }

        public DataSet() { }

        public virtual bool IsEmpty()
        {
            return Ships.Count == 0 && Shipyards.Count == 0
                && Types.Count == 0 && ShipProjects.Count == 0;
        }

        public virtual void CopyFrom(IReadOnlyDataSet other)
        {
            foreach (var obj in other.Ships) Ships.Add(obj);
            foreach (var obj in other.Shipyards) Shipyards.Add(obj);
            foreach (var obj in other.Types) Types.Add(obj);
            foreach (var obj in other.ShipProjects) ShipProjects.Add(obj);
        }
    }
}
