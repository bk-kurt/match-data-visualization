namespace DataModels.Contexts
{
    [System.Serializable]
    public class TrackableBallContext
    {
        public BallState BallState;
        public Possession Possession;
    }
}

public enum BallState
{
    InPlay,
    OutOfPlay,
    GoalScored
}

public enum Possession
{
    None,
    HomeTeam,
    AwayTeam
}