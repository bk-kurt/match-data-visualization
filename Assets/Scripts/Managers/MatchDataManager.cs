using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Linq;
using UnityEngine.Serialization;
using Utilities;

public class MatchDataManager : MonoSingleton<MatchDataManager>
{
    public FrameDataStorage frameDataStorage;
    public bool IsDataLoaded { get; private set; }

    // This method remains largely unchanged but is now more aware of the incremental update capabilities of FrameDataStorage.
    public async Task LoadJsonDataAsync(string path)
    {
        try
        {
            if (!File.Exists(path))
            {
                Debug.LogError($"File does not exist at the given path: {path}");
                return;
            }

            string jsonContent = await Task.Run(() => File.ReadAllText(path));

            FrameDataList frameDataList = JsonUtility.FromJson<FrameDataList>("{\"items\":" + jsonContent + "}");

            // for corrupted or meaningless data cases
            // we can also take the advantage of a creating a custom internal Data Validation microservice
            List<FrameData> validFrames;
            if (VisualizationSettingsProvider.CurrentSettings.isValidationEnabled)
            {
                validFrames = frameDataList.items.Where(frame => frame.IsValid()).ToList();
                Debug.Log($"Valid frames count: {validFrames.Count}");
            }
            else
            {
                validFrames = frameDataList.items;
            }

            frameDataStorage.IncrementallyUpdateFrameData(validFrames);
            IsDataLoaded = true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load and parse the JSON data: {e.Message}");
        }
    }

    public FrameData GetFrameDataAtIndex(int index)
    {
        return frameDataStorage.GetFrameDataAtIndex(index);
    }


    public int GetFrameCount()
    {
        return frameDataStorage.frameDataList?.Count ?? 0;
    }
}


[Serializable]
public class FrameDataList
{
    public List<FrameData> items = new();
}