using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.Data
{
    public static class DataParsingUtilities
    {
        public static List<FrameData> ParseValidFrames(string jsonContent, bool validateFrames = false)
        {
            FrameDataList frameDataList = JsonUtility.FromJson<FrameDataList>("{\"items\":" + jsonContent + "}");
            if (!validateFrames)
            {
                return frameDataList.items;
            }
            return frameDataList.items.Where(frame => frame.IsValid()).ToList();
        }
    }
}