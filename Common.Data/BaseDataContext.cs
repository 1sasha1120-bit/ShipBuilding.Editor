using Common.Data.IO;
using Common.Data.IO.Interfaces;
using System.IO;

namespace Common.Data {
    public abstract class BaseDataContext : IFileStorage {

        private string _directoryName = "";

        public string DirectoryName {
            get { return _directoryName; }
            set {
                _directoryName = value;
                Saving.CheckAndCreateDirectory(ref _directoryName);
            }
        }

        protected string FileName { get; set; }

        public string FilePath {
            get {
                return Path.Combine(DirectoryName,
                    FileName + FileExtension);
            }
        }

        protected BaseDataContext(string directoryName, string fileName) {
            DirectoryName = directoryName;
            FileName = fileName;
        }

        public abstract string FileExtension { get; }

        public void Save() {
            Save(FilePath);
        }

        public bool Load() {
            return Load(FilePath);
        }

        public abstract void Save(string filePath);

        public abstract bool Load(string filePath);


    }
}
