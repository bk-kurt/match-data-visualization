using UnityEngine;
using DataModels;
using DefaultNamespace;
using GamePlay;
using GamePlay.Controllers;
using Utilities;

namespace Managers
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
            if (_instantiatedBall == null)
            {
                _instantiatedBall = _visualElementFactory.CreateBall(ballData);
                CameraController.Instance.SetTarget(_instantiatedBall.transform);
            }
            else
            {
                _instantiatedBall.UpdateState(ballData);
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