using Validation;

namespace Services.Validation.ValidatorService
{
    public class MatchRulesFrameDataValidator : IFrameDataValidator
    {
        public bool IsValid(FrameData frameData)
        {
            // game-specific validation, e.g, check if timestamp is reasonable, positions within bounds...
            return frameData.TimestampUtc > 0 && frameData.Persons.Count > 0; // Simplified example
        }
    }
}