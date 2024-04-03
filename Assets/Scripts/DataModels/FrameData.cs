using System.Collections.Generic;
using System.Linq;
using DataModels;
using DataModels.Contexts;

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


    // I am indecisive about where to place the IsValid method, if validation criterias are changing frequently
    // and needs to be modular accross the cases, calling it externally as a utility method can be good for
    // more dynamic criteria configurations.
    // that way we can even use ServiceLocator approach for validation microservice.
    public bool IsValid()
    {
        // if there's at least one player in the frame?
        // if the ball's position data is valid?
        // should we filter corrupted data or allow them selectively?
        
        // additional checks can be included here based on needs.
        return true;
    }
}