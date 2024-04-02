using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FrameDataStorage", menuName = "GameData/FrameDataStorage")]
public class FrameDataStorage : ScriptableObject
{
    public List<FrameData> frameDataList;

    public event Action<List<FrameData>> OnFrameDataUpdated;

    public void LoadFrameData(List<FrameData> newFrameData)
    {
        if (frameDataList != null && frameDataList != newFrameData)
        {
            frameDataList = newFrameData;
        }

        OnFrameDataUpdated?.Invoke(frameDataList);
    }
}