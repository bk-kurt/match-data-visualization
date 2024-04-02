using UnityEngine;
using Utilities;

namespace GamePlay.Controllers
{
    public class CameraController : MonoSingleton<CameraController>
    {
        private Transform _target;

        void LateUpdate()
        {
            if (_target != null)
            {
                transform.LookAt(_target.transform);
            }
        }


        public void SetTarget(Transform newTarget)
        {
            _target = newTarget;
        }
    }
}