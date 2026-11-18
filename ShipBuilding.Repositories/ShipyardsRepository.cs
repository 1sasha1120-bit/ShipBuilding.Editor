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
    internal class ShipyardsRepository : AutoincrementRepository<Shipyard>, IShipyardsRepository
    {
        public ShipyardsRepository(ICollection<Shipyard> collection) : base(collection)
        {

        }
    }
}
