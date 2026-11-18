using Common.Data;
using Common.Data.IO;
using ShipBuilding.Data.Interfaces;
using ShipBuilding.Data.IO.Interfaces;
using System;

namespace ShipBuilding.Data
{
    public class DataContext : BaseDataContext
    {
        public IDataSet DataSet { get; }

        private IFileIoController _fileIoController = null!;

        public IFileIoController FileIoController
        {
            get => _fileIoController;
            set => _fileIoController = value
                ?? throw new ArgumentNullException("FileIoController");
        }

        public DataContext(IDataSet dataSet, IFileIoController fileIoController,
            string directoryName, string fileName)
            : base(directoryName, fileName)
        {
            DataSet = dataSet;
            FileIoController = fileIoController;
        }

        public DataContext(IFileIoController fileIoController, string directoryName)
            : this(new DataSet(), fileIoController, directoryName, "ShipBuilding") { }

        public DataContext(IFileIoController fileIoController)
            : this(fileIoController, "") { }


        public override string FileExtension => FileIoController.FileExtension;

        public override bool Load(string filePath)
        {
            return FileIoController.Load(DataSet, filePath);
        }

        public override void Save(string filePath)
        {
            Saving.CheckAndPrepareFilePath(ref filePath);
            FileIoController.Save(DataSet, filePath);
        }

    }
}
