using DataModels;
using UnityEngine;

namespace GamePlay.Controllers
{
    public class BallController : MonoBehaviour, ITopLevelController
    {
        public void ApplyTopLevelChanges(IInterpolatedStateData interpolatedStateData)
        {
            BallData ballData = (BallData)interpolatedStateData;
            AddArtificialBounceEffect(ballData);
        }
        
        private void AddArtificialBounceEffect(BallData ballData){}
    }
}