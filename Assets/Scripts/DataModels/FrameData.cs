using System.Collections.Generic;
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


    //qqq1 validation can happen externally instead of the data structure, below is explanatory...
    
    // a dynamic validation microservice that makes possible to work with data validation criterias are changing along with data stream
    // the service and the validation rules are two-way customizable
    public bool IsValid()
    {
        // IFrameDataValidator validator = ValidatorSetups.CustomCompositeValidator();
        // return validator.IsValid(this);
        return true;
    }
}