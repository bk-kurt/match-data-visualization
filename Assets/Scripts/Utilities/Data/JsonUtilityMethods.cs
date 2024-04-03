using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Utilities.Data
{
    /// <summary>
    /// Why I preferred Task.Run over ReadFileAsync?
    ///
    /// 
    /// 1- more beyond Constraints:there are limitations with Unity's JSON utilities dealing with
    /// large files where reading the file in chunks and processing them incrementally is beneficial.
    /// 
    /// 2- complex data processing : JSON processing or validation is CPU-intensive or
    /// requires running synchronous code asynchronously, Task.Run is better.
    /// 
    /// 3- reporting progress. Task.Run allows easy integration for
    /// progress reporting w/ synchronous - CPU used works.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="progressIndicator"></param>
    /// <returns></returns>
    public static class JsonUtilityMethods
    {
        public static async Task<string> LoadJsonContentAsync(string path, IProgress<float> progressIndicator)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File does not exist at the given path: {path}");
            }

            return await Task.Run(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    Task.Delay(10).Wait();
                    progressIndicator?.Report(i / 100f);
                }
                var content = File.ReadAllText(path);
                return content;
            });
        }
    }
    
    [Serializable]
    public class FrameDataList
    {
        public List<FrameData> items;
    }
}