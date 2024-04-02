using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameAssets", menuName = "ScriptableObjects/PersonConfiguration", order = 1)]
    public class PersonConfigSo : ScriptableObject
    {
        public Person personPrefab;
        public int teamSide;
    }
}