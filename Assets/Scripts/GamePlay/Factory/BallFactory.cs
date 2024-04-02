using DataModels;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    namespace GamePlay {
        public static class BallFactory {
            public static Ball Create(BallData ballData, VisualizationAssetsConfigSo visualizationAssetsConfigSo) {
                
                if (ballData == null) {
                    Debug.LogError("BallFactory: Ball data is null.");
                    return null;
                }
                
                BallConfigSo configSo = visualizationAssetsConfigSo.GetConfiguredBall();
                if (configSo == null || configSo.ballPrefab == null) {
                    Debug.LogError("BallFactory: Ball configuration or prefab is null.");
                    return null;
                }

                Vector3 ballPosition = ballData.targetPosition;
                Ball instantiatedBallGo =
                    Object.Instantiate(configSo.ballPrefab, ballPosition, Quaternion.identity);

                Ball ballComponent = instantiatedBallGo.GetComponent<Ball>();
                if (ballComponent == null) {
                    ballComponent = instantiatedBallGo.AddComponent<Ball>();
                }

                ballComponent.Initialize(ballData);
                return ballComponent;
            }
        }
    }
}