using System.Collections;
using System.Collections.Generic;
using DataModels;
using UnityEngine;

public class FrameData : MonoBehaviour
{
    public int FrameCount;
    public int TimestampUtc;
    public List<PersonData> Persons;
    public BallData Ball;
    public GameClockContext GameClockContext;
    public MatchScoreContext MatchScoreContext;
    public PossessionCandidateContext PossessionCandidateContext;
}
