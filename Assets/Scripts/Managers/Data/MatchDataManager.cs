using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Linq;
using Providers;
using Scriptables.Data;
using UnityEditor;
using Utilities;


// the parsed JSON file was primarily prepared/formatted externally using Python, I estimate that the delegation is better choice
// since unity Json capabilities are limited and requires additional dependencies. Also i estimate that in a data oriented pipeline,
// 3rd party data environments plays a powerful role, that way preferences an corruptions can be handled safer.
// by making this application's architecture close to modification

namespace Managers.Data
{
    public class MatchDataManager : MonoSingleton<MatchDataManager>
    {
        public FrameDataStorage frameDataStorage;
        public bool IsDataLoaded { get; private set; }
        public event Action OnDataLoadingComplete;


        /// <summary>
        /// this method is designed aware of the incremental update capabilities of FrameDataStorage.
        /// </summary>
        /// <param name="path"></param>
        public async Task LoadJsonDataAsync(string path)
        {
            var progressIndicator = new Progress<float>(progress =>
            {
                EditorUtility.DisplayProgressBar("Buffering...",
                    "Please wait while JSON file is being processed...", progress);
            });
            try
            {
                if (!File.Exists(path))
                {
                    Debug.LogError($"File does not exist at the given path: {path}");
                    return;
                }

                var jsonContent = await JsonContent(path, progressIndicator);
                var validFrames = ValidFrames(jsonContent);
                frameDataStorage.IncrementallyUpdateFrameData(validFrames);
                IsDataLoaded = true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load and parse the JSON data: {e.Message}");
            }
            finally
            {
                EditorUtility.ClearProgressBar();
                OnDataLoadingComplete?.Invoke();
            }
        }

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
        private static async Task<string> JsonContent(string path, Progress<float> progressIndicator)
        {
            string jsonContent = await Task.Run(() =>
            {
                for (int i = 0; i <= 30; i++)
                {
                    Task.Delay(10).Wait(); // simulate work for progress bar
                    ((IProgress<float>)progressIndicator).Report(i / 30f);
                }

                return File.ReadAllText(path);
            });
            return jsonContent;
        }

        private static List<FrameData> ValidFrames(string jsonContent)
        {
            FrameDataList frameDataList = JsonUtility.FromJson<FrameDataList>("{\"items\":" + jsonContent + "}");

            List<FrameData> validFrames;
            if (VisualizationSettingsProvider.CurrentSettings.isValidationEnabled)
            {
                validFrames = frameDataList.items.Where(frame => frame.IsValid()).ToList();
            }
            else
            {
                validFrames = frameDataList.items;
            }

            return validFrames;
        }


        public FrameData GetFrameDataAtIndex(int index)
        {
            return frameDataStorage.GetFrameDataAtIndex(index);
        }


        public bool HasFrameData()
        {
            return GetFrameCount() > 0;
        }
        public int GetFrameCount()
        {
            return frameDataStorage.frameDataList?.Count ?? 0;
        }
    }
}


[Serializable]
public class FrameDataList
{
    public List<FrameData> items = new();
}