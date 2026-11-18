using Common.Interfaces;
using ShipBuilding.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBuilding.Processing.Models
{
    public class TypeOfShipModel : IIdentifiable, IKeyable
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ParentKey { get; set; }
        public string? Description { get; set; }
        public string? TypeCode { get; set; }

        public string? Key { get; private set; }

        public static explicit operator TypeOfShipModel(TypeOfShip obj)
        {
            if (obj == null) return null!;
            return new TypeOfShipModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                ParentKey = obj.Parent?.Key,
                TypeCode = obj.TypeCode,
                Description = obj.Description,
                Key = obj.Key,
            };
        }

        public TypeOfShipModel Clone()
        {
            return (TypeOfShipModel)this.MemberwiseClone();
        }

        public TypeOfShip ToTypeOfShip(IEnumerable<TypeOfShip> types)
        {
            TypeOfShip? parent = null;
            string? parentKey = ParentKey?.Trim();
            if (!string.IsNullOrEmpty(parentKey))
            {
                parent = types.First(e => e.Name == parentKey);
            }
            return new TypeOfShip(this.Name, parent)
            {
                Id = this.Id,
                Description = this.Description,
                TypeCode = this.TypeCode
            };
        }
    }
}
