using UnityEngine;

namespace DataModels
{
    public interface IInterpolatedStateData
    {
        Vector3 TargetPosition { get; }
        Quaternion TargetRotation { get; }
    }
}