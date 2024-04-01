using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utilities;

public class MatchDataManager : MonoSingleton<MatchDataManager>
{
    public List<FrameData> allFrameData { get; private set; } = new();
    public bool isDataLoaded { get; private set; }
    
    public async Task LoadJsonDataAsync(string path)
    {
            string jsonContent = await FileUtils.ReadFileAsync(path);
            
            FrameDataList frameDataList = JsonUtility.FromJson<FrameDataList>(jsonContent);
            allFrameData = frameDataList.items;
    }
}


[Serializable]
public class FrameDataList
{
    public List<FrameData> items = new();
}