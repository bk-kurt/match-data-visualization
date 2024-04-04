using UnityEngine;

namespace Scriptables.Configuration
{
    [CreateAssetMenu(fileName = "GameAssets", menuName = "ScriptableObjects/BallConfiguration", order = 1)]
    public class BallConfigSo : ScriptableObject, IVisualizationAssetConfigProvider
    {
        public Ball ballPrefab;

        IVisualizationAssetConfigProvider IVisualizationAssetConfigProvider.GetGameAssetsConfig()
        {
            return ((IVisualizationAssetConfigProvider)this).GetGameAssetsConfig();
        }
        public IVisualizationAssetConfigProvider SetGameAssetsConfig(IVisualizationAssetConfigProvider configProvider)
        {
            throw new System.NotImplementedException();
        }

        
    }
}