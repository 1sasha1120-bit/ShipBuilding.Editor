using Common.Repositories;
using ShipBuilding.Entities;
using ShipBuilding.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShipBuilding.Repositories
{
    internal class ShipProjectsRepository : AutoincrementRepository<ShipProject>, IShipProjectsRepository
    {
        public ShipProjectsRepository(ICollection<ShipProject> collection) : base(collection)
        {
        }
    }
}
