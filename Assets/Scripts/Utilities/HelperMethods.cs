using UnityEngine;

namespace Utilities
{
    public static class HelperMethods
    {
        public static Vector3 ArrayToVector3(float[] array)
        {
            if (array == null)
            {
                Debug.LogWarning("ArrayToVector3: Array is null.");
                return Vector3.zero;
            }
            if (array.Length != 3)
            {
                Debug.LogWarning($"ArrayToVector3: Array length is {array.Length}, expected 3.");
                return Vector3.zero;
            }
            return new Vector3(array[0], array[1], array[2]);
        }

    }


}