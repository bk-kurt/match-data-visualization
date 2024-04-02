using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using System;
using Utilities;

public class MatchDataManager : MonoSingleton<MatchDataManager>
{
    public List<FrameData> AllFrameData { get; private set; } = new();
    public bool IsDataLoaded { get; private set; }
    
    public async Task LoadJsonDataAsync(string path)
    {
        try
        {
            if (!File.Exists(path))
            {
                Debug.LogError($"File does not exist at the given path: {path}");
                return;
            }
            
            string jsonContent = await FileUtils.ReadFileAsync(path);

            // wrap to 1 object
            jsonContent = "{\"items\":" + jsonContent + "}";

            FrameDataList frameDataList = JsonUtility.FromJson<FrameDataList>(jsonContent);
            AllFrameData = frameDataList.items;
            IsDataLoaded = true;

            Debug.Log($"Data loaded successfully with {AllFrameData.Count} frames.");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load and parse the JSON data: {e.Message}");
        }
    }
    
    public FrameData GetFrameDataAtIndex(int index)
    {
        if (index >= 0 && index < AllFrameData.Count)
        {
            return AllFrameData[index];
        }
        else
        {
            Debug.LogError("Index out of range.");
            return null;
        }
    }

    public int GetFrameCount()
    {
        return AllFrameData.Count;
    }
}


[Serializable]
public class FrameDataList
{
    public List<FrameData> items = new List<FrameData>();
}