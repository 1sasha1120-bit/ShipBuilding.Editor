using Common.Interfaces;
using ShipBuilding.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBuilding.Processing.Models
{
    public class ShipyardModel : IIdentifiable, IKeyable
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CountryName { get; set; }
        public int? YearOfFounded { get; set; }
        public int? NumberOfDocks { get; set; }
        public double? CapacityPerYear { get; set; }

        public string? Key { get; private set; }

        public static explicit operator ShipyardModel(Shipyard obj)
        {
            if (obj == null) return null!;
            return new ShipyardModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                CountryName = obj.CountryName,
                YearOfFounded = obj.YearOfFounded,
                NumberOfDocks = obj.NumberOfDocks,
                CapacityPerYear = obj.CapacityPerYear,
                Key = obj.Key,
            };
        }

        public ShipyardModel Clone()
        {
            return (ShipyardModel)this.MemberwiseClone();
        }

        public Shipyard ToShipyard()
        {
            return new Shipyard(Name, CountryName, YearOfFounded, NumberOfDocks, CapacityPerYear)
            {
                Id = this.Id,
                Name = this.Name,
                CountryName = this.CountryName,
                YearOfFounded = this.YearOfFounded,
                NumberOfDocks = this.NumberOfDocks,
                CapacityPerYear = this.CapacityPerYear
            };
        }
    }
}
