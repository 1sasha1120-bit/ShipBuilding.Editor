using Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ShipBuilding.Entities
{
    [Serializable]
    public class ShipProject : Entity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ProjectCode { get; set; }
        public decimal? Budget { get; set; }

        //public List<Ship>? Ships { get; set; }
        public Shipyard? Shipyard { get; set; }

        [XmlIgnore]
        public override string Key => Name ?? "Unknown";

        public override string ToString() => "";
                        //$"\tProject: {Name}\n" +
                        //$"\tDescription: {Description}\n" +
                        //$"\tShips:\n{string.Join("\n", Ships!)}";

        public ShipProject(string? name, string? projectCode, decimal? budget, Shipyard? shipyard)
        {
            Name = name;
            ProjectCode = projectCode;
            Budget = budget;
            Shipyard = shipyard;
        }

        public ShipProject(string? name) : this(name, null, null, null) { }

        public ShipProject() : this(null, null, null, null) { }

    }
}
