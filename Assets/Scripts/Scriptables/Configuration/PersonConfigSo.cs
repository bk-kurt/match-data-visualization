using UnityEngine;

namespace Scriptables.Configuration
{
    [CreateAssetMenu(fileName = "GameAssets", menuName = "Scriptable/Configuration/PersonConfiguration", order = 1)]
    public class PersonConfigSo : ScriptableObject
    {
        public Person personPrefab;
        public int teamSide;
    }
}