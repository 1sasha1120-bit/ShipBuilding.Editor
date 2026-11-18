using System;
using Common.Entities;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Entities
{
    [Serializable]
    public class Shipyard : Entity
    {
        public Shipyard() : this(null, null, null, null, null) { }
        
        public Shipyard(string name) : this(name ,null, null, null, null) { }

        public Shipyard(string? name, string? countryName, int? yearOfFounded, 
            int? numberOfDocks, double? capacityPerYear)
        {
            Name = name;
            CountryName = countryName;
            YearOfFounded = yearOfFounded;
            NumberOfDocks = numberOfDocks;
            CapacityPerYear = capacityPerYear;
            
        }

        public string? Name { get; set; }
        public string? CountryName { get; set; }
        public int? YearOfFounded { get; set; }
        public int? NumberOfDocks { get; set; }
        public double? CapacityPerYear { get; set; }
        public override string Key => Name ?? "Unknown";

        public override string ToString() =>
                $"\t{ShipyardCaption} {IdCaption}{Id} \n" +
                $"\t{NameCaption}: {Name}\n" +
                $"\t{CountryNameCaption}: {CountryName}\n" +
                $"\t{YearOfFoundedCaption}: {YearOfFounded}\n" +
                $"\t{NumberOfDocksCaption}: {NumberOfDocks}\n" +
                $"\t{CapacityPerYearCaption}: {CapacityPerYear}\n";
    }
}