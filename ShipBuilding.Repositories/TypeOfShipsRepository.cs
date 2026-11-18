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
    internal class TypeOfShipsRepository : AutoincrementRepository<TypeOfShip>, ITypeOfShipsRepository
    {
        public TypeOfShipsRepository(ICollection<TypeOfShip> collection) : base(collection)
        {
        }
        public override void Update(TypeOfShip item)
        {
            TypeOfShip obj = GetById(item.Id);
            if (obj != null)
            {
                obj.Name = item.Name;
                obj.Parent = item.Parent;
                obj.Description = item.Description;
                obj.TypeCode = item.TypeCode;
            }
        }
    }
}

