
namespace CloudStorage.Core.Utils
{
    public class FileNameHelper
    {
        public static string ExtractExtension(string fileName)
        {
            if (!fileName.Contains('.'))
                return "";

            var lastIndexOfPeriod = fileName.LastIndexOf(".");

            return fileName.Substring(lastIndexOfPeriod);
        }

        public static string RemovePathSegments(string path)
        {
            int lastSlashIndex = path.LastIndexOf("/");
            return path.Substring(lastSlashIndex + 1);
        }

    }

}
