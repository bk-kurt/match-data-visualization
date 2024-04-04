using Services.Validation.ValidatorService;

namespace Validation
{
    public class BasicFrameDataValidator : IFrameDataValidator
    {
        public bool IsValid(FrameData frameData)
        {
            // Perform basic integrity checks, e.g., non-null, basic structure
            return frameData != null && frameData.Persons != null && frameData.Ball != null;
        }
    }
}