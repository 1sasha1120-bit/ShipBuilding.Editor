using Microsoft.AspNetCore.Mvc;
using ShipBuilding.Entities;
using ShipBuilding.Repositories.Interfaces;
using ShipBuilding.Web.Models;
using ShipBuilding.Web.Models.ShipProjectModels;
using ShipBuilding.Web.Models.ShipyardModels;
using ShipBuilding.Web.Models.Ships;
using ShipBuilding.Web.Models.TypeOfShipModels;

namespace ShipBuilding.Web.Models.Extensions
{
    public static class ConvertingMethods
    {

        public static ShipDeletingModel ToDeletingModel(this Ship obj) =>
            new() {
                Id                 = obj.Id,
                Name               = obj.Name,
                RegistrationNumber = obj.RegistrationNumber,
                LaunchDate         = obj.LaunchDate,
                Status             = obj.Status,
                Height             = obj.Height,
                Width              = obj.Width,
                Length             = obj.Length,
                ShipyardName       = obj.Shipyard?.Name,
                TypeOfShipName     = obj.TypeOfShip?.Name,
                ProjectName        = obj.Project?.Name,
            };

        public static ShipProjectDeletingModel ToDeletingModel(this ShipProject obj) =>
            new() {
                Id           = obj.Id,
                Name         = obj.Name,
                ProjectCode  = obj.ProjectCode,
                ShipyardName = obj.Shipyard?.Name,
            };

        public static ShipyardDeletingModel ToDeletingModel(this Shipyard obj) =>
            new() {
                Id              = obj.Id,
                Name            = obj.Name,
                CountryName     = obj.CountryName,
                YearOfFounded   = obj.YearOfFounded,
                NumberOfDocks   = obj.NumberOfDocks,
                CapacityPerYear = obj.CapacityPerYear
            };

        public static TypeOfShipDeletingModel ToDeletingModel(this TypeOfShip obj) =>
            new() {
                Id          = obj.Id,
                Name        = obj.Name,
                TypeCode    = obj.TypeCode,
                ParentName  = obj.Parent?.Name,
                Description = obj.Description
            };

        public static Ship ToShip(this ShipEditingModel model,
            IEnumerable<Shipyard> shipyards, IEnumerable<TypeOfShip> typeOfShips,
            IEnumerable<ShipProject> shipProjects)
        {
            string? shipyardName = model.ShipyardName?.Trim();
            Shipyard? shipyard = shipyards.FirstOrDefault(e =>
                string.Equals(e.Name, shipyardName, StringComparison.OrdinalIgnoreCase));

            string? typeOfShipName = model.TypeOfShipName?.Trim();
            TypeOfShip? typeOfShip = typeOfShips.FirstOrDefault(e =>
                string.Equals(e.Name, typeOfShipName, StringComparison.OrdinalIgnoreCase));

            string? projectName = model.ProjectName?.Trim();
            ShipProject? shipProject = string.IsNullOrWhiteSpace(projectName) ? null
                : shipProjects.FirstOrDefault(e =>
                    string.Equals(e.Name, projectName, StringComparison.OrdinalIgnoreCase));

            return new Ship(model.Name, model.RegistrationNumber,
                model.LaunchDate, model.Status, model.Height, model.Width, model.Length,
                shipyard, typeOfShip, shipProject)
            {
                Id = model.Id
            };
        }

        public static ShipProject ToShipProject(this ShipProjectEditingModel model, 
            IEnumerable<Shipyard> shipyards)
        {
            string? shipyardName = model.ShipyardName?.Trim();
            Shipyard? shipyard = shipyards.FirstOrDefault(e =>
                string.Equals(e.Name, shipyardName, StringComparison.OrdinalIgnoreCase));
            
            return new ShipProject(model.Name, model.ProjectCode, model.Budget, shipyard)
            {
                Id = model.Id
            };
        }

        public static Shipyard ToShipyard(this ShipyardEditingModel model) =>
            new(model.Name, model.CountryName,
                model.YearOfFounded, model.NumberOfDocks, model.CapacityPerYear)
            { Id = model.Id };

        public static TypeOfShip ToTypeOfShip(this TypeOfShipEditingModel model,
            IEnumerable<TypeOfShip> all)
        {
            TypeOfShip? parent = null;
            if (!string.IsNullOrWhiteSpace(model.ParentName))
            {
                parent = all.FirstOrDefault(e =>
                    string.Equals(e.Name, model.ParentName.Trim(), StringComparison.OrdinalIgnoreCase));
            }
            return new TypeOfShip(model.Name, parent, model.TypeCode)
            {
                Id          = model.Id,
                Description = model.Description
            };
        }
    }
}
