using ShipBuilding.Repositories.Interfaces;
using ShipBuilding.Types;
using ShipBuilding.Web.Models.AdminPanelModels;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Models.Extensions
{
    public static class AdminPanelMethods
    {
        private const string TypesController = "TypeOfShipsCrud";
        private const string ShipyardsController = "ShipyardsCrud";
        private const string ProjectsController = "ShipProjectsCrud";
        private const string ShipsController = "ShipsCrud";

        public static AdminPanelModel GetAdminPanelModel(this IUnitOfWork unitOfWork)
        {
            var types = unitOfWork.TypeOfShipsRepository.GetAll().ToList();
            var shipyards = unitOfWork.ShipyardsRepository.GetAll().ToList();
            var projects = unitOfWork.ShipProjectsRepository.GetAll().ToList();
            var ships = unitOfWork.ShipsRepository.GetAll().ToList();

            var incomplete = new List<IncompleteRecord>();

            incomplete.AddRange(ships.Select(s => new IncompleteRecord(ShipsHeader, ShipsController, s.Id, s.Name,
                Missing(
                    (string.IsNullOrWhiteSpace(s.RegistrationNumber), RegistrationNumberCaption),
                    (s.LaunchDate is null, LaunchDateCaption),
                    (s.Status == ShipStatus.Unknown, StatusCaption),
                    (s.TypeOfShip is null, TypeOfShipCaption),
                    (s.Shipyard is null, ShipyardCaption)))));

            incomplete.AddRange(projects.Select(p => new IncompleteRecord(ProjectsHeader, ProjectsController, p.Id, p.Name,
                Missing(
                    (string.IsNullOrWhiteSpace(p.ProjectCode), ProjectCodeCaption),
                    (p.Budget is null, BudgetCaption),
                    (p.Shipyard is null, ShipyardCaption)))));

            incomplete.AddRange(shipyards.Select(y => new IncompleteRecord(ShipyardsHeader, ShipyardsController, y.Id, y.Name,
                Missing(
                    (string.IsNullOrWhiteSpace(y.CountryName), CountryCaption),
                    (y.YearOfFounded is null, YearOfFoundedCaption),
                    (y.NumberOfDocks is null, NumberOfDocksCaption),
                    (y.CapacityPerYear is null, CapacityPerYearCaption)))));

            // кореневий тип не має коду за задумом
            incomplete.AddRange(types.Select(t => new IncompleteRecord(TypeOfShipsHeader, TypesController, t.Id, t.Name,
                Missing(
                    (t.Parent is not null && string.IsNullOrWhiteSpace(t.TypeCode), TypeCodeCaption),
                    (string.IsNullOrWhiteSpace(t.Description), DescriptionCaption)))));

            return new AdminPanelModel
            {
                Sections =
                [
                    new(TypeOfShipsHeader, TypeOfShipsDescription, "fa-solid fa-sitemap", TypesController, types.Count),
                    new(ShipyardsHeader, ShipyardsDescription, "fa-solid fa-industry", ShipyardsController, shipyards.Count),
                    new(ProjectsHeader, ProjectsDescription, "fa-solid fa-compass-drafting", ProjectsController, projects.Count),
                    new(ShipsHeader, ShipsDescription, "fa-solid fa-ship", ShipsController, ships.Count),
                ],
                IncompleteRecords = incomplete
                    .Where(r => r.MissingFields.Count > 0)
                    .OrderByDescending(r => r.MissingFields.Count)
                    .ThenBy(r => r.Section)
                    .ThenBy(r => r.Name)
                    .ToList()
            };
        }

        private static IReadOnlyList<string> Missing(params (bool IsMissing, string Caption)[] checks) =>
            checks.Where(c => c.IsMissing).Select(c => c.Caption).ToList();
    }
}
