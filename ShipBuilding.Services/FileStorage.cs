using ShipBuilding.Data;
using ShipBuilding.Data.IO;
using ShipBuilding.Data.Testing;
using ShipBuilding.Repositories;
using ShipBuilding.Repositories.Interfaces;

namespace ShipBuilding.Services
{
    internal static class FileStorage
    {
        /// <summary>
        /// Створює сховище на основі XML-файлу в теці <paramref name="dataFolder"/>.
        /// Якщо файлу ще немає, за <paramref name="seedTestData"/> заповнює сховище тестовими даними.
        /// </summary>
        public static IUnitOfWork CreateUnitOfWork(string dataFolder, bool seedTestData)
        {
            var dataContext = new DataContext(new XmlFileIoController(), dataFolder);

            // Load() повертає false, якщо файлу даних ще немає
            if (!dataContext.Load() && seedTestData)
            {
                dataContext.DataSet.CreateTestingData();
            }

            return new FileUnitOfWork(dataContext);
        }
    }
}
