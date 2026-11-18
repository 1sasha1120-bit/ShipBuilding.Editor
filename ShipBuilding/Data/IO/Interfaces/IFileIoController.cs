using Common.Interfaces;
using ShipBuilding.Data.Interfaces;

namespace ShipBuilding.Data.IO.Interfaces
{
    public interface IFileIoController : IKeyable
    {
        string FileExtension { get; }

        void Save(IReadOnlyDataSet dataSet, string filePath);
        bool Load(IDataSet dataSet, string filePath);
    }
}
