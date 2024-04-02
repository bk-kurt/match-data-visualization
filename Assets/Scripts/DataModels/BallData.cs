using JetBrains.Annotations;
using UnityEngine;
using Utilities;

namespace DataModels
{
    [System.Serializable][CanBeNull]
    public class BallData
    {
        public int Id;
        public float Timestamp;
        public float[] Position;
        public float Speed;
        public int TeamSide;
        public int JerseyNumber;
        public TrackableBallContext Context;

        public Vector3 targetPosition => UtilityMethods.ArrayToVector3(Position);
        public Quaternion TargetRotation { get; }
    }
}