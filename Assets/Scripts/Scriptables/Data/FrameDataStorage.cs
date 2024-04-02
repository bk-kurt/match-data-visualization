// using scriptable object as a storage is advantageous,
// unity more efficienty uses memory with scriptables,
// also we can ease the time-wide operations, develop tools and perform QA easier.


using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "FrameDataStorage", menuName = "GameData/FrameDataStorage")]
public class FrameDataStorage : ScriptableObject
{
    public List<FrameData> frameDataList;

    public event Action<List<FrameData>> OnFrameDataUpdated;

    // we dont need to grab entire dataset all the time, so then incrementally process
    public void IncrementallyUpdateFrameData(List<FrameData> updatedFrameData)
    {
        frameDataList ??= new List<FrameData>();

        foreach (var frame in updatedFrameData)
        {
            // it's a new update or modification? then handle accordingly.
            var existingFrame = frameDataList.Find(f => f.FrameCount == frame.FrameCount);
            if (existingFrame == null)
            {
                frameDataList.Add(frame);
            }
            else
            {
                // Update existing frame data here if needed
            }
        }

        OnFrameDataUpdated?.Invoke(frameDataList);
    }

    public FrameData GetFrameDataAtIndex(int index)
    {
        if (index >= 0 && index < frameDataList.Count)
        {
            return frameDataList[index];
        }

        Debug.LogError("Index out of range.");
        return null;
    }


    // I believe these two methods below would do a good job on dealing with streaming data.
    public FrameData GetLatestFrameData()
    {
        if (frameDataList == null || frameDataList.Count == 0)
        {
            Debug.LogWarning("No frame data available.");
            return null;
        }

        return frameDataList[^1];
    }

    public FrameData GetFrameDataByTime(float timestamp)
    {
        var frame = frameDataList.FirstOrDefault(f => f.TimestampUtc <= timestamp);

        if (frame == null)
        {
            Debug.LogWarning($"No frame data found for timestamp: {timestamp}");
        }

        return frame;
    }

    public FrameData GetLatestValidFrameData()
    {
        if (frameDataList == null || frameDataList.Count == 0)
        {
            Debug.LogWarning("No frame data available.");
            return null;
        }

        // data meets the criteria?
        for (int i = frameDataList.Count - 1; i >= 0; i--)
        {
            if (frameDataList[i].IsValid())
            {
                return frameDataList[i];
            }
        }

        Debug.LogWarning("No valid frame data found.");
        return null;
    }
}