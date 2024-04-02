using DataModels;
using UnityEngine;

namespace DefaultNamespace
{
    public class Person : MonoBehaviour
    {
        public void Initialize(PersonData personData)
        {
            // todo
            UpdateState(personData);
        }

        public void UpdateState(PersonData personData)
        {
            transform.position = personData.targetPosition;
            transform.rotation = personData.TargetRotation;
        }
    }
}