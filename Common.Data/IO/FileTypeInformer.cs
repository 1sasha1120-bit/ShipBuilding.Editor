using Common.Interfaces;

namespace Common.Data.IO {
    public class FileTypeInformer : IKeyable {
        protected FileTypeInformer(string fileTypeCaption, string fileExtension) {
            FileTypeCaption = fileTypeCaption;
            FileExtension = fileExtension;
        }

        public string FileTypeCaption { get; }

        public string FileExtension { get; }
        // можливість зміни розширення файлу

        public string Key {
            get { return $"{FileTypeCaption} ({FileExtension})"; }
        }
    }
}
