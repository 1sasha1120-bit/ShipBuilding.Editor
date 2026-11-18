using System.IO;

namespace Common.Data.IO {
    public static class Saving {

        public static void CheckAndCreateDirectory(ref string directoryName) {
            directoryName = (directoryName ?? "").Trim();
            if (!string.IsNullOrEmpty(directoryName)
                    && !Directory.Exists(directoryName)) {
                Directory.CreateDirectory(directoryName);
            }
        }

        // можливе повернення false у випадку виняткової ситуації
          
        public static bool CheckAndPrepareFilePath(ref string path) {
            path = (path ?? "").Trim();
            if (string.IsNullOrEmpty(path)) {
                return false;
            }

            string? directoryName = Path.GetDirectoryName(path);
            string directoryNameForCheck = directoryName ?? string.Empty;
            CheckAndCreateDirectory(ref directoryNameForCheck);
            return true;
        }
    }
}
