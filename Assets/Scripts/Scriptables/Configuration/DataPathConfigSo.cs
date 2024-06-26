using Interfaces.Configuration;
using UnityEngine;

namespace Scriptables.Configuration
{
    //qqq30 a path referencing viable across to project is essential
    [CreateAssetMenu(fileName = "DataPathConfig", menuName = "Scriptable/Configuration/Data Path")]
    public class DataPathConfigSo : ScriptableObject, IDataPathConfigProvider
    {
        public string jsonDataPath;
        public string GetJsonDataPath()
        {
            return jsonDataPath;
        }
        public void SetJsonDataPath(string path)
        {
             jsonDataPath=path;
        }
        
    }

}