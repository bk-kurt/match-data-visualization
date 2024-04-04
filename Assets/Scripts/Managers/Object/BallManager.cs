using DataModels;
using GamePlay.Controllers;
using GamePlay.Factory;
using UnityEngine;
using Utilities;

namespace Managers.Object
{
    public class BallManager : MonoSingleton<BallManager>
    {
        private Ball _instantiatedBall;
        private VisualElementFactory _visualElementFactory;

        public void SetVisualElementFactory(VisualElementFactory factory)
        {
            _visualElementFactory = factory;
        }


        public void UpdateBallState(BallData ballData)
        {
            if (ballData == null || _visualElementFactory == null)
            {
                Debug.LogWarning("BallData is null or VisualElementFactory is not set.");
                return;
            }

            if (_instantiatedBall == null)
            {
                CreateAndAssignNewBall(ballData);
            }
            else
            {
                _instantiatedBall.UpdateState(ballData);
            }
        }

        private void CreateAndAssignNewBall(BallData ballData)
        {
            _instantiatedBall = _visualElementFactory.CreateBall(ballData);

            if (_instantiatedBall != null)
            {
                //qqq10 this can be handled other place, sample usage due to project earliness
                // callback system for camera or other dependencies.
                CameraController.Instance.SetTarget(_instantiatedBall.transform);
            }
            else
            {
                Debug.LogError("Failed to instantiate ball.");
            }
        }

        public void ClearBall()
        {
            if (_instantiatedBall != null)
            {
                Destroy(_instantiatedBall.gameObject);
                _instantiatedBall = null;
            }
        }
    }
}