using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using Providers;
using Scriptables.Data;
using UnityEditor;
using Utilities;
using Utilities.Data;


// the parsed JSON file was primarily prepared/formatted externally using Python, I estimate that the delegation is better choice
// since unity Json capabilities are limited and requires additional dependencies if tried. Also i estimate that in a data oriented pipeline,
// python environments plays a powerful role, that way preferences an corruptions can be handled safer.
// also makes this application's architecture close to modification.

namespace Managers.Data
{
    public class MatchDataLoader : MonoSingleton<MatchDataLoader>
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
                EditorUtility.DisplayProgressBar("Loading...", $"Loading JSON from {path}", progress);
            });

            try
            {
                string jsonContent = await JsonUtilityMethods.LoadJsonContentAsync(path, progressIndicator);
                List<FrameData> validFrames = DataParsingUtilities.ParseValidFrames(jsonContent,
                    VisualizationSettingsProvider.CurrentSettings.isValidationEnabled);
                frameDataStorage.IncrementallyUpdateFrameData(validFrames);
                IsDataLoaded = true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Error loading data: {e}");
            }
            finally
            {
                EditorUtility.ClearProgressBar();
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