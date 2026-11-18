using ShipBuilding.Data;
using ShipBuilding.Data.IO;
using ShipBuilding.Repositories;
using ShipBuilding.Repositories.Interfaces;
using ShipBuilding.Data.Testing;

namespace ShipBuilding.Web.Services
{
    public class DataInitializer
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        public DataInitializer(IWebHostEnvironment env, IConfiguration config)
        {
            _env = env;
            _config = config;
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            // Отримуємо шлях з конфігурації або використовуємо дефолтний "files"
            string folderName = _config["Storage:DataFolder"] ?? "files";
            string path = Path.Combine(_env.WebRootPath, folderName);

            var dataContext = new DataContext(new XmlFileIoController(), path);

            if (File.Exists(dataContext.FilePath))
            {
                dataContext.Load();
            }
            else
            {
                if (_env.IsDevelopment())
                {
                    dataContext.DataSet.CreateTestingData();
                }
            }

            return new FileUnitOfWork(dataContext);
        }
    }
}
