using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "VisualizationSettings", menuName = "Settings/VisualizationSettings")]
public class VisualizationSettingsSo : ScriptableObject
{
    [Header("Interpolation")]
    public bool isInterpolationEnabled = true;
    [Range(0.1f, 1f)] public float commonPositionInterpolationSpeed = 0.1f;
    [Range(0.1f, 1f)] public float commonRotationInterpolationSpeed = 0.1f;

    public bool isValidationEnabled = true;
}