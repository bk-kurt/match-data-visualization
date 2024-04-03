using DataModels.Contexts;
using Managers.Data;
using UnityEngine;

namespace Utilities
{
    public static class TimeHelper
    {
        // calculate game clock from timestamp
        public static GameClockContext ClockContextFromTimeStamp(float timeStamp)
        {
            int periodDurationInSeconds = 60 * 45;
            
            float matchStartTime = MatchDataLoader.Instance.GetFrameDataAtIndex(0).TimestampUtc;
            
            int totalSeconds = Mathf.RoundToInt(timeStamp - matchStartTime);
            
            int period = totalSeconds / periodDurationInSeconds;
            
            int remainingSeconds = totalSeconds % periodDurationInSeconds;
            
            int minutes = remainingSeconds / 60;
            int seconds = remainingSeconds % 60;
            
            GameClockContext newGameClockContext = new GameClockContext
            {
                Period = period,
                Minute = minutes,
                Second = seconds
            };

            return newGameClockContext;
        }
    }
}