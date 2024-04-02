using UnityEngine;

namespace GamePlay.Controllers
{
    public class CameraController : MonoBehaviour
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