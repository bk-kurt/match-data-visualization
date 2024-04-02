using UnityEngine;
using Utilities;

namespace Settings
{
    public class VisualizationSettings : MonoSingleton<VisualizationSettings>
    {
        [Tooltip("Speed at which objects interpolate to their target positions.")]
        [Range(0.1f,1f)]public float commonInterpolationSpeed = 0.5f;
    }
}