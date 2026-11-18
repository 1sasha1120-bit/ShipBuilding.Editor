using Common.Interfaces;
using ShipBuilding.Entities;
using ShipBuilding.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipBuilding.Processing.Models
{
    public class ShipModel : IKeyable, IIdentifiable
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? RegistrationNumber { get; set; }
        public DateTime? LaunchDate { get; set; }

        public ShipStatus Status { get; set; }
        public double? Height { get; set; }
        public double? Width { get; set; }
        public double? Length { get; set; }
        public string? ShipyardKey { get; set; }
        public string? TypeOfShipKey { get; set; }
        public string? ProjectKey { get; set; }
        public string? Key { get; private set; }

        public static explicit operator ShipModel?(Ship obj)
        {
            if (obj == null) return null;
            return new ShipModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                RegistrationNumber = obj.RegistrationNumber,
                LaunchDate = obj.LaunchDate,
                Status = obj.Status,

                Height = obj.Height,
                Width = obj.Width,
                Length = obj.Length,
                ShipyardKey = obj.Shipyard?.Key,
                TypeOfShipKey = obj.TypeOfShip?.Key,
                ProjectKey = obj.Project?.Key,
                Key = obj.Key,
            };
        }

        public ShipModel Clone()
        {
            return (ShipModel)this.MemberwiseClone();
        }

        public Ship ToShip(IEnumerable<Shipyard> shipyards, IEnumerable<TypeOfShip> typeOfShips, IEnumerable<ShipProject> shipProjects)
        {

            string? shipyardKey = this.ShipyardKey?.Trim();
                 Shipyard? shipyard = shipyards.FirstOrDefault(
                e => string.Equals(e.Key, shipyardKey, StringComparison.OrdinalIgnoreCase));
           

            string? typeOfShipKey = this.TypeOfShipKey?.Trim();
            TypeOfShip? typeOfShip = typeOfShips.FirstOrDefault(
                e => string.Equals(e.Key, typeOfShipKey, StringComparison.OrdinalIgnoreCase));

            string? projectKey = this.ProjectKey?.Trim();
            ShipProject? shipProject = shipProjects.FirstOrDefault(
                e => string.Equals(e.Key, projectKey, StringComparison.OrdinalIgnoreCase));

            return new Ship(Name, RegistrationNumber, 
                LaunchDate, Status, Height, Width, Length, 
                shipyard, typeOfShip, shipProject)
            {
                Id = this.Id
            };
        }

    }
}
