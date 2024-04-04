using Scriptables.Configuration;
using UnityEngine;


namespace Scriptables.Settings
{
    [CreateAssetMenu(fileName = "VisualizationSettings", menuName = "Settings/VisualizationSettings")]
    public class VisualizationSettingsSo : ScriptableObject
    {
        public bool IsInterpolationEnabled => isInterpolationEnabled;

        public float CommonPositionInterpolationSpeed => commonPositionInterpolationSpeed;

        public float CommonRotationInterpolationSpeed => commonRotationInterpolationSpeed;

        public bool IsValidationEnabled => isValidationEnabled;

        //qqq24 DataPathConfigSo, This is for editor execution reference, normally provided by config provider in runtime. (for quick prototyping)
        public DataPathConfigSo DataPathConfigSo => dataPathConfigSo; 
        
        
        
        [SerializeField] private bool isValidationEnabled = true;

        [Space] [Header("Interpolation")] [SerializeField]
        private bool isInterpolationEnabled = true;

        [SerializeField, Range(0.1f, 1f)] private float commonPositionInterpolationSpeed = 0.1f;

        [SerializeField, Range(0.1f, 1f)] private float commonRotationInterpolationSpeed = 0.1f;


        [SerializeField]
        private DataPathConfigSo
            dataPathConfigSo;
    }
}