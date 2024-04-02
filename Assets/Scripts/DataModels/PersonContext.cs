using UnityEngine;

namespace DataModels
{
    [System.Serializable]
    public class PersonContext
    {
        public bool HasBallPossession;
        public PlayerState PlayerState;
        public float MovementOrientation;
        
        public Quaternion rotation => Quaternion.Euler(0, MovementOrientation, 0);
    }
    
}

public enum PlayerState
{
}