using DataModels;
using GamePlay.Controllers;
using GamePlay.Factory;
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
            if (_instantiatedBall == null)
            {
                _instantiatedBall = _visualElementFactory.CreateBall(ballData);
                
                // for now, skipping to use a camera management system, simply assigning here.
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