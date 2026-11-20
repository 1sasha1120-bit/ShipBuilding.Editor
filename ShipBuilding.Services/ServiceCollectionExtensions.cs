using Microsoft.Extensions.DependencyInjection;
using ShipBuilding.Repositories.Interfaces;
using ShipBuilding.Services.Interfaces;
using ShipBuilding.Services.ShipProjects;
using ShipBuilding.Services.Ships;
using ShipBuilding.Services.Shipyards;
using ShipBuilding.Services.Statistics;
using ShipBuilding.Services.TypeOfShips;

namespace ShipBuilding.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddShipBuildingServices(this IServiceCollection services,
            string dataFolder, bool seedTestData)
        {
            services.AddSingleton<IUnitOfWork>(_ => FileStorage.CreateUnitOfWork(dataFolder, seedTestData));

            services.AddScoped<IShipsService, ShipsService>();
            services.AddScoped<IShipyardsService, ShipyardsService>();
            services.AddScoped<IShipProjectsService, ShipProjectsService>();
            services.AddScoped<ITypeOfShipsService, TypeOfShipsService>();
            services.AddScoped<IStatisticsService, StatisticsService>();

            return services;
        }
    }
}
