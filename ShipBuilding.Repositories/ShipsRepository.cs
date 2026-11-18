using Common.Repositories;
using ShipBuilding.Entities;
using ShipBuilding.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBuilding.Repositories
{
    internal class ShipsRepository : AutoincrementRepository<Ship>, IShipsRepository
    {
        public ShipsRepository(ICollection<Ship> collection) : base(collection)
        {
        }
    }
}
