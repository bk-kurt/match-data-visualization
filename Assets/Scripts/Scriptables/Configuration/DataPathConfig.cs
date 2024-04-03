using UnityEngine;

namespace Scriptables.Configuration
{
    [CreateAssetMenu(fileName = "DataPathConfig", menuName = "Configuration/Data Path")]
    public class DataPathConfig : ScriptableObject
    {
        public string jsonDataPath;
    }

}