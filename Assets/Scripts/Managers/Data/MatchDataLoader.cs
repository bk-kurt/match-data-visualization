using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using Managers.Configuration;
using Providers;
using Scriptables.Data;
using UnityEditor;
using Utilities;
using Utilities.Data;


//qqq8 the parsed JSON file was primarily prepared/formatted externally using Python, I estimate that the delegation is better choice
// since unity Json capabilities are limited and requires additional dependencies if tried. Also i estimate that in a data oriented pipeline,
// python environments plays a powerful role, that way preferences and corruptions can be handled safer.
// also makes this application's architecture close to modification.

namespace Managers.Data
{
    public class MatchDataLoader : MonoSingleton<MatchDataLoader>
    {
        public FrameDataStorage frameDataStorage;

        // also used by editor scripts
        public bool IsDataLoaded { get; private set; }
        public event Action OnDataLoadingComplete;


        /// <summary>
        ///qqq9 this method is designed aware of the incremental update capabilities of FrameDataStorage.
        /// </summary>
        /// <param name="path"></param>
        public async Task LoadJsonDataAsync()
        {
            string path = ConfigurationManager.Instance.GetJsonDataPathConfig();
            var progressIndicator = new Progress<float>(progress =>
            {
                EditorUtility.DisplayProgressBar("Buffering...", $"Loading JSON from {path}", progress);
            });

            try
            {
                string jsonContent = await JsonUtilityMethods.LoadJsonContentAsync(path, progressIndicator);
                List<FrameData> validFrames = DataParsingUtilities.ParseValidFrames(jsonContent,
                    VisualizationSettingsProvider.CurrentSettings.IsValidationEnabled);
                frameDataStorage.IncrementallyUpdateFrameData(validFrames);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error loading data: {e}");
            }
            finally
            {
                EditorUtility.ClearProgressBar();
                IsDataLoaded = true;
                OnDataLoadingComplete?.Invoke();
            }
        }

        public FrameData GetFrameDataAtIndex(int index)
        {
            return frameDataStorage.GetFrameDataAtIndex(index);
        }

        public bool HasInitializableData()
        {
            return IsDataLoaded && HasFrameData();
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