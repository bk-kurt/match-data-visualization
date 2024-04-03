using System.IO;
using System.Threading.Tasks;

namespace Utilities
{
    // since I switched to Task.Run in DataManager. this is no longer used, (prev usage was this)
    public static class FileUtils
    {
        public static async Task<string> ReadFileAsync(string path)
        {
            using StreamReader reader = new StreamReader(path);
            return await reader.ReadToEndAsync();
        }
    }
}