namespace Common.Data.IO.Interfaces {
    public interface IFileStorage {

        string DirectoryName { get; set; }

        void Save(string filePath);

        bool Load(string filePath);

        void Save();

        bool Load();

    }
}
