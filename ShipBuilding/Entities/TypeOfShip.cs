using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Common.Entities;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Entities
{
    [Serializable]
    public class TypeOfShip : Entity
    {

        public TypeOfShip(string? name, TypeOfShip? parent, string? typeCode)
        {
            Name = name;
            Parent = parent;
            TypeCode = typeCode;
        }

        public TypeOfShip(string? name, TypeOfShip? parent) : this(name, parent, null) { }

        public TypeOfShip() : this(null, null, null) { }


        public string? Name { get; set; }
        public TypeOfShip? Parent { get; set; }
        public string? TypeCode { get; set; }

        public string? Description { get; set; }

        public override string Key => Name ?? "Unknown";

        public override string ToString() =>
            $"\t{TypeOfShipCaption} {IdCaption}{Id}\n" +
            $"\t{NameCaption}: {Name}\n" +
            $"\t{ParentCaption}: {Parent?.Key}\n" +
            $"\t{TypeCodeCaption}: {TypeCode}\n" +
            $"\t{DescriptionCaption}: {Description}\n";
    }
}
