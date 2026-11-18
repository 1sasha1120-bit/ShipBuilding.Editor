using ShipBuilding.Repositories.Interfaces;
using ShipBuilding.Statistics.DataQuality;
using ShipBuilding.Web.Models.AdminPanelModels;
using static ShipBuilding.Settings.UiSettings;

namespace ShipBuilding.Web.Models.Extensions
{
    public static class AdminPanelMethods
    {
        public static AdminPanelModel GetAdminPanelModel(this IStatisticsRepository statistics)
        {
            var counts = statistics.GetEntityCounts();

            return new AdminPanelModel
            {
                Sections =
                [
                    new(TypeOfShipsHeader, TypeOfShipsDescription, "fa-solid fa-sitemap", TypesController,
                        counts[EntityKind.TypeOfShip]),
                    new(ShipyardsHeader, ShipyardsDescription, "fa-solid fa-industry", ShipyardsController,
                        counts[EntityKind.Shipyard]),
                    new(ProjectsHeader, ProjectsDescription, "fa-solid fa-compass-drafting", ProjectsController,
                        counts[EntityKind.Project]),
                    new(ShipsHeader, ShipsDescription, "fa-solid fa-ship", ShipsController,
                        counts[EntityKind.Ship]),
                ],
                IncompleteRecords = statistics.GetIncompleteRecords()
            };
        }

        public static string ControllerFor(this EntityKind kind) => kind switch
        {
            EntityKind.TypeOfShip => TypesController,
            EntityKind.Shipyard => ShipyardsController,
            EntityKind.Project => ProjectsController,
            EntityKind.Ship => ShipsController,
            _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null)
        };
    }
}
