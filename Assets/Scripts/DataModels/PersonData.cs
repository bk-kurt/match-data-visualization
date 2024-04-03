using DataModels.Contexts;
using UnityEngine;
using Utilities;

namespace DataModels
{
    [System.Serializable]
    public class PersonData: IInterpolatedStateData
    {
        public int Id;
        public float Timestamp;
        public float[] Position;
        public float Speed;
        public int TeamSide;
        public int JerseyNumber;
        public AnimationContext AnimationContext;
        public PersonContext PersonContext;
        public Vector3 TargetPosition => UtilityMethods.ArrayToVector3(Position);
        public Quaternion TargetRotation => PersonContext.rotation;
    }
}