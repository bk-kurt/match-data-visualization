using UnityEngine;

namespace Scriptables.Configuration
{
    [CreateAssetMenu(fileName = "GameAssets", menuName = "ScriptableObjects/BallConfiguration", order = 1)]
    public class BallConfigSo : ScriptableObject
    {
        public Ball ballPrefab;
    }
}