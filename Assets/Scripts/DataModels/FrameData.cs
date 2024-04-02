using System.Collections.Generic;
using DataModels;

[System.Serializable]
public class FrameData
{
    public int FrameCount;
    public float TimestampUtc;
    public List<PersonData> Persons;
    public BallData Ball;
    public GameClockContext GameClockContext;
    public MatchScoreContext MatchScoreContext;
    public PossessionCandidateContext PossessionCandidateContext;
}
