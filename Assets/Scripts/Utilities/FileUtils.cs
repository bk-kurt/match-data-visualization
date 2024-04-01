using System.IO;
using System.Threading.Tasks;

namespace Utilities
{
    public static class FileUtils
    {
        public static async Task<string> ReadFileAsync(string path)
        {
            using StreamReader reader = new StreamReader(path);
            return await reader.ReadToEndAsync();
        }
    }
}