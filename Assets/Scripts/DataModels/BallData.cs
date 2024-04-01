namespace DataModels
{
    public class BallData
    {
        public int Id;
        public float Timestamp;
        public float[] Position;
        public float Speed;
        public int TeamSide;
        public int JerseyNumber;
        public TrackableBallContext Context;
    }
}